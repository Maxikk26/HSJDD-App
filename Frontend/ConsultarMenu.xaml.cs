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
    /// Lógica de interacción para ConsultarMenu.xaml
    /// </summary>
    public partial class ConsultarMenu : Window
    {
        public ConsultarMenu()
        {
            InitializeComponent();
        }

        private void btnmedico_Click(object sender, RoutedEventArgs e)
        {
            ConsultarMedico med = new ConsultarMedico();
            med.Show();
            this.Close();
        }

        private void btnconsultorio_Click(object sender, RoutedEventArgs e)
        {
            ConsultarConsultorio cons = new ConsultarConsultorio();
            cons.Show();
            this.Close();
        }

        private void btnespecialidades_Click(object sender, RoutedEventArgs e)
        {
            ConsultarEspecialidad esp = new ConsultarEspecialidad();
            esp.Show();
            this.Close();
        }
    }
}
