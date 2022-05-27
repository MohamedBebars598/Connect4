namespace Connect4Server
{
    partial class DataOfRooms
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
            this.components = new System.ComponentModel.Container();
            this.lst_roomsData = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lst_roomsData
            // 
            this.lst_roomsData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst_roomsData.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_roomsData.FormattingEnabled = true;
            this.lst_roomsData.ItemHeight = 36;
            this.lst_roomsData.Location = new System.Drawing.Point(0, 0);
            this.lst_roomsData.Name = "lst_roomsData";
            this.lst_roomsData.Size = new System.Drawing.Size(724, 516);
            this.lst_roomsData.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // DataOfRooms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 516);
            this.Controls.Add(this.lst_roomsData);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(742, 563);
            this.MinimumSize = new System.Drawing.Size(742, 563);
            this.Name = "DataOfRooms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data of rooms";
            this.Load += new System.EventHandler(this.DataOfRooms_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lst_roomsData;
        private System.Windows.Forms.Timer timer1;
    }
}