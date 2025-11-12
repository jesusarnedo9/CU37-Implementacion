using ImplementacionCU37.Dao;
using ImplementacionCU37.Entidades;
using System;
using System.Configuration;
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
        private void opcionCerrarOrdenInspeccion(object sender, EventArgs e)
        {
            var cs = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;
            DatabaseInitializer.Seed(cs);
            Console.WriteLine("CS: " + ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString);

            // Leer directo con el DAO para verificar lo que la app ve en BD
            var ordenDao = new OrdenDeInspeccionDao(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            var todas = ordenDao.GetAll();
            Console.WriteLine($"Ordenes desde DAO: {todas.Count}");


            Sistema sistema = new Sistema();
            PantallaCierreOrden pantalla = new PantallaCierreOrden(sistema);
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
