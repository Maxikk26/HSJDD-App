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
    public partial class ActualizarMedico : Window
    {
        Conexion pg;
        public ActualizarMedico(Conexion con)
        {
            InitializeComponent();
            pg = con;
        }

        private void btndatosbase_Click(object sender, RoutedEventArgs e)
        {
            ActualizarDatosBasicos bas = new ActualizarDatosBasicos(pg);
            bas.Show();
            this.Close();

        }
    }
}
