using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace Service
{
    public class CompanyTableWrapper : SQLWrapper
    {
        public List<Company> RunSelectQuery(int? whereID = null, string whereColumn = "ID")
        {
            string query = "SELECT * FROM COMPANIES";

            if (whereID != null)
            {
                query += $" WHERE {whereColumn} = {whereID.ToString()}";
            }

            PreExecute(query);

            SqlDataReader reader = m_command.ExecuteReader();
            var table = ReadCompaniesTable(reader);

            PostExecute();

            return table;
        }

        public List<Company> ReadCompaniesTable(SqlDataReader reader)
        {
            var compList = new List<Company>();
            while (reader.Read())
            {
                Company comp = new Company();
                comp.Id = Convert.ToInt32(reader[0]);
                comp.Name = reader[1].ToString().TrimEnd(' ');
                compList.Add(comp);
            }

            return compList;
        }

        public bool AddCompany(Company company)
        {
            try
            {
                RunPreparedQuery("INSERT INTO COMPANIES VALUES('" + company.Name + "')");
            }
            catch(SqlException)
            {
                return false;
            }

            return true;
        }

        public bool EditCompany(Company company)
        {
            try
            {
                RunPreparedQuery("UPDATE COMPANIES SET NAME = " + company.Name + " WHERE ID = " + company.Id.ToString());
            }
            catch (SqlException)
            {
                return false;
            }

            return true;
        }

        public bool DeleteCompany(Company company)
        {
            PreExecute();
            m_command.Transaction =  m_command.Connection.BeginTransaction();

            try
            {
                RunPreparedQuery("DELETE FROM EMPLOYEES WHERE COMPANY_ID = " + company.Id.ToString(), true);
                RunPreparedQuery("DELETE FROM COMPANIES WHERE ID = " + company.Id.ToString(), true);
            }
            catch(SqlException)
            {
                m_command.Transaction.Rollback();
                return false;
            }

            m_command.Transaction.Commit();
            PostExecute();

            return true;
        }
    }
}
