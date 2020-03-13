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
    class SpendsViewModel : INotifyPropertyChanged
    {
        public SpendsViewModel()
        {
            //Retrieving data from data base table as a list
            SpentsList = DBController.GetInfo<CostTable>();
            DisplayList = new List<ViewItem>();
            displayList = new List<ViewItem>();

            FromDate = new DateTime((DateTime.Today).Year, (DateTime.Today).Month, 1);

            ToDate = DateTime.Today;

            //Construction of go to item details page command with declared below
            ItemDetails = new Command(async () => 
            {
                //await Application.Current.MainPage.Navigation.PushAsync(new SharableInfoPage(SelectedSharable));
            });

            //Construction of go to add Spend page command with declared below
            Add = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new AddSpendPage());
            });

            Refresh = new Command(() =>
           {
               RefreshFoo();
           });
        }

        //Selected item value
        CostTable selectedSpent;
        public CostTable SelectedSpent 
        { 
            get => selectedSpent; 
            set
            {
                selectedSpent = value;
            }
        }

        //Actual list data value
        public List<CostTable> SpentsList { get; set; }

        private List<ViewItem> displayList;
        //Collection to display
        public List<ViewItem> DisplayList 
        {
            get  { return displayList; } 
            set
            {
                displayList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayList)));
            } 
        }
 
        //Command which called on item tap action
        public Command ItemDetails { get; }

        //Command for refreshing page
        public Command Refresh { get; }

        //Command which called on add button pressed
        public Command Add { get; }

        private DateTime toDate;
        public DateTime ToDate
        {
            set
            {
                toDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToDate)));
            }

            get
            {
                return toDate;
            }
        }

        private DateTime fromDate;
        public DateTime FromDate
        {
            set
            {
                fromDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FromDate)));
            }

            get
            {
                return fromDate;
            }
        }

        //Part of parental interface
        public event PropertyChangedEventHandler PropertyChanged;

        public void RefreshFoo()
        {
            List<ViewItem> TempList = new List<ViewItem>();
            List<CostItem> CostsNames = DB.DBController.GetInfo<CostItem>();

            foreach (var I in SpentsList)
            {
                if (IsInDataRange(I.Date, FromDate, ToDate))
                {
                    ViewItem Item = new ViewItem();

                    foreach (var J in CostsNames)
                    {
                        if (I.CostItemId == J.Id)
                        {
                            bool isInList = false;
                            for(int K = 0; K < TempList.Count; K++)
                            {
                                if(TempList[K].Subconto1 == J.Name)
                                {
                                    float t = float.Parse(TempList[K].Subconto2) + I.Value;
                                    TempList[K].Subconto2 = t.ToString();
                                    isInList = true;
                                }
                            }
                            
                            if(!isInList)
                            {
                                Item.Subconto1 = J.Name;
                                Item.Subconto2 = I.Value.ToString();
                                TempList.Add(Item);
                            }

                        }
                    }
                }
            }

            DisplayList = TempList;
        }

        static bool IsInDataRange(DateTime data, DateTime min, DateTime max)
        {
            return min < data && data < max;
        }
    }
}
