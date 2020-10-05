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
    /// Lógica de interacción para EliminarMenu.xaml
    /// </summary>
    public partial class EliminarMenu : Window
    {
        public EliminarMenu()
        {
            InitializeComponent();
        }

        private void btnmedico_Click(object sender, RoutedEventArgs e)
        {
            EliminarMedico med = new EliminarMedico();
            med.Show();
            this.Close();
        }

        private void btntelefono_Click(object sender, RoutedEventArgs e)
        {
            EliminarTelefono tel = new EliminarTelefono();
            tel.Show();
            this.Close();
        }

        private void btncorreo_Click(object sender, RoutedEventArgs e)
        {
            EliminarCorreo cor = new EliminarCorreo();
            cor.Show();
            this.Close();
        }
    }
}
