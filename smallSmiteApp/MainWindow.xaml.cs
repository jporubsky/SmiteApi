using System.Windows;
using smallSmiteApp.APIcalls;

namespace smallSmiteApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SmiteAPI sapi;
        public MainWindow()
        {
            InitializeComponent();
             sapi = new SmiteAPI();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.textBlock.Text = sapi.Excecute(textBox.Text);
            
        }
    }
}
