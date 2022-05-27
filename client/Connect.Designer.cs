
namespace client
{
    partial class Connect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Connect));
            this.txt_port = new System.Windows.Forms.TextBox();
            this.txt_ipaddress = new System.Windows.Forms.TextBox();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            button_WOC1 = new ePOSOne.btnProduct.Button_WOC();
            this.SuspendLayout();
            // 
            // button_WOC1
            // 
            button_WOC1.BackColor = System.Drawing.Color.Transparent;
            button_WOC1.BorderColor = System.Drawing.Color.Maroon;
            button_WOC1.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            button_WOC1.CausesValidation = false;
            button_WOC1.Cursor = System.Windows.Forms.Cursors.Hand;
            button_WOC1.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            button_WOC1.FlatAppearance.BorderSize = 0;
            button_WOC1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            button_WOC1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            button_WOC1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button_WOC1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            button_WOC1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            button_WOC1.Location = new System.Drawing.Point(350, 445);
            button_WOC1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button_WOC1.Name = "button_WOC1";
            button_WOC1.OnHoverBorderColor = System.Drawing.Color.Maroon;
            button_WOC1.OnHoverButtonColor = System.Drawing.Color.Gold;
            button_WOC1.OnHoverTextColor = System.Drawing.Color.Maroon;
            button_WOC1.Size = new System.Drawing.Size(246, 106);
            button_WOC1.TabIndex = 5;
            button_WOC1.Text = "Connect";
            button_WOC1.TextColor = System.Drawing.Color.White;
            button_WOC1.UseVisualStyleBackColor = false;
            button_WOC1.Click += new System.EventHandler(this.button_WOC1_Click);
            // 
            // txt_port
            // 
            this.txt_port.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Italic);
            this.txt_port.Location = new System.Drawing.Point(333, 109);
            this.txt_port.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_port.Name = "txt_port";
            this.txt_port.Size = new System.Drawing.Size(296, 57);
            this.txt_port.TabIndex = 1;
            this.txt_port.Text = "1000";
            // 
            // txt_ipaddress
            // 
            this.txt_ipaddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Italic);
            this.txt_ipaddress.Location = new System.Drawing.Point(333, 209);
            this.txt_ipaddress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_ipaddress.Name = "txt_ipaddress";
            this.txt_ipaddress.Size = new System.Drawing.Size(296, 57);
            this.txt_ipaddress.TabIndex = 1;
            this.txt_ipaddress.Text = "172.16.10.213";
            // 
            // txt_username
            // 
            this.txt_username.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Italic);
            this.txt_username.Location = new System.Drawing.Point(333, 308);
            this.txt_username.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(296, 57);
            this.txt_username.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(62, 211);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 55);
            this.label1.TabIndex = 3;
            this.label1.Text = "IPaddress";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(196, 111);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 55);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(57, 308);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(255, 55);
            this.label3.TabIndex = 4;
            this.label3.Text = "Username";
            // 
            // Connect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1506, 634);
            this.Controls.Add(button_WOC1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_username);
            this.Controls.Add(this.txt_ipaddress);
            this.Controls.Add(this.txt_port);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Connect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txt_port;
        private System.Windows.Forms.TextBox txt_ipaddress;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

