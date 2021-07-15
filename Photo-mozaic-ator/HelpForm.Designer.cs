
namespace Photo_mozaic_ator
{
    partial class HelpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm));
            this.help1 = new System.Windows.Forms.Label();
            this.help2 = new System.Windows.Forms.Label();
            this.help3 = new System.Windows.Forms.Label();
            this.help4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // help1
            // 
            this.help1.AutoSize = true;
            this.help1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.help1.Location = new System.Drawing.Point(13, 13);
            this.help1.Name = "help1";
            this.help1.Size = new System.Drawing.Size(187, 25);
            this.help1.TabIndex = 0;
            this.help1.Text = "Creating new tileset";
            // 
            // help2
            // 
            this.help2.AutoSize = true;
            this.help2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.help2.Location = new System.Drawing.Point(12, 200);
            this.help2.Name = "help2";
            this.help2.Size = new System.Drawing.Size(179, 25);
            this.help2.TabIndex = 1;
            this.help2.Text = "Generating mozaic";
            // 
            // help3
            // 
            this.help3.AutoSize = true;
            this.help3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.help3.Location = new System.Drawing.Point(13, 38);
            this.help3.Name = "help3";
            this.help3.Size = new System.Drawing.Size(322, 135);
            this.help3.TabIndex = 2;
            this.help3.Text = resources.GetString("help3.Text");
            // 
            // help4
            // 
            this.help4.AutoSize = true;
            this.help4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.help4.Location = new System.Drawing.Point(13, 225);
            this.help4.Name = "help4";
            this.help4.Size = new System.Drawing.Size(328, 150);
            this.help4.TabIndex = 3;
            this.help4.Text = resources.GetString("help4.Text");
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 402);
            this.Controls.Add(this.help4);
            this.Controls.Add(this.help3);
            this.Controls.Add(this.help2);
            this.Controls.Add(this.help1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HelpForm";
            this.Text = "HelpForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label help1;
        private System.Windows.Forms.Label help2;
        private System.Windows.Forms.Label help3;
        private System.Windows.Forms.Label help4;
    }
}