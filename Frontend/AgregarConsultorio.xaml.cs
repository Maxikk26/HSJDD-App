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
    /// Lógica de interacción para AgregarConsultorio.xaml
    /// </summary>
    public partial class AgregarConsultorio : Window
    {
        Conexion pg;
        InsertarBack insert;
        private bool err = false;
        public AgregarConsultorio(Conexion con)
        {
            InitializeComponent();
            pg = con;
            insert = new InsertarBack(pg);
        }

        private void btnagregar_Click(object sender, RoutedEventArgs e)
        {
            string res = String.Empty;
            string piso = String.Empty;
            vacio(txtconsultorio);
            if (!err && cboxpiso.SelectedIndex != -1)
            {
                var item = (ComboBoxItem)cboxpiso.SelectedValue;
                piso = (string)item.Content;
                switch (piso)
                {
                    case "Piso 1":
                        piso = "Piso";
                        break;
                    case "Sotano 1":
                        piso = "Sotano";
                        break;
                }
                Console.WriteLine("Piso: {0}", piso);
                res = insert.crearInsertConsultorio(txtconsultorio.Text,piso);
                MessageBox.Show(res, "Directorio Medico", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
                MessageBox.Show("Por favor ingrese datos validos", "Directorio Medico", MessageBoxButton.OK, MessageBoxImage.Information);

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
    }
}
