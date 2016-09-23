using System;
using GalaSoft.MvvmLight;

namespace CETAPUtilities.ViewModel
    {
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class WelcomePageViewModel :InstitutionMatchPageViewModelBase
        {
        /// <summary>
        /// Initializes a new instance of the WelcomePageViewModel class.
        /// </summary>
        public WelcomePageViewModel():base(null)
            {
            
            }

        public override string DisplayName
            {
            get
                {
                    return "Welcome to Scores Matching Page";
                }
            }

        internal override bool IsValid()
            {
            return true; 
            }
        }
    }