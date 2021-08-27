using System;
using CRM_Shop.Context;
using CRM_Shop.DatabaseTransaction;
using System.Data;
using System.Data.Entity;
using System.Windows;
using System.Windows.Data;
using System.Linq;

namespace CRM_Shop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        CRMContext context = new CRMContext();
        CRMUser CRMUser;
        LoginWindow LoginWindow;
        ViewController View;

        CollectionViewSource TableViewSource;
        DatabaseTable Table;
        public MainWindow(CRMUser user, LoginWindow loginWindow)
        {
            InitializeComponent();
            View = new ViewController();
            Table = new DatabaseTable();

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
                context.Dispose();
            }
            else
                e.Cancel = true;
        }

        private void AddItem_B_Click(object sender, RoutedEventArgs e)
        {
            InsertTransaction(Table);
        }

        private void UpdateItem_B_Click(object sender, RoutedEventArgs e)
        {
            
            if (MainDataGrid.SelectedItems.Count > 0)
            {
                switch (Table.TableName)
                {
                    case "User":
                        User user = MainDataGrid.SelectedItem as User;
                        UpdateTransaction(Table, user.UserId);
                        break;
                    case "Employee":
                        Employee employee = MainDataGrid.SelectedItem as Employee;
                        UpdateTransaction(Table, employee.EmployeeId);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show(
                    "Необходимо выбрать строку!",
                    "Ошибка редактирования");
            }
        }
        
        private void DeleteItem_B_Click(object sender, RoutedEventArgs e)
        {
            if (MainDataGrid.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Вы хотите удалить выбранную запись? Отменить данное действие будет невозможно!",
                    "Подтвердите удаление",
                    MessageBoxButton.YesNo
                );
                if (result == MessageBoxResult.Yes)
                {
                    switch (Table.TableName)
                    {
                        case "User":
                            User user = MainDataGrid.SelectedItem as User;
                            DeleteTransaction(Table, user.UserId);
                            break;
                        case "Employee":
                            Employee employee = MainDataGrid.SelectedItem as Employee;
                            DeleteTransaction(Table, employee.EmployeeId);
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    "Необходимо выбрать строку!",
                    "Ошибка редактирования");
            }
        }

        private void UserMI_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DGDockPanel.DataContext = FindResource("userViewSource");
                Table.TableName = "User";
                View.SetColumns(MainDataGrid, Table.TableName);
                TableViewSource = ((CollectionViewSource)(FindResource("userViewSource")));
                context.Users.Include(r => r.UserRole).Where(u => u.UserId != -1).Load();
                TableViewSource.Source = context.Users.Local;
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Произошла ошибка при открытии таблицы",
                    "Ошибка");
            }
        }

        private void EmployeeMI_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DGDockPanel.DataContext = FindResource("employeeViewSource");
                Table.TableName = "Employee";
                View.SetColumns(MainDataGrid, Table.TableName);
                TableViewSource = ((CollectionViewSource)(FindResource("employeeViewSource")));
                context.Employees.Include(c => c.Post).Include(u => u.User).Load();
                TableViewSource.Source = context.Employees.Local;
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Произошла ошибка при открытии таблицы",
                    "Ошибка");
            }
        }

        private void InsertTransaction(IInsertable insertable)
        {
            insertable.InsertRecord(context);
        }
        private void UpdateTransaction(IUpdatable updatable, int recordId)
        {
            updatable.UpdateRecord(recordId, context);
        }
        private void DeleteTransaction(IDeletable deletable, int recordId)
        {
            deletable.DeleteRecord(recordId, context);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
