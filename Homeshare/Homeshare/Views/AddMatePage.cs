using Homeshare.Viewmodel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

/*
 * View of add mate page. Contains page construction logic
 */

namespace Homeshare.Views
{
    class AddMatePage : ContentPage
    {
        public AddMatePage()
        {
            //Setting of view-model as binding context
            BindingContext = new AddMateViewModel();

            //Main content organizer, helps to display UI items on the page
            var layout = new StackLayout();

            //Fires name. Editable text field setup
            Editor FirstName = new Editor
            {
                //Field hint setup
                Placeholder = "First Name"
            };

            //Binding this field data with corresponding field in view-model
            FirstName.SetBinding(Editor.TextProperty, nameof(AddMateViewModel.NewFirstName));
            //Insertion of an item as the first element on the page
            layout.Children.Add(FirstName);

            //Last name. Editable text field setup
            Editor LastName = new Editor
            {
                //Field hint setup
                Placeholder = "First Name"
            };
            //Binding this field data with corresponding field in view-model
            LastName.SetBinding(Editor.TextProperty, nameof(AddMateViewModel.NewLastName));
            //Insertion of an item as the second element on the page
            layout.Children.Add(LastName);

            //Add item to the database button UI element
            Button AddSharable_button = new Button
            {
                //UI on-button text
                Text = "Add"
            };
            //Binding this UI control element to corresponding view-model command
            AddSharable_button.SetBinding(Button.CommandProperty, nameof(AddMateViewModel.AddButton));
            //Insertion of an item as the third element on the page
            layout.Children.Add(AddSharable_button);

            //Responsible for displaying content on a page
            Content = layout;
        }      
    }
}
