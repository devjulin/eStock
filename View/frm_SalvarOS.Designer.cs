namespace prj_eStock.View
{
    partial class frm_SalvarOS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_SalvarOS));
            this.dgv_ListarDados = new System.Windows.Forms.DataGridView();
            this.btn_Listar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ListarDados)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_ListarDados
            // 
            this.dgv_ListarDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ListarDados.Location = new System.Drawing.Point(454, 262);
            this.dgv_ListarDados.Name = "dgv_ListarDados";
            this.dgv_ListarDados.RowHeadersWidth = 51;
            this.dgv_ListarDados.RowTemplate.Height = 24;
            this.dgv_ListarDados.Size = new System.Drawing.Size(736, 481);
            this.dgv_ListarDados.TabIndex = 0;
            // 
            // btn_Listar
            // 
            this.btn_Listar.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_Listar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Listar.BackgroundImage")));
            this.btn_Listar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Listar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Listar.FlatAppearance.BorderSize = 0;
            this.btn_Listar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_Listar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_Listar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Listar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Listar.ForeColor = System.Drawing.Color.White;
            this.btn_Listar.Location = new System.Drawing.Point(634, 762);
            this.btn_Listar.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Listar.Name = "btn_Listar";
            this.btn_Listar.Size = new System.Drawing.Size(146, 48);
            this.btn_Listar.TabIndex = 184;
            this.btn_Listar.Text = "LISTAR";
            this.btn_Listar.UseVisualStyleBackColor = false;
            this.btn_Listar.Click += new System.EventHandler(this.btn_Listar_Click);
            // 
            // frm_SalvarOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1608, 1034);
            this.Controls.Add(this.btn_Listar);
            this.Controls.Add(this.dgv_ListarDados);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_SalvarOS";
            this.Text = "frm_SalvarOS";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ListarDados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_ListarDados;
        private System.Windows.Forms.Button btn_Listar;
    }
}