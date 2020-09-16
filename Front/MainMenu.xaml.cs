using Directorio.Back;
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

namespace Directorio.Front
{
    /// <summary>
    /// Lógica de interacción para MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        Connection pg;
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        public MainMenu(Connection con)
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
            Insert insert = new Insert(pg);
            insert.Show();
            this.Close();
        }
    }
}
