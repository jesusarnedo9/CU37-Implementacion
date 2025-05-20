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
            this.SuspendLayout();
            // 
            // listaOrdenInspeccion
            // 
            this.listaOrdenInspeccion.FormattingEnabled = true;
            this.listaOrdenInspeccion.Location = new System.Drawing.Point(314, 122);
            this.listaOrdenInspeccion.Name = "listaOrdenInspeccion";
            this.listaOrdenInspeccion.Size = new System.Drawing.Size(121, 21);
            this.listaOrdenInspeccion.TabIndex = 0;
            this.listaOrdenInspeccion.SelectedIndexChanged += new System.EventHandler(this.listaOrdenInspeccion_SelectedIndexChanged);
            // 
            // PantallaCierreOrden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listaOrdenInspeccion);
            this.Name = "PantallaCierreOrden";
            this.Text = "CerrarOrdenInscripcion";
            this.Load += new System.EventHandler(this.PantallaCierreOrden_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox listaOrdenInspeccion;
    }
}