using Homeshare.Viewmodel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

/*
 * View of add sharable page. Contains page construction logic
 */

namespace Homeshare.Views
{
    class AddSharablePage : ContentPage
    {
        public AddSharablePage()
        {
            //Setting of view-model as binding context
            BindingContext = new AddSharableViewModel();

            //Main content organizer, helps to display UI items on the page
            var layout = new StackLayout();

            //Sharable name. Editable text field setup
            Editor Name = new Editor
            {
                //Field hint setup
                Placeholder = "Name of sharable"
            };
            //Binding this field data with corresponding field in view-model
            Name.SetBinding(Editor.TextProperty, nameof(AddSharableViewModel.SharableName));
            //Insertion of an item as the first element on the page
            layout.Children.Add(Name);
            
            //Sharable type. UI picker setup
            Picker SharableType = new Picker
            {
                Title = "Sharable type"
            };

            //Binding picker to enum structure
            SharableType.ItemsSource = Enum.GetValues(typeof(ESharableType));
            //Binding this field data with corresponding field in view-model
            SharableType.SetBinding(Picker.SelectedIndexProperty, nameof(AddSharableViewModel.SharableType));
            //Insertion of an item as the second element on the page
            layout.Children.Add(SharableType);

            //Sharable periodicity. UI picker setup
            Picker Periodicity = new Picker
            {
                Title = "Periodicity"
            };

            //Binding picker to enum structure
            Periodicity.SetBinding(Picker.SelectedIndexProperty, nameof(AddSharableViewModel.Periodicity));
            //Binding this field data with corresponding field in view-model
            Periodicity.ItemsSource = Enum.GetValues(typeof(EPeriodicitys));
            //Insertion of an item as the third element on the page
            layout.Children.Add(Periodicity);

            //Sharable price. Editable text field setup
            Editor Price = new Editor
            {
                //Field hint setup
                Placeholder = "Euro",
                //Enables numeric keyboard to insure numeric value only
                Keyboard = Keyboard.Numeric          
            };
            //Binding this field data with corresponding field in view-model
            Price.SetBinding(Editor.TextProperty, nameof(AddSharableViewModel.Price));
            //Insertion of an item as the fourth element on the page
            layout.Children.Add(Price);

            //Add item to the database button UI element
            Button AddSharable_button = new Button
            {
                //UI on-button text
                Text = "Add"
            };
            //Binding this UI control element to corresponding view-model command
            AddSharable_button.SetBinding(Button.CommandProperty, nameof(AddSharableViewModel.AddButton));
            //Insertion of an item as the third element on the page
            layout.Children.Add(AddSharable_button);

            //Responsible for displaying content on a page
            Content = layout;
        }      
    }
}
