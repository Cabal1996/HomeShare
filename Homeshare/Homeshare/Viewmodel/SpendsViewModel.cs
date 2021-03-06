﻿using Homeshare.DB;
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
    class SpendsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private const float DAY_IN_SECONDS = 86400;
        public SpendsViewModel()
        {
            //Initializing Lists
            DisplayList = new List<ViewItem>();
            displayList = new List<ViewItem>();

            FromDate = new DateTime((DateTime.Today).Year, (DateTime.Today).Month, 1);

            ToDate = (DateTime.Today).AddSeconds(DAY_IN_SECONDS - 1);

            //Construction of go to item details page command with declared below
            ItemDetails = new Command(() => 
            {
                RapidTapPreventorAsync(async () => await Application.Current.MainPage.Navigation.PushAsync(new SpendsListDetailePage(this)));
            });

            //Construction of go to add Spend page command with declared below
            Add = new Command(() =>
            {
                RapidTapPreventorAsync(async () => await Application.Current.MainPage.Navigation.PushAsync(new AddSpendPage()));
            });

            Refresh = new Command(Initialization);
        }

        //Selected item value
        CostItem selectedSpent;
        public CostItem SelectedSpent 
        { 
            get => selectedSpent; 
            set
            {
                selectedSpent = value;
            }
        }

        public ViewItem SelectedTableRow
        {
            set 
            {
                SelectedSpent = ConvertViewItemToCostItem(value);
            }
        }

        //Actual list data value
        public List<CostTable> SpentsList { get; set; }

        private List<ViewItem> displayList;
        //Collection to display
        public List<ViewItem> DisplayList
        {
            get { return displayList; } 
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

        private void Initialization()
        {
            SpentsList = DBController.GetInfo<CostTable>();
            List<ViewItem> TempList = new List<ViewItem>();
            List<CostItem> CostsNames = DBController.GetInfo<CostItem>();
            


            foreach (var I in SpentsList)
            {
                if (IsInDataRange(I.Date))
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

        private bool IsInDataRange(DateTime data)
        {
            if (FromDate == null)
                return data <= ToDate;
            if (ToDate == null)
                return FromDate <= data;
            if (FromDate == null && ToDate == null)
                return true;
            return (FromDate <= data && data <= ToDate);
        }

        private static CostItem ConvertViewItemToCostItem(ViewItem InData)
        {
            List<CostItem> AllCosts = DBController.GetInfo<CostItem>();

            foreach (var I in AllCosts)
            {
                if(I.Name == InData.Subconto1)
                    return I;
            }

            return null;
        }

        //Part of parental interface
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
