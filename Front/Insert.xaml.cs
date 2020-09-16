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
    /// Lógica de interacción para Insert.xaml
    /// </summary>
    public partial class Insert : Window
    {
        InsertBack ins;
        Connection pg;
        public Insert(Connection con)
        {
            InitializeComponent();
            pg = con;
            ins = new InsertBack(con);
            ins.especialidades();
            cboxespecialidad.ItemsSource = ins.dt.Rows.Cast<DataRow>().Select(x => x.Field<string>("especialidad"));
            ins.especialidadesSecundarias();
            cboxsecundaria.ItemsSource = ins.dt.Rows.Cast<DataRow>().Select(x => x.Field<string>("e_secundaria"));
            ins.horaDesde(true);
            cboxdesdedia.ItemsSource = ins.dt.Rows.Cast<DataRow>().Select(x => x.Field<string>("desde"));
            ins.horaHasta(true);
            cboxhastadia.ItemsSource = ins.dt.Rows.Cast<DataRow>().Select(x => x.Field<string>("hasta"));
            ins.horaDesde(false);
            cboxdesdetarde.ItemsSource = ins.dt.Rows.Cast<DataRow>().Select(x => x.Field<string>("desde"));
            ins.horaHasta(false);
            cboxhastatarde.ItemsSource = ins.dt.Rows.Cast<DataRow>().Select(x => x.Field<string>("hasta"));
            ins.consultorios();
            cboxconsultorio.ItemsSource = ins.dt.Rows.Cast<DataRow>().Select(x => x.Field<string>("numero"));
            cboxconsultorio2.ItemsSource = ins.dt.Rows.Cast<DataRow>().Select(x => x.Field<string>("numero"));
        }

        private void btninsertar_Click(object sender, RoutedEventArgs e)
        {
            List<bool> err = new List<bool>();
            vacio(txtcedula,err);
            vacio(txtpnombre,err);
            vacio(txtsnombre, err);
            vacio(txtpapellido, err);
            vacio(txtsapellido, err);
            if (cboxcelular.SelectedIndex == -1 && txtcelular.Text != null && txtcelular.Text != "")
            {
                MessageBox.Show("Seleccione una operadora de telefono celular", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                err.Add(true);
            }
            else if (cboxcelular.SelectedIndex != -1)
                vacio(txtcelular,err);
            if(cboxfijo.SelectedIndex == -1 && txtfijo.Text != null && txtfijo.Text != "")
            {
                MessageBox.Show("Seleccione un codigo de area de telefono fijo", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                err.Add(true);
            }
            else if (cboxfijo.SelectedIndex != -1)
                vacio(txtfijo, err);
            vacio(txtcorreo, err);
            if (cboxespecialidad.SelectedIndex == -1)
            {
                MessageBox.Show("Elija una especialidad", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                err.Add(true);
            }
            else
                err.Add(false);
            List<string> horarioD = new List<string>();
            diasCheck(chboxlunesd, horarioD, true,err);
            diasCheck(chboxmartesd, horarioD, true, err);
            diasCheck(chboxmiercolesd, horarioD, true, err);
            diasCheck(chboxjuevesd, horarioD, true, err);
            diasCheck(chboxviernesd, horarioD, true, err);
            List<string> horarioT = new List<string>();
            diasCheck(chboxlunest, horarioT, false, err);
            diasCheck(chboxmartest, horarioT, false, err);
            diasCheck(chboxmiercolest, horarioT, false, err);
            diasCheck(chboxjuevest, horarioT, false, err);
            diasCheck(chboxviernest, horarioT, false, err);
            if (cboxconsultorio.SelectedIndex == -1 && cboxconsultorio2.SelectedIndex == -1)
            {
                MessageBox.Show("Elija un consultorio", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                err.Add(true);
            }
            else
                err.Add(false);

        }

        private void vacio(TextBox txt, List<bool> err)
        {
            if (txt.Text != null && txt.Text != "")
                err.Add(false);
            else
            {
                err.Add(true);
                txt.BorderBrush = System.Windows.Media.Brushes.Red;
            }
        }

        private void diasCheck(CheckBox chbox, List<string> list, bool horario, List<bool> err)
        {
            if (horario)
            {
                if (chbox.IsChecked == true && cboxdesdedia.SelectedIndex != -1 && cboxhastadia.SelectedIndex != -1)
                {
                    err.Add(false);
                    list.Add(chbox.Content.ToString());
                }
                else
                {
                    err.Add(true);
                    MessageBox.Show("Elija un rango de horas en el horario diurno", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                if (chbox.IsChecked == true && cboxhastatarde.SelectedIndex != -1 && cboxdesdetarde.SelectedIndex != -1)
                {
                    err.Add(false);
                    list.Add(chbox.Content.ToString());
                }
                else
                {
                    err.Add(true);
                    MessageBox.Show("Elija un rango de horas en el horario vespertino", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
                
                
        }

       
    }
}
