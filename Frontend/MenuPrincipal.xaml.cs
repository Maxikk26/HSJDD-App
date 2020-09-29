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
    /// Lógica de interacción para MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        Conexion pg;
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        public MainMenu(Conexion con)
        {
            InitializeComponent();
            this.pg = con;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Delete delete = new Delete(pg);
            delete.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AgregarMenu insert = new AgregarMenu(pg);
            insert.Show();
            this.Close();
        }

        private void btnconsultar_Click(object sender, RoutedEventArgs e)
        {
            ConsultarMenu cons = new ConsultarMenu(pg);
            cons.Show();
            this.Close();
        }

        private void btnacutalizar_Click(object sender, RoutedEventArgs e)
        {
            ActualizarMedico act = new ActualizarMedico(pg);
            act.Show();
            this.Close();
        }
    }
}
