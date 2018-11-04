using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public List<Company> GetAllCompanies()
        {
            return new CompanyTableWrapper().RunSelectQuery();
        }

        public bool AddCompany(Company company)
        {
            return new CompanyTableWrapper().AddCompany(company);
        }

        public bool DeleteCompany(Company company)
        {
            return new CompanyTableWrapper().DeleteCompany(company);
        }

        public List<Employee> GetAllEmployeesPerComapny(Company company)
        {
            return new EmployeeTableWrapper().RunSelectQuery(company.Id, "COMPANY_ID");
        }

        public bool AddEmployee(Employee employee)
        {
            return new EmployeeTableWrapper().AddEmployee(employee);
        }

        public bool EditEmployee(Employee employee)
        {
            return new EmployeeTableWrapper().EditEmployee(employee);
        }

        public bool DeleteEmployee(Employee employee)
        {
            return new EmployeeTableWrapper().DeleteEmployee(employee);
        }
    }
}
