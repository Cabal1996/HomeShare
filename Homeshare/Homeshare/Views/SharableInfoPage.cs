using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Homeshare.Model;
using Xamarin.Forms;

namespace Homeshare.Views
{
    public class SharableInfoPage : ContentPage
    {
        public SharableInfoPage(Sharable SelectedSharable)
        {
            StackLayout layout = new StackLayout();

            Label Name = new Label
            {
                Text = SelectedSharable.Name
            };

            layout.Children.Add(Name);

            Label Type = new Label
            {
                Text = SelectedSharable.Type
            };

            layout.Children.Add(Type);

            Label Periodicty = new Label
            {
                Text = SelectedSharable.Periodicity
            };

            layout.Children.Add(Periodicty);

            Label Price = new Label
            {
                Text = SelectedSharable.Price
            };

            layout.Children.Add(Price);

            Content = layout;
        }
    }
}