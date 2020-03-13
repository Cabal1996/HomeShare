using Homeshare.DB;
using Homeshare.Model;
using Homeshare.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

/*
 *  View-model of a "Add new sharable" page 
 */

namespace Homeshare.Viewmodel
{
    class AddSpendViewModel : INotifyPropertyChanged
    {
        public AddSpendViewModel()
        {
            if(SelectedCost == null)
            {
                SelectedCostName = "Press to Select Cost";
            }

            PostDate = DateTime.Now;
    
            //Construction of command with declared below
            CostSelectButton = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CostItemlistPage(this));
            });

            //Construction of command with declared below
            SpendButton = new Command(async() =>
            {
                //Constructing "Sharable" table item
                CostTable TableItem = new CostTable
                {
                    Date = PostDate,
                    CostItemId = SelectedCost.Id,
                    Value = sum
                };

                //Check existence of a table
                if (DBController.TableExists(nameof(CostTable)))
                {
                    //Insertion table item into the table
                    DBController.InsertItem(TableItem);
                }
                else
                {
                    //Creation of a table then insertion item into it
                    DBController.AddTable(TableItem);
                    DBController.InsertItem(TableItem);
                }

                // Go To Expense list page
                //TODO await Application.Current.MainPage.Navigation.PopAsync(); 

            });
        }

        //Field of "Cost item" name value
        private string selectedCostName;
        public string SelectedCostName
        {
            set 
            {
                selectedCostName = value;
                //Value change notifier
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCostName)));
            }

            get { return selectedCostName; }
        }

        //Field of "sharable" type value
        private float sum;
        //numeric representation of enumerated value (declared below)
        public string Sum
        {
            set { sum = float.Parse(value); }
        }

        //Date of transaction
        private DateTime date;
        //Bind-able value of date
        public DateTime PostDate
        {
            set 
            { 
                date = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PostDate)));
            }

            get { return date; }
        }

        private CostItem selectedCost;
        public CostItem SelectedCost
        { 
            get
            {
                return selectedCost;
            }

            set
            {
                if(value != null)
                SelectedCostName = value.Name;
                selectedCost = value;
            }
        }

        //Command which called on add button pressed
        public Command SpendButton { get; }
        //Command which called on Select Cost button pressed
        public Command CostSelectButton { get; }

        //Part of parental interface
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
