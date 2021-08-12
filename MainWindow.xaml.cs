using CRM_Shop.Context;
using System.Data.Entity;
using System.Windows;
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
        ViewController View;

        CollectionViewSource TableViewSource;

        string TableName;
        public MainWindow(CRMUser user, LoginWindow loginWindow)
        {
            InitializeComponent();
            View = new ViewController();

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
            View.SetColumns(MainDataGrid, TableName);
            TableViewSource = ((CollectionViewSource)(FindResource("userViewSource")));
            _context.Users.Include(r => r.UserRole).Include(c => c.Employee).Load();
            TableViewSource.Source = _context.Users.Local;
            
        }

        private void EmployeeMI_Click(object sender, RoutedEventArgs e)
        {
            DGDockPanel.DataContext = FindResource("employeeViewSource");
            TableName = "Employee";
            View.SetColumns(MainDataGrid, TableName);
            TableViewSource = ((CollectionViewSource)(FindResource("employeeViewSource")));
            _context.Employees.Include(c => c.Post).Load();
            TableViewSource.Source = _context.Employees.Local;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
