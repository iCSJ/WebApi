using System;
using System.Windows;
using System.Windows.Controls;

namespace CyWpf.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class FillWrapPanel : Panel
    {
        // Using a DependencyProperty as the backing store for MinItemWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinItemWidthProperty =
            DependencyProperty.Register("MinItemWidth", typeof(double), typeof(FillWrapPanel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsArrange));

        // Using a DependencyProperty as the backing store for MaxItemWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxItemWidthProperty =
            DependencyProperty.Register("MaxItemWidth", typeof(double), typeof(FillWrapPanel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsArrange));

        // Using a DependencyProperty as the backing store for ItemMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemMarginProperty =
            DependencyProperty.Register("ItemMargin", typeof(double), typeof(FillWrapPanel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsArrange));

        // Using a DependencyProperty as the backing store for RowMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowMarginProperty =
            DependencyProperty.Register("RowMargin", typeof(double), typeof(FillWrapPanel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsArrange));

        // Using a DependencyProperty as the backing store for FloorItemWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FloorItemWidthProperty =
            DependencyProperty.Register("FloorItemWidth", typeof(bool), typeof(FillWrapPanel), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsArrange));

        public bool FloorItemWidth
        {
            get { return (bool)GetValue(FloorItemWidthProperty); }
            set { SetValue(FloorItemWidthProperty, value); }
        }

        public double MinItemWidth
        {
            get { return (double)GetValue(MinItemWidthProperty); }
            set { SetValue(MinItemWidthProperty, value); }
        }

        public double MaxItemWidth
        {
            get { return (double)GetValue(MaxItemWidthProperty); }
            set { SetValue(MaxItemWidthProperty, value); }
        }

        public double ItemMargin
        {
            get { return (double)GetValue(ItemMarginProperty); }
            set { SetValue(ItemMarginProperty, value); }
        }

        public double RowMargin
        {
            get { return (double)GetValue(RowMarginProperty); }
            set { SetValue(RowMarginProperty, value); }
        }

        //protected override Size MeasureOverride(Size availableSize)
        //{
        //    foreach (UIElement child in Children)
        //    {
        //        child.Measure(availableSize);
        //    }

        //    return new Size(0, 0);
        //}
        protected override Size MeasureOverride(Size availableSize)
        {
            double height = CalculateHeight(0.0);
            double maxHeight = 0;
            foreach (UIElement child in Children)
            {
                child.Measure(availableSize);
                double h = CalculateHeight(child.DesiredSize.Height);
                height += h;

                if (maxHeight < h)
                { maxHeight = h; }
            }

            if (this.RenderSize.Width == 0 || this.RenderSize.Height == 0)
            {
                return new Size(0, 0);
            }

            int itemCountInRow = CalculateItemsCountInOneRow(new Size(this.RenderSize.Width, RenderSize.Height));
            if (Children.Count % itemCountInRow != 0)
            {
                height += (itemCountInRow - Children.Count % itemCountInRow) * maxHeight;
            }

            return new Size(0, height / itemCountInRow);
        }

        private double CalculateHeight(double height)
        {
            return height + RowMargin;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double yOffset = 0.0;
            double xOffset = 0.0;
            int itemCountInRow = CalculateItemsCountInOneRow(finalSize);
            double itemWidth = CalculateItemWidth(finalSize.Width, itemCountInRow);

            for (int i = 0; i < Children.Count;)
            {
                double rowHeight = 0;
                for (int column = 0; column < itemCountInRow && i + column < Children.Count; column++)
                {
                    UIElement child = Children[i + column];
                    child.Arrange(new Rect(xOffset, yOffset, itemWidth, child.DesiredSize.Height));
                    if (child.DesiredSize.Height > rowHeight)
                    {
                        rowHeight = child.DesiredSize.Height;
                    }

                    xOffset += itemWidth + ItemMargin;
                }

                yOffset += rowHeight + RowMargin;
                xOffset = 0.0;
                i += itemCountInRow;
            }

            return base.ArrangeOverride(finalSize);
        }

        private int CalculateItemsCountInOneRow(Size finalSize)
        {
            // Calling Math.Floor is necessory or not?
            return (int)Math.Floor(((finalSize.Width + ItemMargin) / (MinItemWidth + ItemMargin)));
        }

        private double CalculateItemWidth(double totalWidth, int itemCountInRow)
        {
            if (itemCountInRow > Children.Count)
            {
                itemCountInRow = Children.Count;
            }

            double itemWidth = (totalWidth - (itemCountInRow - 1) * ItemMargin) / itemCountInRow;

            if (itemWidth > MaxItemWidth)
            {
                itemWidth = MaxItemWidth;
            }

            return FloorItemWidth ? Math.Floor(itemWidth) : itemWidth;
        }
    }
}
