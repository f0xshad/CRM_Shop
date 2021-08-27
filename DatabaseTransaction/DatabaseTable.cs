using CRM_Shop.Context;
using System.Windows;

namespace CRM_Shop.DatabaseTransaction
{
    class DatabaseTable : IInsertable, IUpdatable, IDeletable
    {
        public string TableName;
        
        public bool InsertRecord(CRMContext context)
        {
            switch (TableName)
            {
                case "User":
                    UserForm userForm = new UserForm(context);
                    userForm.ShowDialog();
                    break;
                case "Employee":
                    //EmployeeForm employeeForm = new EmployeeForm(context);
                    //employeeForm.ShowDialog();
                    break;
                default:
                    break;
            }
            return true;
        }

        public bool UpdateRecord(int recordId, CRMContext context)
        {
            switch (TableName)
            {
                case "User":
                    UserForm userForm = new UserForm(recordId, context);
                    userForm.ShowDialog();
                    break;
                case "Employee":
                    //EmployeeForm employeeForm = new EmployeeForm(recordId, context);
                    //employeeForm.ShowDialog();
                    break;
                default:
                    break;
            }

            return true;
        }
        public bool DeleteRecord(int recordId, CRMContext context)
        {
            try
            {
                switch (TableName)
                {
                    case "User":
                        var user = context.Users.Find(recordId);
                        context.Users.Remove(user);
                        break;
                    case "Employee":
                        var employee = context.Employees.Find(recordId);
                        context.Employees.Remove(employee);
                        break;
                    default:
                        break;
                }
                context.SaveChanges();
                MessageBox.Show("Удаление успешно произведено!");
                return true;
            }
            catch (System.Exception)
            {
                MessageBox.Show(
                    "Не удалось провести удаление. Проверьте запись на наличие связей!",
                    "Ошибка");
                return false;
            }
        }
    }
}
