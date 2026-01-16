using System.Drawing;
using System.Windows.Forms;

namespace LandlordPortal.Controls
{
    public partial class PropertiesControl : UserControl
    {
        public PropertiesControl()
        {
            this.BackColor = Styles.Back;
            Label lbl = new Label { Text = "Properties Page", Font = Styles.Header, ForeColor = Styles.TextMain, Location = new Point(50, 50), AutoSize = true };
            this.Controls.Add(lbl);
        }
    }
}