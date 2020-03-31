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
    class SpendsDetailsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private const float DAY_IN_SECONDS = 86400;

        public SpendsDetailsViewModel()
        {
            //Setting date period to current month
            FromDate = new DateTime((DateTime.Today).Year, (DateTime.Today).Month, 1);
            ToDate = (DateTime.Today).AddSeconds(DAY_IN_SECONDS - 1);
        }

        public override void OnVMPop()
        {
            SelectedCostItem = (CostItem)SharedData;
        }

        public SpendsDetailsViewModel(SpendsViewModel inData)
        {
            //Initializing Lists
            DisplayList = new List<ViewItem>();
            displayList = new List<ViewItem>();

            //Setting date period to current month
            FromDate = inData.FromDate;
            ToDate = inData.ToDate;

            SpentsList = inData.SpentsList;

            SelectedCostItem = inData.SelectedSpent;

            Initialization();
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


        private Command refresh;
        //Command for refreshing page
        public Command Refresh
        {
            get
            {
                return refresh
                       ?? (refresh = new Command(
                           () =>
                           {
                               RapidTapPreventor(Initialization);
                           }));
            }
        }

        private Command pickCostItem;
        //Command which called on Cost item button pressed
        public Command PickCostItem
        {
            get
            {
                return pickCostItem
                       ?? (pickCostItem = new Command(
                           () =>
                           {
                               RapidTapPreventorAsync(async () => await Application.Current.MainPage.Navigation.PushAsync(new CostItemListPage(this)));
                           }));
            }
        }

        private string selectedCostItemName;
        public string SelectedCostItemName
        {
            get
            {
                if (SelectedCostItem == null)
                    return "Press to Select Cost Item";
                else
                    return selectedCostItemName;
            }

            set
            {
                selectedCostItemName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCostItem)));
            }
        }

        private CostItem selectedCostItem;

        public CostItem SelectedCostItem
        {
            get => selectedCostItem;
            
            set
            {
                selectedCostItem = value;
                SelectedCostItemName = value.Name;
            }
        }

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

        // Search data in db and prepare ti show it
        private void Initialization()
        {
            if (SelectedCostItem == null)
                return;

            SpentsList = DBController.GetInfo<CostTable>();
            List<ViewItem> tempList = new List<ViewItem>();

            foreach (var I in SpentsList)
            {
                if(IsInDataRange(I.Date) && I.CostItemId == SelectedCostItem.Id)
                {
                    ViewItem DataItem = new ViewItem
                    {
                        Subconto1 = I.Date.ToString(),
                        Subconto2 = I.Value.ToString()
                    };

                    tempList.Add(DataItem);
                }
            }

            DisplayList = tempList;
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
