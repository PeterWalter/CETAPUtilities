using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Data.Entity.Spatial;
using CETAPUtilities.Model;

namespace CETAPUtilities.BDO
{
    public class VenueBDO : ModelBase
    {

        public byte[] RowVersion { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime DateModified { get; set; }
        public DbGeography Place { get; set; }


        /// <summary>
        /// The <see cref="VenueCode" /> property's name.
        /// </summary>
        public const string VenueCodePropertyName = "VenueCode";

        private int _code;

        /// <summary>
        /// Sets and gets the VenueCode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public int VenueCode
        {
            get
            {
                return _code;
            }

            set
            {
                if (_code == value)
                {
                    return;
                }


                _code = value;


                if (_code < 1)
                {
                    AddError(VenueCodePropertyName, "Venue Codes should be positive numbers");
                }
                else
                {
                    RemoveError(VenueCodePropertyName);
                }
                RaisePropertyChanged(VenueCodePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="VenueName" /> property's name.
        /// </summary>
        public const string VenueNamePropertyName = "VenueName";

        private string _venue;

        /// <summary>
        /// Sets and gets the VenueName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string VenueName
        {
            get
            {
                return _venue;
            }

            set
            {
                if (_venue == value)
                {
                    return;
                }

                _venue = value;
                if (string.IsNullOrWhiteSpace(_venue) == true)
                {
                    AddError(VenueNamePropertyName, "Venue Name is required");
                }
                else
                {
                    RemoveError(VenueNamePropertyName);
                }

                RaisePropertyChanged(VenueNamePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ShortName" /> property's name.
        /// </summary>
        public const string ShortNamePropertyName = "ShortName";

        private string _sname;

        /// <summary>
        /// Sets and gets the ShortName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ShortName
        {
            get
            {
                return _sname;
            }

            set
            {
                if (_sname == value)
                {
                    return;
                }

                _sname = value;
                if (string.IsNullOrWhiteSpace(_venue) == true)
                {
                    AddError(ShortNamePropertyName, "Venue Name is required");
                }
                else
                {
                    RemoveError(ShortNamePropertyName);
                }


                RaisePropertyChanged(ShortNamePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="WebSiteName" /> property's name.
        /// </summary>
        public const string WebSiteNamePropertyName = "WebSiteName";

        private string _webname;

        /// <summary>
        /// Sets and gets the WebSiteName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string WebSiteName
        {
            get
            {
                return _webname;
            }

            set
            {
                if (_webname == value)
                {
                    return;
                }

                _webname = value;
                RaisePropertyChanged(WebSiteNamePropertyName);
            }
        }


        /// <summary>
        /// The <see cref="Room" /> property's name.
        /// </summary>
        public const string RoomPropertyName = "Room";

        private string _room;

        /// <summary>
        /// Sets and gets the Room property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Room
        {
            get
            {
                return _room;
            }

            set
            {
                if (_room == value)
                {
                    return;
                }

                _room = value;
                RaisePropertyChanged(RoomPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="VenueType" /> property's name.
        /// </summary>
        public const string VenueTypePropertyName = "VenueType";

        private string _venueType;

        /// <summary>
        /// Sets and gets the VenueType property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string VenueType
        {
            get
            {
                return _venueType;
            }

            set
            {
                if (_venueType == value)
                {
                    return;
                }

                _venueType = value;
                RaisePropertyChanged(VenueTypePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ProvinceID" /> property's name.
        /// </summary>
        public const string ProvinceIDPropertyName = "ProvinceID";

        private int _province;

        /// <summary>
        /// Sets and gets the ProvinceID property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int ProvinceID
        {
            get
            {
                return _province;
            }

            set
            {
                if (_province == value)
                {
                    return;
                }

                _province = value;
                RaisePropertyChanged(ProvinceIDPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Available" /> property's name.
        /// </summary>
        public const string AvailablePropertyName = "Available";

        private bool _available = false;

        /// <summary>
        /// Sets and gets the Available property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool Available
        {
            get
            {
                return _available;
            }

            set
            {
                if (_available == value)
                {
                    return;
                }

                _available = value;
                RaisePropertyChanged(AvailablePropertyName);
            }
        }


        /// <summary>
        /// The <see cref="Capacity" /> property's name.
        /// </summary>
        public const string CapacityPropertyName = "Capacity";

        private Nullable<int> _capacity;

        /// <summary>
        /// Sets and gets the Capacity property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Nullable<int> Capacity
        {
            get
            {
                return _capacity;
            }

            set
            {
                if (_capacity == value)
                {
                    return;
                }

                _capacity = value;
                RaisePropertyChanged(CapacityPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Description" /> property's name.
        /// </summary>
        public const string DescriptionPropertyName = "Description";

        private string _comments;

        /// <summary>
        /// Sets and gets the Description property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Description
        {
            get
            {
                return _comments;
            }

            set
            {
                if (_comments == value)
                {
                    return;
                }

                _comments = value;
                RaisePropertyChanged(DescriptionPropertyName);
            }
        }

        //public virtual ICollection<Batch> Batches { get; set; }
        //public virtual ICollection<Composit> Composits { get; set; }
        //public ProvinceBDO Province { get; set; }
        //public virtual ICollection<WriterList> WriterLists { get; set; }

        //public Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        //public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        //public IEnumerable GetErrors(string propertyName)
        //{
        //    if (String.IsNullOrEmpty(propertyName) || !this._errors.ContainsKey(propertyName))

        //        return null;
        //    return this._errors[propertyName];
        //}

        //public bool HasErrors
        //{
        //    get { return (this._errors.Count > 0); }
        //}

        ////object is valid
        //public bool IsValid
        //{
        //    get { return !this.HasErrors; }
        //}

        //public void AddError(string propertyName, string error)
        //{
        //    // add error to list
        //    this._errors[propertyName] = new List<string>() { error };
        //    this.NotifyErrorsChanged(propertyName);
        //}

        //public void RemoveError(string propertyName)
        //{
        //    // remove error
        //    if (this._errors.ContainsKey(propertyName))
        //        this._errors.Remove(propertyName);
        //    this.NotifyErrorsChanged(propertyName);
        //}

        //private void NotifyErrorsChanged(string propertyName)
        //{
        //    //notify
        //    if (this.ErrorsChanged != null)
        //        this.ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        //}
    }
}
