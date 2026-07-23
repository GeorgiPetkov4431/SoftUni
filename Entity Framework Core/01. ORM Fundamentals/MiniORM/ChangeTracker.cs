namespace MiniORM
{
    // Always use this packet for KeyAttribute in EF Core
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    public class ChangeTracker<T>
        where T : class, new()
    {
        private ICollection<T> allEntities;
        private ICollection<T> added;
        private ICollection<T> removed;

        private ChangeTracker()
        {
            this.added = new List<T>();
            this.removed = new List<T>();
        }

        public ChangeTracker(IEnumerable<T> entities)
            : this()
        {
            this.allEntities = CloneEntities(entities);
        }

        public IReadOnlyCollection<T> AllEntities
        {
            get { return this.allEntities.ToList().AsReadOnly(); }
        }

        public IReadOnlyCollection<T> AddedEntities
        {
            get { return this.added.ToList().AsReadOnly(); }
        }

        public IReadOnlyCollection<T> RemovedEntities
        {
            get { return this.removed.ToList().AsReadOnly(); }
        }

        /// <summary>
        /// Event handler that is called when new entity is added to the context.
        /// It adds the entity to the added collection.
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            this.added.Add(entity);
        }

        /// <summary>
        /// Event handler that is called when new entity is removed from the context.
        /// It adds the entity to the removed collection.
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(T entity)
        {
            this.removed.Add(entity);
        }

        public IEnumerable<T> GetModifiedEntities(DbSet<T> dbSet)
        {
            ICollection<T> modifiedEntities = new List<T>();

            PropertyInfo[] primaryKeys = typeof(T)
                .GetProperties()
                .Where(pi => pi.HasAttribute<KeyAttribute>())
                .ToArray();
            
            foreach (T proxyEntity in AllEntities)
            {
                object[] primaryKeyValues = GetPrimaryKeyValues(primaryKeys, proxyEntity).ToArray();

                T? entity = dbSet.Entities
                    .FirstOrDefault(e => GetPrimaryKeyValues(primaryKeys, e).SequenceEqual(primaryKeyValues));

                if (entity == null)
                {
                    throw new InvalidOperationException("Database entity is not found for a proxy entity in the change tracker! Ensure that DbContext is not used in parallel in a multithreaded environment!");
                }

                bool isEntityModified = IsModified(proxyEntity, entity);
                if (isEntityModified)
                {
                    modifiedEntities.Add(entity);
                }
            }

            return modifiedEntities;
        }

        private static bool IsModified(T proxyEntity, T entity)
        {
            PropertyInfo[] propertiesToCompare = typeof(T)
                .GetProperties()
                .Where(pi => DbContext.AllowedSqlTypes.Contains(pi.PropertyType))
                .ToArray();

            bool isModified = false;
            foreach (PropertyInfo dbProp in propertiesToCompare)
            {
                object? proxyValue = dbProp.GetValue(proxyEntity);
                object? entityValue = dbProp.GetValue(entity);

                if (proxyValue == null && entityValue == null) continue;

                if (proxyValue != null || entityValue != null)
                {
                    if (!(proxyValue?.Equals(entityValue) ?? false))
                    {
                        isModified = true;
                        break;
                    }
                }
            }

            return isModified;
        }

        /* Return type is a collection of non-nullable objects, since PK cannot have null value */
        private static IEnumerable<object> GetPrimaryKeyValues(IEnumerable<PropertyInfo> primaryKeyProperties, T entity)
        {
            return primaryKeyProperties
                .Select(pk => pk.GetValue(entity)!)
                .ToArray();
        }

        private ICollection<T> CloneEntities(IEnumerable<T> entities)
        {
            ICollection<T> clonedEntities = new List<T>();

            PropertyInfo[] propertiesToClone = typeof(T)
                .GetProperties()
                .Where(pi => DbContext.AllowedSqlTypes.Contains(pi.PropertyType))
                .ToArray();

            foreach (T entity in entities)
            {
                T clonedEntity = Activator.CreateInstance<T>();
                foreach (PropertyInfo cloneableProp in propertiesToClone)
                {
                    object? propValue = cloneableProp.GetValue(entity);
                    cloneableProp.SetValue(clonedEntity, propValue);
                }

                clonedEntities.Add(clonedEntity);
            }

            return clonedEntities;
        }
    }
}