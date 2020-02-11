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

        public static string ConnectionString { get; set; }

        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool CanConnectToServer => Host != null && Host.Length > 0 && Username != null && Username.Length > 0 && Password != null && Password.Length > 0;

        public void ConnectToServer()
        {
            string connString = $"server={Host};user id={Username};password={Password};database=project";

            MySqlConnection connection = null;

            try
            {
                // Try to connect to the server
                connection = new MySqlConnection(connString);
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
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringsSection connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            string connectionString = connectionStringsSection.ConnectionStrings["dbEntities"].ConnectionString;

            // Replace connection info
            connectionString = connectionString.Replace("localhost", Host);
            connectionString = connectionString.Replace("server", Username);
            connectionString = connectionString.Replace("123456789", Password);

            // Set the connection string
            ConnectionString = connectionString;
        }
    }
}
