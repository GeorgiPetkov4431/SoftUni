namespace MiniORM
{
    using Microsoft.Data.SqlClient;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public abstract class DbContext : IDisposable
    {
        private readonly DatabaseConnection connection;

        /* Dictionary will store pairs of EntityType and DbSet Property Info */
        private readonly IDictionary<Type, PropertyInfo> dbSetProperties;

        internal static readonly Type[] AllowedSqlTypes =
        {
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(byte?),
            typeof(sbyte?),
            typeof(short?),
            typeof(ushort?),
            typeof(int?),
            typeof(uint?),
            typeof(long?),
            typeof(ulong?),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(float?),
            typeof(double?),
            typeof(decimal?),
            typeof(bool),
            typeof(bool?),
            typeof(string),
            typeof(char),
            typeof(char?),
            typeof(DateTime),
            typeof(DateTime?),
            typeof(Guid),
            typeof(TimeSpan)
        };

        protected DbContext(string connectionString)
        {
            this.connection = new DatabaseConnection(connectionString);

            this.dbSetProperties = this.DiscoverDbSetProperties();

            using (ConnectionManager connectionMgr = new ConnectionManager(this.connection))
            {
                InitializeDbSets();
            }

            MapAllRelations();
        }

        public void SaveChanges()
        {
            IEnumerable<object?> dbSetInstances = this.dbSetProperties
                .Select(kvp => kvp.Value.GetValue(this))
                .ToArray();

            foreach (IEnumerable<object> dbSetInstance in dbSetInstances)
            {
                bool hasInvalidEntitiesValidation = dbSetInstance
                    .Any(entity => !IsObjectValid(entity));

                if (hasInvalidEntitiesValidation)
                {
                    throw new InvalidOperationException("One or more entities in the DbSet are invalid.");
                }
            }

            using ConnectionManager connectionMgr = new ConnectionManager(this.connection);
            using SqlTransaction transaction = this.connection.StartTransaction();

            foreach (IEnumerable<object> dbSetInstance in dbSetInstances)
            {
                if (dbSetInstance == null) continue;

                MethodInfo persistMethod = typeof(DbContext)
                    .GetMethod(nameof(Persist), BindingFlags.Instance | BindingFlags.NonPublic)?
                    .MakeGenericMethod(dbSetInstance.GetType().GetGenericArguments().Single() ?? throw new InvalidOperationException());

                try
                {
                    try
                    {
                        persistMethod.Invoke(this, new object?[] { });
                    }
                    catch (TargetInvocationException tie) when (tie.InnerException != null)
                    {
                        throw tie.InnerException;
                    }
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                
            }
        }

        private void Persist<TEntity>(DbSet<TEntity> dbSetInstance)
            where TEntity : class, new()
        {
            string tableName = GetTableName(typeof(TEntity));
            string[] dbColumnNames = this.connection
                .FetchColumnNames(tableName)
                .ToArray();

            if (dbSetInstance.ChangeTracker.AddedEntities.Any())
            {
                this.connection.InsertEntities(dbSetInstance.ChangeTracker.AddedEntities, tableName, dbColumnNames);
            }

            IEnumerable<TEntity> modifiedEntities = dbSetInstance.ChangeTracker.GetModifiedEntities(dbSetInstance);
            if (modifiedEntities.Any())
            {
                this.connection.UpdateEntities(modifiedEntities, tableName, dbColumnNames);
            }

            if (dbSetInstance.ChangeTracker.RemovedEntities.Any())
            {
                this.connection.DeleteEntities(dbSetInstance.ChangeTracker.RemovedEntities, tableName, dbColumnNames);
            }
        }

        private IDictionary<Type, PropertyInfo> DiscoverDbSetProperties()
        {
            return this.GetType()
                .GetProperties()
                .Where(pi => pi.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .ToDictionary(pi => pi.PropertyType.GetGenericArguments().First(), pi => pi);
        }

        private void InitializeDbSets()
        {
            foreach ((Type dbSetType, PropertyInfo dbSetProperty) in this.dbSetProperties)
            {
                MethodInfo? populateDbSetMethod = typeof(DbContext)
                    .GetMethod(nameof(PopulateDbSet), BindingFlags.Instance | BindingFlags.NonPublic)
                    .MakeGenericMethod(dbSetType) ?? throw new InvalidOperationException("A general error occurred while initializing the DbSet data! Please raise an issue with detailed information.");
                populateDbSetMethod.Invoke(this, new object?[] { dbSetProperty });
            }
        }

        private void PopulateDbSet<TEntity>(PropertyInfo dbSetProperty)
            where TEntity : class, new()
        {
            IEnumerable<TEntity> entities = LoadTableEntities<TEntity>();
            DbSet<TEntity> dbSetInstance = new DbSet<TEntity>(entities);

            ReflectionHelper.ReplaceBackingField(this, dbSetProperty.Name, dbSetInstance);
        }

        private IEnumerable<TEntity> LoadTableEntities<TEntity>()
            where TEntity : class, new()
        {
            string tableName = GetTableName(typeof(TEntity));
            string[] entityColumns = GetEntityTableColumns(typeof(TEntity)).ToArray();

            IEnumerable<TEntity> fetchedEntityRows = this.connection
                .FetchResultSet<TEntity>(tableName, entityColumns);

            return fetchedEntityRows;
        }

        private string GetTableName(Type entityType)
        {
            string? tableName = entityType.GetCustomAttribute<TableAttribute>()?.Name;
            if (string.IsNullOrWhiteSpace(tableName))
            {
                // Fallback to the DbSet property name if the TableAttribute is not present
                tableName = this.dbSetProperties[entityType].Name;
            }

            return tableName;
        }

        private IEnumerable<string> GetEntityTableColumns(Type entityType)
        {
            string tableName = GetTableName(entityType);
            IEnumerable<string> dbColumnNames = this.connection
                .FetchColumnNames(tableName);

            IEnumerable<string> entityColumnNames = entityType
                .GetProperties()
                .Where(pi => dbColumnNames.Contains(pi.Name)
                        && !pi.HasAttribute<NotMappedAttribute>()
                        && AllowedSqlTypes.Contains(pi.PropertyType))
                .Select(pi => pi.Name)
                .ToArray();

            return entityColumnNames;
        }

        private void MapAllRelations()
        {
            foreach ((Type dbSetType, PropertyInfo dbSetProperty) in this.dbSetProperties)
            {
                object? dbSetInstance = dbSetProperty.GetValue(this);
                if (dbSetInstance == null)
                {
                    throw new InvalidOperationException($"DbSet<{dbSetType.Name}> property '{dbSetProperty.Name}' is not initialized! Ensure that the DbContext is properly constructed.");
                }

                MethodInfo? mapRelationsMethod = typeof(DbContext)
                    .GetMethod(nameof(MapRelations), BindingFlags.Instance | BindingFlags.NonPublic)
                    .MakeGenericMethod(dbSetType) ?? throw new InvalidOperationException("A general error occurred while mapping relations! Please raise an issue with detailed information.");

                mapRelationsMethod.Invoke(this, new object?[] { dbSetInstance });
            }
        }

        private void MapRelations<TEntity>(DbSet<TEntity> dbSetInstance)
            where TEntity : class, new()
        {
            Type entityType = typeof(TEntity);

            MapNavigationProperties(dbSetInstance);

            PropertyInfo[] navigationCollections = entityType
                .GetProperties()
                .Where(pi => pi.PropertyType.IsGenericType
                    && pi.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                .ToArray();

            foreach (PropertyInfo navCollectionProp in navigationCollections)
            {
                Type collectionEntityType = navCollectionProp
                    .PropertyType
                    .GenericTypeArguments
                    .Single();

                MethodInfo mapCollectionMethod = typeof(DbContext)
                    .GetMethod(nameof(MapCollection), BindingFlags.Instance | BindingFlags.NonPublic)?
                    .MakeGenericMethod(entityType, collectionEntityType) ?? throw new InvalidOperationException();
                mapCollectionMethod.Invoke(this, new object?[] { dbSetInstance, navCollectionProp });
            }
        }

        private void MapNavigationProperties<TEntity>(DbSet<TEntity> dbSetInstance)
            where TEntity : class, new()
        {
            Type entityType = typeof(TEntity);
            PropertyInfo[] foreignKeyProps = entityType
                .GetProperties()
                .Where(pi => pi.HasAttribute<ForeignKeyAttribute>())
                .ToArray();

            foreach (PropertyInfo foreignKeyPropInfo in foreignKeyProps)
            {
                string navigationPropertyName = foreignKeyPropInfo
                    .GetCustomAttribute<ForeignKeyAttribute>()!.Name;

                PropertyInfo navigationPropertyInfo = entityType
                    .GetProperty(navigationPropertyName) ?? throw new InvalidOperationException($"Navigation property '{navigationPropertyName}' not found in entity '{entityType.Name}'.");

                if (navigationPropertyInfo == null) continue;

                object navEntityDbSet = this.dbSetProperties[navigationPropertyInfo.PropertyType]
                    .GetValue(this) ?? throw new InvalidOperationException($"DbSet for navigation property '{navigationPropertyName}' is not initialized.");
                PropertyInfo navEntityPrimaryKeyProp = navigationPropertyInfo.PropertyType
                    .GetProperties()
                    .Single(pi => pi.HasAttribute<KeyAttribute>());

                foreach (TEntity entity in dbSetInstance)
                {
                    object? foreignKeyValue = foreignKeyPropInfo.GetValue(entity);

                    if (foreignKeyValue == null) continue;

                    object navigationPropertyEntityInstanceValue = ((IEnumerable<object>)navEntityDbSet)
                        .Single(navEntity => navEntityPrimaryKeyProp.GetValue(navEntity)?.Equals(foreignKeyValue) ?? false);

                    navigationPropertyInfo.SetValue(entity, navigationPropertyEntityInstanceValue);
                }
            }
        }

        private void MapCollection<TEntity, TCollection>(DbSet<TEntity> dbSetInstance, PropertyInfo navCollectionProp)
            where TEntity : class, new()
            where TCollection : class, new()
        {
            Type entityType = typeof(TEntity);
            Type navigationCollectionType = typeof(TCollection);

            // If 1:N => we will have an array of single PK
            // If N:M => we will have an array of composite PKs
            PropertyInfo[] navigationEntityPrimaryKeyProps = navigationCollectionType
                .GetProperties()
                .Where(pi => pi.HasAttribute<KeyAttribute>())
                .ToArray();

            PropertyInfo navigationEntityPrimaryKeyProp = navigationEntityPrimaryKeyProps
                .First();
            PropertyInfo entityPrimaryKeyProp = entityType
                .GetProperties()
                .Single(pi => pi.HasAttribute<KeyAttribute>());
        
            bool isManyToMany = navigationEntityPrimaryKeyProps.Length > 1;
            if (isManyToMany)
            {
                navigationEntityPrimaryKeyProp = navigationCollectionType
                    .GetProperties()
                    .Single(pi => navigationCollectionType
                        .GetProperty(pi.GetCustomAttribute<ForeignKeyAttribute>()!.Name)?.PropertyType == entityType);
            }

            DbSet<TCollection> navEntityDbSet = (DbSet<TCollection>)(this.dbSetProperties[navigationCollectionType]
                .GetValue(this) ?? throw new InvalidOperationException("A general error."));

            foreach (TEntity entity in dbSetInstance)
            {
                object entityPrimaryKeyValue = entityPrimaryKeyProp.GetValue(entity) ?? throw new InvalidOperationException($"Entity '{entityType.Name}' has a null primary key value.");
                IEnumerable<object> navCollectionEntities = navEntityDbSet
                    .Where(navEntity => navigationEntityPrimaryKeyProp.GetValue(navEntity)?.Equals(entityPrimaryKeyValue) ?? false)
                    .ToArray();

                ReflectionHelper.ReplaceBackingField(entity, navCollectionProp.Name, navCollectionEntities);
            }
        }

        private bool IsObjectValid(object entity)
        {
            ValidationContext validationContext = new ValidationContext(entity);
            IList<ValidationResult> validationResults = new List<ValidationResult>();
            bool isValid = Validator
                .TryValidateObject(entity, validationContext, validationResults, true);
            return isValid;
        }

        public void Dispose()
        {
            this.connection.Dispose();
        }
    }
}
