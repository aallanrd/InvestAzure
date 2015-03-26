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
using InvestAzure.ServiceReference1;

namespace InvestAzure.ChildWindows
{
    public partial class Navegar : ChildWindow
    {

        SServiceClient client = new SServiceClient();
        int index_View = 0;
        public Navegar()
        {
            InitializeComponent();

            client.GetAllEmpAsync();
            client.GetAllEmpCompleted += new EventHandler<GetAllEmpCompletedEventArgs>(completed);

            //load(0);

        
        }

        List<Employee> miListaEmpleados;
        private void completed(object sender, GetAllEmpCompletedEventArgs e)
        {
            miListaEmpleados = new List<Employee>();

            // cb1.ItemsSource = e.Result;
            System.Collections.ObjectModel.ObservableCollection<Employee> employees = e.Result;
            foreach (Employee emp in employees)
            {
                if (emp != null)
                {

                    miListaEmpleados.Add(emp);
                }
               // cb1.SelectedIndex = 0;
            }

            load(0);
           
        }

        public void load(int num)
        {
            Employee emp = miListaEmpleados.ElementAt(num);
            txtName.Text = emp.nbrEmpleado;
            txtFecha.Text = emp.fechaIngreso;
            

        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            load(0);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try {
                load(index_View - 1);
                index_View = index_View - 1;
            }
            catch(Exception ee){

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try {
                
                load(index_View + 1);
                index_View = index_View + 1;

            }
            catch(Exception ee){

            }
        }
    }
}

