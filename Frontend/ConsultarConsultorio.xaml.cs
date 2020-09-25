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
    /// Lógica de interacción para ConsultarConsultorio.xaml
    /// </summary>
    public partial class ConsultarConsultorio : Window
    {
        Conexion pg;
        Consultar consult;
        public ConsultarConsultorio(Conexion con)
        {
            InitializeComponent();
            pg = con;
            consult = new Consultar(pg);
            consult.todosConsultorios();
            dgconsultorio.ItemsSource = consult.dt.DefaultView;
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
