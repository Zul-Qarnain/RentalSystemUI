
namespace RentalSystemUI.Forms.DashboardSections
{
    partial class ReviewList
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
            this.lblTitle = new AntdUI.Label();
            this._flow = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(24)))), ((int)(((byte)(255)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(20, 25, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(1200, 80);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Tenant Reviews";
            // 
            // _flow
            // 
            this._flow.AutoScroll = true;
            this._flow.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this._flow.Location = new System.Drawing.Point(0, 80);
            this._flow.Name = "_flow";
            this._flow.Padding = new System.Windows.Forms.Padding(20);
            this._flow.Size = new System.Drawing.Size(1200, 720);
            this._flow.TabIndex = 1;
            this._flow.WrapContents = false;
            // 
            // ReviewList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this._flow);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReviewList";
            this.Text = "ReviewList";
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel _flow;
    }
}
