namespace FractalEngine
{
    partial class InputControl
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
            this.submit_button = new System.Windows.Forms.Button();
            this.inputBoxText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // submit_button
            // 
            this.submit_button.Location = new System.Drawing.Point(12, 56);
            this.submit_button.Name = "submit_button";
            this.submit_button.Size = new System.Drawing.Size(118, 23);
            this.submit_button.TabIndex = 0;
            this.submit_button.Text = "submit";
            this.submit_button.UseVisualStyleBackColor = true;
            this.submit_button.Click += new System.EventHandler(this.submit_button_Click);
            // 
            // inputBoxText
            // 
            this.inputBoxText.Location = new System.Drawing.Point(12, 12);
            this.inputBoxText.Name = "inputBoxText";
            this.inputBoxText.Size = new System.Drawing.Size(120, 20);
            this.inputBoxText.TabIndex = 1;
            // 
            // InputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(144, 91);
            this.Controls.Add(this.inputBoxText);
            this.Controls.Add(this.submit_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "InputControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pen Thickness";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button submit_button;
        private System.Windows.Forms.TextBox inputBoxText;
    }
}