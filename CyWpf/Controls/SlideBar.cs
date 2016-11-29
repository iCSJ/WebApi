using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utils;

namespace CyWpf.Controls
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:bbv.WpfNavigation"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:bbv.WpfNavigation;assembly=bbv.WpfNavigation"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:SlideBar/>
    ///
    /// </summary>
    //[Localizability(LocalizationCategory.ListBox)]
    //[StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(ListBoxItem))]
    public class SlideBar : HeaderedItemsControl
    {
        static SlideBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SlideBar), new FrameworkPropertyMetadata(typeof(SlideBar)));
        }
        public int OpenWidth
        {
            get { return (int)GetValue(OpenWidthProperty); }
            set { SetValue(OpenWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OpenWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpenWidthProperty =
            DependencyProperty.Register("OpenWidth", typeof(int), typeof(SlideBar), new PropertyMetadata(0));

        public static readonly RoutedEvent ItemClickEvent = EventManager.RegisterRoutedEvent("ItemClick", RoutingStrategy.Bubble, typeof(EventHandler<ListBoxItemEventArgs>), typeof(SlideBar));
        //CLR事件包装器  
        public event EventHandler<ListBoxItemEventArgs> ItemClick
        {
            add { this.AddHandler(ItemClickEvent, value); }
            remove { this.RemoveHandler(ItemClickEvent, value); }
        }

        public class ListBoxItemEventArgs : RoutedEventArgs
        {
            public ListBoxItemEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source) { }
            public SlideBar SlideBar { get; set; }
            public ListBoxItem Item { get; set; }
            public int Index { get; set; }
            public object Data { get; set; }
        }

        private bool isMouseOver;
        private bool isOpened;
        private ThicknessAnimation ta = new ThicknessAnimation();
        public RoutedCommand ToggleCommand { get; set; }
        public bool IsOpened
        {
            get { return isOpened; }
            set
            {
                isOpened = value;
                if (IsOpened)
                {
                    Open();
                }
                else
                {
                    Collapse();
                }
            }
        }
        public SlideBar()
        {
            ToggleCommand = new RoutedCommand("ToggleOpen", typeof(SlideBar));
            initCommand();
            Loaded += OnLoaded;
            MouseLeftButtonUp += (o, a) =>
            {
                ListBoxItemEventArgs arg = new ListBoxItemEventArgs(ItemClickEvent, this);
                SlideBar slideBar = (SlideBar)o;
                if (null == slideBar) return;
                ListBoxItem listItem = UITool.GetElementUnderMouse<ListBoxItem>();
                if (null == listItem) return;
                arg.SlideBar = slideBar;
                arg.Item = listItem;
                arg.Index = slideBar.ItemContainerGenerator.IndexFromContainer(listItem);
                arg.Data = listItem.DataContext;
                IsOpened = false;
                RaiseEvent(arg);
            };
        }

        private void initCommand()
        {
            ToggleCommand.InputGestures.Add(new KeyGesture(Key.F12));
            CommandBinding cb = new CommandBinding(ToggleCommand, HamburgToggle);
            CommandBindings.Add(cb);
        }

        private void Open()
        {
            ta.Duration = TimeSpan.FromMilliseconds(100);
            ta.To = new Thickness(0, 0, -OpenWidth, 0);
            BeginAnimation(MarginProperty, ta);
        }
        private void Collapse()
        {
            ta.Duration = TimeSpan.FromMilliseconds(0);
            ta.To = new Thickness(0, 0, 0, 0);
            BeginAnimation(MarginProperty, ta);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(UIElement), PreviewMouseDownEvent, new MouseButtonEventHandler(PreMouseDown));
            MouseLeave += (o, a) => { isMouseOver = false; };
            MouseEnter += (o, a) => { isMouseOver = true; };
        }
        private void PreMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isMouseOver)
                return;
            Debug.WriteLine("PreMouseDown:" + sender.GetType().Name, "所有控件鼠标按下事件");
            IsOpened = false;
        }

        private void HamburgToggle(object sender, ExecutedRoutedEventArgs e)
        {
            IsOpened = !IsOpened;
        }
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ListBoxItem();
        }
    }
}
