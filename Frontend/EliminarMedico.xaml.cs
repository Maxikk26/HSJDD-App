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
    public partial class Delete : Window
    {
        Conexion pg;
        Eliminar del;
        Consultar consult;
        public Delete(Conexion con)
        {
            InitializeComponent();
            this.pg = con;
            consult = new Consultar(pg);
            try
            {
                del = new Eliminar();
                consult.medicosActivos(pg);
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
            consult.medicoEspecificoActivo(pg, nombre);
            dgmedico.ItemsSource = consult.dt.DefaultView;


        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            int id = Int32.Parse(txtId.Text);
            bool x = del.eliminar(pg,id);
            if (x)
            {
                MessageBox.Show("Médico eliminado satisfactoriamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                string nombre = txtbuscador.Text;
                if (nombre != null && nombre != "")
                    nombre = nombre.First().ToString().ToUpper() + nombre.Substring(1);
                consult.medicoEspecificoActivo(pg, nombre);
                dgmedico.ItemsSource = consult.dt.DefaultView;
            }
            else
                MessageBox.Show("No se pudo eliminar el médico", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
