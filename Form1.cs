using ImplementacionCU37.Controlador;
using ImplementacionCU37.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImplementacionCU37
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Boton Cerrar Orden
        private void btnCerrar(object sender, EventArgs e)
        {
            Sistema sistema = new Sistema();
            PantallaCierreOrden pantalla = new PantallaCierreOrden(sistema);
            pantalla.ShowDialog();
            pantalla.habilitarPantalla();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
