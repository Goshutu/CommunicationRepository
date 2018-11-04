using HumanResources.ServiceReference1;
using System.Windows;
using System.Windows.Data;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Linq;

namespace HumanResources
{
    /// <summary>
    /// Interaction logic for WorkerWindow.xaml
    /// </summary>
    public partial class WorkerWindow : Window
    {
        enum Experience
        {
            A,
            B,
            C,
            D
        }

        Service1Client m_serviceClient;
        bool m_addMode;

        public WorkerWindow(Service1Client service, Company company, Employee employee, bool addMode = false)
        {
            InitializeComponent();

            Title += " - " + company.Name;

            m_addMode = addMode;
            m_serviceClient = service;

            this.DataContext = employee;

            employeeExperenceComboBox.ItemsSource = Enum.GetValues(typeof(Experience)).Cast<Int32>();
        }

        private bool Validate(Employee employee)
        {
            if(employee.Name == null || employee.Name.Length == 0)
            {
                MessageBox.Show("Employee name cannot be empty");
                employeeName.Focus();
                return false;
            }

            if (employee.Starting_date == new DateTime())
            {
                MessageBox.Show("Employee starting date cannot be empty");
                employeeName.Focus();
                return false;
            }

            if (employee.Salary == 0)
            {
                MessageBox.Show("Employee salary cannot be empty");
                employeeName.Focus();
                return false;
            }

            if (employee.Vacation_days == 0)
            {
                MessageBox.Show("Employee vacation days cannot be empty");
                employeeName.Focus();
                return false;
            }

            return true;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var employee = DataContext as Employee;

            if (!Validate(employee))
            {
                return;
            }

            if(m_addMode)
            {
                m_serviceClient.AddEmployee(employee);
            }
            else
            {
                m_serviceClient.EditEmployee(employee);
            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
