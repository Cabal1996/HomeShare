using Homeshare.Model;
using Homeshare.Viewmodel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

/*
 * View of Mate list page. Contains page construction logic
 */

namespace Homeshare.Views
{
    class MatesListPage : ContentPage
    {
        public MatesListPage()
        {
            //Setting of view-model as binding context
            BindingContext = new MatesViewModel();

            //Setting up stack content organizer
            StackLayout layout = new StackLayout();

            //Setting up top toolbar button
            ToolbarItem AddButton = new ToolbarItem
            {
                Text = "Add",
                Order = ToolbarItemOrder.Primary,
                Priority = 0,

            };
            //Adding items to toolbar
            ToolbarItems.Add(AddButton);
            //Binding view-model command go to add mate page
            AddButton.SetBinding(ToolbarItem.CommandProperty, nameof(MatesViewModel.Add));

            //List viewer 
            CollectionView matesList = new CollectionView
            {
                ItemTemplate = new NotesTemplate(),
                SelectionMode = SelectionMode.Single
            };

            //Adding item in specific order
            matesList.SetBinding(CollectionView.ItemsSourceProperty, nameof(MatesViewModel.MatesList));
            matesList.SetBinding(CollectionView.SelectedItemProperty, nameof(MatesViewModel.SelectedMate));
            matesList.SetBinding(CollectionView.SelectionChangedCommandProperty, nameof(MatesViewModel.ItemDetails));
            layout.Children.Add(matesList);


            Content = layout;

        }

        //Template of list item data representation on screen
        class NotesTemplate : DataTemplate
        {
            public NotesTemplate() : base(LoadTemplate)
            {
                
            }

            static StackLayout LoadTemplate()
            {
                
                Grid grid = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width  = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width  = new GridLength(1, GridUnitType.Star) }
                    },

                    RowDefinitions =
                    {
                        new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                    }

                };


                var FName = new Label();
                FName.SetBinding(Label.TextProperty, nameof(Mate.FirstName));

                var LName = new Label();
                LName.SetBinding(Label.TextProperty, nameof(Mate.LastName));

                grid.Children.Add(FName, 0, 0);
                grid.Children.Add(LName, 1, 0);



                var frame = new Frame
                {
                    VerticalOptions = LayoutOptions.Center,
                    Content = grid
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
