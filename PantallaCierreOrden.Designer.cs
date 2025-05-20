namespace ImplementacionCU37
{
    partial class PantallaCierreOrden
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listaOrdenInspeccion = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtObservacionCierre = new System.Windows.Forms.TextBox();
            this.btnConfirmarObservacion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listaOrdenInspeccion
            // 
            this.listaOrdenInspeccion.FormattingEnabled = true;
            this.listaOrdenInspeccion.Location = new System.Drawing.Point(234, 122);
            this.listaOrdenInspeccion.Name = "listaOrdenInspeccion";
            this.listaOrdenInspeccion.Size = new System.Drawing.Size(284, 21);
            this.listaOrdenInspeccion.TabIndex = 0;
            this.listaOrdenInspeccion.SelectedIndexChanged += new System.EventHandler(this.listaOrdenInspeccion_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(231, 189);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Observacion de Cierre";
            this.label1.Visible = false;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtObservacionCierre
            // 
            this.txtObservacionCierre.Location = new System.Drawing.Point(360, 189);
            this.txtObservacionCierre.Name = "txtObservacionCierre";
            this.txtObservacionCierre.Size = new System.Drawing.Size(158, 20);
            this.txtObservacionCierre.TabIndex = 2;
            this.txtObservacionCierre.Visible = false;
            this.txtObservacionCierre.TextChanged += new System.EventHandler(this.txtObservacionCierre_TextChanged);
            // 
            // btnConfirmarObservacion
            // 
            this.btnConfirmarObservacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnConfirmarObservacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmarObservacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmarObservacion.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnConfirmarObservacion.ForeColor = System.Drawing.Color.White;
            this.btnConfirmarObservacion.Location = new System.Drawing.Point(387, 263);
            this.btnConfirmarObservacion.Name = "btnConfirmarObservacion";
            this.btnConfirmarObservacion.Size = new System.Drawing.Size(131, 46);
            this.btnConfirmarObservacion.TabIndex = 4;
            this.btnConfirmarObservacion.Text = "Confirmar";
            this.btnConfirmarObservacion.UseVisualStyleBackColor = false;
            this.btnConfirmarObservacion.Visible = false;
            this.btnConfirmarObservacion.Click += new System.EventHandler(this.btnCerrarOrden_Click);
            // 
            // PantallaCierreOrden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnConfirmarObservacion);
            this.Controls.Add(this.txtObservacionCierre);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listaOrdenInspeccion);
            this.Name = "PantallaCierreOrden";
            this.Text = "CerrarOrdenInscripcion";
            this.Load += new System.EventHandler(this.PantallaCierreOrden_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox listaOrdenInspeccion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtObservacionCierre;
        private System.Windows.Forms.Button btnConfirmarObservacion;
    }
}