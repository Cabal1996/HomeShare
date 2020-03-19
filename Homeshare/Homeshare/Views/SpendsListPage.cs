using Homeshare.Behavior;
using Homeshare.Model;
using Homeshare.Viewmodel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Homeshare.Views
{
    class SpendsListPage : ContentPage
    {
        public SpendsListPage()
        {
            BindingContext = new SpendsViewModel();

            StackLayout layout = new StackLayout();

            ToolbarItem AddButton = new ToolbarItem
            {
                Text = "Add",
                Order = ToolbarItemOrder.Primary,
                Priority = 0,
            };

            ToolbarItems.Add(AddButton);

            AddButton.SetBinding(ToolbarItem.CommandProperty, nameof(SpendsViewModel.Add));

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
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)}        
                }
            };
            layout.Children.Add(dateGrid);

            DatePicker fromDatePicker = new DatePicker
            {
                Date = new DateTime((DateTime.Today).Year, (DateTime.Today).Month, 1)
            };
            fromDatePicker.SetBinding(DatePicker.DateProperty, nameof(SpendsViewModel.FromDate));
            dateGrid.Children.Add(fromDatePicker, 0, 0);

            DatePicker toDatePicker = new DatePicker
            {
                Date = DateTime.Today
            };
            toDatePicker.SetBinding(DatePicker.DateProperty, nameof(SpendsViewModel.ToDate));
            dateGrid.Children.Add(toDatePicker, 1, 0);

            Button applyButton = new Button
            {
                Text = "Apply"
            };
            applyButton.SetBinding(Button.CommandProperty, nameof(SpendsViewModel.Refresh));
            dateGrid.Children.Add(applyButton, 2, 0);
            

            CollectionView spentList = new CollectionView
            {
                ItemTemplate = new NotesTemplate(),
                SelectionMode = SelectionMode.Single
            };

            spentList.SetBinding(CollectionView.ItemsSourceProperty, nameof(SpendsViewModel.DisplayList));
            spentList.SetBinding(CollectionView.SelectedItemProperty, nameof(SpendsViewModel.SelectedSpent));
            spentList.SetBinding(CollectionView.SelectionChangedCommandProperty, nameof(SpendsViewModel.ItemDetails));
            layout.Children.Add(spentList);

            Content = layout;

            Behaviors.Add(new EventToCommandBehavior
            {
                EventName = "Appearing",
                Command = ((SpendsViewModel)BindingContext).Refresh
            });
        }

        class NotesTemplate : DataTemplate
        {
            public NotesTemplate() : base(LoadTemplate)
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
}
