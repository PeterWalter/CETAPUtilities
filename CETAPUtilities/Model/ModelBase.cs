using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.Collections;

namespace CETAPUtilities.Model
{
    public class ModelBase : ObservableObject, INotifyDataErrorInfo
    {
        public Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors
        {
            get { return (this._errors.Count > 0); }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName) || !this._errors.ContainsKey(propertyName))

                return null;
            return this._errors[propertyName];
        }
        private void NotifyErrorsChanged(string propertyName)
        {
            //notify
            if (this.ErrorsChanged != null)
                this.ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public void RemoveError(string propertyName)
        {
            // remove error
            if (this._errors.ContainsKey(propertyName))
                this._errors.Remove(propertyName);
            this.NotifyErrorsChanged(propertyName);
        }

        //object is valid
        public bool IsValid
        {
            get { return !this.HasErrors; }
        }

        public void AddError(string propertyName, string error)
        {
            // add error to list
            this._errors[propertyName] = new List<string>() { error };
            this.NotifyErrorsChanged(propertyName);
        }
    }
}
