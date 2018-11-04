using HumanResources.ServiceReference1;
using System.Windows;
using System.Windows.Data;
using System;
using System.Globalization;
using System.Windows.Controls;

namespace HumanResources
{
    /// <summary>
    /// Interaction logic for EmployeesWindow.xaml
    /// </summary>
    public partial class EmployeesWindow : Window
    {
        Service1Client m_serviceClient;
        Company m_company;

        public EmployeesWindow(Service1Client service, Company company)
        {
            InitializeComponent();

            Title += " - " + company.Name;

            m_serviceClient = service;
            m_company = company;

            EmployeesComboBox.ItemsSource = m_serviceClient.GetAllEmployeesPerComapny(m_company);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new WorkerWindow(m_serviceClient, m_company, 
                new Employee{ Company_id = m_company.Id, Starting_date=DateTime.Now }, true);

            bool? dialogResult = window.ShowDialog();
            if(dialogResult == null || dialogResult.Value != true)
            {
                return;
            }

            EmployeesComboBox.ItemsSource = m_serviceClient.GetAllEmployeesPerComapny(m_company);
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new WorkerWindow(m_serviceClient, m_company, EmployeesComboBox.SelectedItem as Employee, false);

            bool? dialogResult = window.ShowDialog();
            if (dialogResult == null || dialogResult.Value != true)
            {
                return;
            }

            EmployeesComboBox.ItemsSource = m_serviceClient.GetAllEmployeesPerComapny(m_company);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var employee = EmployeesComboBox.SelectedItem as Employee;

            if (!m_serviceClient.DeleteEmployee(employee))
            {
                MessageBox.Show($"There was a problem deleting employee\"{employee.Name}\".");
                return;
            }

            EmployeesComboBox.ItemsSource = m_serviceClient.GetAllEmployeesPerComapny(m_company);
        }

        private void EmployeesComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(EmployeesComboBox.SelectedItem is Employee)
            {
                OpenButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }
            else
            {
                OpenButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
