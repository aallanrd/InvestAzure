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
using System.Windows.Media.Imaging;
using InvestAzure.ServiceReference1;

namespace InvestAzure.ChildWindows
{
    public partial class AddEmployee : ChildWindow
    {

        SServiceClient client = new SServiceClient();
        public  int id = 0;
        public AddEmployee(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            String name = txtName.Text;
            String dir = txtDir.Text;


            client.insertEmpAsync(name, id, null, dir);
            client.insertEmpCompleted += new EventHandler<insertEmpCompletedEventArgs>(insert_Dep);

           this.DialogResult = true;
        }

        private void insert_Dep(object sender, insertEmpCompletedEventArgs e)
        {
            bool mibol = e.Result;
            MessageBox.Show("Empleado Insertado", "Mensaje!", MessageBoxButton.OK);
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
               txtDir.Text = ofd.File.ToString();
            }
            catch (Exception)
            {
               // img.Text = "Error Loading File";
            }
        }
    }
}

