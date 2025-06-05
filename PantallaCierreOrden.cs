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
        private List<MotivoFueraServicio> motivosSeleccionados;
        private List<MotivoTipo> motivos;

        public PantallaCierreOrden(Sistema sistema)
        {
            InitializeComponent();
            gestor = new GestorOrdenInspeccion(sistema, this);
            motivos = new List<MotivoTipo>();
            motivosSeleccionados = new List<MotivoFueraServicio>();
            this.AcceptButton = btnConfirmarMotivos;
        }
        //LOAD
        private void PantallaCierreOrden_Load(object sender, EventArgs e)
        {
            gestor.opcionCerrarOrdenInspeccion();
        }
        // Atributos
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
            this.ShowDialog();
        }
        public void cerrarVentana()
        {
            this.Close();
        }
        public void solicitarSeleccionOrden(List<OrdenDeInspeccion> ordenesRealizadas)
        {
            listaOrdenInspeccion.Items.Clear();
            if (ordenesRealizadas == null || ordenesRealizadas.Count == 0)
            {
                MessageBox.Show("No hay ordenes realizdas", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }
            foreach (var orden in ordenesRealizadas)
            {
                listaOrdenInspeccion.Items.Add(orden);
            }
        }

        /*public void mostrarOrdenSelecionada();
        {
            if (listaOrdenInspeccion.SelectedItem != null)
            {
                OrdenDeInspeccion ordenSeleccionada = listaOrdenInspeccion.SelectedItem as OrdenDeInspeccion;
                if (ordenSeleccionada != null)
                {
                    txtObservacionCierre.Text = ordenSeleccionada.observacionCierre;
                }
}
        }implementarlo abajo*/ 

        private void tomarOrdenSeleccionada(object sender, EventArgs e)
        {
            try
            {
                if (listaOrdenInspeccion.SelectedItem == null)
                {
                    throw new Exception("Debe seleccionar una orden válida.");
                }
                // Pasar la orden seleccionada al gestor
                string numeroOrden = (listaOrdenInspeccion.SelectedItem as OrdenDeInspeccion).numeroOrden.ToString();
                gestor.tomarOrdenSeleccionada(numeroOrden);

                // Ocultar controles de motivos por si estaban visibles
                lblSeleccionarMotivo.Visible = false;
                chkMotivos.Visible = false;
                btnConfirmarMotivos.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtObservacionCierre.Text = "";
            }
        }
        public void solicitarObservacionCierre()
        {
            label1.Visible = true;
            txtObservacionCierre.Visible = true;
            btnConfirmarObservacion.Visible = true;
            txtObservacionCierre.Focus();
        }
        public string tomarObservacionCierre()
        {
            return txtObservacionCierre.Text;
        }
        /*public void tomarObservacionCierre(object sender, EventArgs e)//Este metodo lo tengo que cambiar en el designer
        {

        }*/
        public void opcionCerrarOrdenInspeccion()
        {
            gestor.opcionCerrarOrdenInspeccion();   
        }
        public void solicitarConfirmacionCierre()
        {
            bool confirmacion = MessageBox.Show("¿Confirmar cierre de Orden de Inspeccion?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            tomarConfirmacionCierre(confirmacion);
        }
        public void tomarConfirmacionCierre(bool confirmacion) 
        { 
            gestor.tomarConfirmacionCierre(confirmacion);
        }
        public void solicitarSeleccionMotivo(List<MotivoTipo> motivos)//tengo que hacer el metodo tomarSeleccionMotivo
        {
            chkMotivos.Items.Clear();
            chkMotivos.DisplayMember = "descripcion";
            foreach (var motivo in motivos)
                chkMotivos.Items.Add(motivo);

            chkMotivos.Visible = true;
            btnConfirmarMotivos.Visible = true;
        }

        private void tomarSeleccionMotivo(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                MotivoTipo motivoSeleccionado = chkMotivos.Items[e.Index] as MotivoTipo;
                if (motivoSeleccionado == null) return;

                string comentario = solicitarComentario(motivoSeleccionado);

                if (!string.IsNullOrEmpty(comentario))
                {
                    MotivoFueraServicio motivoFueraServicio = new MotivoFueraServicio(motivoSeleccionado, comentario);
                    motivosSeleccionados.Add(motivoFueraServicio);
                }
                else
                {
                    e.NewValue = CheckState.Unchecked;
                }
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                MotivoTipo motivo = chkMotivos.Items[e.Index] as MotivoTipo;
                if (motivo == null) return;
                motivosSeleccionados.RemoveAll(m => m.tipo == motivo);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void btnCerrarOrden_Click(object sender, EventArgs e)
        {
            string observacion = txtObservacionCierre.Text;
        }
        private void txtObservacionCierre_TextChanged(object sender, EventArgs e)
        {
        }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {   
        }
        private void btnConfirmarMotivos_Click(object sender, EventArgs e)
        {
            foreach (MotivoFueraServicio motivoFS in motivosSeleccionados)
            {
                gestor.tomarMotivoYComentario(motivoFS.tipo, motivoFS.comentario);
            }
            chkMotivos.Visible = false;

        }
        public string solicitarComentario(MotivoTipo motivo)
        {
            using (var form = new VentanaComentario(motivo.descripcion))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    return form.tomarComentario();
                }
                return string.Empty;
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
            string observacion = tomarObservacionCierre();
            gestor.tomarObservacionCierre(observacion);
            List<MotivoTipo> motivosDisponibles = gestor.buscarMotivo();
            solicitarSeleccionMotivo(motivosDisponibles);

            lblSeleccionarMotivo.Visible = true;
            chkMotivos.Visible = true;
            btnConfirmarObservacion.Visible = false;
        }
        private void label3_Click(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void mostrarActualizacionEstado(EstacionSismologica estacion, string sismografo, List<MotivoFueraServicio> motivosSeleccionados, Empleado empleado, object fechaHoraCierre)
        {
            string mensaje =
                $"Estado actualizado con éxito.\n" +
                $"Estación: {estacion.nombre}\n" +
                $"Sismógrafo: {sismografo}\n" +
                $"Motivos: {string.Join(", ", motivos.Select(m => m.descripcion))}\n" +
                $"Responsable: {empleado.id}, {empleado.apellido} {empleado.nombre}\n" +
                $"Fecha/Hora de cierre: {fechaHoraCierre}";

            MessageBox.Show(mensaje, "Actualización Exitosa");
        }
    }
}
