
namespace client
{
    partial class actAsTheGame
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
            ePOSOne.btnProduct.Button_WOC button_WOC1;
            ePOSOne.btnProduct.Button_WOC button_WOC2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(actAsTheGame));
            this.Waiting = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            button_WOC1 = new ePOSOne.btnProduct.Button_WOC();
            button_WOC2 = new ePOSOne.btnProduct.Button_WOC();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_WOC1
            // 
            button_WOC1.BackColor = System.Drawing.Color.Transparent;
            button_WOC1.BorderColor = System.Drawing.Color.DarkGreen;
            button_WOC1.ButtonColor = System.Drawing.Color.YellowGreen;
            button_WOC1.CausesValidation = false;
            button_WOC1.Cursor = System.Windows.Forms.Cursors.Hand;
            button_WOC1.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            button_WOC1.FlatAppearance.BorderSize = 0;
            button_WOC1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            button_WOC1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            button_WOC1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button_WOC1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            button_WOC1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            button_WOC1.Location = new System.Drawing.Point(69, 72);
            button_WOC1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button_WOC1.Name = "button_WOC1";
            button_WOC1.OnHoverBorderColor = System.Drawing.Color.MediumSeaGreen;
            button_WOC1.OnHoverButtonColor = System.Drawing.Color.Gold;
            button_WOC1.OnHoverTextColor = System.Drawing.Color.White;
            button_WOC1.Size = new System.Drawing.Size(165, 68);
            button_WOC1.TabIndex = 6;
            button_WOC1.Text = "Accept";
            button_WOC1.TextColor = System.Drawing.Color.White;
            button_WOC1.UseVisualStyleBackColor = false;
            button_WOC1.Click += new System.EventHandler(this.button_WOC1_Click);
            // 
            // button_WOC2
            // 
            button_WOC2.BackColor = System.Drawing.Color.Transparent;
            button_WOC2.BorderColor = System.Drawing.Color.Maroon;
            button_WOC2.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            button_WOC2.CausesValidation = false;
            button_WOC2.Cursor = System.Windows.Forms.Cursors.Hand;
            button_WOC2.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            button_WOC2.FlatAppearance.BorderSize = 0;
            button_WOC2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            button_WOC2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            button_WOC2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button_WOC2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            button_WOC2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            button_WOC2.Location = new System.Drawing.Point(69, 194);
            button_WOC2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button_WOC2.Name = "button_WOC2";
            button_WOC2.OnHoverBorderColor = System.Drawing.Color.Maroon;
            button_WOC2.OnHoverButtonColor = System.Drawing.Color.Gold;
            button_WOC2.OnHoverTextColor = System.Drawing.Color.Maroon;
            button_WOC2.Size = new System.Drawing.Size(165, 68);
            button_WOC2.TabIndex = 7;
            button_WOC2.Text = "Refuse";
            button_WOC2.TextColor = System.Drawing.Color.White;
            button_WOC2.UseVisualStyleBackColor = false;
            button_WOC2.Click += new System.EventHandler(this.button_WOC2_Click);
            // 
            // Waiting
            // 
            this.Waiting.AutoEllipsis = true;
            this.Waiting.AutoSize = true;
            this.Waiting.BackColor = System.Drawing.Color.Transparent;
            this.Waiting.Font = new System.Drawing.Font("Segoe Print", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Waiting.Location = new System.Drawing.Point(313, 9);
            this.Waiting.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Waiting.Name = "Waiting";
            this.Waiting.Size = new System.Drawing.Size(685, 94);
            this.Waiting.TabIndex = 1;
            this.Waiting.Text = "Waiting player to join ...";
            this.Waiting.TextChanged += new System.EventHandler(this.Waiting_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(button_WOC2);
            this.panel1.Controls.Add(button_WOC1);
            this.panel1.Location = new System.Drawing.Point(523, 297);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(297, 331);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // actAsTheGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1365, 803);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Waiting);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "actAsTheGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "actAsTheGame";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ActAsTheGame_FormClosing);
            this.Load += new System.EventHandler(this.actAsTheGame_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Waiting;
        private System.Windows.Forms.Panel panel1;
    }
}