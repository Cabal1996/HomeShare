using Homeshare.Model;
using Homeshare.Viewmodel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Homeshare.Views
{
    class CostItemlistPage : ContentPage
    {
        public CostItemlistPage(AddSpendViewModel spendViewModel)
        {
            BindingContext = new CostItemListViewModel(spendViewModel);

            var layout = new StackLayout();

            ToolbarItem AddButton = new ToolbarItem
            {
                Text = "Add",
                Order = ToolbarItemOrder.Primary,
                Priority = 0,

            };

            ToolbarItems.Add(AddButton);

            AddButton.SetBinding(ToolbarItem.CommandProperty, nameof(CostItemListViewModel.Add));


            CollectionView costItemsList = new CollectionView
            {
                ItemTemplate = new NotesTemplate(),
                SelectionMode = SelectionMode.Single
            };


            costItemsList.SetBinding(CollectionView.ItemsSourceProperty, nameof(CostItemListViewModel.CostItemSharableList));
            costItemsList.SetBinding(CollectionView.SelectedItemProperty, nameof(CostItemListViewModel.SelectedCostItem));
            costItemsList.SetBinding(CollectionView.SelectionChangedCommandProperty, nameof(CostItemListViewModel.SelectItemCmd));
            layout.Children.Add(costItemsList);


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
                textLabel.SetBinding(Label.TextProperty, nameof(CostItem.Name));

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
