﻿using Homeshare.DB;
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
    class AddCostItemViewModel : ViewModelBase , INotifyPropertyChanged
    {
        public AddCostItemViewModel()
        {
            //Construction of command with declared below
            AddButton = new Command(() =>
            {
                

                RapidTapPreventorAsync(async () =>
                {
                    AddNewCostItemToDatabase();
                    // return to previous page
                    await Application.Current.MainPage.Navigation.PopAsync();
                }); 
            });
        }

        //Field of "sharable" name value
        private string costItemName;
        public string CostItemName
        {
            set { costItemName = value; }

            get { return costItemName; }
        }


        private void  AddNewCostItemToDatabase()
        {

            //Constructing "Cost Item" table item
            CostItem TableItem = new CostItem
            {
                Name = CostItemName,
            };

            //Check existence of a table
            if (DBController.TableExists(nameof(CostItem)))
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

        //Command which called on add button pressed
        public Command AddButton { get; }

        //Part of parental interface
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
