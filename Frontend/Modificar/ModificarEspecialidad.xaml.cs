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
    /// Lógica de interacción para ModificarEspecialidad.xaml
    /// </summary>
    public partial class ModificarEspecialidad : Window
    {
        Actualizar act;
        Consultar consult;
        public ModificarEspecialidad()
        {
            InitializeComponent();
            btnactualizar.IsEnabled = false;
            act = new Actualizar();
            consult = new Consultar();
            consultar();
        }

        private void txtid_TextChanged(object sender, TextChangedEventArgs e)
        {
            validarCampos();
        }

        private void txtespecialidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            validarCampos();
        }

        private void btnactualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string res = act.actualizarEspecialidad(txtid.Text, txtespecialidad.Text);
                MessageBox.Show(res, "Directorio Medico", MessageBoxButton.OK, MessageBoxImage.Information);
                consultar();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error del sistema, intente de nuevo", "Directorio Medico", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
        private void validarCampos()
        {
            if (txtespecialidad.Text != "" && txtespecialidad.Text != null && txtid.Text != null && txtid.Text != "")
                btnactualizar.IsEnabled = true;
            else
                btnactualizar.IsEnabled = false;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtnombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            consultar();
        }

        private void consultar()
        {
            string nombre = txtnombre.Text;
            if (nombre != null && nombre != "")
                nombre = nombre.First().ToString().ToUpper() + nombre.Substring(1);
            consult.especialidadEspecifica(nombre);
            dgespecialidad.ItemsSource = consult.dt.DefaultView;
        }
    }
}
