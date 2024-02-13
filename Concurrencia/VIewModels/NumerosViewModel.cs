using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Concurrencia.VIewModels
{
    public class NumerosViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public NumerosViewModel()
        {
            SumarCommand = new RelayCommand(SumarAsync);
        }

        private void SumarSincrono()
        {
            long suma = 0;
            for (long i = 0; i < 1000000000; i++)
            {
                suma += i;
            }
            Suma = suma;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Suma)));
        }
        private async void SumarAsync()
        {
            _= Task.Run(() =>
            {
                long suma = 0;
                for (long i = 0; i < 1000000000; i++)
                {
                    suma += i;
                }
                Suma = suma;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Suma)));
            });
            
           
        }
        public ICommand SumarCommand { get; set; }  
        public long Suma { get; private set; }




    }
}
