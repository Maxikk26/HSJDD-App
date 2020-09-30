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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Directorio.Backend;
using Directorio.Frontend;
using Npgsql;

namespace Directorio
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = txtusername.Text;
            string password = txtpassword.Password;

            if(username == "admin" && password == "admin")
            {
                try
                {
                    Conexion con = new Conexion();
                    con.start();
                    MainMenu menu = new MainMenu();
                    menu.Show();
                    this.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    throw;
                }
                
            }
            else
            {
                MessageBox.Show("Usuario y/o Contraseña Incorrecta", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }
    }
}
