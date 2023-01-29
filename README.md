One way to achieve the outcome you describe is to make a custom `UserControl` containing a docked `GroupBox` that contains a docked `TableLayoutPanel`.

[![user control designer][1]][1]

To adjust the width when the container changes, attach to the `SizeChanged` event of the parent.

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
**Main Form**

The main form is laid out similar to your image except to substitute a `FlowLayoutPanel` on the right side.

[![main form designer][2]][2]

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

[![resizing][3]][3]

***
**Clear**

An example of clearing the sensors would be to add a button and set a handler in the same Load method:

    buttonClear.Click += (sender, e) => flowLayoutPanel.Controls.Clear();

  [1]: https://i.stack.imgur.com/xpHhk.png
  [2]: https://i.stack.imgur.com/W5ok1.png
  [3]: https://i.stack.imgur.com/PpM18.png