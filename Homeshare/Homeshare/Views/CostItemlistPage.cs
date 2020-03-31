using Homeshare.Model;
using Homeshare.Viewmodel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Homeshare.Behavior;

namespace Homeshare.Views
{
    class CostItemListPage : ContentPage
    {
         public CostItemListPage(ViewModelBase Caller)
        {
        
            BindingContext = new CostItemListViewModel(Caller);

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
                ItemTemplate = new CostItemNotesTemplate(),
                SelectionMode = SelectionMode.Single
            };
            

            costItemsList.SetBinding(CollectionView.ItemsSourceProperty, nameof(CostItemListViewModel.CostItemList));
            costItemsList.SetBinding(CollectionView.SelectedItemProperty, nameof(CostItemListViewModel.SelectedCostItem));
            costItemsList.SetBinding(CollectionView.SelectionChangedCommandProperty, nameof(CostItemListViewModel.SelectItemCmd));
            layout.Children.Add(costItemsList);
           
            Behaviors.Add(new EventToCommandBehavior
            {
                EventName = "Appearing",
                Command = ((CostItemListViewModel)BindingContext).Refresh
            });

            Content = layout;

        }

        class CostItemNotesTemplate : DataTemplate
        {
            public CostItemNotesTemplate() : base(LoadTemplate)
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
