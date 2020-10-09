using Directorio.Backend;
using Directorio.Frontend;
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
    /// Lógica de interacción para ModificarTelefono.xaml
    /// </summary>
    public partial class AgregarTelefono : Window
    {
        List<bool> err;
        Insertar ins;
        Consultar consult;
        public AgregarTelefono()
        {
            InitializeComponent();
            ins = new Insertar();
            consult = new Consultar();
            txtcelular.IsEnabled = false;
            txtfijo.IsEnabled = false;
            cboxcelular.IsEnabled = false;
            cboxfijo.IsEnabled = false;
        }

        private void btnagregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string res = "";
                err = new List<bool>();
                if (chboxcelular.IsChecked == false && chboxfijo.IsChecked == false)
                {
                    MessageBox.Show("Elija un telefono celular o fijo para agregar", "Directorio Medico", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    vacio(txtid);
                    if (chboxfijo.IsChecked == true)
                    {
                        vacio(txtfijo);
                        verificarPrefijo(cboxfijo);
                        if (!err.Contains(true))
                        {
                            var item = (ComboBoxItem)cboxfijo.SelectedValue;
                            var content = (string)item.Content;
                            string numero = content + "-" + txtfijo.Text;
                            if(numero.Length > 13)
                                MessageBox.Show("El numero debe contener maximo 13 caracteres", "Directorio Medico", MessageBoxButton.OK, MessageBoxImage.Information);
                            else
                                res = ins.crearInsertTelefono(txtid.Text,numero)+"\r\n";

                        }
                    }
                    else if (chboxcelular.IsChecked == true)
                    {
                        vacio(txtcelular);
                        verificarPrefijo(cboxcelular);
                        if (!err.Contains(true))
                        {
                            var item = (ComboBoxItem)cboxcelular.SelectedValue;
                            var content = (string)item.Content;
                            string numero = content + "-" + txtcelular.Text;
                            if(numero.Length > 13)
                                MessageBox.Show("El numero debe contener maximo 13 caracteres", "Directorio Medico", MessageBoxButton.OK, MessageBoxImage.Information);
                            else
                                res = ins.crearInsertTelefono(txtid.Text, numero) + "\r\n";
                        }
                    }
                    MessageBox.Show(res, "Directorio Medico", MessageBoxButton.OK, MessageBoxImage.Information);


                }
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

        private void verificarPrefijo(ComboBox cbox)
        {
            if (cbox.SelectedIndex == -1)
                err.Add(true);
            else
                err.Add(false);
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void chboxcelular_Click(object sender, RoutedEventArgs e)
        {
            if (chboxcelular.IsChecked == true)
            {
                cboxcelular.IsEnabled = true;
                txtcelular.IsEnabled = true;
            }
            else
            {
                cboxcelular.IsEnabled = false;
                txtcelular.IsEnabled = false;
            }
        }

        private void chboxfijo_Click(object sender, RoutedEventArgs e)
        {
            if (chboxfijo.IsChecked == true)
            {
                cboxfijo.IsEnabled = true;
                txtfijo.IsEnabled = true;
            }
            else
            {
                cboxfijo.IsEnabled = false;
                txtfijo.IsEnabled = false;
            }
        }

        private void txtnombre_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            string nombre = txtnombre.Text;
            if (nombre != null && nombre != "")
                nombre = nombre.First().ToString().ToUpper() + nombre.Substring(1);
            consult.telefonos(nombre,true);
            dgtelefono.ItemsSource = consult.dt.DefaultView;
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            AgregarMenu menu = new AgregarMenu();
            menu.Show();
            this.Close();
        }
    }
}
