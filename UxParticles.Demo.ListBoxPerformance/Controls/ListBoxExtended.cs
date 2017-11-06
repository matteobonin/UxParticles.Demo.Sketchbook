using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UxParticles.Demo.ListBoxPerformance.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    public class ListBoxExtended : ListBox
    {
        public static readonly DependencyProperty DefaultTextBlockStyleProperty = DependencyProperty.Register("DefaultTextBlockStyle", typeof(Style), typeof(ListBoxExtended), new PropertyMetadata(default(Style)));

        public Style DefaultTextBlockStyle
        {
            get
            {
                return (Style)GetValue(DefaultTextBlockStyleProperty);
            }
            set
            {
                SetValue(DefaultTextBlockStyleProperty, value);
            }
        }
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ListBoxItemExtended();
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            var content = item as DemoItem;
            if (content == null)
            {
                return;
            }

            var myListBoxItem = element as ListBoxItemExtended;
            var sp = new StackPanel() { Orientation = Orientation.Horizontal };
            var cv = new StackPanel();// {Width = 1080, Height = 22};
            cv.Children.Add(sp);
            myListBoxItem.Content = cv;

            for (int i = 0; i < content.ColumnCount; i++)
            {
                var binding = new Binding($"Columns[{i}]")
                                  {
                                      Source = content,
                                      Mode = BindingMode.OneWay
                                  };

                var tb = new TextBlock();
                tb.Style = this.DefaultTextBlockStyle;
                BindingOperations.SetBinding(tb, TextBlock.TextProperty, binding);
                
                var widthBinding = new Binding($"Widths[{i}].Width") { Source = content, Mode = BindingMode.OneWay };
                BindingOperations.SetBinding(tb, TextBlock.WidthProperty, widthBinding);

                sp.Children.Add(
                    new ContentControl() { Content = 
                    new Border()
                        {
                            Child = tb,
                            BorderBrush = Brushes.DarkCyan,
                            Margin = new Thickness(2),
                            BorderThickness = new Thickness(1),
                            Background = Brushes.White
                        }});
            }

           
        }
    }
}
