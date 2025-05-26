using System.Drawing;

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
            this.chkMotivos = new System.Windows.Forms.CheckedListBox();
            this.btnConfirmarMotivos = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSeleccionarMotivo = new System.Windows.Forms.Label();
            this.btnCancelarCerrarOI = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listaOrdenInspeccion
            // 
            this.listaOrdenInspeccion.FormattingEnabled = true;
            this.listaOrdenInspeccion.Location = new System.Drawing.Point(157, 120);
            this.listaOrdenInspeccion.Margin = new System.Windows.Forms.Padding(4);
            this.listaOrdenInspeccion.Name = "listaOrdenInspeccion";
            this.listaOrdenInspeccion.Size = new System.Drawing.Size(707, 24);
            this.listaOrdenInspeccion.TabIndex = 0;
            this.listaOrdenInspeccion.SelectedIndexChanged += new System.EventHandler(this.listaOrdenInspeccion_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(308, 212);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Observacion de Cierre";
            this.label1.Visible = false;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtObservacionCierre
            // 
            this.txtObservacionCierre.Location = new System.Drawing.Point(480, 212);
            this.txtObservacionCierre.Margin = new System.Windows.Forms.Padding(4);
            this.txtObservacionCierre.Name = "txtObservacionCierre";
            this.txtObservacionCierre.Size = new System.Drawing.Size(209, 22);
            this.txtObservacionCierre.TabIndex = 2;
            this.txtObservacionCierre.Visible = false;
            this.txtObservacionCierre.Click += new System.EventHandler(this.listaOrdenInspeccion_SelectedIndexChanged);
            this.txtObservacionCierre.TextChanged += new System.EventHandler(this.txtObservacionCierre_TextChanged);
            // 
            // btnConfirmarObservacion
            // 
            this.btnConfirmarObservacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnConfirmarObservacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmarObservacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmarObservacion.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnConfirmarObservacion.ForeColor = System.Drawing.Color.White;
            this.btnConfirmarObservacion.Location = new System.Drawing.Point(516, 273);
            this.btnConfirmarObservacion.Margin = new System.Windows.Forms.Padding(4);
            this.btnConfirmarObservacion.Name = "btnConfirmarObservacion";
            this.btnConfirmarObservacion.Size = new System.Drawing.Size(175, 57);
            this.btnConfirmarObservacion.TabIndex = 4;
            this.btnConfirmarObservacion.Text = "Confirmar";
            this.btnConfirmarObservacion.UseVisualStyleBackColor = false;
            this.btnConfirmarObservacion.Visible = false;
            this.btnConfirmarObservacion.Click += new System.EventHandler(this.btnConfirmarObservacion_Click);
            // 
            // chkMotivos
            // 
            this.chkMotivos.FormattingEnabled = true;
            this.chkMotivos.Location = new System.Drawing.Point(314, 265);
            this.chkMotivos.Margin = new System.Windows.Forms.Padding(4);
            this.chkMotivos.Name = "chkMotivos";
            this.chkMotivos.Size = new System.Drawing.Size(377, 123);
            this.chkMotivos.TabIndex = 5;
            this.chkMotivos.Visible = false;
            this.chkMotivos.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // btnConfirmarMotivos
            // 
            this.btnConfirmarMotivos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnConfirmarMotivos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmarMotivos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmarMotivos.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnConfirmarMotivos.ForeColor = System.Drawing.Color.White;
            this.btnConfirmarMotivos.Location = new System.Drawing.Point(364, 406);
            this.btnConfirmarMotivos.Margin = new System.Windows.Forms.Padding(4);
            this.btnConfirmarMotivos.Name = "btnConfirmarMotivos";
            this.btnConfirmarMotivos.Size = new System.Drawing.Size(257, 52);
            this.btnConfirmarMotivos.TabIndex = 6;
            this.btnConfirmarMotivos.Text = "Confirmar Motivos";
            this.btnConfirmarMotivos.UseVisualStyleBackColor = false;
            this.btnConfirmarMotivos.Visible = false;
            this.btnConfirmarMotivos.Click += new System.EventHandler(this.btnConfirmarMotivos_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(435, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Ordenes de Inspeccion";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // lblSeleccionarMotivo
            // 
            this.lblSeleccionarMotivo.AutoSize = true;
            this.lblSeleccionarMotivo.Location = new System.Drawing.Point(435, 245);
            this.lblSeleccionarMotivo.Name = "lblSeleccionarMotivo";
            this.lblSeleccionarMotivo.Size = new System.Drawing.Size(129, 16);
            this.lblSeleccionarMotivo.TabIndex = 8;
            this.lblSeleccionarMotivo.Text = "Seleccionar Motivos";
            this.lblSeleccionarMotivo.VisibleChanged += new System.EventHandler(this.label1_Click);
            this.lblSeleccionarMotivo.Click += new System.EventHandler(this.label3_Click);
            // 
            // btnCancelarCerrarOI
            // 
            this.btnCancelarCerrarOI.BackColor = Color.Tomato;
            this.btnCancelarCerrarOI.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelarCerrarOI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarCerrarOI.FlatAppearance.BorderSize = 0;
            this.btnCancelarCerrarOI.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCancelarCerrarOI.ForeColor = System.Drawing.Color.White;
            this.btnCancelarCerrarOI.Location = new System.Drawing.Point(13, 484);
            this.btnCancelarCerrarOI.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelarCerrarOI.Name = "btnCancelarCerrarOI";
            this.btnCancelarCerrarOI.Size = new System.Drawing.Size(175, 57);
            this.btnCancelarCerrarOI.TabIndex = 9;
            this.btnCancelarCerrarOI.Text = "Cancelar";
            this.btnCancelarCerrarOI.UseVisualStyleBackColor = false;
            this.btnCancelarCerrarOI.Visible = false;
            this.btnCancelarCerrarOI.Click += new System.EventHandler(this.button1_Click);

            // 
            // PantallaCierreOrden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Beige;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.btnCancelarCerrarOI);
            this.Controls.Add(this.lblSeleccionarMotivo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnConfirmarMotivos);
            this.Controls.Add(this.btnConfirmarObservacion);
            this.Controls.Add(this.txtObservacionCierre);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listaOrdenInspeccion);
            this.Controls.Add(this.chkMotivos);
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.CheckedListBox chkMotivos;
        private System.Windows.Forms.Button btnConfirmarMotivos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSeleccionarMotivo;
        private System.Windows.Forms.Button btnCancelarCerrarOI;
    }
}