using Homeshare.Viewmodel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

/*
 * View of add cost item page. Contains page construction logic
 */

namespace Homeshare.Views
{
    class AddCostItemPage : ContentPage
    {
        public AddCostItemPage()
        {
            //Setting of view-model as binding context
            BindingContext = new AddCostItemViewModel();

            //Main content organizer, helps to display UI items on the page
            var layout = new StackLayout();

            //Cost name. Editable text field setup
            Editor Name = new Editor
            {
                //Field hint setup
                Placeholder = "Name of Cost"
            };
            //Binding this field data with corresponding field in view-model
            Name.SetBinding(Editor.TextProperty, nameof(AddCostItemViewModel.CostItemName));
            //Insertion of an item as the first element on the page
            layout.Children.Add(Name);       

            //Add item to the database button UI element
            Button AddCostItem_button = new Button
            {
                //UI on-button text
                Text = "Add"
            };
            //Binding this UI control element to corresponding view-model command
            AddCostItem_button.SetBinding(Button.CommandProperty, nameof(AddSharableViewModel.AddButton));
            //Insertion of an item as the third element on the page
            layout.Children.Add(AddCostItem_button);

            //Responsible for displaying content on a page
            Content = layout;
        }      
    }
}
