using Homeshare.Model;
using Homeshare.Viewmodel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Homeshare.Views
{
    class SharablesListPage : ContentPage
    {
        public SharablesListPage()
        {
            BindingContext = new SharablesViewModel();

            var layout = new StackLayout();

            ToolbarItem AddButton = new ToolbarItem
            {
                Text = "Add",
                Order = ToolbarItemOrder.Primary,
                Priority = 0,

            };

            ToolbarItems.Add(AddButton);

            AddButton.SetBinding(ToolbarItem.CommandProperty, nameof(SharablesViewModel.Add));


            CollectionView sahrableList = new CollectionView
            {
                ItemTemplate = new NotesTemplate(),
                SelectionMode = SelectionMode.Single
            };
            

            sahrableList.SetBinding(CollectionView.ItemsSourceProperty, nameof(SharablesViewModel.SharableList));
            sahrableList.SetBinding(CollectionView.SelectedItemProperty, nameof(SharablesViewModel.SelectedSharable));
            sahrableList.SetBinding(CollectionView.SelectionChangedCommandProperty, nameof(SharablesViewModel.ItemDetails));
            layout.Children.Add(sahrableList);


            Content = layout;


        }

        class NotesTemplate : DataTemplate
        {
            public NotesTemplate() : base(LoadTemplate)
            {

            }

            static StackLayout LoadTemplate()
            {
                var textLabel = new Label();
                textLabel.SetBinding(Label.TextProperty, nameof(Sharable.Name));

                var frame = new Frame
                {
                    VerticalOptions = LayoutOptions.Center,
                    Content = textLabel
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
