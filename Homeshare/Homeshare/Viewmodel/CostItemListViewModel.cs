﻿using Homeshare.DB;
using Homeshare.Model;
using Homeshare.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

/*
 *  View-model of a Cost items list page 
 */

namespace Homeshare.Viewmodel
{

    class CostItemListViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public CostItemListViewModel(ViewModelBase Caller)
        {
            
            //Construction of go to item select Cost item in spend VM or details page. Command declared below
            SelectItemCmd = new Command(() =>
            {
                Caller.SharedData = SelectedCostItem;

                Caller.OnVMPop();

                RapidTapPreventorAsync(async () => await Application.Current.MainPage.Navigation.PopAsync());
            });
           


            //Construction of go to add new cost item page command with declared below
            Add = new Command(() =>
            {
                RapidTapPreventorAsync(async () => await Application.Current.MainPage.Navigation.PushAsync(new AddCostItemPage()));
            });

            Refresh = new Command(Initialization);
        }

        //Selected item value
        CostItem selectedCostItem;
        public CostItem SelectedCostItem 
        { 
            get => selectedCostItem; 
            set
            {
                selectedCostItem = value;
            }
        }

        public void Initialization()
        {
            Console.WriteLine("Hello!");

            if (DBController.TableExists("CostItem"))
            {
                //Retrieving data from data base table as a list
                CostItemList = DBController.GetInfo<CostItem>();
            }
            else
            {
                Application.Current.MainPage.Navigation.PushAsync(new AddCostItemPage());
            }
        }


        private List<CostItem> list;
        //Cost list data value
        public List<CostItem> CostItemList
        {
            get { return list; }
            set
            {
                list = new List<CostItem>(value);

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CostItemList)));
            }
        }

        //Command which called on item tap action
        public Command SelectItemCmd { get; }

        //Command which called on add button pressed
        public Command Add { get; }

        public Command Refresh { get; }

        //Part of parental interface
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
