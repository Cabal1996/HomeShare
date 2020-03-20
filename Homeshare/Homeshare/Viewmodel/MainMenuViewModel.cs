using Homeshare.Model;
using Homeshare.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using Xamarin.Forms;
using Homeshare.DB;

/*
 *  View-model of a main menu page 
 */

namespace Homeshare.Viewmodel
{
    class MainMenuViewModel : INotifyPropertyChanged
    {
        public MainMenuViewModel()
        {
            //Construction of go to calculation page command with declared below
            QuickCalcCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new  QuickCalcPage());
            });

            //Construction of add sharable button command with declared below
            SharableCmd = new Command(async () =>
            {
                
                //Check if sharable table exists within data base
                if(DBController.TableExists(nameof(Homeshare.Model.Sharable)))
                {
                    //Go to sharable list page (table exists)
                    await Application.Current.MainPage.Navigation.PushAsync((Page)new SharablesListPage());
                }
                else
                {
                    //Go to Add sharable page (table does not exists)
                    await Application.Current.MainPage.Navigation.PushAsync((Page)new AddSharablePage());
                }
            });

            //Construction of add mate button command with declared below
            MateCmd = new Command(async () =>
            {
                //Check if "Mate" table exists within data base
                if (DBController.TableExists(nameof(Mate)))
                {
                    //Go to mate list page (table exists)
                    await Application.Current.MainPage.Navigation.PushAsync(new MatesListPage());
                }
                else
                {
                    //Go to Add mate page (table does not exists)
                    await Application.Current.MainPage.Navigation.PushAsync(new AddMatePage());
                }
            });

            //Construction of Clear base button command with declared below
            ClearBaseCmd = new Command(DBController.DeleteDatabase);

            //Construction of Post spend button command with declared below
            PostSpendCmd = new Command(async () =>
            {
                if (DBController.TableExists(nameof(CostTable)))
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new SpendsListPage());
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new AddSpendPage());
                }
            });
        }

        //Command with called on Quick Calculate button press
        public Command QuickCalcCommand { get; }

        //Command with called on Sharable button press
        public Command SharableCmd { get; }

        //Command with called on Mate button press
        public Command MateCmd { get; }

        //Command with clear up all data from database
        public Command ClearBaseCmd { get; }

        //Command with called on PostSpend button press
        public Command PostSpendCmd { get; }

        //Part of parental interface
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
