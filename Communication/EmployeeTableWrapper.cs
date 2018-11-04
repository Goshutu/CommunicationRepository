using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace Service
{
    public class EmployeeTableWrapper : SQLWrapper
    {
        public List<Employee> RunSelectQuery(int? whereID = null, string whereColumn = "ID")
        {
            string query = "SELECT * FROM EMPLOYEES";

            if(whereID != null)
            {
                query += $" WHERE {whereColumn} = {whereID.ToString()}";
            }

            PreExecute(query);

            SqlDataReader reader = m_command.ExecuteReader();
            var table = ReadEmployeesTable(reader);

            PostExecute();

            return table;
        }

        public List<Employee> ReadEmployeesTable(SqlDataReader reader)
        {
            var employeeList = new List<Employee>();
            while (reader.Read())
            {
                Employee employee = new Employee();
                employee.Id = Convert.ToInt32(reader[0]);
                employee.Name = reader[1].ToString().TrimEnd(' ');
                employee.Company_id = Convert.ToInt32(reader[2]);
                employee.Experience = Convert.ToInt32(reader[3]);
                employee.Starting_date = Convert.ToDateTime(reader[4]);
                employee.Salary = Convert.ToDouble(reader[5]);
                employee.Vacation_days = Convert.ToInt32(reader[6]);

                employeeList.Add(employee);
            }

            return employeeList;
        }

        public bool AddEmployee(Employee employee)
        {
            try
            {
                RunPreparedQuery($"INSERT INTO EMPLOYEES VALUES('{employee.Name}',{employee.Company_id},{employee.Experience}, '{employee.Starting_date}',{employee.Salary},{employee.Vacation_days})");
            }
            catch (SqlException ex)
            {
                return false;
            }

            return true;
        }

        public bool EditEmployee(Employee employee)
        {
            try
            {
                RunPreparedQuery($"UPDATE EMPLOYEES SET NAME = '{employee.Name}',COMPANY_ID = {employee.Company_id},EXPERIENCE = {employee.Experience},STARTING_DATE = '{employee.Starting_date}',SALARY = {employee.Salary},VACATION_DAYS = {employee.Vacation_days} WHERE ID = {employee.Id}");
            }
            catch (SqlException ex)
            {
                return false;
            }

            return true;
        }

        public bool DeleteEmployee(Employee employee)
        {
            try
            {
                RunPreparedQuery("DELETE FROM EMPLOYEES WHERE ID = " + employee.Id.ToString());
            }
            catch (SqlException ex)
            {
                return false;
            }

            return true;
        }
    }
}
