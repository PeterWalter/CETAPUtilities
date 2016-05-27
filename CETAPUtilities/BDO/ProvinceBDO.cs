using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CETAPUtilities.Model;

namespace CETAPUtilities.BDO
{
    public class ProvinceBDO : ModelBase
    {

        /// <summary>
        /// The <see cref="Id" /> property's name.
        /// </summary>
        public const string IdPropertyName = "Id";

        private int _myID;

        /// <summary>
        /// Sets and gets the Id property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Id
        {
            get
            {
                return _myID;
            }

            set
            {
                if (_myID == value)
                {
                    return;
                }

                _myID = value;
                RaisePropertyChanged(IdPropertyName);
            }
        }
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// The <see cref="Name" /> property's name.
        /// </summary>
        public const string NamePropertyName = "Name";

        private string _name;

        /// <summary>
        /// Sets and gets the Name property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (_name == value)
                {
                    return;
                }

                _name = value;
                RaisePropertyChanged(NamePropertyName);
            }
        }
        public int Code { get; set; }
        public override string ToString()
        {
            return Name;
        }

        // public virtual ICollection<TestVenue> TestVenues { get; set; }
    }
}
