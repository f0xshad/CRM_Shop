using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CRM_Shop
{
    public class CRMUser
    {
        private string _login { get; set; }
        private string _userRole { get; set; }
        private string _employeeName { get; set; }
        public CRMUser(string login, string role, string name)
        {
            _login = login;
            _userRole = role;
            _employeeName = name;
        }

        internal string GetEmployeeName()
        {
            return _employeeName;
        }
    }
}
