using System.Windows;

namespace CRM_Shop
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            CRMContext context = new CRMContext();

            var user = context.GetUserData(Login_TB.Text, Password_PB.Password);

            foreach (var u in user)
            {
                CRMUser User = new CRMUser(u.Login, u.RoleName, u.EmployeeName);
            }
        }
    }
}
