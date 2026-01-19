
namespace RentalSystemUI.Forms.DashboardSections
{
    partial class DashboardHome
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
            this.title = new AntdUI.Label();
            this.sub = new AntdUI.Label();
            this.btnAdd = new AntdUI.Button();
            this.statsFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.splitLayout = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(24)))), ((int)(((byte)(255)))));
            this.title.Location = new System.Drawing.Point(25, 30);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(184, 45);
            this.title.TabIndex = 0;
            this.title.Text = "Dashboard";
            // 
            // sub
            // 
            this.sub.AutoSize = true;
            this.sub.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.sub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(174)))), ((int)(((byte)(208)))));
            this.sub.Location = new System.Drawing.Point(30, 75);
            this.sub.Name = "sub";
            this.sub.Size = new System.Drawing.Size(262, 19);
            this.sub.TabIndex = 1;
            this.sub.Text = "Overview of your properties and bookings";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(24)))), ((int)(((byte)(255)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(1000, 30);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Radius = 15;
            this.btnAdd.Size = new System.Drawing.Size(160, 45);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "+ Add Property";
            this.btnAdd.Type = AntdUI.TTypeMini.Primary;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // statsFlow
            // 
            this.statsFlow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statsFlow.AutoSize = true;
            this.statsFlow.Location = new System.Drawing.Point(25, 120);
            this.statsFlow.Name = "statsFlow";
            this.statsFlow.Size = new System.Drawing.Size(1200, 150);
            this.statsFlow.TabIndex = 3;
            // 
            // splitLayout
            // 
            this.splitLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitLayout.BackColor = System.Drawing.Color.Transparent;
            this.splitLayout.ColumnCount = 2;
            this.splitLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.splitLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.splitLayout.Location = new System.Drawing.Point(25, 300);
            this.splitLayout.Name = "splitLayout";
            this.splitLayout.RowCount = 1;
            this.splitLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.splitLayout.Size = new System.Drawing.Size(1135, 400); // Approximate default size
            this.splitLayout.TabIndex = 4;
            // 
            // DashboardHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.splitLayout);
            this.Controls.Add(this.statsFlow);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.sub);
            this.Controls.Add(this.title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DashboardHome";
            this.Text = "DashboardHome";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AntdUI.Label title;
        private AntdUI.Label sub;
        private AntdUI.Button btnAdd;
        private System.Windows.Forms.FlowLayoutPanel statsFlow;
        private System.Windows.Forms.TableLayoutPanel splitLayout;
    }
}
