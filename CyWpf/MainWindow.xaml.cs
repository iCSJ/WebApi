using CyApiClient;
using CyModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using Utils;

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
            CyHttpClient client = new CyHttpClient("api/option");
            client.Get().TryParseResult(out content);
            MessageBox.Show(content.ToString());

            var x = new { where = new { Key = "m", ShopId = 1 } };
            client.Get(JsonConvert.SerializeObject(x)).TryParseResult(out content);
            MessageBox.Show(content.ToString());

            client.Get(pageIndex: 2, pageSize: 8, asc: false).TryParseResult(out content);
            MessageBox.Show(content.ToString());
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            object content;
            Option option = new Option();
            option.Key = "key1";
            option.Value = "Value1";
            Option option1 = new Option();
            option1.Key = "key2";
            option1.Value = "Value2";
            var json = new { model = new List<Option>() { option, option1 } };

            new CyHttpClient("api/option").Post(JsonConvert.SerializeObject(json)).TryParseResult(out content);
            string s = content.ToString();
            MessageBox.Show(s);



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
