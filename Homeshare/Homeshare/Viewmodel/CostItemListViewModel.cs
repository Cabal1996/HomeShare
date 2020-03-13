using Homeshare.DB;
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
    class CostItemListViewModel : INotifyPropertyChanged
    {
        public CostItemListViewModel(AddSpendViewModel spendViewModel)
        {
            if(DBController.TableExists("CostItem"))
            {
                //Retrieving data from data base table as a list
                CostItemSharableList = DBController.GetInfo<CostItem>();
            }
            else
            {
                Application.Current.MainPage.Navigation.PushAsync(new AddCostItemPage()); 
            }
            
            //Construction of go to item select Cost item in spend VM or details page. Command declared below
            SelectItemCmd = new Command(async () => 
            {
                if(spendViewModel != null)
                {
                    spendViewModel.SelectedCost = SelectedCostItem;
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    //await Application.Current.MainPage.Navigation.PushAsync(new SharableInfoPage(SelectedSharable));
                }
            });;

            //Construction of go to add new cost item page command with declared below
            Add = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new AddCostItemPage());
            });
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

        //Cost list data value
        public List<CostItem> CostItemSharableList { get; set; }

        //Command which called on item tap action
        public Command SelectItemCmd { get; }

        //Command which called on add button pressed
        public Command Add { get; }

        //Part of parental interface
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
