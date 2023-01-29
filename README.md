One way to achieve the outcome you describe is to make a custom UserControl containing a docked `GroupBox` that contains a docked `TableLayoutPanel`.

![user-control]

To adjust the width when the container changes, attach to the `SizeChenges` event of the parent.

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

***
Now test it out with this minimal code in the method that loads the Main Form:

    public partial class MainForm : Form
    {
        int _id = 0;
        public MainForm()
        {
            InitializeComponent();
            flowLayoutPanel.AutoScroll= true;
            for (int i = 0; i < 5; i++)
            {
                var customGroupBox = new CustomGroupBox
                {
                    Text = $"Sensor {++_id}",
                    Width = flowLayoutPanel.Width - SystemInformation.VerticalScrollBarWidth,
                    Padding = new Padding(),
                    Margin = new Padding(),
                };
                flowLayoutPanel.Controls.Add(customGroupBox);
            }
        }
    }
