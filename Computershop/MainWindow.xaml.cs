using System;
using System.Data.SqlClient;
using System.Windows;
using ComputerShop.Services;


namespace Computershop
{
    public partial class MainWindow : Window
    {
        IDatabase database = new Users();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
          MessageBox.Show(database.GetData(UsernameTextBox.Text, PasswordVisible.Text).ToString());
        }
    }
}