using Homeshare.Viewmodel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

/*
 * View of Main menu page. Contains page construction logic
 */


namespace Homeshare.Views
{
    public class MainMenuPage : ContentPage
    {
        public MainMenuPage()
        {
            //Setting of view-model as binding context
            BindingContext = new MainMenuViewModel();

            //Setting up top toolbar title 
            ToolbarItem PageName = new ToolbarItem
            {
                Text = "HOMESHARE",
                Order = ToolbarItemOrder.Primary,
                Priority = 0,
                
            };

            //Setting up top toolbar button
            ToolbarItem ToolbarButtonSharables = new ToolbarItem
            {
                Text = "Sharables",
                Order = ToolbarItemOrder.Secondary,
                Priority = 1
                
            };
            //Binding view-model command go to Sharable items
            ToolbarButtonSharables.SetBinding(ToolbarItem.CommandProperty, nameof(MainMenuViewModel.SharableCmd));

            //Setting up top toolbar button
            ToolbarItem ToolbarButtonMates = new ToolbarItem
            {
                Text = "Mates",
                Order = ToolbarItemOrder.Secondary,
                Priority = 2
            };
            //Binding view-model command go to Mates items
            ToolbarButtonMates.SetBinding(ToolbarItem.CommandProperty, nameof(MainMenuViewModel.MateCmd));

            //Adding items to toolbar in specific order
            this.ToolbarItems.Add(PageName);
            this.ToolbarItems.Add(ToolbarButtonSharables);
            this.ToolbarItems.Add(ToolbarButtonMates);

            //Button go to calculation page
            Button postSpendButton = new Button
            {
                Text = "Post spend",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            //Binding view-model command
            postSpendButton.SetBinding(Button.CommandProperty, nameof(MainMenuViewModel.PostSpendCmd));
          

            //Setting up text field as spacer
            Label spacer = new Label
            {
                VerticalOptions = LayoutOptions.Start
            };

            //Button go to setup page
            Button buttonSetup = new Button
            {
                Text = "Quick Calculate",
                IsEnabled = true,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            //Binding view-model command
            buttonSetup.SetBinding(Button.CommandProperty, nameof(MainMenuViewModel.QuickCalcCommand));

            //Setting up stack content organizer and adding item in specific order
            StackLayout layout = new StackLayout();
            layout.Children.Add(postSpendButton);
            layout.Children.Add(spacer);
            layout.Children.Add(buttonSetup);
            layout.Margin = new Thickness(30);

            //Responsible for displaying content on a page
            Content = layout;
        }
    }
}
