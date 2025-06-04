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

        public string tomarComentario()
        {
            return txtComentario.Text.Trim();
        }


        private void btnAceptarComentario_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtComentario.Text))
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

        private void txtComentario_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
