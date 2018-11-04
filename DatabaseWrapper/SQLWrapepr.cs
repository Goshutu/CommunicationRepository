using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace CompanyDatabae
{
    public class SQLWrapepr : IDisposable
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

        protected void PreExecute()
        {
            EnsureConnectionIsOpen();

            m_command = new SqlCommand();
            m_command.Connection = sqlConnection;
            m_command.Connection.Open();
        }

        protected void PostExecute()
        {
            m_command.Connection.Close();
        }

        public void RunUpdateQuery(string query, int id)
        {
            PreExecute();
            m_command.CommandText = query + " WHERE [ID] = " + id.ToString();
            m_command.ExecuteNonQuery();

            PostExecute();
        }

        public void Dispose()
        {
            if (sqlConnection != null)
                sqlConnection.Close();
        }
    }
}
