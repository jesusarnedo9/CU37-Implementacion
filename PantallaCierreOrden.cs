using ImplementacionCU37.Controlador;
using ImplementacionCU37.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ImplementacionCU37
{
    public partial class PantallaCierreOrden : Form
    {
        private GestorOrdenInspeccion gestor;

        public PantallaCierreOrden(Sistema sistema)
        {
            InitializeComponent();
            gestor = new GestorOrdenInspeccion(sistema, this);
            this.AcceptButton = btnConfirmarMotivos;
        }
        //LOAD
        private void PantallaCierreOrden_Load(object sender, EventArgs e)
        {
            gestor?.opcionCerrarOrdenInspeccion();
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            try
            {
                gestor?.RecargarOrdenesDesdeBD();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelarCerrarOI_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Está seguro que desea cancelar? Se descartarán los cambios no guardados.", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

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
        public void solicitarSeleccionOrden(List<OrdenInspeccionDTO> ordenesRealizadas)
        {
            listaOrdenInspeccion.Items.Clear();

            if (ordenesRealizadas == null || ordenesRealizadas.Count == 0)
            {
                MessageBox.Show("No hay ordenes realizadas", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listaOrdenInspeccion.Enabled = false;
                return;
            }

            listaOrdenInspeccion.DisplayMember = "Texto";
            foreach (var dto in ordenesRealizadas)
            {
                listaOrdenInspeccion.Items.Add(dto);
            }
        }
        private void tomarOrdenSeleccionada(object sender, EventArgs e)
        {
            if (listaOrdenInspeccion.SelectedItem is OrdenInspeccionDTO dto)
            {
                string numeroOrden = dto.Id;
                gestor.tomarOrdenSeleccionada(numeroOrden);

                lblSeleccionarMotivo.Visible = false;
                chkMotivos.Visible = false;
                btnConfirmarMotivos.Visible = false;
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

        private void btnConfirmarObservacion_Click(object sender, EventArgs e)
        {
            string observacion = tomarObservacionCierre();
            gestor.tomarObservacionCierre(observacion);
            txtObservacionCierre.Enabled = false;

            lblSeleccionarMotivo.Visible = true;
            chkMotivos.Visible = true;
            btnConfirmarObservacion.Visible = false;
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
        private void tomarSeleccionMotivo(object sender, ItemCheckEventArgs e)
        {
            string descripcionSeleccionada = chkMotivos.Items[e.Index].ToString();
            if (e.NewValue == CheckState.Checked)
            {
                gestor.tomarMotivoSeleccionado(descripcionSeleccionada, e.Index);
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                gestor.quitarMotivoSeleccionado(descripcionSeleccionada, e.Index);
            }
        }
        public void solicitarComentario(string descripcion, int indiceCheckbox)
        {
            using (var form = new VentanaComentario(descripcion))
            {
                form.IndiceCheckbox = indiceCheckbox; 

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
        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void btnCerrarOrden_Click(object sender, EventArgs e)
        {
            string observacion = txtObservacionCierre.Text;

        }
        public void txtObservacionCierre_TextChanged(object sender, EventArgs e)
        {
        }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void btnConfirmarMotivos_Click(object sender, EventArgs e)
        {
            gestor.motivosConfirmados();
        }
        private void label2_Click(object sender, EventArgs e)
        {
        }
        internal void mostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }
        private void label3_Click(object sender, EventArgs e)
        {
        }

        public void mostrarActualizacionEstado(
            EstacionSismologica estacion,
            string sismografo,
            List<MotivoFueraServicioDTO> motivosSeleccionados,
            Empleado empleado,
            object fechaHoraCierre)
        {
            string motivos = string.Join(
                "\n - ",
                motivosSeleccionados.Select(m => $"{m.Motivo.descripcion}: {m.Comentario}")
            );

            string mensaje =
                $"Estado actualizado con éxito.\n" +
                $"Estación: {estacion.nombre}\n" +
                $"Sismógrafo: {sismografo}\n" +
                $"Motivos Seleccionados:\n - {motivos}\n" +
                $"Responsable: {empleado.idEmpleado}, {empleado.apellido} {empleado.nombre}\n" +
                $"Fecha/Hora de cierre: {fechaHoraCierre}";

            MessageBox.Show(mensaje, "Actualización Exitosa");
        }

        public void restaurarPantallaParaObservacion()
        {
            // Evitar disparar lógica de selección mientras desmarcamos
            chkMotivos.ItemCheck -= tomarSeleccionMotivo;
            try
            {
                for (int i = 0; i < chkMotivos.Items.Count; i++)
                {
                    if (chkMotivos.GetItemChecked(i))
                        chkMotivos.SetItemChecked(i, false);
                }
            }
            finally
            {
                chkMotivos.ItemCheck += tomarSeleccionMotivo;
            }

            lblSeleccionarMotivo.Visible = false;
            chkMotivos.Visible = false;
            btnConfirmarMotivos.Visible = false;

            txtObservacionCierre.Enabled = true;
            btnConfirmarObservacion.Visible = true;
            txtObservacionCierre.Focus();
        }
    }
}
