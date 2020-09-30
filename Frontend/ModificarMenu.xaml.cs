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
    /// Lógica de interacción para ModificarMenu.xaml
    /// </summary>
    public partial class ModificarMenu : Window
    {
        public ModificarMenu()
        {
            InitializeComponent();
        }

        private void btnmedico_Click(object sender, RoutedEventArgs e)
        {
            ModificarMedico med = new ModificarMedico();
            med.Show();
            this.Close();
        }

        private void btnconsultorio_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnespecialidad_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
