
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
            mainLayout = new TableLayoutPanel();
            header = new Panel();
            toolbar = new FlowLayoutPanel();
            txtSearch = new AntdUI.Input();
            spacer1 = new AntdUI.Label();
            selectFilter = new AntdUI.Select();
            spacer2 = new AntdUI.Label();
            btnAdd = new AntdUI.Button();
            lblTitle = new AntdUI.Label();
            _propertiesFlow = new FlowLayoutPanel();
            mainLayout.SuspendLayout();
            header.SuspendLayout();
            toolbar.SuspendLayout();
            SuspendLayout();
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.Controls.Add(header, 0, 0);
            mainLayout.Controls.Add(_propertiesFlow, 0, 1);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 0);
            mainLayout.Margin = new Padding(4, 5, 4, 5);
            mainLayout.Name = "mainLayout";
            mainLayout.Padding = new Padding(29, 0, 29, 0);
            mainLayout.RowCount = 2;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 167F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Size = new Size(1714, 1226);
            mainLayout.TabIndex = 0;
            // 
            // header
            // 
            header.BackColor = Color.Transparent;
            header.Controls.Add(toolbar);
            header.Controls.Add(lblTitle);
            header.Dock = DockStyle.Fill;
            header.Location = new Point(33, 5);
            header.Margin = new Padding(4, 5, 4, 5);
            header.Name = "header";
            header.Padding = new Padding(0, 50, 0, 0);
            header.Size = new Size(1648, 157);
            header.TabIndex = 0;
            // 
            // toolbar
            // 
            toolbar.AutoSize = true;
            toolbar.Controls.Add(txtSearch);
            toolbar.Controls.Add(spacer1);
            toolbar.Controls.Add(selectFilter);
            toolbar.Controls.Add(spacer2);
            toolbar.Controls.Add(btnAdd);
            toolbar.Dock = DockStyle.Right;
            toolbar.Location = new Point(794, 50);
            toolbar.Margin = new Padding(4, 5, 4, 5);
            toolbar.Name = "toolbar";
            toolbar.Size = new Size(854, 107);
            toolbar.TabIndex = 1;
            toolbar.WrapContents = false;
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.Location = new Point(4, 5);
            txtSearch.Margin = new Padding(4, 5, 4, 5);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search properties...";
            txtSearch.PrefixSvg = "search";
            txtSearch.Radius = 10;
            txtSearch.Size = new Size(357, 75);
            txtSearch.TabIndex = 0;
            // 
            // spacer1
            // 
            spacer1.Location = new Point(369, 5);
            spacer1.Margin = new Padding(4, 5, 4, 5);
            spacer1.Name = "spacer1";
            spacer1.Size = new Size(14, 38);
            spacer1.TabIndex = 1;
            // 
            // selectFilter
            // 
            selectFilter.Font = new Font("Segoe UI", 10F);
            selectFilter.List = true;
            selectFilter.Location = new Point(391, 5);
            selectFilter.Margin = new Padding(4, 5, 4, 5);
            selectFilter.Name = "selectFilter";
            selectFilter.PlaceholderText = "Status";
            selectFilter.Radius = 10;
            selectFilter.Size = new Size(200, 75);
            selectFilter.TabIndex = 2;
            // 
            // spacer2
            // 
            spacer2.Location = new Point(599, 5);
            spacer2.Margin = new Padding(4, 5, 4, 5);
            spacer2.Name = "spacer2";
            spacer2.Size = new Size(14, 38);
            spacer2.TabIndex = 3;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(67, 24, 255);
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.IconSvg = "plus";
            btnAdd.Location = new Point(621, 5);
            btnAdd.Margin = new Padding(4, 5, 4, 5);
            btnAdd.Name = "btnAdd";
            btnAdd.Radius = 15;
            btnAdd.Size = new Size(229, 75);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "New Property";
            btnAdd.Type = AntdUI.TTypeMini.Primary;
            btnAdd.Click += OnAddPropertyClick;
            // 
            // lblTitle
            // 
            lblTitle.AutoSizeMode = AntdUI.TAutoSize.Auto;
            lblTitle.Dock = DockStyle.Left;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(67, 24, 255);
            lblTitle.Location = new Point(0, 50);
            lblTitle.Margin = new Padding(4, 5, 4, 5);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(318, 64);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "My Properties";
            // 
            // _propertiesFlow
            // 
            _propertiesFlow.AutoScroll = true;
            _propertiesFlow.BackColor = Color.Transparent;
            _propertiesFlow.Dock = DockStyle.Fill;
            _propertiesFlow.Location = new Point(33, 172);
            _propertiesFlow.Margin = new Padding(4, 5, 4, 5);
            _propertiesFlow.Name = "_propertiesFlow";
            _propertiesFlow.Padding = new Padding(14, 33, 0, 33);
            _propertiesFlow.Size = new Size(1648, 1049);
            _propertiesFlow.TabIndex = 1;
            // 
            // MyProperties
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 247, 254);
            ClientSize = new Size(1714, 1226);
            Controls.Add(mainLayout);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "MyProperties";
            Text = "MyProperties";
            mainLayout.ResumeLayout(false);
            header.ResumeLayout(false);
            header.PerformLayout();
            toolbar.ResumeLayout(false);
            ResumeLayout(false);

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
