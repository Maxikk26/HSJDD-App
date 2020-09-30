using Directorio.Backend;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Lógica de interacción para Insert.xaml
    /// </summary>
    public partial class AgregarMedico : Window
    {
        Insertar ins;
        Consultar consult;
        private List<bool> err = new List<bool>();
        private Medico medico = new Medico();
        private Horario diurno = new Horario();
        private Horario vespertino = new Horario();
        private string mensajesError = String.Empty;
        public AgregarMedico()
        {
            InitializeComponent();
            chboxlunesd.IsEnabled = false;
            chboxmartesd.IsEnabled = false;
            chboxmiercolesd.IsEnabled = false;
            chboxjuevesd.IsEnabled = false;
            chboxviernesd.IsEnabled = false;
            cboxdesdedia.IsEnabled = false;
            cboxhastadia.IsEnabled = false;
            chboxlunest.IsEnabled = false;
            chboxmartest.IsEnabled = false;
            chboxmiercolest.IsEnabled = false;
            chboxjuevest.IsEnabled = false;
            chboxviernest.IsEnabled = false;
            cboxdesdetarde.IsEnabled = false;
            cboxhastatarde.IsEnabled = false;
            ins = new Insertar();
            consult = new Consultar();
            consult.especialidades();
            cboxespecialidad.ItemsSource = consult.dt.Rows.Cast<DataRow>().Select(x => x.Field<string>("especialidad"));
            consult.especialidadesSecundarias();
            cboxsecundaria.ItemsSource = consult.dt.Rows.Cast<DataRow>().Select(x => x.Field<string>("e_secundaria"));
            consult.horaDesde(true);
            cboxdesdedia.ItemsSource = consult.dt.Rows.Cast<DataRow>().Select(x => x.Field<string>("desde"));
            consult.horaHasta(true);
            cboxhastadia.ItemsSource = consult.dt.Rows.Cast<DataRow>().Select(x => x.Field<string>("hasta"));
            consult.horaDesde(false);
            cboxdesdetarde.ItemsSource = consult.dt.Rows.Cast<DataRow>().Select(x => x.Field<string>("desde"));
            consult.horaHasta(false);
            cboxhastatarde.ItemsSource = consult.dt.Rows.Cast<DataRow>().Select(x => x.Field<string>("hasta"));
            consult.consultorios();
            cboxconsultorio.ItemsSource = consult.dt.Rows.Cast<DataRow>().Select(x => x.Field<string>("numero"));
            cboxconsultorio2.ItemsSource = consult.dt.Rows.Cast<DataRow>().Select(x => x.Field<string>("numero"));
        }

        private void btninsertar_Click(object sender, RoutedEventArgs e)
        {
            vacio(txtcedula, err);
            medico.cedula = txtcedula.Text;
            vacio(txtpnombre, err);
            medico.pnombre = txtpnombre.Text;
            medico.snombre = txtsnombre.Text;
            vacio(txtpapellido, err);
            medico.papellido = txtpapellido.Text;
            vacio(txtsapellido, err);
            medico.sapellido = txtsapellido.Text;
            telefono(cboxcelular, true);
            telefono(cboxfijo, false);
            vacio(txtcorreo, err);
            medico.correo = txtcorreo.Text;
            validarCargo();
            if (cboxespecialidad.SelectedIndex == -1)
            {
                mensajesError += "- Elija una especialidad \r\n";
                err.Add(true);
            }
            else
            {
                medico.especialidad = cboxespecialidad.SelectedItem.ToString();
                if (cboxsecundaria.SelectedIndex == -1)
                    medico.esecundaria = "";
                else
                    cboxsecundaria.SelectedItem.ToString();
                err.Add(false);
            }
            if(chboxdiurno.IsChecked == true)
            {
                if (chboxlunesd.IsChecked == false && chboxmartesd.IsChecked == false && chboxmiercolesd.IsChecked == false && chboxjuevesd.IsChecked == false && chboxviernesd.IsChecked == false)
                {
                    err.Add(true);
                    mensajesError += "- Elija un día en el horario diurno \r\n";
                }
                else
                {
                    diasCheck(chboxlunesd, diurno, true, err);
                    diasCheck(chboxmartesd, diurno, true, err);
                    diasCheck(chboxmiercolesd, diurno, true, err);
                    diasCheck(chboxjuevesd, diurno, true, err);
                    diasCheck(chboxviernesd, diurno, true, err);
                }
            }
            if(chboxvespertino.IsChecked == true)
            {
                if (chboxlunest.IsChecked == false && chboxmartest.IsChecked == false && chboxmiercolest.IsChecked == false && chboxjuevest.IsChecked == false && chboxviernest.IsChecked == false)
                {
                    err.Add(true);
                    mensajesError += "- Elija un día en el horario vespertino \r\n";
                }
                else
                {
                    diasCheck(chboxlunest, vespertino, false, err);
                    diasCheck(chboxmartest, vespertino, false, err);
                    diasCheck(chboxmiercolest, vespertino, false, err);
                    diasCheck(chboxjuevest, vespertino, false, err);
                    diasCheck(chboxviernest, vespertino, false, err);
                }
            }
            if (cboxconsultorio.SelectedIndex == -1 && cboxconsultorio2.SelectedIndex == -1)
            {
                mensajesError += "- Elija un consultorio\r\n";
                err.Add(true);
            }
            else
            {
                medico.consultorio = cboxconsultorio.SelectedItem.ToString();
                if (cboxconsultorio2.SelectedIndex != -1)
                    medico.consultorio2 = cboxconsultorio2.SelectedItem.ToString();
                else
                    medico.consultorio2 = "";
                err.Add(false);
            }
            string res = "";
            if (!err.Contains(true))
                res = ins.crearInsert(medico, diurno, vespertino);
            if (res != "")
                MessageBox.Show(res, "Directorio Medico", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show(mensajesError, "Directorio Medico", MessageBoxButton.OK, MessageBoxImage.Information);



        }


        private void vacio(TextBox txt, List<bool> err)
        {
            if (txt.Text != null && txt.Text != "")
            {
                err.Add(false);
            }
            else
            {
                err.Add(true);
                txt.BorderBrush = System.Windows.Media.Brushes.Red;
            }
        }

        private void diasCheck(CheckBox chbox, Horario horario, bool hora, List<bool> err)
        {
            if (hora)
            {
                if (cboxdesdedia.SelectedIndex != -1 && cboxhastadia.SelectedIndex != -1)
                {
                    err.Add(false);
                    var dia = chbox.Content.ToString();
                    if (chbox.IsChecked == true)
                        switch (dia)
                        {
                            case "Lunes":
                                horario.Dias.Add(dia);
                                break;
                            case "Martes":
                                horario.Dias.Add(dia);
                                break;
                            case "Miércoles":
                                horario.Dias.Add(dia);
                                break;
                            case "Jueves":
                                horario.Dias.Add(dia);
                                break;
                            case "Viernes":
                                horario.Dias.Add(dia);
                                break;
                        }
                    horario.desde = cboxdesdedia.SelectedItem.ToString();
                    horario.hasta = cboxhastadia.SelectedItem.ToString();
                }
                else
                {
                    err.Add(true);
                    mensajesError += "- Elija un rango de horas en el horario diurno\r\n";
                }

            }
            else
            {
                if (cboxhastatarde.SelectedIndex != -1 && cboxdesdetarde.SelectedIndex != -1)
                {
                    err.Add(false);
                    var dia = chbox.Content.ToString();
                    if (chbox.IsChecked == true)
                        switch (dia)
                        {
                            case "Lunes":
                                horario.Dias.Add(dia);
                                break;
                            case "Martes":
                                horario.Dias.Add(dia);
                                break;
                            case "Miércoles":
                                horario.Dias.Add(dia);
                                break;
                            case "Jueves":
                                horario.Dias.Add(dia);
                                break;
                            case "Viernes":
                                horario.Dias.Add(dia);
                                break;
                        }
                    horario.desde = cboxdesdetarde.SelectedItem.ToString();
                    horario.hasta = cboxhastatarde.SelectedItem.ToString();
                }
                else
                {
                    err.Add(true);
                    mensajesError += "- Elija un rango de horas en el horario vespertino\r\n";
                    
                }
            }


        }

        private void telefono(ComboBox cbox, bool x)
        {
            TextBox txt;
            if (x)
                txt = txtcelular;
            else
                txt = txtfijo;
            if (cbox.SelectedIndex == -1)
                mensajesError += "- Seleccione una operadora\r\n";
            else if (cbox.SelectedIndex == -1 && txt.Text != null && txt.Text != "")
            {
                mensajesError += "- Seleccione una operadora\r\n";
                err.Add(true);
            }
            else if (cbox.SelectedIndex != -1 && txt.Text == null && txt.Text == "")
                mensajesError += "- Coloque un numero de telefono\r\n";
            else if (cbox.SelectedIndex != -1 && txt.Text != null && txt.Text != "")
            {
                var item = (ComboBoxItem)cbox.SelectedValue;
                var content = (string)item.Content;
                if (x)
                {
                    medico.cprefijo = content;
                    medico.cnumero = txt.Text;
                }
                else
                {
                    medico.fprefijo = content;
                    medico.fnumero = txt.Text;
                }

            }

        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void LetterValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Z]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void EmailValidation(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }
  

        private void chboxvespertino_Click(object sender, RoutedEventArgs e)
        {
            if (chboxvespertino.IsChecked == true)
            {
                chboxlunest.IsEnabled = true;
                chboxmartest.IsEnabled = true;
                chboxmiercolest.IsEnabled = true;
                chboxjuevest.IsEnabled = true;
                chboxviernest.IsEnabled = true;
                cboxdesdetarde.IsEnabled = true;
                cboxhastatarde.IsEnabled = true;
            }
            else
            {
                chboxlunest.IsEnabled = false;
                chboxmartest.IsEnabled = false;
                chboxmiercolest.IsEnabled = false;
                chboxjuevest.IsEnabled = false;
                chboxviernest.IsEnabled = false;
                cboxdesdetarde.IsEnabled = false;
                cboxhastatarde.IsEnabled = false;
            }
        }

        private void chboxdiurno_Click(object sender, RoutedEventArgs e)
        {
            if (chboxdiurno.IsChecked == true)
            {
                chboxlunesd.IsEnabled = true;
                chboxmartesd.IsEnabled = true;
                chboxmiercolesd.IsEnabled = true;
                chboxjuevesd.IsEnabled = true;
                chboxviernesd.IsEnabled = true;
                cboxdesdedia.IsEnabled = true;
                cboxhastadia.IsEnabled = true;
            }
            else
            {
                chboxlunesd.IsEnabled = false;
                chboxmartesd.IsEnabled = false;
                chboxmiercolesd.IsEnabled = false;
                chboxjuevesd.IsEnabled = false;
                chboxviernesd.IsEnabled = false;
                cboxdesdedia.IsEnabled = false;
                cboxhastadia.IsEnabled = false;
            }
        }

        private void chboxdr_Click(object sender, RoutedEventArgs e)
        {
            if(chboxdr.IsChecked == true)
            {
                chboxdra.IsEnabled = false;
                chboxlic.IsEnabled = false;
                chboxtec.IsEnabled = false;
            }
            else
            {
                chboxdra.IsEnabled = true;
                chboxlic.IsEnabled = true;
                chboxtec.IsEnabled = true;
            }
        }

        private void chboxdra_Click(object sender, RoutedEventArgs e)
        {
            if (chboxdra.IsChecked == true)
            {
                chboxdr.IsEnabled = false;
                chboxlic.IsEnabled = false;
                chboxtec.IsEnabled = false;
            }
            else
            {
                chboxdr.IsEnabled = true;
                chboxlic.IsEnabled = true;
                chboxtec.IsEnabled = true;
            }
        }

        private void chboxtec_Click(object sender, RoutedEventArgs e)
        {
            if (chboxtec.IsChecked == true)
            {
                chboxdr.IsEnabled = false;
                chboxlic.IsEnabled = false;
                chboxdra.IsEnabled = false;
            }
            else
            {
                chboxdr.IsEnabled = true;
                chboxlic.IsEnabled = true;
                chboxdra.IsEnabled = true;
            }
        }

        private void chboxlic_Click(object sender, RoutedEventArgs e)
        {
            if (chboxlic.IsChecked == true)
            {
                chboxdr.IsEnabled = false;
                chboxtec.IsEnabled = false;
                chboxdra.IsEnabled = false;
            }
            else
            {
                chboxdr.IsEnabled = true;
                chboxtec.IsEnabled = true;
                chboxdra.IsEnabled = true;
            }
        }

        private void validarCargo()
        {
            if(chboxdr.IsChecked == false && chboxdra.IsChecked == false && chboxtec.IsChecked == false && chboxlic.IsChecked == false)
            {
                err.Add(true);
                return;
            }
            if (chboxdr.IsChecked == true)
                medico.cargo = chboxdr.Content.ToString();
            if(chboxdra.IsChecked == true)
                medico.cargo = chboxdra.Content.ToString();
            if (chboxtec.IsChecked == true)
                medico.cargo = chboxtec.Content.ToString();
            if (chboxlic.IsChecked == true)
                medico.cargo = chboxlic.Content.ToString();


        }
    }
}
