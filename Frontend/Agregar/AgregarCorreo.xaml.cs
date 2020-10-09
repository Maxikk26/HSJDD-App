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
    /// Lógica de interacción para ActualizarCorreo.xaml
    /// </summary>
    public partial class AgregarCorreo : Window
    {
        Insertar ins;
        Consultar consult;
        List<bool> err;
        public AgregarCorreo()
        {
            InitializeComponent();
            ins = new Insertar();
            consult = new Consultar();
            err = new List<bool>();
        }

        private void btnagregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                err.Clear();
                vacio(txtid);
                vacio(txtcorreo);
                if (!err.Contains(true))
                {
                    string res = ins.crearInsertCorreo(txtid.Text, txtcorreo.Text);
                    MessageBox.Show(res, "Directorio Medico", MessageBoxButton.OK, MessageBoxImage.Information);
                    consultar();

                }
                else
                    MessageBox.Show("Ingrese los datos requeridos", "Directorio Medico", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error del sistema, intente de nuevo", "Directorio Medico", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void vacio(TextBox txt)
        {
            if (txt.Text != null && txt.Text != "")
            {
                err.Add(false);
            }
            else
            {
                err.Add(true);
                txt.BorderBrush = System.Windows.Media.Brushes.Red;
            }
        }

        private void consultar()
        {
            string nombre = txtnombre.Text;
            if (nombre != null && nombre != "")
                nombre = nombre.First().ToString().ToUpper() + nombre.Substring(1);
            consult.correosEspecificos(nombre);
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

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            AgregarMenu menu = new AgregarMenu();
            menu.Show();
            this.Close();
        }
    }
}
