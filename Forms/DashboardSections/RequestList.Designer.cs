
namespace RentalSystemUI.Forms.DashboardSections
{
    partial class RequestList
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.header = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTitle = new AntdUI.Label();
            this.btnAll = new AntdUI.Button();
            this.btnPending = new AntdUI.Button();
            this.btnAccepted = new AntdUI.Button();
            this._flow = new System.Windows.Forms.FlowLayoutPanel();
            this.header.SuspendLayout();
            this.SuspendLayout();
            // 
            // header
            // 
            this.header.Controls.Add(this.lblTitle);
            this.header.Controls.Add(this.btnAll);
            this.header.Controls.Add(this.btnPending);
            this.header.Controls.Add(this.btnAccepted);
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Padding = new System.Windows.Forms.Padding(20, 25, 0, 0);
            this.header.Size = new System.Drawing.Size(1200, 80);
            this.header.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(24)))), ((int)(((byte)(255)))));
            this.lblTitle.Location = new System.Drawing.Point(23, 25);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0, 0, 40, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(242, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Tenant Requests";
            // 
            // btnAll
            // 
            this.btnAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(24)))), ((int)(((byte)(255)))));
            this.btnAll.ForeColor = System.Drawing.Color.White;
            this.btnAll.Location = new System.Drawing.Point(308, 28);
            this.btnAll.Name = "btnAll";
            this.btnAll.Radius = 15;
            this.btnAll.Size = new System.Drawing.Size(120, 36);
            this.btnAll.TabIndex = 1;
            this.btnAll.Text = "All Requests";
            this.btnAll.Type = AntdUI.TTypeMini.Primary;
            // 
            // btnPending
            // 
            this.btnPending.BackColor = System.Drawing.Color.White;
            this.btnPending.BorderWidth = 0F;
            this.btnPending.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(174)))), ((int)(((byte)(208)))));
            this.btnPending.Location = new System.Drawing.Point(434, 28);
            this.btnPending.Name = "btnPending";
            this.btnPending.Radius = 15;
            this.btnPending.Size = new System.Drawing.Size(100, 36);
            this.btnPending.TabIndex = 2;
            this.btnPending.Text = "Pending";
            this.btnPending.Type = AntdUI.TTypeMini.Default;
            // 
            // btnAccepted
            // 
            this.btnAccepted.BackColor = System.Drawing.Color.White;
            this.btnAccepted.BorderWidth = 0F;
            this.btnAccepted.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(174)))), ((int)(((byte)(208)))));
            this.btnAccepted.Location = new System.Drawing.Point(540, 28);
            this.btnAccepted.Name = "btnAccepted";
            this.btnAccepted.Radius = 15;
            this.btnAccepted.Size = new System.Drawing.Size(100, 36);
            this.btnAccepted.TabIndex = 3;
            this.btnAccepted.Text = "Accepted";
            this.btnAccepted.Type = AntdUI.TTypeMini.Default;
            // 
            // _flow
            // 
            this._flow.AutoScroll = true;
            this._flow.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this._flow.Location = new System.Drawing.Point(0, 80);
            this._flow.Name = "_flow";
            this._flow.Padding = new System.Windows.Forms.Padding(20, 10, 20, 20);
            this._flow.Size = new System.Drawing.Size(1200, 720);
            this._flow.TabIndex = 1;
            this._flow.WrapContents = false;
            // 
            // RequestList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this._flow);
            this.Controls.Add(this.header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RequestList";
            this.Text = "RequestList";
            this.header.ResumeLayout(false);
            this.header.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel header;
        private AntdUI.Label lblTitle;
        private AntdUI.Button btnAll;
        private AntdUI.Button btnPending;
        private AntdUI.Button btnAccepted;
        private System.Windows.Forms.FlowLayoutPanel _flow;
    }
}
