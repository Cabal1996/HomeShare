using Homeshare.Behavior;
using Homeshare.Model;
using Homeshare.Viewmodel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Homeshare.Views
{
    class SpendsListDetailePage : ContentPage
    {
        public SpendsListDetailePage()
        {
            BindingContext = new SpendsDetailsViewModel();
            PageInit();
        }

        public SpendsListDetailePage(SpendsViewModel inData)
        {
            BindingContext = new SpendsDetailsViewModel(inData);
            PageInit();
        }

        private void PageInit()
        {
            StackLayout layout = new StackLayout();

            Grid dateGrid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                },

                RowDefinitions =
                {
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition {Height = new GridLength(2, GridUnitType.Star)}
                }
            };
            layout.Children.Add(dateGrid);

            Button costItemPickerButton = new Button
            {
                
            };
            costItemPickerButton.SetBinding(Button.CommandProperty, nameof(SpendsDetailsViewModel.PickCostItem));
            costItemPickerButton.SetBinding(Button.TextProperty, nameof(SpendsDetailsViewModel.SelectedCostItemName));
            dateGrid.Children.Add(costItemPickerButton, 0, 0);
            Grid.SetColumnSpan(costItemPickerButton, 3);

            DatePicker fromDatePicker = new DatePicker
            {
                Date = new DateTime((DateTime.Today).Year, (DateTime.Today).Month, 1)
            };
            fromDatePicker.SetBinding(DatePicker.DateProperty, nameof(SpendsDetailsViewModel.FromDate));
            dateGrid.Children.Add(fromDatePicker, 0, 1);

            DatePicker toDatePicker = new DatePicker
            {
                Date = DateTime.Today
            };
            toDatePicker.SetBinding(DatePicker.DateProperty, nameof(SpendsDetailsViewModel.ToDate));
            dateGrid.Children.Add(toDatePicker, 1, 1);

            Button applyButton = new Button
            {
                Text = "Apply"
            };
            applyButton.SetBinding(Button.CommandProperty, nameof(SpendsDetailsViewModel.Refresh));
            dateGrid.Children.Add(applyButton, 2, 1);

            CollectionView spentListDetails = new CollectionView
            {
                ItemTemplate = new TableNotesTemplate(),
                //SelectionMode = SelectionMode.Single
            };

            spentListDetails.SetBinding(CollectionView.ItemsSourceProperty, nameof(SpendsDetailsViewModel.DisplayList));
            //spentListDetails.SetBinding(CollectionView.SelectedItemProperty, nameof(SpendsDetailsViewModel.SelectedSpent));
            //spentListDetails.SetBinding(CollectionView.SelectionChangedCommandProperty, nameof(SpendsDetailsViewModel.ItemDetails));

            layout.Children.Add(spentListDetails);

            Content = layout;

            Behaviors.Add(new EventToCommandBehavior
            {
                EventName = "Appearing",
                Command = ((SpendsDetailsViewModel)BindingContext).Refresh
            });
        }
    }
}
