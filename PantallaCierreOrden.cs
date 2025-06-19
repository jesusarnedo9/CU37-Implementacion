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
        private List<MotivoFueraServicio> motivosSeleccionados;//arreglar
        

        public PantallaCierreOrden(Sistema sistema)
        {
            InitializeComponent();
            gestor = new GestorOrdenInspeccion(sistema, this);
            motivosSeleccionados = new List<MotivoFueraServicio>();
            this.AcceptButton = btnConfirmarMotivos;
        }
        //LOAD
        private void PantallaCierreOrden_Load(object sender, EventArgs e)
        {
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
            gestor.opcionCerrarOrdenInspeccion();
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
        private void tomarOrdenSeleccionada(object sender, EventArgs e)
        {
             // Pasar la orden seleccionada al gestor
             string numeroOrden = (listaOrdenInspeccion.SelectedItem as OrdenDeInspeccion).numeroOrden.ToString();
             gestor.tomarOrdenSeleccionada(numeroOrden);
             // Ocultar controles de motivos por si estaban visibles
             lblSeleccionarMotivo.Visible = false;
             chkMotivos.Visible = false;
             btnConfirmarMotivos.Visible = false;
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
        public void solicitarConfirmacionCierre()
        {
            bool confirmacion = MessageBox.Show("¿Confirmar cierre de Orden de Inspeccion?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            tomarConfirmacionCierre(confirmacion);
        }
        public void tomarConfirmacionCierre(bool confirmacion) 
        { 
            gestor.tomarConfirmacionCierre(confirmacion);
        }
        public void solicitarSeleccionMotivo(List<string> motivos)
        {
            chkMotivos.Items.Clear();
            foreach (var motivo in motivos)
                chkMotivos.Items.Add(motivo);

            chkMotivos.Visible = true;
            btnConfirmarMotivos.Visible = true;
        }
        //Evento para manejar la selección de motivos
        private void tomarSeleccionMotivo(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                string descripcionSeleccionada = chkMotivos.Items[e.Index].ToString();
                gestor.tomarMotivoSeleccionado(descripcionSeleccionada, e.Index);
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                string descripcionSeleccionada = chkMotivos.Items[e.Index].ToString(); 
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
            gestor.motivosConfirmados();
        }
        public void solicitarComentario(string descripcion, int indiceCheckbox)
        {
            using (var form = new VentanaComentario(descripcion))
            {
                form.IndiceCheckbox = indiceCheckbox; // Pasás el índice

                if (form.ShowDialog() == DialogResult.OK)
                {
                    var comentario = form.tomarComentario();
                    gestor.tomarComentario(descripcion, comentario);
                }
                else
                {
                    chkMotivos.ItemCheck -= tomarSeleccionMotivo;
                    chkMotivos.BeginInvoke((MethodInvoker)(() =>
                    {
                        chkMotivos.SetItemChecked(indiceCheckbox, false);
                    }));
                    chkMotivos.ItemCheck += tomarSeleccionMotivo;
                }
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
                //$"Motivos: {string.Join(", ", motivos.Select(m => m.descripcion))}\n" +
                $"Responsable: {empleado.id}, {empleado.apellido} {empleado.nombre}\n" +
                $"Fecha/Hora de cierre: {fechaHoraCierre}";

            MessageBox.Show(mensaje, "Actualización Exitosa");
        }
    }

}
