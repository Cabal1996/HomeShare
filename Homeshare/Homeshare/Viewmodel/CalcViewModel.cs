using Homeshare.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

/*
 *  View-model of a calculation page 
 */

namespace Homeshare.Viewmodel
{
    public class CalcViewModel : ViewModelBase
    {
        public CalcViewModel()
        {
          
        }

        //Sum value
        float Sum;
        //Sum UI representation
        public string TottalPrice
        {
            set
            {
                Sum = float.Parse(value);
            }
        }

        //Amount of days in period
        int Days;
        //Period UI representation
        public string TottalPeriod
        {
            set
            {
                Days = int.Parse(value);
            }
        }

        //Command of calculate button
        public Command Calculate
        {
            get
            {
                return new Command<List<Editor>>((MatesDaysData) =>
                {
                    RapidTapPreventorAsync(async () =>
                    {
                        //Go to result page and passing result value into page constructor
                        await Application.Current.MainPage.Navigation.PushAsync(new CalculateResaultPage(Calculation(MatesDaysData)));
                    });
                });
            }
        }

        //Actual calculation
        public List<float> Calculation(List<Editor> MatesDaysData)
        {

            
            // results list
            List<float> resalt = new List<float>();


            float deltaPrice = 0;
            int mateDaySumm = 0;

            //Summarization of all day values in intake data
            foreach (var mate in MatesDaysData)
            {
                mateDaySumm += int.Parse(mate.Text);
            }

            deltaPrice = Sum / (float)mateDaySumm;

            //Calculating and filling up result list
            foreach (var mate in MatesDaysData)
            {
                resalt.Add(deltaPrice * int.Parse(mate.Text));
            }

            return resalt;
        }

        //Part of parental interface
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
