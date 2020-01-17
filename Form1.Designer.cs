namespace FractalEngine
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.background_picture = new System.Windows.Forms.PictureBox();
            this.start_button = new System.Windows.Forms.Button();
            this.about_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.background_picture)).BeginInit();
            this.SuspendLayout();
            // 
            // background_picture
            // 
            this.background_picture.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.background_picture.Image = ((System.Drawing.Image)(resources.GetObject("background_picture.Image")));
            this.background_picture.Location = new System.Drawing.Point(0, -3);
            this.background_picture.Name = "background_picture";
            this.background_picture.Size = new System.Drawing.Size(961, 507);
            this.background_picture.TabIndex = 0;
            this.background_picture.TabStop = false;
            // 
            // start_button
            // 
            this.start_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.start_button.BackColor = System.Drawing.Color.LightSlateGray;
            this.start_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.start_button.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.start_button.Location = new System.Drawing.Point(308, 180);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(348, 27);
            this.start_button.TabIndex = 1;
            this.start_button.Text = "START ENGINE";
            this.start_button.UseVisualStyleBackColor = false;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // about_button
            // 
            this.about_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.about_button.BackColor = System.Drawing.Color.SlateGray;
            this.about_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.about_button.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.about_button.Location = new System.Drawing.Point(308, 252);
            this.about_button.Name = "about_button";
            this.about_button.Size = new System.Drawing.Size(348, 27);
            this.about_button.TabIndex = 2;
            this.about_button.Text = "ABOUT";
            this.about_button.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 502);
            this.Controls.Add(this.about_button);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.background_picture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.background_picture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox background_picture;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.Button about_button;
    }
}

