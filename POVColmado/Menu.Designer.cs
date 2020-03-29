namespace POVColmado
{
    partial class Menu
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
            this.Venderbutton = new System.Windows.Forms.Button();
            this.Pulselabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Venderbutton
            // 
            this.Venderbutton.Location = new System.Drawing.Point(289, 174);
            this.Venderbutton.Name = "Venderbutton";
            this.Venderbutton.Size = new System.Drawing.Size(193, 103);
            this.Venderbutton.TabIndex = 0;
            this.Venderbutton.Text = "Vender";
            this.Venderbutton.UseVisualStyleBackColor = true;
            this.Venderbutton.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Pulselabel
            // 
            this.Pulselabel.AutoSize = true;
            this.Pulselabel.Location = new System.Drawing.Point(310, 120);
            this.Pulselabel.Name = "Pulselabel";
            this.Pulselabel.Size = new System.Drawing.Size(132, 13);
            this.Pulselabel.TabIndex = 1;
            this.Pulselabel.Text = "Pulse para iniciar a vender";
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Pulselabel);
            this.Controls.Add(this.Venderbutton);
            this.Name = "Menu";
            this.Text = "Menu Principal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Venderbutton;
        private System.Windows.Forms.Label Pulselabel;
    }
}

