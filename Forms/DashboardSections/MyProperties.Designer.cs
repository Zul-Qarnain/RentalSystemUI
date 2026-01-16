
namespace RentalSystemUI.Forms.DashboardSections
{
    partial class MyProperties
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
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.header = new System.Windows.Forms.Panel();
            this.toolbar = new System.Windows.Forms.FlowLayoutPanel();
            this.txtSearch = new AntdUI.Input();
            this.spacer1 = new AntdUI.Label();
            this.selectFilter = new AntdUI.Select();
            this.spacer2 = new AntdUI.Label();
            this.btnAdd = new AntdUI.Button();
            this.lblTitle = new AntdUI.Label();
            this._propertiesFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.mainLayout.SuspendLayout();
            this.header.SuspendLayout();
            this.toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Controls.Add(this.header, 0, 0);
            this.mainLayout.Controls.Add(this._propertiesFlow, 0, 1);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.mainLayout.RowCount = 2;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Size = new System.Drawing.Size(1200, 800);
            this.mainLayout.TabIndex = 0;
            // 
            // header
            // 
            this.header.BackColor = System.Drawing.Color.Transparent;
            this.header.Controls.Add(this.toolbar);
            this.header.Controls.Add(this.lblTitle);
            this.header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.header.Location = new System.Drawing.Point(23, 3);
            this.header.Name = "header";
            this.header.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.header.Size = new System.Drawing.Size(1154, 94);
            this.header.TabIndex = 0;
            // 
            // toolbar
            // 
            this.toolbar.AutoSize = true;
            this.toolbar.Controls.Add(this.txtSearch);
            this.toolbar.Controls.Add(this.spacer1);
            this.toolbar.Controls.Add(this.selectFilter);
            this.toolbar.Controls.Add(this.spacer2);
            this.toolbar.Controls.Add(this.btnAdd);
            this.toolbar.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolbar.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.toolbar.Location = new System.Drawing.Point(634, 30);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(520, 64);
            this.toolbar.TabIndex = 1;
            this.toolbar.WrapContents = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(3, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "Search properties...";
            this.txtSearch.PrefixSvg = "search";
            this.txtSearch.Radius = 10;
            this.txtSearch.Size = new System.Drawing.Size(250, 45);
            this.txtSearch.TabIndex = 0;
            // 
            // spacer1
            // 
            this.spacer1.Location = new System.Drawing.Point(259, 3);
            this.spacer1.Name = "spacer1";
            this.spacer1.Size = new System.Drawing.Size(10, 23);
            this.spacer1.TabIndex = 1;
            // 
            // selectFilter
            // 
            this.selectFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.selectFilter.List = true;
            this.selectFilter.Location = new System.Drawing.Point(275, 3);
            this.selectFilter.Name = "selectFilter";
            this.selectFilter.PlaceholderText = "Status";
            this.selectFilter.Radius = 10;
            this.selectFilter.Size = new System.Drawing.Size(140, 45);
            this.selectFilter.TabIndex = 2;
            // 
            // spacer2
            // 
            this.spacer2.Location = new System.Drawing.Point(421, 3);
            this.spacer2.Name = "spacer2";
            this.spacer2.Size = new System.Drawing.Size(10, 23);
            this.spacer2.TabIndex = 3;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(24)))), ((int)(((byte)(255)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.IconSvg = "plus";
            this.btnAdd.Location = new System.Drawing.Point(437, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Radius = 15;
            this.btnAdd.Size = new System.Drawing.Size(160, 45);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "New Property";
            this.btnAdd.Type = AntdUI.TTypeMini.Primary;
            this.btnAdd.Click += new System.EventHandler(this.OnAddPropertyClick);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(24)))), ((int)(((byte)(255)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(232, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "My Properties";
            // 
            // _propertiesFlow
            // 
            this._propertiesFlow.AutoScroll = true;
            this._propertiesFlow.BackColor = System.Drawing.Color.Transparent;
            this._propertiesFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this._propertiesFlow.Location = new System.Drawing.Point(23, 103);
            this._propertiesFlow.Name = "_propertiesFlow";
            this._propertiesFlow.Padding = new System.Windows.Forms.Padding(10, 20, 0, 20);
            this._propertiesFlow.Size = new System.Drawing.Size(1154, 694);
            this._propertiesFlow.TabIndex = 1;
            // 
            // MyProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.mainLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MyProperties";
            this.Text = "MyProperties";
            this.mainLayout.ResumeLayout(false);
            this.header.ResumeLayout(false);
            this.header.PerformLayout();
            this.toolbar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.Panel header;
        private System.Windows.Forms.FlowLayoutPanel toolbar;
        private AntdUI.Input txtSearch;
        private AntdUI.Label spacer1;
        private AntdUI.Select selectFilter;
        private AntdUI.Label spacer2;
        private AntdUI.Button btnAdd;
        private AntdUI.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel _propertiesFlow;
    }
}
