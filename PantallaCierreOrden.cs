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


        //LOAD
        private void PantallaCierreOrden_Load(object sender, EventArgs e)
        {
            gestor.opcionCerrarOrdenInspeccion();

            // Ocultamos todo al inicio
            label1.Visible = false;
            txtObservacionCierre.Visible = false;
            btnConfirmarObservacion.Visible = false;
            lblSeleccionarMotivo.Visible = false;
            chkMotivos.Visible = false;
            btnConfirmarMotivos.Visible = false;

            

        }


        // Atributos: Controles de la pantalla
        private Button btnCancelar;
        private Button btnConfirmar;
        private TextBox inputComentario;

        //private TextBox inputObservacionCierre;
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

        public void solicitarSeleccionMotivo(List<MotivoTipo> motivos)
        {
            chkMotivos.DisplayMember = "Descripcion";
            chkMotivos.Items.Clear();

            foreach (var motivo in motivos)
            {
                chkMotivos.Items.Add(motivo); // Mostrará la descripción si sobreescribiste ToString()
            }

            chkMotivos.Visible = true;
            btnConfirmarMotivos.Visible = true;
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
            return txtObservacionCierre.Text;
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
            // Mostrar los controles de observación
            label1.Visible = true;
            txtObservacionCierre.Visible = true;
            btnConfirmarObservacion.Visible = true;

            // Ocultar controles de motivos por si estaban visibles
            lblSeleccionarMotivo.Visible = false;
            chkMotivos.Visible = false;
            btnConfirmarMotivos.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrarOrden_Click(object sender, EventArgs e)
        {
            string observacion = txtObservacionCierre.Text;

            // Enviar la observación al gestor
            gestor.tomarObservacionCierre(observacion);

            /*
            // Ocultar los controles de observación
            label1.Visible = false;
            txtObservacionCierre.Visible = false;
            btnConfirmarObservacion.Visible = false;*/

        }

        private void txtObservacionCierre_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnConfirmarMotivos_Click(object sender, EventArgs e)
        {
            List<string> solicitudMotivo = new List<string>();

            foreach (var item in chkMotivos.CheckedItems)
            {
                if (item is MotivoTipo motivo)
                {
                    solicitudMotivo.Add(motivo.descripcion);
                }
            }

            gestor.tomarSeleccionMotivo(solicitudMotivo);

            // Ocultar luego de confirmar
            chkMotivos.Visible = false;
            btnConfirmarMotivos.Visible = false;

            // Mostrar cuadro de confirmación
            DialogResult resultado = MessageBox.Show(
                "¿Estás seguro de cerrar la orden de inspección?",
                "Confirmar Cierre",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                gestor.validarDatosIngresados();
            }
            else
            {
                // Opcional: mostrar mensaje de cancelación
                MessageBox.Show("Cierre cancelado.");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        internal void mostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        private void btnConfirmarObservacion_Click(object sender, EventArgs e)
        {
            lblSeleccionarMotivo.Visible = true;
            chkMotivos.Visible = true;
            btnConfirmarMotivos.Visible = true;
            btnConfirmarObservacion.Visible = false;

            GestorOrdenInspeccion gestor = new GestorOrdenInspeccion(this);

            chkMotivos.Items.Clear();
            foreach (MotivoTipo motivo in gestor.buscarMotivo())
            {
                chkMotivos.Items.Add(motivo);
            }
        }


        private void label3_Click(object sender, EventArgs e)
        {
    
        }
    }
}
