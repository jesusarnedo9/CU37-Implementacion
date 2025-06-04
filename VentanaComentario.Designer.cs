namespace ImplementacionCU37
{
    partial class VentanaComentario
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
            this.lblMotivo = new System.Windows.Forms.Label();
            this.txtComentario = new System.Windows.Forms.TextBox();
            this.btnAceptarComentario = new System.Windows.Forms.Button();
            this.btnCancelarComentario = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMotivo
            // 
            this.lblMotivo.AutoSize = true;
            this.lblMotivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMotivo.Location = new System.Drawing.Point(118, 18);
            this.lblMotivo.Name = "lblMotivo";
            this.lblMotivo.Size = new System.Drawing.Size(86, 18);
            this.lblMotivo.TabIndex = 0;
            this.lblMotivo.Text = "Comentario";
            // 
            // txtComentario
            // 
            this.txtComentario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtComentario.Location = new System.Drawing.Point(38, 53);
            this.txtComentario.Multiline = true;
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.Size = new System.Drawing.Size(538, 59);
            this.txtComentario.TabIndex = 1;
            this.txtComentario.TextChanged += new System.EventHandler(this.txtComentario_TextChanged);
            // 
            // btnAceptarComentario
            // 
            this.btnAceptarComentario.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAceptarComentario.Location = new System.Drawing.Point(327, 125);
            this.btnAceptarComentario.Name = "btnAceptarComentario";
            this.btnAceptarComentario.Size = new System.Drawing.Size(98, 29);
            this.btnAceptarComentario.TabIndex = 2;
            this.btnAceptarComentario.Text = "Aceptar";
            this.btnAceptarComentario.UseVisualStyleBackColor = true;
            this.btnAceptarComentario.Click += new System.EventHandler(this.btnAceptarComentario_Click);
            // 
            // btnCancelarComentario
            // 
            this.btnCancelarComentario.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancelarComentario.Location = new System.Drawing.Point(184, 123);
            this.btnCancelarComentario.Name = "btnCancelarComentario";
            this.btnCancelarComentario.Size = new System.Drawing.Size(101, 31);
            this.btnCancelarComentario.TabIndex = 3;
            this.btnCancelarComentario.Text = "Cancelar";
            this.btnCancelarComentario.UseVisualStyleBackColor = false;
            this.btnCancelarComentario.Click += new System.EventHandler(this.btnCancelarComentario_Click);
            // 
            // VentanaComentario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(628, 162);
            this.Controls.Add(this.btnCancelarComentario);
            this.Controls.Add(this.btnAceptarComentario);
            this.Controls.Add(this.txtComentario);
            this.Controls.Add(this.lblMotivo);
            this.Name = "VentanaComentario";
            this.Text = "VentanaComentario";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.TextBox txtComentario;
        private System.Windows.Forms.Button btnAceptarComentario;
        private System.Windows.Forms.Button btnCancelarComentario;
    }
}