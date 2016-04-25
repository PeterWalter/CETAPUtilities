using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CETAPUtilities.Model;

namespace CETAPUtilities.BDO
{
    public class UserBDO : ModelBase
    {
        /// <summary>
        /// The <see cref="StaffID" /> property's name.
        /// </summary>
        public const string StaffIDPropertyName = "StaffID";

        private string _staffno;

        /// <summary>
        /// Sets and gets the StaffID property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string StaffID
        {
            get
            {
                return _staffno;
            }

            set
            {
                if (_staffno == value)
                {
                    return;
                }

                _staffno = value;
                RaisePropertyChanged(StaffIDPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Name" /> property's name.
        /// </summary>
        public const string NamePropertyName = "Name";

        private string _username;

        /// <summary>
        /// Sets and gets the Name property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Name
        {
            get
            {
                return _username;
            }

            set
            {
                if (_username == value)
                {
                    return;
                }

                _username = value;
                RaisePropertyChanged(NamePropertyName);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
