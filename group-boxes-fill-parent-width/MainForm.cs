namespace group_boxes_fill_parent_width
{
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
            buttonClear.Click += (sender, e) => flowLayoutPanel.Controls.Clear();
        }
    }
}