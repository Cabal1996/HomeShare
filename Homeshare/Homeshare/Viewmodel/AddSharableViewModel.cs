using Homeshare.DB;
using Homeshare.Model;
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
    class AddSharableViewModel : ViewModelBase
    {
        public AddSharableViewModel()
        {
            //Construction of command with declared below
            AddButton = new Command(() =>
            {
                RapidTapPreventorAsync(async () =>
                {
                    AddNewItemToDatabase();

                    // return to previous page
                    await Application.Current.MainPage.Navigation.PopAsync();

                    // TODO open "Sharable" list page
                });
            });
        }

       

        //Field of "sharable" name value
        private string sharableName;
        public string SharableName
        {
            set { sharableName = value; }

            get { return sharableName; }
        }

        //Field of "sharable" type value
        private string sharableType;
        //numeric representation of enumerated value (declared below)
        public int SharableType
        {
            set {  sharableType = ((ESharableType)value).ToString(); }
        }

        //Field of "sharable" periodicity value
        private string periodicity;
        //numeric representation of enumerated value (declared below)
        public int Periodicity
        {
            set { periodicity = ((EPeriodicitys)value).ToString(); }
        }

        //Field of "Sharable" price
        private string price;
        public string Price
        {
            set { price = value; }

            get { return price; }
        }

        //Command which called on add button pressed
        public Command AddButton { get; }

        private void AddNewItemToDatabase()
        {
            //Constructing "Sharable" table item
            Sharable TableItem = new Sharable
            {
                Name = SharableName,
                Type = sharableType,
                Periodicity = periodicity,
                Price = Price
            };

            //Check existence of a table
            if (DBController.TableExists(nameof(Sharable)))
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
        }
    }

    //Enumerated value for type
    public enum ESharableType : int
    {
        Fixed,
        Mutable
    }

    //Enumerated value for periodicity
    public enum EPeriodicitys : int
    {
        Daily,
        Weekly,
        Monthly,
        Anualy
    }
}
