namespace Connect_4_Project
{
    partial class GameBoard
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
            ePOSOne.btnProduct.Button_WOC button_WOC2;
            ePOSOne.btnProduct.Button_WOC button_WOC1;
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            button_WOC2 = new ePOSOne.btnProduct.Button_WOC();
            button_WOC1 = new ePOSOne.btnProduct.Button_WOC();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(button_WOC2);
            this.panel1.Controls.Add(button_WOC1);
            this.panel1.Location = new System.Drawing.Point(248, 112);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(472, 342);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label1.Location = new System.Drawing.Point(14, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(454, 32);
            this.label1.TabIndex = 8;
            this.label1.Text = "Congratulation mohamed you won ";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkGray;
            this.button1.Location = new System.Drawing.Point(830, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 45);
            this.button1.TabIndex = 4;
            this.button1.Text = "Leave";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click_1);
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
            button_WOC2.Location = new System.Drawing.Point(92, 243);
            button_WOC2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button_WOC2.Name = "button_WOC2";
            button_WOC2.OnHoverBorderColor = System.Drawing.Color.Maroon;
            button_WOC2.OnHoverButtonColor = System.Drawing.Color.Gold;
            button_WOC2.OnHoverTextColor = System.Drawing.Color.Maroon;
            button_WOC2.Size = new System.Drawing.Size(287, 68);
            button_WOC2.TabIndex = 7;
            button_WOC2.Text = "Leave";
            button_WOC2.TextColor = System.Drawing.Color.White;
            button_WOC2.UseVisualStyleBackColor = false;
            button_WOC2.Click += new System.EventHandler(this.Button_WOC2_Click);
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
            button_WOC1.Location = new System.Drawing.Point(92, 165);
            button_WOC1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button_WOC1.Name = "button_WOC1";
            button_WOC1.OnHoverBorderColor = System.Drawing.Color.MediumSeaGreen;
            button_WOC1.OnHoverButtonColor = System.Drawing.Color.Gold;
            button_WOC1.OnHoverTextColor = System.Drawing.Color.White;
            button_WOC1.Size = new System.Drawing.Size(287, 68);
            button_WOC1.TabIndex = 6;
            button_WOC1.Text = "Play again";
            button_WOC1.TextColor = System.Drawing.Color.White;
            button_WOC1.UseVisualStyleBackColor = false;
            button_WOC1.Click += new System.EventHandler(this.Button_WOC1_Click);
            // 
            // GameBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 520);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1014, 576);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1014, 576);
            this.Name = "GameBoard";
            this.Text = "GameBoard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameBoard_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}

