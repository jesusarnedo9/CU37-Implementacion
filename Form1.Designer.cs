namespace ImplementacionCU37
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCerrarOrden = new System.Windows.Forms.Button();
            this.btnCrearOrden = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnVerOrden = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCerrarOrden
            // 
            this.btnCerrarOrden.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnCerrarOrden.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarOrden.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrarOrden.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCerrarOrden.ForeColor = System.Drawing.Color.White;
            this.btnCerrarOrden.Location = new System.Drawing.Point(216, 52);
            this.btnCerrarOrden.Margin = new System.Windows.Forms.Padding(4);
            this.btnCerrarOrden.Name = "btnCerrarOrden";
            this.btnCerrarOrden.Size = new System.Drawing.Size(309, 60);
            this.btnCerrarOrden.TabIndex = 0;
            this.btnCerrarOrden.Text = "Cerrar Orden Inspeccion";
            this.btnCerrarOrden.UseVisualStyleBackColor = false;
            this.btnCerrarOrden.Click += new System.EventHandler(this.btnCerrar);
            // 
            // btnCrearOrden
            // 
            this.btnCrearOrden.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnCrearOrden.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCrearOrden.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCrearOrden.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCrearOrden.ForeColor = System.Drawing.Color.White;
            this.btnCrearOrden.Location = new System.Drawing.Point(216, 223);
            this.btnCrearOrden.Margin = new System.Windows.Forms.Padding(4);
            this.btnCrearOrden.Name = "btnCrearOrden";
            this.btnCrearOrden.Size = new System.Drawing.Size(309, 60);
            this.btnCrearOrden.TabIndex = 1;
            this.btnCrearOrden.Text = "Crear Orden Inspeccion";
            this.btnCrearOrden.UseVisualStyleBackColor = false;
            this.btnCrearOrden.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnModificar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnModificar.ForeColor = System.Drawing.Color.White;
            this.btnModificar.Location = new System.Drawing.Point(216, 137);
            this.btnModificar.Margin = new System.Windows.Forms.Padding(4);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(309, 60);
            this.btnModificar.TabIndex = 2;
            this.btnModificar.Text = "Modificar Orden Inspeccion";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnVerOrden
            // 
            this.btnVerOrden.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnVerOrden.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerOrden.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnVerOrden.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnVerOrden.ForeColor = System.Drawing.Color.White;
            this.btnVerOrden.Location = new System.Drawing.Point(216, 310);
            this.btnVerOrden.Margin = new System.Windows.Forms.Padding(4);
            this.btnVerOrden.Name = "btnVerOrden";
            this.btnVerOrden.Size = new System.Drawing.Size(309, 60);
            this.btnVerOrden.TabIndex = 3;
            this.btnVerOrden.Text = "Ver Orden Inspeccion";
            this.btnVerOrden.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Beige;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnVerOrden);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnCrearOrden);
            this.Controls.Add(this.btnCerrarOrden);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Pantalla Principal";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCerrarOrden;
        private System.Windows.Forms.Button btnCrearOrden;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnVerOrden;
    }
}

