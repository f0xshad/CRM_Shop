using CRM_Shop.Context;
using System.Windows;
using System.Linq;
using System;

namespace CRM_Shop.DatabaseTransaction
{
    class EmployeeName
    {
        public string FullName { get; set; }
    }
    
    public partial class UserForm : Window
    {
        CRMContext _context;
        User user;

        public UserForm(CRMContext context)
        {
            InitializeComponent();
            _context = context;
            FillList();
        }

        public UserForm(int recordId, CRMContext context)
        {
            InitializeComponent();
            _context = context;
            FillList();
            FillForm(recordId);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var role = _context.UserRoles.Single(c => c.RoleName == UserRole_CB.SelectedItem.ToString());

                if (user == null)
                {
                    user = new User()
                    {
                        Login = Login_TB.Text,
                        Password = Password_PB.Password,
                        RoleId = role.RoleId
                    };
                    _context.Users.Add(user);
                }
                else
                {
                    user.Login = Login_TB.Text;
                    user.Password = Password_PB.Password;
                    user.RoleId = role.RoleId;
                }

                _context.SaveChanges();
                Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Произошла ошибка при добавлении/изменении записи!\nПроверьте введенные данные");
                MessageBox.Show(ex.Message);
            }
        }

        private void FillList()
        {
            var roles = _context.UserRoles.ToList();
            foreach (var role in roles)
            {
                UserRole_CB.Items.Add(role.RoleName);
            }
        }

        private void FillForm(int recordId)
        {
            user = _context.Users.Find(recordId);
            Login_TB.Text = user.Login;
            Password_PB.Password = user.Password;
            UserRole_CB.SelectedItem = user.UserRole.RoleName;
        }
    }
}
