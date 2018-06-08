using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Know.WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DialogResult result;
        Metodos method = new Metodos();
        public ObservableCollection<string> lsEstado = new ObservableCollection<string>();
        public ObservableCollection<string> lsGenero = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();
            AddVar();

            this.btnBuscar.Click += BtnBuscar_Click;
            this.btnAgregar.Click += BtnAgregar_Click;
        }


        public void EnableControls()
        {
            tbColor.IsEnabled = true;
            tbDef.IsEnabled = true;
            cbEstado.IsEnabled = true;
            cbEstado.IsEnabled = true;
            cbGenero.IsEnabled = true;
            btnAgregar.IsEnabled = true;
        }

        public void AddVar()
        {
            lsEstado.Add("Animado");
            lsEstado.Add("Inanimado");

            lsGenero.Add("El");
            lsGenero.Add("La");

            cbEstado.ItemsSource = lsEstado;
            cbGenero.ItemsSource = lsGenero;
        }

        public void LimpiarEnable()
        {
            tbBuscar.Text = "";
            tbDef.Text = "";
            tbColor.Text = "";
        }

        private async void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            var Obj = tbBuscar.Text.ToLower().ToString();
            var Def = tbDef.Text.ToLower().ToString();
            var Color = tbColor.Text.ToLower().ToString();
            var ani = cbEstado.Text.ToLower().ToString();
            var gen = cbGenero.Text.ToLower().ToString();

            var num = await method.GetUltimoNun();
            var cont = await method.PostConocimiento(Obj, Def, Color, ani, gen, num);

            if (cont == -1)
            {
                System.Windows.Forms.MessageBox.Show("Agregado a la knowledge");
                LimpiarEnable();
            }

            else
                System.Windows.Forms.MessageBox.Show("Hey algo salio mal, intentalo nuevamente");
        }

        private async void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (!tbBuscar.Text.ToString().Equals(""))
            {

                var cont = await method.GetUltimoNun();
                var x = await method.GetConocimientos(tbBuscar.Text.ToLower().ToString());

                if (x.Count == 0)
                {
                    result = System.Windows.Forms.MessageBox.Show
                        ("No se encontro en la knowledge, deseas agregar el conocimiento",
                        "Null", System.Windows.Forms.MessageBoxButtons.YesNo);
                }

                foreach (var item in x)
                {
                    if (x.Count == 0)
                    {
                        result = System.Windows.Forms.MessageBox.Show
                            ("No se encontro en la knowledge, deseas agregar el conocimiento",
                            "Null", System.Windows.Forms.MessageBoxButtons.YesNo);
                    }
                    else
                    {

                        System.Windows.Forms.MessageBox.Show
                        ($"{item.Genero} {item.Nombre} es {item.Definicion} y su color mas usual es {item.Color}",
                         "Know", System.Windows.Forms.MessageBoxButtons.OK);
                    }
                }

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    EnableControls();
                }
            }
        }
        
    }
}
