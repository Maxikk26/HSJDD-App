using Directorio.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Lógica de interacción para ActualizarDatosBasicos.xaml
    /// </summary>
    public partial class ModificarDatosBasicos : Window
    {
        Consultar consult;
        List<bool> err;
        Medico med;
        Actualizar act;
        string id;
        public ModificarDatosBasicos()
        {
            InitializeComponent();
            consult = new Consultar();
            err = new List<bool>();
            med = new Medico();
            act = new Actualizar();
            consult.medicos();
            dgmedico.ItemsSource = consult.dt.DefaultView;
        }

        private void btnactualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                vacio(txtid);
                id = txtid.Text;
                vacio(txtpnombre);
                med.pnombre = txtpnombre.Text;
                vacio(txtpapellido);
                med.papellido = txtpapellido.Text;
                vacio(txtsapellido);
                med.sapellido = txtsapellido.Text;
                if (!err.Contains(true))
                {
                    string res = "";
                    if (txtsnombre.Text != null && txtsnombre.Text != "")
                    {
                        med.snombre = txtsnombre.Text;
                        res = act.actualizarMedico1(med, id);
                    }
                    else
                    {
                        res = act.actualizarMedico2(med, id);
                    }
                }
                else
                    MessageBox.Show("Ingrese los datos requeridos", "Directorio Medico", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error del sistema, intente de nuevo", "Directorio Medico", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }

        private void txtnombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            string nombre = txtnombre.Text;
            if (nombre != null && nombre != "")
                nombre = nombre.First().ToString().ToUpper() + nombre.Substring(1);
            consult.medicoEspecifico(nombre);
            dgmedico.ItemsSource = consult.dt.DefaultView;
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

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            ModificarMenu menu = new ModificarMenu();
            menu.Show();
            this.Close();
        }
    }
}
