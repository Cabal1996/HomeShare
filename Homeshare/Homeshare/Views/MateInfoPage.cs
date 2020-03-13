using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Homeshare.Model;
using Xamarin.Forms;

/*
 * View of Main menu page. Contains page construction logic
 */

namespace Homeshare.Views
{
    public class MateInfoPage : ContentPage
    {
        public MateInfoPage(Mate SelectedMate)
        {
            //Setting up stack content organizer
            StackLayout layout = new StackLayout();
            
            //Setting up text field for First Name value to display 
            Label FirstName = new Label
            {
                Text = SelectedMate.FirstName
            };
            //Add to organizer in order
            layout.Children.Add(FirstName);

            //Setting up text field for Last Name value to display 
            Label LastName = new Label
            {
                Text = SelectedMate.LastName
            };
            //Add to organizer in order
            layout.Children.Add(LastName);

            //Setting up text field for database info of total money spend by this mate
            Label MoneySpent = new Label
            {
                Text = SelectedMate.MoneySpent
            };
            //Add to organizer in order
            layout.Children.Add(MoneySpent);

            //Responsible for displaying content on a page
            Content = layout;
        }
    }
}