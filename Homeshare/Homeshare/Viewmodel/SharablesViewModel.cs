using Homeshare.DB;
using Homeshare.Model;
using Homeshare.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

/*
 *  View-model of a Mates list page 
 */

namespace Homeshare.Viewmodel
{
    class SharablesViewModel : INotifyPropertyChanged
    {
        public SharablesViewModel()
        {
            //Retrieving data from data base table as a list
            SharableList = DBController.GetInfo<Sharable>();

            //Construction of go to item details page command with declared below
            ItemDetails = new Command(async () => 
            {
                await Application.Current.MainPage.Navigation.PushAsync(new SharableInfoPage(SelectedSharable));
            });

            //Construction of go to add sharable page command with declared below
            Add = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new AddSharablePage());
            });
        }

        //Selected item value
        Sharable selectedSharable;
        public Sharable SelectedSharable 
        { 
            get => selectedSharable; 
            set
            {
                selectedSharable = value;
            }
        }

        //Sharable list data value
        public List<Sharable> SharableList { get; set; }

        //Command which called on item tap action
        public Command ItemDetails { get; }

        //Command which called on add button pressed
        public Command Add { get; }

        //Part of parental interface
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
