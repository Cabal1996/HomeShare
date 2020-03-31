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
    class MatesViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public MatesViewModel()
        {
            //Retrieving data from data base table as a list
            MatesList = DBController.GetInfo<Mate>();

            //Construction of go to item details page command with declared below
            ItemDetails = new Command(() => 
            {
                RapidTapPreventorAsync(async () => await Application.Current.MainPage.Navigation.PushAsync(new MateInfoPage(SelectedMate)));
            });

            //Construction of go to add mate page command with declared below
            Add = new Command(() =>
            {
                RapidTapPreventorAsync(async () => await Application.Current.MainPage.Navigation.PushAsync(new AddMatePage()));
            });
        }

        //Selected item value
        Mate selectedMate;
        public Mate SelectedMate
        { 
            get => selectedMate;
            set
            {
                selectedMate = value;
            }
        }

        //Mate list data value
        public List<Mate> MatesList { get; set; }

        //Command which called on item tap action
        public Command ItemDetails { get; }

        //Command which called on add button pressed
        public Command Add { get; }

        //Part of parental interface
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
