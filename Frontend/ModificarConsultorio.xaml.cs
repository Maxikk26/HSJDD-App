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
    public partial class ModificarConsultorio : Window
    {
        Consultar consult;
        Actualizar act;
        public ModificarConsultorio()
        {
            InitializeComponent();
            btnactualizar.IsEnabled = false;
            consult = new Consultar();
            act = new Actualizar();
        }

        private void btnactualizar_Click(object sender, RoutedEventArgs e)
        {
            string piso = String.Empty;
            var item = (ComboBoxItem)cboxpiso.SelectedValue;
            piso = (string)item.Content;
            switch (piso)
            {
                case "Piso 1":
                    piso = "Piso";
                    break;
                case "Sotano 1":
                    piso = "Sotano";
                    break;
            }
            
        }

        private void txtid_TextChanged(object sender, TextChangedEventArgs e)
        {
            validarCampos();
        }

        private void cboxpiso_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            validarCampos();
        }

        private void txtconsultorio_TextChanged(object sender, TextChangedEventArgs e)
        {
            validarCampos();

        }
        private void validarCampos()
        {
            if (cboxpiso.SelectedIndex != -1 && txtid.Text != null && txtid.Text != "" && txtconsultorio.Text != null && txtconsultorio.Text != "")
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
            string nombre = txtnombre.Text;
            if (nombre != null && nombre != "")
                nombre = nombre.First().ToString().ToUpper() + nombre.Substring(1);
            consult.consultorioEspecifico(nombre);
            dgconsultorio.ItemsSource = consult.dt.DefaultView;
        }
    }
}
