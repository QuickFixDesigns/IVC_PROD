using FirstFloor.ModernUI.Windows.Controls;
using NeoTracker.DAL;
using NeoTracker.Pages.Dialogs;
using NeoTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using static NeoTracker.ViewModels.MainViewModel;

namespace NeoTracker.Models
{
    public class OrderViewModel : ViewModelBase
    {
        private string _Code;
        public string Code
        {
            get { return _Code; }
            set { SetProperty(ref _Code, value); }
        }
        private string _Client;
        public string Client
        {
            get { return _Client; }
            set { SetProperty(ref _Client, value); }
        }
        private string _Po;
        public string Po
        {
            get { return _Po; }
            set { SetProperty(ref _Po, value); }
        }
        private DateTime? _Date;
        public DateTime? Date
        {
            get { return _Date; }
            set { SetProperty(ref _Date, value); }
        }
    }
}
