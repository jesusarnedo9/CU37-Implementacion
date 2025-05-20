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
    public partial class PantallaCierreOrden : Form
    {
        private GestorOrdenInspeccion gestor;
        public PantallaCierreOrden()
        {
            InitializeComponent();
            gestor = new GestorOrdenInspeccion(this); // Pasa referencia de pantalla
            gestor.opcionCerrarOrdenInspeccion(); // Llamá acá directamente
        }

        private void PantallaCierreOrden_Load(object sender, EventArgs e)
        {
            gestor.opcionCerrarOrdenInspeccion();
        }

        //Agrego metodos y atributos del otro proyecto

        // Atributos: Controles de la pantalla
        private Button btnCancelar;
        private Button btnConfirmar;
        private TextBox inputComentario;
        private TextBox inputObservacionCierre;
        private Label lblComentario;
        private Label lblObservacionCierre;
        private CheckedListBox listaMotivo;



        // Métodos

        public void habilitarPantalla()
        {
            this.Enabled = true;
        }


        public void opcionCerrarOrdenInspeccion()
        {
            gestor.opcionCerrarOrdenInspeccion();
        }

        public void solicitarComentario()
        {
            gestor.tomarComentario(inputComentario.Text);
        }

        public void solicitarConfirmacionCierre()
        {
            gestor.tomarConfirmacionCierre(tomarConfirmacionCierre());
        }

        public void solicitarObservacionCierre()
        {
            gestor.tomarObservacionCierre(tomarObservacionCierre());
        }

        public void solicitarSeleccionMotivo()
        {
            gestor.tomarSeleccionMotivo(tomarSeleccionMotivo());
        }

        public void solicitarSeleccionOrden(List<OrdenDeInspeccion> ordenes)
        {
            listaOrdenInspeccion.Items.Clear();
            foreach (var orden in ordenes)
            {
                listaOrdenInspeccion.Items.Add(orden);
            }
        }


        public string tomarComentario()
        {
            return inputComentario.Text;
        }

        public bool tomarConfirmacionCierre()
        {
            // Simula que hay una confirmación, podés mejorarlo con un diálogo
            return MessageBox.Show("¿Confirmar cierre?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes;
        }

        public string tomarObservacionCierre()
        {
            return inputObservacionCierre.Text;
        }

        public OrdenDeInspeccion tomarOrdenSeleccionada()
        {
            return listaOrdenInspeccion.SelectedItem as OrdenDeInspeccion;
        }

        public List<string> tomarSeleccionMotivo()
        {
            var motivos = new List<string>();
            foreach (var item in listaMotivo.CheckedItems)
            {
                motivos.Add(item.ToString());
            }
            return motivos;
        }

        private void listaOrdenInspeccion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
