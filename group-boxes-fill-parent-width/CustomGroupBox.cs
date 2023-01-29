using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace group_boxes_fill_parent_width
{
    public partial class CustomGroupBox : UserControl
    {
        public CustomGroupBox() => InitializeComponent();
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if((Parent != null) &&!DesignMode)
            {
                Parent.SizeChanged += onParentSizeChanged;
            }
        }
        private void onParentSizeChanged(object? sender, EventArgs e)
        {
            if(sender is FlowLayoutPanel flowLayoutPanel)
            {
                Debug.WriteLine($"{flowLayoutPanel.Width}");
                Width = 
                    flowLayoutPanel.Width - 
                    SystemInformation.VerticalScrollBarWidth;
            }
        }
        public new string Text
        {
            get=>groupBox.Text;
            set=>groupBox.Text = value;
        }
    }
}
