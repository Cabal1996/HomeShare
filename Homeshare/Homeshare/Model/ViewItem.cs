using System.Collections.Generic;
using Xamarin.Forms;

namespace Homeshare.Model
{
    class ViewItem
    {
        public string Subconto1 { get; set; }
        public string Subconto2 { get; set; }
        public string Subconto3 { get; set; }
    }

    class TableNotesTemplate : DataTemplate
    {
        public TableNotesTemplate() : base(LoadTemplate)
        {

        }

        static StackLayout LoadTemplate()
        {
            Grid Line = new Grid
            {
                ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                    },

                RowDefinitions =
                    {
                        new RowDefinition { Height = new GridLength(1, GridUnitType.Star)}
                    }
            };
            var CostName = new Label();
            CostName.SetBinding(Label.TextProperty, nameof(ViewItem.Subconto1));
            Line.Children.Add(CostName, 0, 0);

            var CostValue = new Label();
            CostValue.SetBinding(Label.TextProperty, nameof(ViewItem.Subconto2));
            Line.Children.Add(CostValue, 1, 0);

            var frame = new Frame
            {
                VerticalOptions = LayoutOptions.Center,
                Content = Line
            };

            return new StackLayout
            {
                Children = { frame },
                Padding = new Thickness(10, 10)
            };
        }
    }
}
