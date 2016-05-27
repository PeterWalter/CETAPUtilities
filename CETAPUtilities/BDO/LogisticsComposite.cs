using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CETAPUtilities.BDO
{
    public class LogisticsComposite
    {
        public string SessionId { get; set; }
        public string NBT { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleInitials { get; set; }
        public string SouthAfricanID { get; set; }
        public string Passport { get; set; } // same as foreign ID
        public string Birth { get; set; }
        public string WroteAL { get; set; }
        public string WroteQL { get; set; }
        public string WroteMat { get; set; }
        public string StudentNo { get; set; }
        public string Faculty { get; set; }
        public string Programme { get; set; }
        public string TestDate { get; set; }
        public string Venue { get; set; }
        public string Sex { get; set; }
        public string ALScore { get; set; }
        public string ALLevel { get; set; }
        public string QLScore { get; set; }
        public string QLLevel { get; set; }
        public string MatScore { get; set; }
        public string MatLevel { get; set; }
        public string AQLLanguage { get; set; }
        public string MatLanguage { get; set; }
        public string TestType { get; set; }
        public string VenueProvince { get; set; }
    }
}
