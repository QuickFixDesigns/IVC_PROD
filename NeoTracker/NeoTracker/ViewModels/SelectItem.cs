using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoTracker.ViewModels
{
    public class SelectItem : ViewModelBase
    {
        private int _Value;
        public int Value
        {
            get { return _Value; }
            set { SetProperty(ref _Value, value); }
        }
        private string _text;
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }
        private bool _IsSelected;
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { SetProperty(ref _IsSelected, value); }
        }
    }
}
