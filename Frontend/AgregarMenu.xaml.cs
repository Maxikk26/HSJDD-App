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
    /// Lógica de interacción para AgregarMenu.xaml
    /// </summary>
    public partial class AgregarMenu : Window
    {
        Conexion pg;
        public AgregarMenu(Conexion con)
        {
            InitializeComponent();
            pg = con;
        }

        private void btnmedico_Click(object sender, RoutedEventArgs e)
        {
            AgregarMedico med = new AgregarMedico(pg);
            med.Show();
            this.Close();
        }

        private void btnespecialidad_Click(object sender, RoutedEventArgs e)
        {
            AgregarEspecialidad esp = new AgregarEspecialidad(pg);
            esp.Show();
            this.Close();
        }

        private void btnconsultorio_Click(object sender, RoutedEventArgs e)
        {
            AgregarConsultorio con = new AgregarConsultorio(pg);
            con.Show();
            this.Close();
        }
    }
}
