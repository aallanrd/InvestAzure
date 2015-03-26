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
    public partial class MainPage : UserControl
    {

     SServiceClient client = new SServiceClient();
        public MainPage()
        {
            InitializeComponent();

          

            client.GetAllDepAsync();
            client.GetAllDepCompleted += new EventHandler<GetAllDepCompletedEventArgs>(client_GetAllDepCompleted);

            ImageBrush img = new ImageBrush();
            img.ImageSource = (ImageSource)new ImageSourceConverter().ConvertFromString("Img/images.jpg");
            // System.Windows.Controls.Grid g = new System.Windows.Controls.Grid();
            //g.Background = img;
            LayoutRoot.Background = img;
            cb1.SelectionChanged+= 
			new System.Windows.Controls.SelectionChangedEventHandler(ComboBox1_SelectedIndexChanged);
          //  window.Close();
        }


        private void ComboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
           // MessageBox.Show(cb1.SelectedItem.ToString(), "Mensaje System", MessageBoxButton.OK);
            disparaGetIdByName();
           // client.insertDepCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(insert_Dep);
        }

        

        void client_GetAllDepCompleted(object sender, GetAllDepCompletedEventArgs e)
        {

            cb1.Items.Clear();
            System.Collections.ObjectModel.ObservableCollection<Department> depart = e.Result;
            foreach (Department dep in depart){
                if (dep != null){

                    cb1.Items.Add(dep.nbrDepto);
                }
             cb1.SelectedIndex = 0;
            }
          //  client.insertDepCompleted+= new EventHandler<client_insertDepComplete


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            disparaGetIdByName();
                  
      }
        public void disparaGetIdByName()
        {
            try
            {
                string item1 = cb1.SelectedItem.ToString();

                client.GetDepIdbyNameAsync(item1);
                client.GetDepIdbyNameCompleted += new EventHandler<GetDepIdbyNameCompletedEventArgs>(client_GetDepIdbyNameCompleted);
                
            }
            catch (Exception e)
            {

            }
           

        }

   
        void client_GetAllEmpCompleted(object sender, GetAllEmpCompletedEventArgs e)
        {
            dg1.ItemsSource = e.Result;
        }

        Department myIdDepSelec = new Department();
        void client_GetDepIdbyNameCompleted(object sender, GetDepIdbyNameCompletedEventArgs e)
        {
            Department a = e.Result;
            client.getEmployeesAsync(Convert.ToInt32(a.IdDepto));
            list1.Items.Clear();
            list1.Items.Add("Nombre Dept: "+a.nbrDepto);
            list1.Items.Add("Id Depto: " + a.IdDepto);
            list1.Items.Add("Encargado: " + a.idEncargado);
            myIdDepSelec = a;
            client.getEmployeesCompleted += new EventHandler<getEmployeesCompletedEventArgs>(client_getEmployeesCompleted);
            

        }
        void client_getEmployeesCompleted(object sender, getEmployeesCompletedEventArgs e)
        {
            dg1.ItemsSource = e.Result;
        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            try
            {
                string getID = ((TextBlock)dg1.Columns[1].GetCellContent(dg1.SelectedItem)).Text;
                int ax = Convert.ToInt32(getID);
                client.DelEmpAsync(ax);
                client.DelEmpCompleted += new EventHandler<DelEmpCompletedEventArgs>(client_DelEmpCompleted);

               
            }
            catch (Exception exc)
            {
                MessageBox.Show("Selecciona un empleado", "Mensaje System", MessageBoxButton.OK);

            }
        }

        private void client_DelEmpCompleted(object sender, DelEmpCompletedEventArgs e)
        {
            MessageBox.Show("Empleado Eliminado", "Mensaje System", MessageBoxButton.OK);
            disparaGetIdByName();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddEmployee aE = new AddEmployee(myIdDepSelec.IdDepto);
            aE.Show();
            
            aE.Closed += new EventHandler(eWindowx);
            
        }
       
        ////////////////////////////////////////////////////////////////////
        
        private void eWindowx(object sender, EventArgs e)
        {
            var window = sender as AddEmployee;
            if (window != null && window.DialogResult == true)
            {

                disparaGetIdByName();
                
                //client.GetAllDepAsync();
                //client.GetAllDepCompleted += new EventHandler<GetAllDepCompletedEventArgs>(client_GetAllDepCompleted);
                //list1.Items.Clear();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Department dep = myIdDepSelec;
            EditDep eD = new EditDep(dep.nbrDepto, dep.ubicacion,myIdDepSelec.IdDepto);
            eD.Show();
            eD.Closed += new EventHandler(eDWindowCloseEvent);
        }
      
        private void beP_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AddDepartment dialog = new AddDepartment();
           

           dialog.Show();

           dialog.Closed += new EventHandler(pWindow);

        }

        private void pWindow(object sender, EventArgs e)
        {
            var window = sender as AddDepartment;
            if (window != null && window.DialogResult == true)
            {
                System.Windows.Browser.HtmlPage.Document.Submit();
            }
        }

        
        private void bdD_Click(object sender, RoutedEventArgs e)
        {
            try
            {
              //  string getID = ((TextBlock)dg1.Columns[1].GetCellContent(dg1.SelectedItem)).Text;
                int ax = myIdDepSelec.IdDepto;
                client.DelDepAsync(ax);
                // client.DelEmpCompleted += new EventHandler<DelEmpCompletedEventArgs>(client_DelEmpCompleted);

               
                list1.Items.Clear();
                client.GetAllDepAsync();
                client.GetAllDepCompleted += new EventHandler<GetAllDepCompletedEventArgs>(client_GetAllDepCompleted);
                MessageBox.Show("Departamento Eliminado", "Mensaje System", MessageBoxButton.OK);

                System.Windows.Browser.HtmlPage.Document.Submit();
                //   dg1.DataSource = null;
             //   dg1.DataBind();
                
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ups! Ocurrio un error", "Mensaje System", MessageBoxButton.OK);

            }

        }
        
     

        Employee emp;
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
              //  Department dep = myIdDepSelec;
                EditEmp eD = new EditEmp(emp);
                eD.Show();
               // eD.Closed += new EventHandler(eEWindowCloseEvent);
            }
            catch (Exception ex)
            {
               // MessageBox.Show("Selecciona un empleado", "Mensaje System", MessageBoxButton.OK);

            }
        }

        private void getEpByName(object sender, GetEmpByNameCompletedEventArgs e)
        {
            emp = e.Result;
        }
        

        private void eDWindowCloseEvent(object sender, EventArgs e)
        {
            var window = sender as EditDep;
            if (window != null && window.DialogResult == true)
            {
                System.Windows.Browser.HtmlPage.Document.Submit();
            }
        }
        private void eEWindowCloseEvent(object sender, EventArgs e)
        {
            var window = sender as EditEmp;
            if (window != null && window.DialogResult == true)
            {
                System.Windows.Browser.HtmlPage.Document.Submit();
            }
        }

        private void dgTaskLinks_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            try
            {
                string getID = ((TextBlock)dg1.Columns[4].GetCellContent(dg1.SelectedItem)).Text;


                client.GetEmpByNameAsync(getID);
                client.GetEmpByNameCompleted += new EventHandler<GetEmpByNameCompletedEventArgs>(getEpByName);
            }
            catch (Exception ex)
            {

            }

        }

        private void btnNavegar_Click(object sender, RoutedEventArgs e)
        {
            Navegar nave = new Navegar();
            nave.Show();

        }
    }

}
