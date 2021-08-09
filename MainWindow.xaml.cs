﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CRM_Shop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CRMUser CRMUser;
        LoginWindow LoginWindow;
        public MainWindow(CRMUser user, LoginWindow loginWindow)
        {
            InitializeComponent();

            CRMUser = user;
            LoginWindow = loginWindow;
            EmployeeName_L.Text = CRMUser.GetEmployeeName();
        }
    }
}
