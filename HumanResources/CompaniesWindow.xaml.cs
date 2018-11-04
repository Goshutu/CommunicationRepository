using HumanResources.ServiceReference1;
using System.Windows;
using System.Windows.Data;
using System;
using System.Globalization;
using System.Windows.Controls;

namespace HumanResources
{
    /// <summary>
    /// Interaction logic for CompaniesWindow.xaml
    /// </summary>
    public partial class CompaniesWindow : Window
    {
        Service1Client m_serviceClient;

        public CompaniesWindow(Service1Client service)
        {
            InitializeComponent();

            m_serviceClient = service;
            Reload();
        }

        private void Reload()
        {
            CompaniesComboBox.ItemsSource = m_serviceClient.GetAllCompanies();
            CompaniesComboBox_SelectionChanged(null, null);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var text = CompaniesComboBox.Text;
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Cannot add - company name cannot be empty.");
                return;
            }

            var items = CompaniesComboBox.ItemsSource as Company[];

            foreach(Company item in items)
            {
                if(item.Name.ToLower() == text.ToLower())
                {
                    MessageBox.Show($"Cannot add - company \"{text} \" already exists.");
                    return;
                }
            }

            if(!m_serviceClient.AddCompany(new Company { Name = text }))
            {
                MessageBox.Show("There was a problem adding a new company.");
                return;
            }

            CompaniesComboBox.Text = "";
            Reload();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var company = CompaniesComboBox.SelectedItem as Company;
            var window = new EmployeesWindow(m_serviceClient, company);
            window.Owner = this;
            window.ShowDialog();
        }
        
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var company = CompaniesComboBox.SelectedItem as Company;

            if(!m_serviceClient.DeleteCompany(company))
            {
                MessageBox.Show($"There was a problem deleting company \"{company.Name}\".");
                return;
            }

            Reload();
        }

        private void CompaniesComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (CompaniesComboBox.SelectedItem is Company)
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
