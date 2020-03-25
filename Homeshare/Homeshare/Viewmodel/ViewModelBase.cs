using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Homeshare.Viewmodel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        bool isInCall = false;
        private object syncLock = new object();

        //Prevents multiple rapid call of in function (asynchronous version)
        protected async void RapidTapPreventorAsync(Func<Task<Page>> inFunction)
        {
            lock (syncLock)
            {
                if (isInCall)
                    return;
                isInCall = true;
            }

            try
            {
                await inFunction();
            }
            finally
            {
                lock (syncLock)
                {
                    isInCall = false;
                }
            }
        }

        protected async void RapidTapPreventorAsync(Func<Task> inFunction)
        {
            lock (syncLock)
            {
                if (isInCall)
                    return;
                isInCall = true;
            }

            try
            {
                await inFunction();
            }
            finally
            {
                lock (syncLock)
                {
                    isInCall = false;
                }
            }
        }

        //Prevents multiple rapid call of in function (synchronous version)
        protected void RapidTapPreventor(Action inFunction)
        {
            lock (syncLock)
            {
                if (isInCall)
                    return;
                isInCall = true;
            }

            try
            {
                inFunction();
            }
            finally
            {
                lock (syncLock)
                {
                    isInCall = false;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
