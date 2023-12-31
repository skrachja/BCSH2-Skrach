using BCSH2_Skrach.Model;
using BCSH2_Skrach.View;
using System;
using System.Data.SQLite;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace BCSH2_Skrach
{
    public class LoginViewModel : ViewModelBase
    {

        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _surname;
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        private Window _loginWindow;

        public LoginViewModel(Window loginWindow)
        {
            _loginWindow = loginWindow;
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
            RegisterCommand = new RelayCommand(ExecuteRegister, CanExecuteLogin);

        }

        private bool CanExecuteLogin(object parameter)
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        private void ExecuteLogin(object parameter)
        {
            DatabaseConnector connector = new DatabaseConnector();
            connector.Connect();
            string sql = "SELECT * FROM Uzivatel WHERE username = @username AND heslo = @password";
            SQLiteParameter usernameParam = new SQLiteParameter("@username", _username);
            string passwordHash = CreateMD5(_password);
            SQLiteParameter passwordParam = new SQLiteParameter("@password", passwordHash);

            SQLiteDataReader reader = connector.ExecuteQuery(sql, usernameParam, passwordParam);

            // Check if the entered username and password are correct
            if (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("id"));
                int opravneni = reader.GetInt32(reader.GetOrdinal("opravneni"));
                string? jmeno = reader["jmeno"].ToString();
                string? prijmeni = reader["prijmeni"].ToString();
                string? username = reader["username"].ToString();
                string? password = reader["heslo"].ToString();
                connector.Disconnect();
                Uzivatel uzivatel = new(id, opravneni, jmeno, prijmeni, username, password);
                // Successful login - create and show a new window
                MainWindow mainWindow = new MainWindow(uzivatel);
                mainWindow.Show();

                // Close the login window
                _loginWindow.Close();

                // Call the method for successful login in the App class
                ((App)Application.Current).OnSuccessfulLogin();
            }
            else
            {
                // Show a message box indicating that the username or password is incorrect
                MessageBox.Show("Incorrect username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ExecuteRegister(object parameter)
        {
            DatabaseConnector connector = new DatabaseConnector();
            connector.Connect();

            // Check if the username already exists in the database
            string sqlCheck = "SELECT COUNT(*) FROM Uzivatel WHERE username = @username";
            SQLiteParameter usernameParam = new SQLiteParameter("@username", _username);
            long count = connector.ExecuteScalar<long>(sqlCheck, usernameParam);

            if (count > 0)
            {
                // Show a message box indicating that the username already exists
                MessageBox.Show("Username already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Open the registration dialog
            RegistrationDialogView dialog = new RegistrationDialogView();
            RegistrationDialogViewModel viewModel = new RegistrationDialogViewModel();
            dialog.DataContext = viewModel;

            if (dialog.ShowDialog() == true)
            {
                // Get the name and surname from the ViewModel
                _name = viewModel.Name;
                _surname = viewModel.Surname;

                // Insert the new user into the database
                string sqlInsert = "INSERT INTO Uzivatel (username, heslo, jmeno, prijmeni, opravneni) VALUES (@username, @password, @name, @surname, @opravneni)";
                string passwordHash = CreateMD5(_password);
                SQLiteParameter passwordParam = new SQLiteParameter("@password", passwordHash);
                SQLiteParameter nameParam = new SQLiteParameter("@name", _name);
                SQLiteParameter surnameParam = new SQLiteParameter("@surname", _surname);
                SQLiteParameter opravneniParam = new SQLiteParameter("@opravneni", 1);

                connector.ExecuteNonQuery(sqlInsert, usernameParam, passwordParam, nameParam, surnameParam, opravneniParam);
                connector.Disconnect();
                // Show a message box indicating that the registration was successful
                MessageBox.Show("Registration successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }




        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes);
            }
        }

    }
}
