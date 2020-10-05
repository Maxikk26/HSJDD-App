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
    /// Lógica de interacción para EliminarCorreo.xaml
    /// </summary>
    public partial class EliminarCorreo : Window
    {
        Consultar consult;
        Eliminar del;
        public EliminarCorreo()
        {
            InitializeComponent();
            consult = new Consultar();
            del = new Eliminar();
            btneliminar.IsEnabled = false;
        }


        private void btneliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string res = del.eliminarCorreoID(txtid.Text);
                MessageBox.Show(res, "Directorio Medico", MessageBoxButton.OK, MessageBoxImage.Information);
                consultar();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error del sistema, intente de nuevo", "Directorio Medico", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void consultar()
        {
            string nombre = txtnombre.Text;
            if (nombre != null && nombre != "")
                nombre = nombre.First().ToString().ToUpper() + nombre.Substring(1);
            consult.correos();
            dgcorreo.ItemsSource = consult.dt.DefaultView;
        }

        private void txtnombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            consultar();
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
    }
}
