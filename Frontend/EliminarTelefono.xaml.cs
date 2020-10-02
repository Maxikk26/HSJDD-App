using Directorio.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Directorio.Frontend
{
    /// <summary>
    /// Lógica de interacción para EliminarTelefono.xaml
    /// </summary>
    public partial class EliminarTelefono : Window
    {
        Consultar consult;
        Eliminar del;
        List<bool> err;
        public EliminarTelefono()
        {
            InitializeComponent();
            btneliminar.IsEnabled = false;
            consult = new Consultar();
            err = new List<bool>();
            del = new Eliminar();
        }

        private void txtnombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            string nombre = txtnombre.Text;
            if (nombre != null && nombre != "")
                nombre = nombre.First().ToString().ToUpper() + nombre.Substring(1);
            consult.telefonos(nombre,false);
            dgtelefonos.ItemsSource = consult.dt.DefaultView;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtid_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtid.Text != null && txtid.Text != "")
                btneliminar.IsEnabled = true;
            else
                btneliminar.IsEnabled = false;
        }

        private void btneliminar_Click(object sender, RoutedEventArgs e)
        {
            err.Clear();
            string res = del.eliminarTelefonoID(txtid.Text);
            MessageBox.Show(res, "Directorio Medico", MessageBoxButton.OK, MessageBoxImage.Information);
            string nombre = txtnombre.Text;
            if (nombre != null && nombre != "")
                nombre = nombre.First().ToString().ToUpper() + nombre.Substring(1);
            consult.telefonos(nombre, false);
            dgtelefonos.ItemsSource = consult.dt.DefaultView;
        }
    }
}
