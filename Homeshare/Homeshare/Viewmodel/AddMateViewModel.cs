using Homeshare.DB;
using Homeshare.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

/*
 *  View-model of a "Add new mate" page 
 */

namespace Homeshare.Viewmodel
{
    class AddMateViewModel : INotifyPropertyChanged
    {
        public AddMateViewModel()
        {
            //Construction of command with declared below
            AddButton = new Command(async() =>
            {
                //Constructing "mate" table item
                Mate TableItem = new Mate
                {
                    FirstName = NewFirstName,
                    LastName = NewLastName,
                };

                //Check existence of a table
                if (DBController.TableExists(nameof(Mate)))
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

                // return to previous page
                await Application.Current.MainPage.Navigation.PopAsync();
                // TODO open "Mate" list page
            });
        }

        //Field of first value
        private string firstName;
        public string NewFirstName
        {
            set { firstName = value; }

            get { return firstName; }
        }

        //Field of last name value
        private string lastName;
        public string NewLastName
        {
            set { lastName = value; }

            get { return lastName; }
        }

        //Command which called on add button pressed
        public Command AddButton { get; }

        //Part of parental interface
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
