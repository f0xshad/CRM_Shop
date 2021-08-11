using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CRM_Shop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CRMContext _context = new CRMContext();
        CRMUser CRMUser;
        LoginWindow LoginWindow;

        string TableName;
        public MainWindow(CRMUser user, LoginWindow loginWindow)
        {
            InitializeComponent();

            CRMUser = user;
            LoginWindow = loginWindow;
            EmployeeName_L.Text = CRMUser.GetEmployeeName();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show
            (
                "Вы точно хотите выйти?\nВсе несохраненные данные будут утеряны",
                "Подтвердите действие",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
            if (result == MessageBoxResult.Yes)
            {
                LoginWindow.Show();
                _context.Dispose();
            }
            else
                e.Cancel = true;
        }

        private void UserMI_Click(object sender, RoutedEventArgs e)
        {
            DGDockPanel.DataContext = FindResource("userViewSource");
            TableName = "User";
            SetColumns();
            CollectionViewSource userViewSource = ((CollectionViewSource)(FindResource("userViewSource")));
            _context.Users.Include(r => r.UserRole).Include(c => c.Employee).Load();
            userViewSource.Source = _context.Users.Local;
            
        }

        private void EmployeeMI_Click(object sender, RoutedEventArgs e)
        {
            DGDockPanel.DataContext = FindResource("employeeViewSource");
            TableName = "Employee";
            SetColumns();
            CollectionViewSource employeeViewSource = ((CollectionViewSource)(FindResource("employeeViewSource")));
            _context.Employees.Include(c => c.Post).Load();
            employeeViewSource.Source = _context.Employees.Local;
        }

        private void SetColumns()
        {
            MainDataGrid.Columns.Clear();
            switch (TableName)
            {
                case "User":
                    DataGridTextColumn c1 = new DataGridTextColumn()
                    {
                        Header = "Логин",
                        Binding = new Binding("Login")
                    };
                    MainDataGrid.Columns.Add(c1);
                    DataGridTextColumn c2 = new DataGridTextColumn()
                    {
                        Header = "Роль пользователя",
                        Binding = new Binding("UserRole.RoleName")
                    };
                    MainDataGrid.Columns.Add(c2);
                    DataGridTextColumn c3 = new DataGridTextColumn()
                    {
                        Header = "Сотрудник",
                        Binding = new Binding("Employee.LastName")
                    };
                    break;
                case "Employee":
                    DataGridTextColumn c4 = new DataGridTextColumn()
                    {
                        Header = "Фамилия",
                        Binding = new Binding("LastName")
                    };
                    MainDataGrid.Columns.Add(c4);
                    DataGridTextColumn c5 = new DataGridTextColumn()
                    {
                        Header = "Имя",
                        Binding = new Binding("FirstName")
                    };
                    MainDataGrid.Columns.Add(c5);
                    DataGridTextColumn c6 = new DataGridTextColumn()
                    {
                        Header = "Отчество",
                        Binding = new Binding("MiddleName")
                    };
                    MainDataGrid.Columns.Add(c6);
                    DataGridTextColumn c7 = new DataGridTextColumn()
                    {
                        Header = "Должность",
                        Binding = new Binding("Post.PostName")
                    };
                    MainDataGrid.Columns.Add(c7);
                    break;
                default:
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            // Загрузите данные, установив свойство CollectionViewSource.Source:
            // userViewSource.Source = [универсальный источник данных]
        }

        
    }
}
