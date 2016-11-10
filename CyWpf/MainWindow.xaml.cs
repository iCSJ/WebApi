using CyApiClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CyWpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            object content;
            new CyHttpClient("api/option").Get().TryParseResult(out content);
            string s = content.ToString();
            MessageBox.Show(s);

            //JsonConvert.DefaultSettings = () => { return new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss" }; };
            //var p = new { Id = 1, Name = "Lili", IsValid = true, Visible = ScrollBarVisibility.Visible, Date = DateTime.Now };
            //var x = JsonConvert.SerializeObject(p);
            //string str = x.ToString();
            //var y = JsonConvert.DeserializeObject<dynamic>(x);
            //var ystr = y.ToString();
            //if (y == p)
            //{
            //    MessageBox.Show("equar");
            //}
        }
    }
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsValid { get; set; }
        public ScrollBarVisibility Visible { get; set; }
        public DateTime Date { get; set; }
    }

}
