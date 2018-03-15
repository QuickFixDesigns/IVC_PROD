using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NeoTracker.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IEditableObject
    {
        private DateTime _CreatedAt;
        public DateTime CreatedAt
        {
            get { return _CreatedAt; }
            set { SetProperty(ref _CreatedAt, value); }
        }
        private DateTime _UpdatedAt;
        public DateTime UpdatedAt
        {
            get { return _UpdatedAt; }
            set { SetProperty(ref _UpdatedAt, value); }
        }
        private string _UpdatedBy;
        public string UpdatedBy
        {
            get { return _UpdatedBy; }
            set { SetProperty(ref _UpdatedBy, value); }
        }
        private bool _IsActive;
        public bool IsActive
        {
            get { return _IsActive; }
            set { SetProperty(ref _IsActive, value); }
        }
        private bool _CanDelete;
        public bool CanDelete
        {
            get { return _CanDelete; }
            set { SetProperty(ref _CanDelete, value); }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;
            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        //for edit form
        Hashtable props = null;

        public void BeginEdit()
        {
            //enumerate properties
            PropertyInfo[] properties = (this.GetType()).GetProperties
                        (BindingFlags.Public | BindingFlags.Instance);

            props = new Hashtable(properties.Length - 1);

            for (int i = 0; i < properties.Length; i++)
            {
                //check if there is set accessor
                if (null != properties[i].GetSetMethod())
                {
                    object value = properties[i].GetValue(this, null);
                    props.Add(properties[i].Name, value);
                }
            }
        }
        public void EndEdit()
        {
            //delete current values
            props = null;
        }

        public void CancelEdit()
        {
            //check for inappropriate call sequence
            if (null == props) return;

            //restore old values
            PropertyInfo[] properties = (this.GetType()).GetProperties
                (BindingFlags.Public | BindingFlags.Instance);
            for (int i = 0; i < properties.Length; i++)
            {
                //check if there is set accessor
                if (null != properties[i].GetSetMethod())
                {
                    object value = props[properties[i].Name];
                    properties[i].SetValue(this, value, null);
                }
            }

            //delete current values
            props = null;
        }
    }
}
