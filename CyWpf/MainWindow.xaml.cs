using CyApiClient;
using CyModel;
using CyWpf.Services;
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
            List<Option> list = new List<Option>();
            switch ((e.OriginalSource as Button).Tag.ToString())
            {
                case "0":
                    list = new UoptionService().GetAll();
                    gr.ItemsSource = list;
                    break;
                case "1":
                    var x = new { where = new { Value = "Value2" } };
                    list = new UoptionService().GetWhere(x.JsonSerial());
                    gr.ItemsSource = list;
                    break;
                case "2":
                    int total;
                    list = new UoptionService().GetPage("", out total, 2, 5);
                    gr.ItemsSource = list;
                    break;
                case "3":
                    var item = new UoptionService().Get("30");
                    if (null != item)
                        list.Add(item);
                    gr.ItemsSource = list;
                    break;
                case "4":
                    Option option = new Option() { Key = "k9", Value = "v9" };
                    int rtn = new UoptionService().Post(option.JsonSerial());
                    MessageBox.Show(rtn.ToString());
                    break;
                case "5":
                    Option option1 = new Option() {Id=30, Key = "k9", Value = "v9" };
                    int rtn1 = new UoptionService().Put(option1.JsonSerial());
                    MessageBox.Show(rtn1.ToString());
                    break;
                case "6":
                    var option2 = new { Id =30, Key = "k9", Value = "v9" };
                    int rtn2 = new UoptionService().Modify(option2.JsonSerial());
                    MessageBox.Show(rtn2.ToString());
                    break;
                case "7":
                    MessageBox.Show(new UoptionService().Delete("33").ToString());
                    break;
                case "8":
                    MessageBox.Show(new UoptionService().LogicDelete("33").ToString());
                    break;
                default:
                    break;
            }


            //client.Seg= "api/rolefunc";
            //client.LogicDelete("[2,1]").TryParseResult(out content);
            //MessageBox.Show(content.ToString());

            //client.Get().TryParseResult(out content);
            //MessageBox.Show(content.ToString());

            //var x = new { where = new { Key = "m", ShopId = 1 } };
            //client.Get(JsonConvert.SerializeObject(x)).TryParseResult(out content);
            //MessageBox.Show(content.ToString());

            //client.Get(pageIndex: 2, pageSize: 8, asc: false).TryParseResult(out content);
            //MessageBox.Show(content.ToString());

            //client.Seg = "api/rolefunc";
            //client.Get("[2,1]").TryParseResult(out content);
            //MessageBox.Show(content.ToString());

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            gr.ItemsSource = null;
            //object content;
            //Option option = new Option();
            //option.Key = "key1";
            //option.Value = "Value1";
            //Option option1 = new Option();
            //option1.Key = "key2";
            //option1.Value = "Value2";
            ////var json = new { model = new List<Option>() { option, option1 } };
            //var json = new List<Option>() { option, option1 };
            //new CyHttpClient("api/option").Post(JsonConvert.SerializeObject(json)).TryParseResult(out content);
            //string s = content.ToString();
            //MessageBox.Show(s);
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
