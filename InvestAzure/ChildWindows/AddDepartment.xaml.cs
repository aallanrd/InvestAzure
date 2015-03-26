using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections;
using InvestAzure.ServiceReference1;
using InvestAzure.ChildWindows;

namespace InvestAzure
{
    public partial class AddDepartment : ChildWindow
    {
        SServiceClient client = new SServiceClient();
       // public static event EventHandler pWindowRefeshListBox;

        public AddDepartment()
        {
            InitializeComponent();
            
           
            client.GetAllEmpAsync();
            client.GetAllEmpCompleted += new EventHandler<GetAllEmpCompletedEventArgs>(client_GetAllEmpCompleted);
            
            
        }
        void client_GetAllEmpCompleted(object sender, GetAllEmpCompletedEventArgs e)
        {
           // cb1.ItemsSource = e.Result;
            System.Collections.ObjectModel.ObservableCollection<Employee> employees = e.Result;
            foreach (Employee emp in employees)
            {
                if (emp != null)
                {

                    cb1.Items.Add(emp.nbrEmpleado);
                }
                cb1.SelectedIndex = 0;
            }
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            String enc = "";
            String name = txtName.Text.ToString();
            
            enc = cb1.SelectedItem.ToString();
            
           
            String ubic = txtUbicacion.Text.ToString();

            client.GetEmpByNameAsync(enc);
            client.GetEmpByNameCompleted += new EventHandler<GetEmpByNameCompletedEventArgs>(getEmpByName);
            

            client.insertDepAsync(name, select.IdEmpleado, ubic);
            client.insertDepCompleted += new EventHandler<insertDepCompletedEventArgs>(insert_Dep);

          
        
        }

      
        Employee select = new Employee();
        private void getEmpByName(object sender, GetEmpByNameCompletedEventArgs e)
        {
            select = e.Result;
         //  MessageBox.Show("Select to:" + select.IdEmpleado, "Mensaje!", MessageBoxButton.OK);
        }

        private void insert_Dep(object sender, insertDepCompletedEventArgs e)
        {
            bool mibol = e.Result;
            MessageBox.Show("Departamento Insertado", "Mensaje!", MessageBoxButton.OK);
         //   MainPage.upload();
        }

            

        
/*
        private void client_ExecProcedureCompleted(object sender, insertDepCompletedEventArgs e)
        {
            MessageBox.Show(e.Result.ToString(), "Mensaje System", MessageBoxButton.OK);
        }
        */
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }


    }
}

