using System;
using System.Configuration;
using System.Windows.Forms;

namespace ImplementacionCU37
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //var cs = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;
            //ImplementacionCU37.DatabaseInitializer.Seed(cs);
            Application.Run(new Form1());
        }
    }
}
