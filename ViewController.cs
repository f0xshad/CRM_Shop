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
                    BindingList.AddRange(new string[] { "Login", "UserRole.RoleName", "Employee.LastName"});
                    HeaderList.AddRange(new string[] { "Логин", "Роль пользователя", "Сотрудник" });
                    break;
                case "Employee":
                    BindingList.AddRange(new string[] { "LastName", "FirstName", "MiddleName", "Post.PostName" });
                    HeaderList.AddRange(new string[] { "Фамилия", "Имя", "Отчество", "Должность" });
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
        }
    }
}
