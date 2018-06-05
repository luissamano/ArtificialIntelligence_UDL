using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnowDesktop
{
    public partial class Form1 : Form
    {



        DialogResult Gua;
        Get get = new Get();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void LimpiarEnable () 
        {
            tbBuscar.Text = "";
            tbDescr.Text = "";
            tbColor.Text = "";
            btnAgregar.Enabled = false;
            
            tbDescr.Enabled = false;
            tbColor.Enabled = false;
            cbEstado.Enabled = false;
            cbGenero.Enabled = false;
            btnAgregar.Enabled = false;
            btnBuscar.Enabled = true;
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            
            var res = await get.GetConocimientos(tbBuscar.Text.ToString());
            
            foreach (var item in res)
            {
                if (item.Nombre.Equals(tbBuscar.Text.ToString()))
                    MessageBox.Show($"{item.Genero} {item.Nombre} es {item.Definicion} y su color es {item.Color}");
                else
                {
                   Gua = MessageBox.Show("No lo conozco, quieres agregarlo a la DB?", "Desconocido", MessageBoxButtons.YesNo);
                }
            }

            if (res.Count == 0) 
            { 
                Gua = MessageBox.Show("Hey no lo conozco, quieres agregarlo a la Knowlagde?", "Info", MessageBoxButtons.YesNo);
            }

            if (Gua == System.Windows.Forms.DialogResult.Yes)
            {
                tbDescr.Enabled = true;
                tbColor.Enabled = true;
                cbEstado.Enabled = true;
                cbGenero.Enabled = true;
                btnAgregar.Enabled = true;
                btnBuscar.Enabled = false;
                MessageBox.Show("Por favor, llena los campos que faltan!");              
            }
            
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            var Obj = tbBuscar.Text.ToString();
            var Def = tbDescr.Text.ToString();
            var Color = tbColor.Text.ToString();
            var ani = cbEstado.Text.ToString();
            var gen = cbGenero.Text.ToString();
            var num = await get.GetUltimoNun();

            var cont = await get.PostConocimiento(Obj, Def, Color, ani, gen, num);

            if (cont != 0)
            {
                MessageBox.Show("Agregado a la database");
                LimpiarEnable();
            }

            else
                MessageBox.Show("Error, intenta de nuevo");
        }
    }
}
