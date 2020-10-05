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
    /// Lógica de interacción para ActualizarMedico.xaml
    /// </summary>
    public partial class ModificarMedico : Window
    {
        public ModificarMedico()
        {
            InitializeComponent();
        }

        private void btndatosbase_Click(object sender, RoutedEventArgs e)
        {
            ModificarDatosBasicos bas = new ModificarDatosBasicos();
            bas.Show();
            this.Close();

        }

        private void btncorreo_Click(object sender, RoutedEventArgs e)
        {
            AgregarCorreo correo = new AgregarCorreo();
            correo.Show();
            this.Close();
        }
    }
}
