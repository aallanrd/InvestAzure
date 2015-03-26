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
    public partial class EditDep : ChildWindow
    {
        SServiceClient client = new SServiceClient();
        int idSelected;
        public EditDep(String name, String ubic,int id)
        {
            InitializeComponent();
            idSelected = id;
            txtNombre.Text = name;
            txtNombre_Copy.Text = ubic;
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
            String name = txtNombre.Text;
            String ubic = txtNombre_Copy.Text;

            client.updateDepAsync(idSelected, name, 0, ubic);
            client.updateDepCompleted += new EventHandler<updateDepCompletedEventArgs>(updateCompleted);
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void updateCompleted(object sender, updateDepCompletedEventArgs e)
        {
            bool mibol = e.Result;
            MessageBox.Show("Departamento Actualizado", "Mensaje!", MessageBoxButton.OK);
        }
    }
}

