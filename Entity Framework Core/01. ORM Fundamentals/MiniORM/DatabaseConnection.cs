
/// <summary>
/// Used for accessing a database, inserting/updating/deleting entities
/// and mapping database columns to entity classes.
/// </summary>
namespace MiniORM
{
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;
    using Microsoft.Data.SqlClient;

    internal class DatabaseConnection : IDisposable
    {
        private readonly SqlConnection connection;

        private SqlTransaction transaction;

        public DatabaseConnection(string connectionString)
        {
            this.connection = new SqlConnection(connectionString);
        }

        private SqlCommand CreateCommand(string queryText, params SqlParameter[] parameters)
        {
            var command = new SqlCommand(queryText, this.connection, this.transaction);

            foreach (var param in parameters)
            {
                command.Parameters.Add(param);
            }

            return command;
        }

        public int ExecuteNonQuery(string queryText, params SqlParameter[] parameters)
        {
            using (SqlCommand query = CreateCommand(queryText, parameters))
            {
                int result = query.ExecuteNonQuery();

                return result;
            }
        }

        public IEnumerable<string> FetchColumnNames(string tableName)
        {
            ICollection<string> rows = new List<string>();

            var queryText = $@"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";

            using (var query = CreateCommand(queryText))
            {
                using (var reader = query.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var column = reader.GetString(0);

                        rows.Add(column);
                    }
                }
            }

            return rows;
        }

        public IEnumerable<T> ExecuteQuery<T>(string queryText)
        {
            List<T> rows = new List<T>();

            using (SqlCommand query = CreateCommand(queryText))
            {
                using (SqlDataReader reader = query.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var columnValues = new object[reader.FieldCount];
                        reader.GetValues(columnValues);

                        var obj = reader.GetFieldValue<T>(0);
                        rows.Add(obj);
                    }
                }
            }

