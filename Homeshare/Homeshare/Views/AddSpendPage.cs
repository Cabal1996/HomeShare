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
    class AddSpendPage : ContentPage
    {
        public AddSpendPage()
        {
            //Setting of view-model as binding context
            BindingContext = new AddSpendViewModel();

            //Main content organizer, helps to display UI items on the page
            var layout = new StackLayout();

            //Date picker for this post
            DatePicker datePicker = new DatePicker();
            //Binding date value of picker with view model value
            datePicker.SetBinding(DatePicker.DateProperty, nameof(AddSpendViewModel.PostDate));
            //Insertion of an item as the first element on the page
            layout.Children.Add(datePicker);

            //Button to select cost item for this post
            Button SelectCostItem_button = new Button();
            //Button Text field binding
            SelectCostItem_button.SetBinding(Button.TextProperty, nameof(AddSpendViewModel.SelectedCostName));
            //Binding this UI control element to corresponding view-model command
            SelectCostItem_button.SetBinding(Button.CommandProperty, nameof(AddSpendViewModel.CostSelectButton));
            //Insertion of an item as the second element on the page
            layout.Children.Add(SelectCostItem_button);

            //Cost name. Editable text field setup
            Editor Sum = new Editor
            {
                //Field hint setup
                Placeholder = "Price",
                //Enables numeric keyboard to insure numeric value only
                Keyboard = Keyboard.Numeric
            };
            //Binding this field data with corresponding field in view-model
            Sum.SetBinding(Editor.TextProperty, nameof(AddSpendViewModel.Sum));
            //Insertion of an item as the third element on the page
            layout.Children.Add(Sum);       

            //Add item to the database button UI element
            Button Spend_button = new Button
            {
                //UI on-button text
                Text = "Add"
            };
            //Binding this UI control element to corresponding view-model command
            Spend_button.SetBinding(Button.CommandProperty, nameof(AddSpendViewModel.SpendButton));
            //Insertion of an item as the fourth element on the page
            layout.Children.Add(Spend_button);

            //Responsible for displaying content on a page
            Content = layout;
        }      
    }
}
