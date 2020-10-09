using Directorio.Backend;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para Delete.xaml
    /// </summary>
    public partial class EliminarMedico : Window
    {
        Eliminar del;
        Consultar consult;
        public EliminarMedico()
        {
            InitializeComponent();
            consult = new Consultar();
            try
            {
                del = new Eliminar();
                consult.medicosActivos();
                dgmedico.ItemsSource = consult.dt.DefaultView;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }

        private void txtbuscador_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            string nombre = txtbuscador.Text;
            if(nombre != null && nombre != "")
                nombre = nombre.First().ToString().ToUpper() + nombre.Substring(1);
            consult.medicoEspecificoActivo(nombre);
            dgmedico.ItemsSource = consult.dt.DefaultView;


        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Int32.Parse(txtId.Text);
                bool x = del.eliminar(id);
                if (x)
                {
                    MessageBox.Show("Médico eliminado satisfactoriamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                    string nombre = txtbuscador.Text;
                    if (nombre != null && nombre != "")
                        nombre = nombre.First().ToString().ToUpper() + nombre.Substring(1);
                    consult.medicoEspecificoActivo(nombre);
                    dgmedico.ItemsSource = consult.dt.DefaultView;
                }
                else
                    MessageBox.Show("No se pudo eliminar el médico", "Error", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error del sistema, intente de nuevo", "Directorio Medico", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            EliminarMenu menu = new EliminarMenu();
            menu.Show();
            this.Close();
        }
    }
}
