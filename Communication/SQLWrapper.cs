using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace Service
{
    public class SQLWrapper : IDisposable
    {
        private SqlConnection sqlConnection;
        private string connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=CompanyDatabae;Integrated Security=True;Pooling=False;Connect Timeout=30";
        protected SqlCommand m_command;

        public void EnsureConnectionIsOpen()
        {
            if (sqlConnection == null)
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
            }
        }

        protected void PreExecute(string query = null)
        {
            EnsureConnectionIsOpen();

            if (string.IsNullOrEmpty(query))
            {
                m_command = new SqlCommand();
                m_command.Connection = sqlConnection;
            }
            else
            {
                m_command = new SqlCommand(query, sqlConnection);
            }
        }

        protected void PostExecute()
        {
            m_command.Connection.Close();
        }

        public void RunPreparedQuery(string query, bool inTransaction = false)
        {
            if(!inTransaction)
                PreExecute();

            m_command.CommandText = query;
            m_command.ExecuteNonQuery();

            if (!inTransaction)
                PostExecute();
        }

        public void Dispose()
        {
            if (sqlConnection != null)
                sqlConnection.Close();
        }
    }
}
