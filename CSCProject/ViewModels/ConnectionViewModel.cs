using Caliburn.Micro;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CSCProject.ViewModels
{
    class ConnectionViewModel : Screen, INotifyPropertyChanged
    {
        private WindowManager windowManager = new WindowManager();

        public static string ConnectionString { get; set; } = ((ConnectionStringsSection)ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).GetSection("connectionStrings")).ConnectionStrings["dbEntities"].ConnectionString;

        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool CanConnectToServer => Host != null && Host.Length > 0 && Username != null && Username.Length > 0 && Password != null && Password.Length > 0;

        public void ConnectToServer()
        {
            string[] HostDetails = Host.Split(':');

            string connString = $"server={HostDetails[0]};port={(HostDetails.Count() > 1 ? int.Parse(HostDetails[1]) : 3306)};user id={Username};password={Password};database=project";
            try
            {
                // Try to connect to the server
                MySqlConnection connection = new MySqlConnection(connString);
                connection.Open();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Save the connection string
            SaveConnectionString();

            // Show the shell view model
            windowManager.ShowWindow(new ShellViewModel());

            // Close the window
            TryClose();
        }

        private void SaveConnectionString()
        {
            string[] HostDetails = Host.Split(':');

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringsSection connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            string connectionString = connectionStringsSection.ConnectionStrings["dbEntities"].ConnectionString;

            // Replace connection info
            connectionString = connectionString.Replace("localhost", HostDetails[0]);
            connectionString = connectionString.Replace("3306", HostDetails.Count() > 1 ? HostDetails[1] : "3306");
            connectionString = connectionString.Replace("server", Username);
            connectionString = connectionString.Replace("123456789", Password);

            // Set the connection string
            ConnectionString = connectionString;
        }
    }
}
