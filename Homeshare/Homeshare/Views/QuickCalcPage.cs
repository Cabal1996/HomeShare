using Homeshare.Viewmodel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

/*
 * View of Calculation page. Contains page construction logic
 */

namespace Homeshare.Views
{
    class QuickCalcPage : ContentPage
    {
        //Container foe mate items
        StackLayout MatesInfoContainer = new StackLayout();

        //Counts amount of added mate item to this page
        int mateCounter = 0;

        //Editable field for day amount
        Editor TottalDays = new Editor
        {
            Placeholder = "Tottal Period (days)",
            Keyboard = Keyboard.Numeric
        };

        CalcViewModel vievmodel;
        public QuickCalcPage()
        {
            vievmodel = new CalcViewModel();
            BindingContext = vievmodel;

            Grid grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                },

                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };

            Editor TottalSum = new Editor
            {
                Placeholder = "Tottal cost",
                Keyboard = Keyboard.Numeric
            };
            TottalSum.SetBinding(Editor.TextProperty, nameof(CalcViewModel.TottalPrice));
            grid.Children.Add(TottalSum, 0, 0);

            
            TottalDays.SetBinding(Editor.TextProperty, nameof(CalcViewModel.TottalPeriod));
            grid.Children.Add(TottalDays, 1, 0);

            grid.Children.Add(MatesInfoContainer, 0, 1);
            Grid.SetColumnSpan(MatesInfoContainer, 2);

            Button AddMateButton = new Button
            {
                Text = "Add"
            };
            AddMateButton.Clicked += AddMate;
            grid.Children.Add(AddMateButton, 0, 2);
            Grid.SetColumnSpan(AddMateButton, 2);


            Button CalculateButton = new Button
            {
                Text = "Calculate"
            };
            
            CalculateButton.Clicked +=  (object sender, EventArgs e) =>
            {
                vievmodel.Calculation(DaysCollection);
            };
            grid.Children.Add(CalculateButton, 0, 3);
            Grid.SetColumnSpan(CalculateButton, 2);

            Content = grid;
        }

        List<Editor> DaysCollection = new List<Editor>(); 

        //Add mate UI build logic
        private void AddMate(object sender, EventArgs e)
        {
            mateCounter++;
            Grid MateInfoWraper = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)}
                },

                RowDefinitions =
                {
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)}
                }
            };

            Label Name = new Label
            {
                Text = "Mate" + mateCounter
            };

            Editor NumberOfDays = new Editor
            {
                Placeholder = "EnterNumberOdDays",
                Keyboard= Keyboard.Numeric,
                Text = TottalDays.Text
            };

            DaysCollection.Add(NumberOfDays);
            
            MateInfoWraper.Children.Add(Name, 0, 0);
            MateInfoWraper.Children.Add(NumberOfDays, 1, 0);

            MatesInfoContainer.Children.Add(MateInfoWraper);
        }

    }
}
