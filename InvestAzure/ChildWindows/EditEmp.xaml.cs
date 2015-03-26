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
using System.Windows.Media.Imaging;

namespace InvestAzure.ChildWindows
{
    public partial class EditEmp : ChildWindow
    {

        SServiceClient client = new SServiceClient();
        private  int k = 0;
        public EditEmp(Employee emp)
        {
           
            InitializeComponent();
            this.emple = emp;
            txtName.Text = emp.nbrEmpleado;
            txtDir.Text = emp.fotoStr;
            txtFecha.Text = emp.fechaIngreso;

            ///////
           

            client.GetAllDepAsync();
            client.GetAllDepCompleted += new EventHandler<GetAllDepCompletedEventArgs>(client_GetAllDepCompleted);

            try
            {
                Uri uri = new Uri(emp.fotoStr, UriKind.Absolute);
                ImageSource imgSource = new BitmapImage(uri);

                this.img.Source = imgSource;
                //////
            }
            catch (Exception e)
            {

            }

        }




        Employee emple;
        void client_GetAllDepCompleted(object sender, GetAllDepCompletedEventArgs e)
        {

            cb1.Items.Clear();
            System.Collections.ObjectModel.ObservableCollection<Department> depart = e.Result;
            foreach (Department dep in depart)
            {
                if (dep != null)
                {

                    cb1.Items.Add(dep.nbrDepto);
                }
                cb1.SelectedIndex = 0;
            }
            //  client.insertDepCompleted+= new EventHandler<client_insertDepComplete


        }

        String name;
        String dir;
        String fecha;
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {

            
            /////////////////////////////////////
             name = txtName.Text;
            fecha="Sin Fecha Edit";

            try
            {
                if (txtFecha2.Text == "")
                {
                    fecha = txtFecha.Text;
                }
                else
                {
                    fecha = txtFecha2.Text;
                }
            }
            catch(Exception efd){

            }
            //int dep = myDepSelect.IdDepto;
            dir = txtDir.Text;


            getDepIdbyNa();

        //   MessageBox.Show("Empleado a Actializar ID"  + k, "Mensaje!", MessageBoxButton.OK);
           
        

          
            
          this.DialogResult = true;
        

             
        }

       
        private void getDepIdbyNa(){
        try
            {
                string item1 = cb1.SelectedItem.ToString();

                client.GetDepIdbyNameAsync(item1);
                client.GetDepIdbyNameCompleted += new EventHandler<GetDepIdbyNameCompletedEventArgs>(client_GetDepIdbyNameCompleted);
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Seleccione" , "Mensaje!", MessageBoxButton.OK);
            }
}
       
        private void client_GetDepIdbyNameCompleted(object sender, GetDepIdbyNameCompletedEventArgs e)
        {
            Department dep = e.Result;
            //MessageBox.Show("k=" + dep.IdDepto, "Mensaje!", MessageBoxButton.OK);
            k = dep.IdDepto;

              client.updateEmpAsync(emple.IdEmpleado, name, k, dir, fecha);
              client.updateEmpCompleted += new EventHandler<updateEmpCompletedEventArgs>(updateCompleted);
           
           
        }

        private void updateCompleted(object sender, updateEmpCompletedEventArgs e)
        {
            bool mibol = e.Result;
            MessageBox.Show("Empleado Actualizado" + mibol, "Mensaje!", MessageBoxButton.OK);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            bool? result = ofd.ShowDialog();
            if (!result.HasValue || result.Value == false)
                return;

            BitmapImage imageSource = new BitmapImage();
            try
            {
                imageSource.SetSource(ofd.File.OpenRead());
                img.Source = imageSource;
                txtDir.Text = ofd.File.Name;
            }
            catch (Exception)
            {
                // img.Text = "Error Loading File";
            }
        }
    }
}

