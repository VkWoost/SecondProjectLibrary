using Library.DAL.Interfaces;
using Library.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Library.DAL.Repositories
{
    public class EFGenericADORepository<TEntity> : IGenericRepository<TEntity> where TEntity : Basic
    {
        private string _connectionString;

        public EFGenericADORepository(string conn)
        {
            _connectionString = conn;
        }

        public TEntity Get(int id)
        {
            string _sqlGet = String.Format("SELECT * FROM {0}s WHERE Id = {1}", typeof(TEntity).Name, id);
            TEntity item = (TEntity)Activator.CreateInstance(typeof(TEntity));

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(_sqlGet, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        foreach (var i in item.GetType().GetProperties())
                        {
                            i.SetValue(item, reader.GetValue(reader.GetOrdinal(i.Name)));
                        }
                    }
                }
            }
            return item;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            string _sqlGetAll = String.Format("SELECT * FROM {0}s", typeof(TEntity).Name);
            List<TEntity> result = new List<TEntity>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(_sqlGetAll, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        TEntity item = (TEntity)Activator.CreateInstance(typeof(TEntity));
                        foreach (var i in item.GetType().GetProperties())
                        {
                            i.SetValue(item, reader.GetValue(reader.GetOrdinal(i.Name)));
                        }
                        result.Add(item);
                    }
                }
                return result;
            }
        }

        public virtual void Create(TEntity item)
        {
            var stringOfColumns = string.Join(", ", GetColumnsWithoutId());
            var stringOfParameters = string.Join(", ", GetColumnsWithoutId().Select(e => "@" + e));
            string _sqlCreate = String.Format("INSERT INTO {0}s ({1}) VALUES ({2})", typeof(TEntity).Name, stringOfColumns, stringOfParameters);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(_sqlCreate, connection);
                List<string> list = new List<string>(GetColumnsWithoutId());
                int temp = 0;
                var types = item.GetType().GetProperties().Where(x => x.Name != "Id");
                foreach (var i in types)
                {
                    command.Parameters.Add(new SqlParameter(String.Format("@{0}", list[temp++]), i.GetValue(item)));
                }
                command.ExecuteNonQuery();
            }
        }

        public virtual void Update(TEntity item)
        {
            var stringOfColumns = string.Join(", ", GetColumnsWithoutId().Select(e => $"{e} = @{e}"));
            string _sqlUpdate = String.Format("UPDATE {0}s SET {1} WHERE Id = @Id", typeof(TEntity).Name, stringOfColumns);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(_sqlUpdate, connection);

                List<string> list = new List<string>(GetColumns());
                int temp = 0;
                foreach (var i in item.GetType().GetProperties())
                {
                    command.Parameters.Add(new SqlParameter(String.Format("@{0}", list[temp++]), i.GetValue(item)));
                }
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string _sqlDelete = String.Format("DELETE FROM {0}s WHERE id = {1}", typeof(TEntity).Name, id);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(_sqlDelete, connection);
                command.ExecuteNonQuery();
            }
        }

        private IEnumerable<string> GetColumns()
        {
            return typeof(TEntity)
                    .GetProperties()
                    .Select(e => e.Name);
        }

        private IEnumerable<string> GetColumnsWithoutId()
        {
            return typeof(TEntity)
                    .GetProperties()
                    .Where(e => e.Name != "Id" && !e.PropertyType.GetTypeInfo().IsGenericType)
                    .Select(e => e.Name);
        }
    }
}
