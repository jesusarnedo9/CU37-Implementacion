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
    public partial class VentanaComentario : Form
    {
        public string ComentarioIngresado { get; private set; }

        public VentanaComentario(string motivo)
        {
            InitializeComponent();
            lblMotivo.Text = $"Comentario para: {motivo}";
            this.AcceptButton = btnAceptarComentario;

        }

        private void btnAceptarComentario_Click(object sender, EventArgs e)
        {
            ComentarioIngresado = txtComentario.Text.Trim();
            if (string.IsNullOrEmpty(ComentarioIngresado))
            {
                MessageBox.Show("Debe ingresar un comentario.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelarComentario_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

}
