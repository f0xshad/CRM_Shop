using CRM_Shop.Context;
using System;
using System.Linq;
using System.Windows;

namespace CRM_Shop
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        CRMUser User;
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            CRMContext context = new CRMContext();
            int? userId = context.ValidateUser(Login_TB.Text, Password_PB.Password).FirstOrDefault();

            switch (userId.Value)
            {
                case -1:
                    MessageBox.Show("Неправильный логин или пароль", "Ошибка");
                    break;
                default:
                    var userdata = context.GetUserData(userId);
                    EnterSystem(userdata);
                    break;
            }
        }

        private void EnterSystem(System.Data.Entity.Core.Objects.ObjectResult<UserData> userdata)
        {
            foreach (var u in userdata)
            {
                User = new CRMUser(u.Login, u.RoleName, u.EmployeeName);
            }
            MainWindow mainWindow = new MainWindow(User, this);
            mainWindow.Show();
            Hide();
        }
    }
}
