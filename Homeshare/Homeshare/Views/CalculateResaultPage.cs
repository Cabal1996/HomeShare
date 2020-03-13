using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

/*
 * View of calculation result page. Contains page construction logic
 */

namespace Homeshare.Views
{
    class CalculateResaultPage : ContentPage
    {
        public CalculateResaultPage(List<float> Resault)
        {

            StackLayout layout = new StackLayout();

            //Looping though result list and building UI based on data quantity
            for(int i = 0; i < Resault.Count; i++)
            {
                //Individual line grid element for each result value
                Grid line = new Grid
                {
                    ColumnDefinitions = new ColumnDefinitionCollection
                    {
                        new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star)},
                        new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star)}
                    },

                    RowDefinitions = new RowDefinitionCollection
                    {
                        new RowDefinition { Height = new GridLength(1, GridUnitType.Star)}
                    }
                };

                //Name text UI element
                Label Name = new Label
                {
                    Text = "Mate" + (i + 1)
                };

                //Name text UI element
                Label Price = new Label
                {
                    Text = Resault[i].ToString()
                };

                //adding text elements to the line grid
                line.Children.Add(Name, 0, 0);
                line.Children.Add(Price, 1, 0);

                //Adding line grid to thee main stack layout
                layout.Children.Add(line);
            }

            //Responsible for displaying content on a page
            Content = layout;
        }
    }
}
