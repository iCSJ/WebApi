using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Utils
{
    /// <summary>
    /// 控件移动工具类
    /// </summary>
    public static class ControlMoveHelper
    {
        private static bool isDragDropInEffect = false;
        private static Point pos = new Point();
        /// <summary>
        /// 注册以启用控件移动
        /// </summary>
        /// <param name="LayoutRoot"></param>
        public static void RegisterContainer(Grid LayoutRoot)
        {
            foreach (UIElement uiEle in LayoutRoot.Children)
            {
                if (uiEle is Button || uiEle is TextBox)
                {
                    uiEle.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(Element_MouseLeftButtonDown), true);
                    uiEle.AddHandler(UIElement.MouseMoveEvent, new MouseEventHandler(Element_MouseMove), true);
                    uiEle.AddHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(Element_MouseLeftButtonUp), true);
                    continue;
                }
                uiEle.MouseMove += new MouseEventHandler(Element_MouseMove);
                uiEle.MouseLeftButtonDown += new MouseButtonEventHandler(Element_MouseLeftButtonDown);
                uiEle.MouseLeftButtonUp += new MouseButtonEventHandler(Element_MouseLeftButtonUp);
            }
        }
        /// <summary>
        /// 取消注册以禁用控件移动
        /// </summary>
        /// <param name="LayoutRoot"></param>
        public static void UnRegisterContainer(Grid LayoutRoot)
        {
            foreach (UIElement uiEle in LayoutRoot.Children)
            {
                if (uiEle is Button || uiEle is TextBox)
                {
                    uiEle.RemoveHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(Element_MouseLeftButtonDown));
                    uiEle.AddHandler(UIElement.MouseMoveEvent, new MouseEventHandler(Element_MouseMove));
                    uiEle.AddHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(Element_MouseLeftButtonUp));
                    continue;
                }
                uiEle.MouseMove -= new MouseEventHandler(Element_MouseMove);
                uiEle.MouseLeftButtonDown -= new MouseButtonEventHandler(Element_MouseLeftButtonDown);
                uiEle.MouseLeftButtonUp -= new MouseButtonEventHandler(Element_MouseLeftButtonUp);
            }
        }
        static void Element_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragDropInEffect)
            {
                FrameworkElement currEle = sender as FrameworkElement;
                double xPos = e.GetPosition(null).X - pos.X + currEle.Margin.Left;
                double yPos = e.GetPosition(null).Y - pos.Y + currEle.Margin.Top;
                double right = currEle.Margin.Right - (e.GetPosition(null).X - pos.X);
                double bottom = currEle.Margin.Bottom - (e.GetPosition(null).Y - pos.Y);
                currEle.Margin = new Thickness(xPos, yPos, right, bottom);
                pos = e.GetPosition(null);
            }
        }
        static void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            FrameworkElement fEle = sender as FrameworkElement;
            isDragDropInEffect = true;
            pos = e.GetPosition(null);
            fEle.CaptureMouse();
        }

        static void Element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragDropInEffect)
            {
                FrameworkElement ele = sender as FrameworkElement;
                isDragDropInEffect = false;
                ele.ReleaseMouseCapture();
            }
        }
    }
}
