
namespace client
{
    partial class CoinColors
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
            this.red = new System.Windows.Forms.Panel();
            this.yellow = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnStart = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // red
            // 
            this.red.BackColor = System.Drawing.Color.Red;
            this.red.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.red.Cursor = System.Windows.Forms.Cursors.Hand;
            this.red.Location = new System.Drawing.Point(0, 0);
            this.red.Name = "red";
            this.red.Size = new System.Drawing.Size(106, 83);
            this.red.TabIndex = 0;
            this.red.Click += new System.EventHandler(this.red_Click);
            // 
            // yellow
            // 
            this.yellow.BackColor = System.Drawing.Color.Yellow;
            this.yellow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.yellow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.yellow.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.yellow.Location = new System.Drawing.Point(112, 0);
            this.yellow.Name = "yellow";
            this.yellow.Size = new System.Drawing.Size(114, 83);
            this.yellow.TabIndex = 1;
            this.yellow.Click += new System.EventHandler(this.yellow_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 35);
            this.label1.TabIndex = 2;
            this.label1.Text = "Choose your color :";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.red);
            this.panel3.Controls.Add(this.yellow);
            this.panel3.Location = new System.Drawing.Point(47, 58);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(226, 86);
            this.panel3.TabIndex = 3;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.Font = new System.Drawing.Font("Segoe Print", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(111, 170);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 56);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // CoinColors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(318, 264);
            this.ControlBox = false;
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(334, 280);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(334, 280);
            this.Name = "CoinColors";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form3_Load);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel red;
        private System.Windows.Forms.Panel yellow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnStart;
    }
}