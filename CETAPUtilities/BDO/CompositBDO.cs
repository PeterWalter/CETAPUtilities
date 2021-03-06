﻿using System;

namespace CETAPUtilities.BDO
{
    public class CompositBDO
    {
        public long RefNo { get; set; }
        public long Barcode { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Initials { get; set; }
        public Nullable<long> SAID { get; set; }
        public string ForeignID { get; set; }
        public System.DateTime DOB { get; set; }
        public string ID_Type { get; set; }
        public Nullable<int> Citizenship { get; set; }
        public string Classification { get; set; }
        public string Gender { get; set; }
        public string Faculty { get; set; }
        public System.DateTime DOT { get; set; }
        public int VenueCode { get; set; }
        public string VenueName { get; set; }
        public Nullable<int> HomeLanguage { get; set; }
        public string GR12Language { get; set; }
        public string AQLLanguage { get; set; }
        public Nullable<int> AQLCode { get; set; }
        public string Batch { get; set; }
        public string MatLanguage { get; set; }
        public Nullable<int> MatCode { get; set; }

        public string WroteAL { get; set; }
        public string WroteQL { get; set; }
        public string WroteMat { get; set; }

        public Nullable<int> ALScore { get; set; }
        public string ALLevel { get; set; }
        public Nullable<int> QLScore { get; set; }
        public string QLLevel { get; set; }
        public Nullable<int> MATScore { get; set; }
        public string MATLevel { get; set; }

        public string Faculty2 { get; set; }
        public string Faculty3 { get; set; }
        public System.Guid RowGuid { get; set; }
        public byte[] RowVersion { get; set; }
        public System.DateTime DateModified { get; set; }
    }
}
