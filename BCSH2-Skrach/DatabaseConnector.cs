using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace BCSH2_Skrach
{
    public class DatabaseConnector : IDisposable
    {
        private SQLiteConnection _connection;
        private string databasePath = "Database.db";

        public IDbConnection Connection
        {
            get { return _connection; }
        }

        public DatabaseConnector()
        {
            _connection = new SQLiteConnection($"Data Source={databasePath};Version=3;");
        }

        public void Connect()
        {
            _connection.Open();
        }

        public void Disconnect()
        {
            _connection.Close();
        }

        public SQLiteDataReader ExecuteQuery(string sql, params SQLiteParameter[] parameters)
        {
            SQLiteCommand command = _connection.CreateCommand();
            command.CommandText = sql;

            foreach (var param in parameters)
            {
                command.Parameters.Add(param);
            }

            return command.ExecuteReader();
        }

        public T ExecuteScalar<T>(string sql, params SQLiteParameter[] parameters)
        {
            SQLiteCommand command = _connection.CreateCommand();
            command.CommandText = sql;

            foreach (var param in parameters)
            {
                command.Parameters.Add(param);
            }

            return (T)(dynamic)command.ExecuteScalar();
        }

        public async Task ConnectAsync()
        {
            if (_connection.State != ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
        }

        public void ExecuteNonQuery(string sql, params SQLiteParameter[] parameters)
        {
            SQLiteCommand command = _connection.CreateCommand();
            command.CommandText = sql;

            foreach (var param in parameters)
            {
                command.Parameters.Add(param);
            }

            command.ExecuteNonQuery();
        }
        public async Task<SQLiteDataReader> ExecuteQueryAsync(string sqlQuery, params SQLiteParameter[] parameters)
        {
            using (var command = new SQLiteCommand(sqlQuery, _connection))
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                await _connection.OpenAsync();
                return (SQLiteDataReader)await command.ExecuteReaderAsync();
            }
        }

        public IEnumerable<T> Query<T>(string sql, object parameters = null)
        {
            using (var command = new SQLiteCommand(sql, _connection))
            {
                if (parameters != null)
                {
                    foreach (var param in parameters.GetType().GetProperties())
                    {
                        command.Parameters.AddWithValue(param.Name, param.GetValue(parameters));
                    }
                }

                return _connection.Query<T>(sql, parameters);
            }
        }


        public void Dispose()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }
    }
}


