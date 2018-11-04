using Microsoft.VisualStudio.TestTools.UnitTesting;
using HumanResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HumanResources.ServiceReference1;

namespace HumanResources.Tests
{
    [TestClass()]
    public class WorkerWindowTests
    {
        Service1Client m_serviceClient = new Service1Client();

        [TestMethod()]
        public void LoadWorkersTest()
        {
            var workers = m_serviceClient.GetAllEmployeesPerComapny(new Company { Id = 1 });

            Assert.IsTrue(workers.Length > 0);
            Assert.IsFalse(string.IsNullOrEmpty(workers[0].Name));
            Assert.IsTrue(workers[0].Starting_date != null); // correctly loaded date
            Assert.IsTrue(workers[0].Starting_date != new DateTime()); // correctly loaded date
        }

        [TestMethod()]
        public void AddEditDeleteWorkerTests()
        {
            var employee = new Employee { Name = "TestEmployee", Company_id = 1, Experience = 1, Starting_date = DateTime.Now, Salary = 1, Vacation_days = 1 };
            Assert.IsTrue(m_serviceClient.AddEmployee(employee));

            var workers = m_serviceClient.GetAllEmployeesPerComapny(new Company { Id = 1 });
            var testEmployees = workers.Where(worker => worker.Name == "TestEmployee");

            Assert.AreEqual(1, testEmployees.Count());

            Assert.IsTrue(m_serviceClient.DeleteEmployee(testEmployees.First()));

            workers = m_serviceClient.GetAllEmployeesPerComapny(new Company { Id = 1 });
            testEmployees = workers.Where(worker => worker.Name == "TestEmployee");

            Assert.AreEqual(0, testEmployees.Count());
        }

        [TestMethod()]
        public void DeleteCompanyTest()
        {
            Assert.IsTrue(m_serviceClient.AddCompany(new Company { Name = "TestCompany" }));

            var company = m_serviceClient.GetAllCompanies().Where(item => item.Name == "TestCompany").First();

            var employee = new Employee { Name = "TestEmployee", Company_id = company.Id, Experience = 1, Starting_date = DateTime.Now, Salary = 1, Vacation_days = 1 };
            Assert.IsTrue(m_serviceClient.AddEmployee(employee));

            // It deletes its employees as well
            Assert.IsTrue(m_serviceClient.DeleteCompany(company));

            Assert.AreEqual(m_serviceClient.GetAllCompanies().Where(item => item.Name == "TestCompany").Count(), 0);
            Assert.AreEqual(m_serviceClient.GetAllEmployeesPerComapny(new Company{ Id = company.Id }).Count(), 0);
        }
    }
}
