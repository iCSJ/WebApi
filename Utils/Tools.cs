using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Utils
{
    /// <summary>
    /// UI操作工具类
    /// </summary>
    public static class UITool
    {
        /// <summary>
        /// 检查列表控件的项是否在可见范围内
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listBox"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool IsUserVisible<T>(this ListBox listBox, T element) where T : FrameworkElement
        {
            if (!element.IsVisible)
                return false;
            ScrollViewer container = FindVisualChild<ScrollViewer>(listBox);
            if (container == null)
                return false;
            var control = element as Control;
            Rect bounds =
                element.TransformToAncestor(container).TransformBounds(new Rect(0.0, 0.0, element.ActualWidth - 1, element.ActualHeight - 1));
            var rect = new Rect(0.0, 0.0, container.ActualWidth, container.ActualHeight);
            return rect.Contains(bounds.TopLeft) || rect.Contains(bounds.BottomRight);
        }
        /// <summary>
        /// 获取列表控件的可见项到新的列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listBox"></param>
        /// <returns></returns>
        public static List<T> GetVisibleItemsFromListbox<T>(this ListBox listBox) where T : FrameworkElement
        {
            var items = new List<T>();

            foreach (var item in listBox.Items)
            {
                if (listBox.IsUserVisible((T)listBox.ItemContainerGenerator.ContainerFromItem(item)))
                {
                    items.Add((T)listBox.ItemContainerGenerator.ContainerFromItem(item));
                }
                else if (items.Any())
                {
                    break;
                }
            }
            return items;
        }
        /// <summary>
        /// 获取控件模板子无素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T ControlTemplateFindElement<T>(Control obj, string name)
        {
            return (T)obj.Template.FindName(name, obj);

        }

        /// <summary>
        /// 获取控件错误模板子无素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T ErrorTemplateFindElement<T>(FrameworkElement obj, string name)
        {
            return (T)Validation.GetErrorTemplate(obj).FindName(name, obj);

        }
        /// <summary>
        /// 获取数据模板子无素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T DataTemplateFindElement<T>(ItemsControl obj, string name)
        {
            var item = obj.ItemContainerGenerator.ContainerFromItem(obj.Items.CurrentItem);
            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(item);
            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            T element = (T)myDataTemplate.FindName(name, myContentPresenter);
            return element;
        }
        /// <summary>
        /// 查找对应UI类别的子无素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T FindVisualChild<T>(DependencyObject obj)
            where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
        /// <summary>
        /// 查找对应UI类别的父元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        public static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;
            while (parent != null)
            {
                var correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }

                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }
        /// <summary>
        /// 获取鼠标所在位置的对应UI类别元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetElementUnderMouse<T>() where T : UIElement
        {
            return FindVisualParent<T>(Mouse.DirectlyOver as UIElement);
        }
        /// <summary>
        /// 根据字符串返回对应的依懒属性
        /// </summary>
        /// <param name="fe"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static DependencyProperty TestCode(FrameworkElement fe, string propertyName)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(fe);
            PropertyDescriptor myProperty = properties[propertyName];
            if (myProperty == null) return null;
            DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(myProperty);
            if (dpd == null) return null;
            DependencyProperty dp = dpd.DependencyProperty;
            return dp;
        }
        /// <summary>
        /// 刷新界面
        /// </summary>
        [SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrames), frame);
            try { Dispatcher.PushFrame(frame); }
            catch (InvalidOperationException) { }
        }
        private static object ExitFrames(object frame)
        {
            ((DispatcherFrame)frame).Continue = false;
            return null;
        }
    }
    /// <summary>
    /// 文件操作工具类
    /// </summary>
    public static class FileTool
    {/// <summary>
     /// 从文件读取文本字符
     /// </summary>
     /// <param name="path"></param>
     /// <returns></returns>
        public static string ReadText(string path)
        {

            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            string line;
            StringBuilder sb = new StringBuilder();
            while ((line = sr.ReadLine()) != null)
            {
                sb.Append(line.ToString());
            }
            return sb.ToString();
        }
        public static object FromPhoto(string photourl)
        {
            FileStream fs = new FileStream(photourl, FileMode.Open, FileAccess.Read);//创建FileStream对象，用于向BinaryReader写入字节数据流
            BinaryReader br = new BinaryReader(fs);//创建BinaryReader对象，用于写入下面的byte数组
            byte[] photo = br.ReadBytes((int)fs.Length);//新建byte数组，写入br中的数据
            br.Close();//记得要关闭br
            fs.Close();//还有fs
            return photo;
        }
        public static void CreatePhoto(string url, byte[] photo)
        {
            try
            {
                FileStream fs = new FileStream(url, FileMode.CreateNew); //创建FileStream对象，用于写入字节数据流
                fs.Write(photo, 0, photo.Length);//将byte数组中的数据写入fs
                fs.Close();//关闭fs
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool FileExists(string file)
        {
            return File.Exists(AppDomain.CurrentDomain.BaseDirectory + file);
        }
        /// <summary>
        /// 计算文件夹大小
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static long DirSize(DirectoryInfo d)

        {
            long Size = 0;
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                Size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                Size += DirSize(di);
            }
            return (Size);
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        public static void CreateDirIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
    /// <summary>
    /// Json工具类
    /// </summary>
    public class JsonTool
    {
        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object o)
        {
            string json = JsonConvert.SerializeObject(o);
            return json;
        }

        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }

        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        {
            T t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            return t;
        }

        /// <summary>
        /// 解析简单JSON值
        /// </summary>
        /// <param name="json"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ParseValue(string jsonStr, string key)
        {
            try
            {
                JObject json = JObject.Parse(jsonStr);
                return json[key].ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    /// <summary>
    /// 一般工具类
    /// </summary>
    public class Tools
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5Encode(string str)
        {
            byte[] result = Encoding.Default.GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "").ToLower();
        }
        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Base64Decode(string str)
        {
            byte[] bpath = Convert.FromBase64String(str);
            return System.Text.ASCIIEncoding.Default.GetString(bpath);
        }
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Base64Encode(string str)
        {
            System.Text.Encoding encode = System.Text.Encoding.ASCII;
            byte[] bytedata = encode.GetBytes(str);
            return Convert.ToBase64String(bytedata, 0, bytedata.Length);
        }
        /// <summary>
        /// 读取INI文件值
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static string IniRead(string filename, string section, string key, string def)
        {
            StringBuilder str = new StringBuilder();
            GetPrivateProfileString(section, key, def, str, int.MaxValue, filename);
            if (string.IsNullOrEmpty(str.ToString()))
            {
                return def;
            }
            return str.ToString();
        }

        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long IniWrite(string filename, string section, string key, string value)
        {
            return WritePrivateProfileString(section, key, value, filename);
        }

        /// <summary>
        /// 解析HTTP Get参数
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string ParseHttpArg(IDictionary<string, object> dic)
        {
            string str = string.Empty;
            foreach (var item in dic)
            {
                str += item.Key + "=" + item.Value.ToString() + "&";
            }
            if (str.Length > 0)
            {
                str = str.Substring(0, str.Length - 1);
            }
            return str;
        }
    }
    /// <summary>
    /// 注册表操作类
    /// </summary>
    public class RegisterTool
    {
        /// <summary>
        /// 注册表根类型
        /// </summary>
        public enum RegRoot
        {
            CurrentUser,
            LocalMachine,
            Users,
            ClassRoot
        }

        private static RegistryKey getRegistryRoot(RegRoot root)
        {
            if (root == RegRoot.CurrentUser)
                return Registry.CurrentUser;
            if (root == RegRoot.LocalMachine)
                return Registry.LocalMachine;
            if (root == RegRoot.ClassRoot)
                return Registry.ClassesRoot;
            if (root == RegRoot.Users)
                return Registry.Users;

            return Registry.CurrentUser;
        }

        /// <summary>
        /// 读取指定名称的注册表的值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetValue(RegRoot root, string subkey, string name)
        {
            string registData = "";
            RegistryKey myKey = getRegistryRoot(root).OpenSubKey(subkey, true);
            if (myKey != null)
            {
                registData = myKey.GetValue(name).ToString();
            }
            return registData;
        }

        /// <summary>
        /// 向注册表中写数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tovalue"></param> 
        public void SetValue(RegRoot root, string subkey, string name, string value)
        {
            RegistryKey r = getRegistryRoot(root).CreateSubKey(subkey);
            r.SetValue(name, value);
        }

        /// <summary>
        /// 删除注册表中指定的注册表项
        /// </summary>
        /// <param name="name"></param>
        public void Delete(RegRoot root, string subkey, string name)
        {
            string[] subkeyNames;
            RegistryKey myKey = getRegistryRoot(root).OpenSubKey(subkey, true);
            subkeyNames = myKey.GetSubKeyNames();
            foreach (string aimKey in subkeyNames)
            {
                if (aimKey == name)
                    myKey.DeleteSubKeyTree(name);
            }
        }

        /// <summary>
        /// 判断指定注册表项是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExist(RegRoot root, string subkey, string name)
        {
            bool _exit = false;
            string[] subkeyNames;
            RegistryKey myKey = getRegistryRoot(root).OpenSubKey(subkey, true);
            subkeyNames = myKey.GetSubKeyNames();
            foreach (string keyName in subkeyNames)
            {
                if (keyName == name)
                {
                    _exit = true;
                    return _exit;
                }
            }
            return _exit;
        }
    }

}
