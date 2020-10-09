using Directorio.Backend;
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
    /// Lógica de interacción para AgregarEspecialidad.xaml
    /// </summary>
    public partial class AgregarEspecialidad : Window
    {
        Insertar insertar;
        Consultar consult;
        private bool err;
        public AgregarEspecialidad()
        {
            InitializeComponent();
            insertar = new Insertar();
            consult = new Consultar();
        }

        private void btnañadir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                vacio(txtespecialidad);
                string especialidad = String.Empty;
                string res = String.Empty;
                if (!err)
                {
                    especialidad = txtespecialidad.Text;
                    consult.especialidades();
                    if (especialidad != null && especialidad != "")
                        especialidad = especialidad.First().ToString().ToUpper() + especialidad.Substring(1);
                    bool contains = consult.dt.AsEnumerable().Any(row => especialidad == row.Field<String>("especialidad"));
                    if (contains)
                        MessageBox.Show("La especialidad ya existe", "Directorio Medico", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                    {
                        res = insertar.crearInsertEspecialidad(especialidad);
                        MessageBox.Show(res, "Directorio Medico", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

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
                err = false;
            }
            else
            {
                err = true;
                txt.BorderBrush = System.Windows.Media.Brushes.Red;
            }
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            AgregarMenu menu = new AgregarMenu();
            menu.Show();
            this.Close();
        }
    }
}