            return rows;
        }

        public IEnumerable<T> FetchResultSet<T>(string tableName, params string[] columnNames)
        {
            var rows = new List<T>();

            var escapedColumns = string.Join(", ", columnNames.Select(EscapeColumn));
            var queryText = $@"SELECT {escapedColumns} FROM {tableName}";

            using (var query = CreateCommand(queryText))
            {
                using (var reader = query.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var columnValues = new object[reader.FieldCount];
                        reader.GetValues(columnValues);

                        var obj = MapColumnsToObject<T>(columnNames, columnValues);
                        rows.Add(obj);
                    }
                }
            }

            return rows;
        }

        public void InsertEntities<T>(IEnumerable<T> entities, string tableName, string[] columns)
            where T : class
        {
            IEnumerable<string> identityColumns = GetIdentityColumns(tableName);
            IEnumerable<string> columnsToInsert = columns
                .Except(identityColumns)
                .ToArray();

            IEnumerable<string> escapedColumns = columnsToInsert
                .Select(EscapeColumn)
                .ToArray();

            IEnumerable<object[]> rowValues = entities
                .Select(entity => columnsToInsert
                    .Select(c => entity.GetType().GetProperty(c).GetValue(entity))
                    .ToArray())
                .ToArray();

            IEnumerable<string[]> rowParameterNames = Enumerable.Range(1, rowValues.Count())
                .Select(i => columnsToInsert.Select(c => c + i).ToArray())
                .ToArray();

            string sqlColumns = string.Join(", ", escapedColumns);

            string sqlRows = string.Join(", ",
                rowParameterNames.Select(p =>
                    string.Format("({0})",
                        string.Join(", ", p.Select(a => $"@{a}")))));

            string query = string.Format(
                "INSERT INTO {0} ({1}) VALUES {2}",
                tableName,
                sqlColumns,
                sqlRows
            );

            SqlParameter[] parameters = rowParameterNames
                .Zip(rowValues, (@params, values) =>
                    @params.Zip(values, (param, value) =>
                        new SqlParameter(param, value ?? DBNull.Value)))
                .SelectMany(p => p)
                .ToArray();

            int insertedRows = this.ExecuteNonQuery(query, parameters);

            if (insertedRows != entities.Count())
            {
                throw new InvalidOperationException($"Could not insert {entities.Count() - insertedRows} rows.");
            }
        }

        public void UpdateEntities<T>(IEnumerable<T> modifiedEntities, string tableName, string[] columns)
            where T : class
        {
            IEnumerable<string> identityColumns = GetIdentityColumns(tableName);

            IEnumerable<string> columnsToUpdate = columns.Except(identityColumns).ToArray();

            PropertyInfo[] primaryKeyProperties = typeof(T)
                .GetProperties()
                .Where(pi => pi.HasAttribute<KeyAttribute>())
                .ToArray();

            foreach (T entity in modifiedEntities)
            {
                object?[] primaryKeyValues = primaryKeyProperties
                    .Select(c => c.GetValue(entity))
                    .ToArray();

                SqlParameter[] primaryKeyParameters = primaryKeyProperties
                    .Zip(primaryKeyValues, (param, value) => new SqlParameter(param.Name, value))
                    .ToArray();

                object?[] rowValues = columnsToUpdate
                    .Select(c => entity.GetType().GetProperty(c).GetValue(entity) ?? DBNull.Value)
                    .ToArray();

                SqlParameter[] columnsParameters = columnsToUpdate.Zip(rowValues, (param, value) => new SqlParameter(param, value))
                    .ToArray();

                string columnsSql = string.Join(", ",
                    columnsToUpdate.Select(c => $"{c} = @{c}"));

                string primaryKeysSql = string.Join(" AND ",
                    primaryKeyProperties.Select(pk => $"{pk.Name} = @{pk.Name}"));

                string query = string.Format("UPDATE {0} SET {1} WHERE {2}",
                    tableName,
                    columnsSql,
                    primaryKeysSql);

                int updatedRows = this.ExecuteNonQuery(query, columnsParameters.Concat(primaryKeyParameters).ToArray());

                if (updatedRows != 1)
                {
                    throw new InvalidOperationException($"Update for table {tableName} failed.");
                }
            }
        }

        public void DeleteEntities<T>(IEnumerable<T> entitiesToDelete, string tableName, string[] columns)
            where T : class
        {
            PropertyInfo[] primaryKeyProperties = typeof(T).GetProperties()
                .Where(pi => pi.HasAttribute<KeyAttribute>())
                .ToArray();

            foreach (var entity in entitiesToDelete)
            {
                object?[] primaryKeyValues = primaryKeyProperties
                    .Select(c => c.GetValue(entity))
                    .ToArray();

                SqlParameter[] primaryKeyParameters = primaryKeyProperties
                    .Zip(primaryKeyValues, (param, value) => new SqlParameter(param.Name, value))
                    .ToArray();

                string primaryKeysSql = string.Join(" AND ",
                    primaryKeyProperties.Select(pk => $"{pk.Name} = @{pk.Name}"));

                string query = string.Format("DELETE FROM {0} WHERE {1}",
                    tableName,
                    primaryKeysSql);

                int updatedRows = this.ExecuteNonQuery(query, primaryKeyParameters);

                if (updatedRows != 1)
                {
                    throw new InvalidOperationException($"Delete for table {tableName} failed.");
                }
            }
        }

        private IEnumerable<string> GetIdentityColumns(string tableName)
        {
            const string identityColumnsSql =
                "SELECT COLUMN_NAME FROM (SELECT COLUMN_NAME, COLUMNPROPERTY(OBJECT_ID(TABLE_NAME), COLUMN_NAME, 'IsIdentity') AS IsIdentity FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{0}') AS IdentitySpecs WHERE IsIdentity = 1";

            // TODO: Introduce SqlParameters to prevent SQL injection
            string parametrizedSql = string.Format(identityColumnsSql, tableName);

            IEnumerable<string> identityColumns = ExecuteQuery<string>(parametrizedSql);

            return identityColumns;
        }

        public SqlTransaction StartTransaction()
        {
            this.transaction = this.connection.BeginTransaction();
            return this.transaction;
        }

        public void Open() => this.connection.Open();

        public void Close() => this.connection.Close();

        private static string EscapeColumn(string c)
        {
            var escapedColumn = $"[{c}]";
            return escapedColumn;
        }

        private static T MapColumnsToObject<T>(string[] columnNames, object[] columns)
        {
            var obj = Activator.CreateInstance<T>();

            for (var i = 0; i < columns.Length; i++)
            {
                var columnName = columnNames[i];
                var columnValue = columns[i];

                if (columnValue is DBNull)
                {
                    columnValue = null;
                }

                var property = typeof(T).GetProperty(columnName);
                property.SetValue(obj, columnValue);
            }

            return obj;
        }

        public void Dispose()
        {
            this.connection.Dispose();
            this.transaction?.Dispose();
        }
    }
}
