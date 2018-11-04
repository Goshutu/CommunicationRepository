using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        //[OperationContract]
        //string GetData(int value);

        //[OperationContract]
        //CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
        [OperationContract]
        List<Company> GetAllCompanies();

        [OperationContract]
        bool AddCompany(Company company);

        [OperationContract]
        bool DeleteCompany(Company company);

        [OperationContract]
        List<Employee> GetAllEmployeesPerComapny(Company company);

        [OperationContract]
        bool AddEmployee(Employee employee);

        [OperationContract]
        bool EditEmployee(Employee employee);

        [OperationContract]
        bool DeleteEmployee(Employee employee);
    }

    [DataContract]
    public class Company
    {
        int id;
        string name;

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }

    public class Employee
    {
        int id;
        string name;
        int company_id;
        int experience;
        DateTime starting_date;
        double salary;
        int vacation_days;

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public int Company_id
        {
            get { return company_id; }
            set { company_id = value; }
        }

        [DataMember]
        public int Experience
        {
            get { return experience; }
            set { experience = value; }
        }

        [DataMember]
        public DateTime Starting_date
        {
            get { return starting_date; }
            set { starting_date = value; }
        }

        [DataMember]
        public double Salary
        {
            get { return salary; }
            set { salary = value; }
        }

        [DataMember]
        public int Vacation_days
        {
            get { return vacation_days; }
            set { vacation_days = value; }
        }
    }
}
