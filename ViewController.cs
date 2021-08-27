using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;

namespace CRM_Shop
{
    class ViewController
    {
        List<string> BindingList = new List<string>();
        List<string> HeaderList = new List<string>();
        
        public void SetColumns(DataGrid MainDataGrid, string tableName)
        {
            MainDataGrid.Columns.Clear();
            BindingList.Clear();
            HeaderList.Clear();
            switch (tableName)
            {
                case "User":
                    BindingList.AddRange(new string[] { "UserId", "Login", "UserRole.RoleName" });
                    HeaderList.AddRange(new string[] { "ID", "Логин", "Роль пользователя" });
                    break;
                case "Employee":
                    BindingList.AddRange(new string[] { "EmployeeId", "LastName", "FirstName", "MiddleName", "Post.PostName", "User.Login"});
                    HeaderList.AddRange(new string[] { "ID", "Фамилия", "Имя", "Отчество", "Должность", "Учетная запись" });
                    break;
                default:
                    break;
            }

            for (int i = 0; i < HeaderList.Count; i++)
            {
                DataGridTextColumn column = new DataGridTextColumn()
                {
                    Header = HeaderList[i],
                    Binding = new Binding(BindingList[i])
                };
                MainDataGrid.Columns.Add(column);
            }

            MainDataGrid.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
