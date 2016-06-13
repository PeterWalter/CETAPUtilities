using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using CETAPUtilities.BDO;
using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;
using CETAPUtilities.Helper;
using CETAPUtilities.Database;
using System.Data.Entity;
//using log4net.Config;

namespace CETAPUtilities.Model
{
    public class DataService : IDataService
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ObservableCollection<VenueBDO> AllVenues;
        ObservableCollection<CompositBDO> AllScores;

        public List<UserBDO> GetAllUsers()
        {
            List<UserBDO> myUsers = new List<UserBDO>();
               using (var context = new NBT_ProductionEntities())
                    try
                    {
                        List<User> DbUsers = context.Users.Select(x => x).ToList();
                        foreach (User person in DbUsers)
                        {
                            UserBDO M_User = new UserBDO();
                            UserDALToUserBDO(M_User, person);
                            myUsers.Add(M_User);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
            
            return myUsers;
        }

        public IntakeYear GetIntakeRecord(int year)
        {
            var myYear = new IntakeYear();
            using (var context = new NBT_ProductionEntities())
            {
                myYear = context.IntakeYears.Where(x => x.Year == year).Select(m => m).FirstOrDefault();
            }
            return myYear;
        }

        public List<IntakeYearsBDO> GetAllIntakeYears()
        {
            var periods = new List<IntakeYearsBDO>();
            using (var context = new NBT_ProductionEntities())
            {
                var period = context.IntakeYears.ToList();
              foreach(var yr in period)
                {
                   var yrBDO = new IntakeYearsBDO();
                    IntakeYearDalToIntakeYearBDO(yr, yrBDO);
                    periods.Add(yrBDO);
                }
            }
            return periods;
        }

        #region Translate
        static void IntakeYearDalToIntakeYearBDO(IntakeYear intakeDAL, IntakeYearsBDO intakeBDO)
        {
            intakeBDO.Year = intakeDAL.Year;
            intakeBDO.yearID = intakeDAL.yearID;
            intakeBDO.yearStart = intakeDAL.yearStart;
            intakeBDO.yearEnd = intakeDAL.yearEnd;
            intakeBDO.DateModified = intakeDAL.DateModified;
        }

        static void UserDALToUserBDO(UserBDO userBDO, User user)
        {
           // userBDO.Areas = user.Areas;
            userBDO.Name = user.Name;
            userBDO.StaffID = user.StaffID;
        }
        #endregion

        public async Task<bool> GetAllNBTScoresAsync(IntakeYear period)
        {

            log4net.Config.XmlConfigurator.Configure();
            // BasicConfigurator.Configure();
            bool isloaded = false;
            AllScores = new ObservableCollection<CompositBDO>();
            using (var context = new NBT_ProductionEntities())
            {
                var scores = await context.Composits.Where(x => x.DOT > period.yearStart && x.DOT < period.yearEnd).Select(m => m).ToListAsync();
                if (scores != null)
                {
                    isloaded = true;
                    foreach(var score in scores)
                    {
                        var NBTScore = new CompositBDO();
                        CompositDALToCompositBDO(NBTScore, score);
                        AllScores.Add(NBTScore);
                    }
                
                }
            }
            log.Info("Composite is loaded");
                return isloaded;
        }
        // generate collection of LOgistics data
        ObservableCollection<LogisticsComposite> LComposite = new ObservableCollection<LogisticsComposite>();
        private async Task CompositToLogisticsCompositeAsync()
        {
            if (AllScores != null)
            {
                var Compo = (from mydata in AllScores.AsParallel()
                             select new
                             {
                                 SessionID = mydata.Barcode.ToString(),
                                 NBTNumber = mydata.RefNo.ToString(),
                                 Surname = mydata.Surname,
                                 Name = mydata.Name,
                                 Initial = mydata.Initials,
                                 SouthAfricanID = mydata.SAID.ToString(),
                                 Passport = mydata.ForeignID,
                                 Birth = (mydata.SAID != null ? HelperUtils.DOBfromSAID(mydata.SAID.ToString()) :
                                 String.Format("{0:dd/MM/yyyy}", mydata.DOB)),

                                 W_AL = mydata.WroteAL,
                                 W_QL = mydata.WroteQL,
                                 W_Mat = mydata.WroteMat,
                                 StNo = "",
                                 Faculty = "",
                                 Programme = "",
                                 DateTest = String.Format("{0:yyyyMMdd}", mydata.DOT),
                                 Venue = mydata.VenueName,
                                 Sex = (mydata.Gender == "1" ? "M" :
                                        mydata.Gender == "2" ? "F" : mydata.Gender),

                                 ALScore = mydata.ALScore,
                                 ALLevel = mydata.ALLevel,
                                 QLScore = mydata.QLScore,
                                 QLLevel = mydata.QLLevel,
                                 MatScore = mydata.MATScore,
                                 MatLevel = mydata.MATLevel,
                                 AQL_Lang = mydata.AQLLanguage,
                                 Mat_Lang = mydata.MatLanguage,
                                 mm = mydata.Batch,
                                 TestType = MytestType(mydata.Batch),                                 
                                 province = getProvinceAsync(mydata.VenueCode)

                             });

                // create the collection
                foreach (var a in Compo)
                {
                    LogisticsComposite FC = new LogisticsComposite();


                    FC.SessionId = a.SessionID;
                    FC.NBT = a.NBTNumber;
                    FC.Surname = a.Surname;
                    FC.Name = a.Name;
                    FC.MiddleInitials = a.Initial;
                    FC.SouthAfricanID = a.SouthAfricanID;
                    FC.Passport = a.Passport;
                    FC.Birth = a.Birth;
                    FC.WroteAL = a.W_AL;
                    FC.WroteQL = a.W_QL;
                    FC.WroteMat = a.W_Mat;
                    FC.StudentNo = a.StNo;
                    FC.Faculty = a.Faculty;
                    FC.Programme = a.Programme;
                    FC.TestDate = a.DateTest;
                    FC.Venue = a.Venue;
                    FC.Sex = a.Sex;
                    FC.ALScore = a.ALScore.ToString();
                    FC.ALLevel = a.ALLevel;
                    FC.QLScore = a.QLScore.ToString();
                    FC.QLLevel = a.QLLevel;
                    FC.MatScore = a.MatScore.ToString();
                    FC.MatLevel = a.MatLevel;
                    FC.AQLLanguage = a.AQL_Lang;
                    FC.MatLanguage = a.Mat_Lang;
                    FC.TestType = a.TestType;
                    FC.VenueProvince = await a.province;
                    //FC.RefNo = a.RefNo;


                    LComposite.Add(FC);
                }
            }
        }
        private async Task<string> getProvinceAsync(int venuecode)
        {
            VenueBDO venue = AllVenues.Where(x => x.VenueCode == venuecode).FirstOrDefault();


            ProvinceBDO prov =  getProvinceByID(venue.ProvinceID);
            return prov.Name;
        }
        private string MytestType(string datfile)
        {
            datFileAttributes mydat = new datFileAttributes();
            
            mydat.SName = datfile;
            return mydat.Client;
        }
        public ProvinceBDO getProvinceByID(int code)
        {
            ProvinceBDO provinceBDO = null;
            int ProvID = code;
            using (var context = new NBT_ProductionEntities())
            {
                var prov = context.Provinces.Where(x => x.Code == ProvID).FirstOrDefault();

                if (prov != null)
                {
                    provinceBDO = new ProvinceBDO();
                    ProvinceToProvinceBDO(provinceBDO, prov);

                }

            }
            return provinceBDO;
        }

       
        ObservableCollection<FullComposite> FComposite = new ObservableCollection<FullComposite>();
        private async Task CompositToFullCompositeAsync()
        {
            if (AllScores != null)
            {
                var Compo = from mydata in AllScores.AsParallel()
                            select new
                            {
                                RefNo = mydata.RefNo.ToString(),
                                Barcode = mydata.Barcode.ToString(),
                                LastName = mydata.Surname,
                                FName = mydata.Name,
                                Initials = mydata.Initials,
                                SAID = HelperUtils.ToSAID(mydata.SAID),
                                FID = mydata.ForeignID,
                                DOB = String.Format("{0:yyyyMMdd}", mydata.DOB),
                                IDType = mydata.ID_Type,
                                Citizenship = (mydata.Citizenship != null ? mydata.Citizenship : null),
                                Classification = mydata.Classification,
                                Gender = mydata.Gender,
                                faculty1 = HelperUtils.GetFacultyName(mydata.Faculty),
                                Testdate = String.Format("{0:yyyyMMdd}", mydata.DOT),
                                VenueCode = mydata.VenueCode.ToString("D5"),
                                VenueName = mydata.VenueName,
                                Hlanguage = mydata.HomeLanguage,
                                G12L = mydata.GR12Language,
                                AQLLang = mydata.AQLLanguage,
                                AQLCode = mydata.AQLCode,
                                MatLang = mydata.MatLanguage,
                                MatCode = mydata.MatCode,
                                Faculty2 = HelperUtils.GetFacultyName(mydata.Faculty2),
                                Faculty3 = HelperUtils.GetFacultyName(mydata.Faculty3),
                                SessionID = mydata.Barcode.ToString(),
                                NBTNumber = mydata.RefNo.ToString(),
                                Surname = mydata.Surname,
                                Name = mydata.Name,
                                Initial = mydata.Initials,
                                SouthAfricanID = HelperUtils.ToSAID(mydata.SAID),
                                Passport = mydata.ForeignID,
                                Birth = (mydata.SAID != null ? HelperUtils.DOBfromSAID(mydata.SAID.ToString()) :
                                String.Format("{0:dd/MM/yyyy}", mydata.DOB)),

                                W_AL = mydata.WroteAL,
                                W_QL = mydata.WroteQL,
                                W_Mat = mydata.WroteMat,
                                StNo = "",
                                Faculty = "",
                                Programme = "",
                                DateTest = String.Format("{0:yyyyMMdd}", mydata.DOT),
                                Venue = mydata.VenueName,
                                Sex = (mydata.Gender == "1" ? "M" :
                                       mydata.Gender == "2" ? "F" : mydata.Gender),
                                street1 = "",
                                street2 = "",
                                Suburb = "",
                                City = "",
                                Province = "",
                                Postal = "",
                                Email = "",
                                Landline = "",
                                Mobile = "",
                                ALScore = mydata.ALScore,
                                ALLevel = mydata.ALLevel,
                                QLScore = mydata.QLScore,
                                QLLevel = mydata.QLLevel,
                                MatScore = mydata.MATScore,
                                MatLevel = mydata.MATLevel,
                                AQL_Lang = mydata.AQLLanguage,
                                Mat_Lang = mydata.MatLanguage

                            };
                
                foreach (var a in Compo)
                {
                    FullComposite FC = new FullComposite();

                    FC.RefNo = a.RefNo;
                    FC.Barcode = a.Barcode;
                    FC.LastName = a.LastName;
                    FC.FirstName = a.FName;
                    FC.Initials = a.Initials;
                    FC.SAID = a.SAID;
                    FC.FID = a.FID;
                    FC.DOB = a.DOB;
                    FC.IDType = a.IDType;
                    FC.Citizenship = a.Citizenship.ToString();
                    FC.Classification = a.Classification;
                    FC.Gender = a.Gender;
                    FC.Faculty1 = a.faculty1;
                    FC.DOT = a.Testdate;
                    FC.VenueCode = a.VenueCode;
                    FC.VenueName = a.VenueName;
                    FC.HomeLanguage = a.Hlanguage.ToString();
                    FC.SchLanguage = a.G12L;
                    FC.AQLLang = a.AQLLang;
                    FC.AQLCode = a.AQLCode.ToString();
                    FC.MATLang = a.MatLang;
                    FC.MATCode = a.MatCode.ToString();
                    FC.Faculty2 = a.Faculty2;
                    FC.Faculty3 = a.Faculty3;
                    FC.SessionId = a.SessionID;
                    FC.NBT = a.NBTNumber;
                    FC.Surname = a.Surname;
                    FC.Name = a.Name;
                    FC.MiddleInitials = a.Initial;
                    FC.SouthAfricanID = a.SouthAfricanID;
                    FC.Passport = a.Passport;
                    FC.Birth = a.Birth;
                    FC.WroteAL = a.W_AL;
                    FC.WroteQL = a.W_QL;
                    FC.WroteMat = a.W_Mat;
                    FC.StudentNo = a.StNo;
                    FC.Faculty = a.Faculty;
                    FC.Programme = a.Programme;
                    FC.TestDate = a.DateTest;
                    FC.Venue = a.Venue;
                    FC.Sex = a.Sex;
                    FC.Street1 = a.street1;
                    FC.Street2 = a.street2;
                    FC.Suburb = a.Suburb;
                    FC.City = a.City;
                    FC.Province = a.Province;
                    FC.Postal = a.Postal;
                    FC.Email = a.Email;
                    FC.Landline = a.Landline;
                    FC.Mobile = a.Mobile;
                    FC.ALScore = a.ALScore.ToString();
                    FC.ALLevel = a.ALLevel;
                    FC.QLScore = a.QLScore.ToString();
                    FC.QLLevel = a.QLLevel;
                    FC.MatScore = a.MatScore.ToString();
                    FC.MatLevel = a.MatLevel;
                    FC.AQLLanguage = a.AQL_Lang;
                    FC.MatLanguage = a.Mat_Lang;

                        FComposite.Add(FC);
                }
            }
        }
        private async Task LoadNBTVenuesAsync() // need to load all venues in memory to make processing of Composites faster
        {
            AllVenues = new ObservableCollection<VenueBDO>(GetAllvenues().OrderBy(x => x.VenueCode));

        }
        public List<VenueBDO> GetAllvenues()
        {
            List<VenueBDO> venuesBDO = new List<VenueBDO>();

                using (var context = new NBT_ProductionEntities())
                {
                    List<TestVenue> venues = (from a in context.TestVenues
                                              select a).ToList();
                    foreach (var v in venues)
                    {
                        VenueBDO vbdo = new VenueBDO();
                        TestVenueToVenueBDO(vbdo, v);
                        // vbdo.IsDirty = false;
                        //   vbdo.Province = getProvinceByID(vbdo.ProvinceID);

                        venuesBDO.Add(vbdo);
                    }

                }
                       
        return venuesBDO;
        }

        #region OpenXML

        // Open document for saving excel data
        public async Task<bool> SaveLargeExcelAsync(string folder, ObservableCollection<CompositBDO> composite, IntakeYearsBDO intake)
        {
            AllScores = composite;
           
           // string path = Path.GetDirectoryName(folder);
            bool isgen = false;
            string n = " n = " + AllScores.Count() + ".xlsx";
            string filename = intake.Year.ToString();
            filename += " intake cycle Composite ";
            // Get the date today
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            string myDay = dt.Year + dt.Month.ToString("00") + dt.Day.ToString("00") + "_" + dt.Hour.ToString("00") + dt.Minute.ToString("00");

            filename = filename + myDay + n;

            filename = Path.Combine(folder, filename);
            // make a copy of the template
            //File.Copy("intake cycle Composite.xlsx", filename, true);



            //using (SpreadsheetDocument SpDoc = SpreadsheetDocument.Open(filename, true))
            //{
            await CompositToFullCompositeAsync();
            composite.Clear();
            AllScores.Clear();
            
            // column heading for excel
            string[] ColumnHeadings = new[] {
                       
                            "Ref No", "Barcode", "Last Name", "First_Name", "INITIALS", "ID NUMBER",
                            "ID_Foreign", "Date of Birth", "ID Type", "Citizenship", "Classification",
                            "Gender 1", "Faculty 1", "DATE", "Test Centre Code", "Venue Name",
                            "Home Lang", "GR12 Language", "AQL LANG", "AQL CODE", "MAT LANG", "MAT CODE",
                            "Faculty 2", "Faculty 3", "Test Session ID", "NBT Reference", "Surname", "First Name",
                            "Middle Initials", "South African ID", "Foreign ID", "Date of Birth", "Wrote AL",
                            "Wrote QL", "Wrote Maths", "Student Number", "Faculty", "Programme", "Date_of_Test",
                            "Venue", "Gender", "Street and Number", "Street Name", "Suburb", "City/Town", "Province/Region",
                            "Postal Code", "e-mail Address", "Landline Number", "Mobile Number", "AL Score",
                            "AL Performance", "QL Score", "QL Performance", "Maths Score", "Maths Performance",
                            "AQL TEST LANGUAGE", "MATHS TEST LANGUAGE",

                           };

            // Size of spreadsheet
            var data = new[]
                        {
                                new {
                                    SheetName = "Composite",
                                    NumberCols = ColumnHeadings.Count(),
                                    Numberrows = FComposite.Count(),

                                    },

                           };

            // generate the  workbook and spreadsheets
            WorkbookDfn workbook = new WorkbookDfn
            {
                Worksheets = data
                            .Select(d => new WorksheetDfn
                                              {
                                                  Name = d.SheetName,
                                                  ColumnHeadings = Enumerable.Range(0, d.NumberCols)
                                                                       .Select(c => new CellDfn
                                                                       {
                                                                           Value = ColumnHeadings[c],
                                                                           Bold = true,
                                                                       }),
                                                  Rows = Enumerable.Range(0, d.Numberrows)
                                                                       .Select(r => new RowDfn
                                                                       {
                                                                           Cells = Enumerable.Range(0, d.NumberCols)
                                                                                        .Select(c =>

                                                                                               new CellDfn
                                                                                                 {
                                                                                                     Value = c == 0 ? FComposite.ElementAt(r).RefNo :
                                                                                                           c == 1 ? FComposite.ElementAt(r).Barcode :
                                                                                                           c == 2 ? FComposite.ElementAt(r).LastName :
                                                                                                           c == 3 ? FComposite.ElementAt(r).FirstName :
                                                                                                           c == 4 ? FComposite.ElementAt(r).Initials :
                                                                                                           c == 5 ? FComposite.ElementAt(r).SAID :
                                                                                                           c == 6 ? FComposite.ElementAt(r).FID :
                                                                                                           c == 7 ? FComposite.ElementAt(r).DOB :
                                                                                                           c == 8 ? FComposite.ElementAt(r).IDType :
                                                                                                           c == 9 ? FComposite.ElementAt(r).Citizenship :
                                                                                                           c == 10 ? FComposite.ElementAt(r).Classification :
                                                                                                           c == 11 ? FComposite.ElementAt(r).Gender :
                                                                                                           c == 12 ? FComposite.ElementAt(r).Faculty1 :
                                                                                                           c == 13 ? FComposite.ElementAt(r).DOT :
                                                                                                           c == 14 ? FComposite.ElementAt(r).VenueCode :
                                                                                                           c == 15 ? FComposite.ElementAt(r).VenueName :
                                                                                                           c == 16 ? FComposite.ElementAt(r).HomeLanguage :
                                                                                                           c == 17 ? FComposite.ElementAt(r).SchLanguage :
                                                                                                           c == 18 ? FComposite.ElementAt(r).AQLLang :
                                                                                                           c == 19 ? FComposite.ElementAt(r).AQLCode :
                                                                                                           c == 20 ? FComposite.ElementAt(r).MATLang :
                                                                                                           c == 21 ? FComposite.ElementAt(r).MATCode :
                                                                                                           c == 22 ? FComposite.ElementAt(r).Faculty2 :
                                                                                                           c == 23 ? FComposite.ElementAt(r).Faculty3 :
                                                                                                           c == 24 ? FComposite.ElementAt(r).SessionId :
                                                                                                           c == 25 ? FComposite.ElementAt(r).NBT :
                                                                                                           c == 26 ? FComposite.ElementAt(r).Surname :
                                                                                                           c == 27 ? FComposite.ElementAt(r).Name :
                                                                                                           c == 28 ? FComposite.ElementAt(r).MiddleInitials :
                                                                                                           c == 29 ? FComposite.ElementAt(r).SouthAfricanID :
                                                                                                           c == 30 ? FComposite.ElementAt(r).Passport :
                                                                                                           c == 31 ? FComposite.ElementAt(r).Birth :
                                                                                                           c == 32 ? FComposite.ElementAt(r).WroteAL :
                                                                                                           c == 33 ? FComposite.ElementAt(r).WroteQL :
                                                                                                           c == 34 ? FComposite.ElementAt(r).WroteMat :
                                                                                                           c == 35 ? FComposite.ElementAt(r).StudentNo :
                                                                                                           c == 36 ? FComposite.ElementAt(r).Faculty :
                                                                                                           c == 37 ? FComposite.ElementAt(r).Programme :
                                                                                                           c == 38 ? FComposite.ElementAt(r).TestDate :
                                                                                                           c == 39 ? FComposite.ElementAt(r).Venue :
                                                                                                           c == 40 ? FComposite.ElementAt(r).Sex :
                                                                                                           c == 41 ? FComposite.ElementAt(r).Street1 :
                                                                                                           c == 42 ? FComposite.ElementAt(r).Street2 :
                                                                                                           c == 43 ? FComposite.ElementAt(r).Suburb :
                                                                                                           c == 44 ? FComposite.ElementAt(r).City :
                                                                                                           c == 45 ? FComposite.ElementAt(r).Province :
                                                                                                           c == 46 ? FComposite.ElementAt(r).Postal :
                                                                                                           c == 47 ? FComposite.ElementAt(r).Email :
                                                                                                           c == 48 ? FComposite.ElementAt(r).Landline :
                                                                                                           c == 49 ? FComposite.ElementAt(r).Mobile :
                                                                                                           c == 50 ? FComposite.ElementAt(r).ALScore :
                                                                                                           c == 51 ? FComposite.ElementAt(r).ALLevel :
                                                                                                           c == 52 ? FComposite.ElementAt(r).QLScore :
                                                                                                           c == 53 ? FComposite.ElementAt(r).QLLevel :
                                                                                                           c == 54 ? FComposite.ElementAt(r).MatScore :
                                                                                                           c == 55 ? FComposite.ElementAt(r).MatLevel :
                                                                                                           c == 56 ? FComposite.ElementAt(r).AQLLanguage :
                                                                                                           c == 57 ? FComposite.ElementAt(r).MatLanguage : "",
                                                                                                     CellDataType =
                                                                                                                c == 7 ? CellDataType.String :
                                                                                                                c == 2 ? CellDataType.String :
                                                                                                                c == 3 ? CellDataType.String : CellDataType.String,
                                                                                                     HorizontalCellAlignment = c == 4 ? HorizontalCellAlignment.Center :
                                                                                                                             c == 33 ? HorizontalCellAlignment.Center :
                                                                                                                             c == 34 ? HorizontalCellAlignment.Center :
                                                                                                                             c == 32 ? HorizontalCellAlignment.Center :
                                                                                                                             c == 40 ? HorizontalCellAlignment.Center :
                                                                                                                             c == 18 ? HorizontalCellAlignment.Center :
                                                                                                                             c == 20 ? HorizontalCellAlignment.Center :
                                                                                                                             c == 50 ? HorizontalCellAlignment.Center :
                                                                                                                             c == 52 ? HorizontalCellAlignment.Center :
                                                                                                                             c == 54 ? HorizontalCellAlignment.Center :
                                                                                                                             c == 56 ? HorizontalCellAlignment.Center :
                                                                                                                             c == 57 ? HorizontalCellAlignment.Center : HorizontalCellAlignment.Left,
                                                                                                      // FormatCode = c == 5 ? "0000000000000" :"",


                                                                                                  })
                                                                       }),


                                              })

            };

            // stream write to excel file
            SpreadsheetWriter.Write(filename, workbook);
            // SpreadsheetWriter.Write()
            isgen = true;
            FComposite.Clear();
            FComposite = null;
            return isgen;
        }
        public async Task<bool> SaveLargeExcel2Async(string folder, ObservableCollection<CompositBDO> composite, IntakeYearsBDO intake)
        {
            AllScores = composite;
           // string path = Path.GetDirectoryName(myfile);
            bool isgen = false;
            string n = " n = " + AllScores.Count() + ".xlsx";
            string filename = intake.Year.ToString();
            filename += " intake cycle Logistics Composite ";
            // Get the date today
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            string myDay = dt.Year + dt.Month.ToString("00") + dt.Day.ToString("00") + "_" + dt.Hour.ToString("00") + dt.Minute.ToString("00");

            filename = filename + myDay + n;

            filename = Path.Combine(folder, filename);

            await LoadNBTVenuesAsync(); // loads all venues into memory

            //generate a collection of Logistics objects
            await CompositToLogisticsCompositeAsync();
            composite.Clear();
            AllScores.Clear();

            // column heading for excel
            string[] ColumnHeadings = new[]
                           {
                            "Test Session ID", "NBT Reference", "Surname", "First Name",
                            "Middle Initials", "South African ID", "Foreign ID", "Date of Birth", "Wrote AL",
                            "Wrote QL", "Wrote Maths", "Student Number", "Faculty", "Programme", "Date_of_Test",
                            "Venue", "Gender",  "AL Score",
                            "AL Performance", "QL Score", "QL Performance", "Maths Score", "Maths Performance",
                            "AQL TEST LANGUAGE", "MATHS TEST LANGUAGE", "Test Type", "Venue Province",

                           };

            // Size of spreadsheet
            var data = new[]
                            {
                                new {
                                    SheetName = "Logistics Composite",
                                    NumberCols = ColumnHeadings.Count(),
                                    Numberrows = LComposite.Count(),

                                    },

                           };

            // generate the  workbook and spreadsheets
            WorkbookDfn wb = new WorkbookDfn
            {
                Worksheets = data
                               .Select(d => new WorksheetDfn
                               {
                                   Name = d.SheetName,
                                   ColumnHeadings = Enumerable.Range(0, d.NumberCols)
                                                       .Select(c => new CellDfn
                                                       {
                                                           Value = ColumnHeadings[c],
                                                           Bold = true,
                                                       }),
                                   Rows = Enumerable.Range(0, d.Numberrows)
                                                       .Select(r => new RowDfn
                                                       {
                                                     
                                                           Cells = Enumerable.Range(0, d.NumberCols)
                                                                     .Select(c =>

                                                                             new CellDfn
                                                                             {
                                                                                 Value =
                                                                                         c == 0 ? LComposite.ElementAt(r).SessionId :
                                                                                         c == 1 ? LComposite.ElementAt(r).NBT :
                                                                                         c == 2 ? LComposite.ElementAt(r).Surname :
                                                                                         c == 3 ? LComposite.ElementAt(r).Name :
                                                                                         c == 4 ? LComposite.ElementAt(r).MiddleInitials :
                                                                                         c == 5 ? LComposite.ElementAt(r).SouthAfricanID :
                                                                                         c == 6 ? LComposite.ElementAt(r).Passport :
                                                                                         c == 7 ? LComposite.ElementAt(r).Birth :
                                                                                         c == 8 ? LComposite.ElementAt(r).WroteAL :
                                                                                         c == 9 ? LComposite.ElementAt(r).WroteQL :
                                                                                         c == 10 ? LComposite.ElementAt(r).WroteMat :
                                                                                         c == 11 ? LComposite.ElementAt(r).StudentNo :
                                                                                         c == 12 ? LComposite.ElementAt(r).Faculty :
                                                                                         c == 13 ? LComposite.ElementAt(r).Programme :
                                                                                         c == 14 ? LComposite.ElementAt(r).TestDate :
                                                                                         c == 15 ? LComposite.ElementAt(r).Venue :
                                                                                         c == 16 ? LComposite.ElementAt(r).Sex :

                                                                                         c == 17 ? LComposite.ElementAt(r).ALScore :
                                                                                         c == 18 ? LComposite.ElementAt(r).ALLevel :
                                                                                         c == 19 ? LComposite.ElementAt(r).QLScore :
                                                                                         c == 20 ? LComposite.ElementAt(r).QLLevel :
                                                                                         c == 21 ? LComposite.ElementAt(r).MatScore :
                                                                                         c == 22 ? LComposite.ElementAt(r).MatLevel :
                                                                                         c == 23 ? LComposite.ElementAt(r).AQLLanguage :
                                                                                         c == 24 ? LComposite.ElementAt(r).MatLanguage :
                                                                                         c == 25 ? LComposite.ElementAt(r).TestType :
                                                                                         c == 26 ? LComposite.ElementAt(r).VenueProvince : "",

                                                                                 HorizontalCellAlignment = c == 4 ? HorizontalCellAlignment.Center :
                                                                                                                  c == 8 ? HorizontalCellAlignment.Center :
                                                                                                                  c == 9 ? HorizontalCellAlignment.Center :
                                                                                                                  c == 10 ? HorizontalCellAlignment.Center :
                                                                                                                  c == 16 ? HorizontalCellAlignment.Center :
                                                                                                                  c == 17 ? HorizontalCellAlignment.Center :
                                                                                                                  c == 19 ? HorizontalCellAlignment.Center :
                                                                                                                  c == 21 ? HorizontalCellAlignment.Center :
                                                                                                                  c == 23 ? HorizontalCellAlignment.Center :
                                                                                                                  c == 24 ? HorizontalCellAlignment.Center : HorizontalCellAlignment.Left,
                                                                                 // FormatCode = c == 5 ? "0000000000000" :"",


                                                                             })
                                                       }),


                               })

            };

            // stream write to excel file
            SpreadsheetWriter.Write(filename, wb);
            // SpreadsheetWriter.Write()
            isgen = true;
            LComposite.Clear();
            LComposite = null;
            return isgen;
        }
        #endregion
        #region Translation
        static void CompositBDOToCompositDAL(CompositBDO compositBDO, Composit composit)
        {
            composit.ALScore = compositBDO.ALScore;
            composit.AQLCode = compositBDO.AQLCode;
            composit.AQLLanguage = compositBDO.AQLLanguage;
            composit.Barcode = compositBDO.Barcode;
            composit.Citizenship = compositBDO.Citizenship;
            composit.Classification = compositBDO.Classification;
            composit.DOB = compositBDO.DOB;
            composit.DOT = compositBDO.DOT;
            composit.ALLevel = compositBDO.ALLevel;
            composit.Faculty = compositBDO.Faculty;
            composit.Faculty2 = compositBDO.Faculty2;
            composit.Faculty3 = compositBDO.Faculty3;
            composit.ForeignID = compositBDO.ForeignID;
            composit.Gender = compositBDO.Gender;
            composit.GR12Language = compositBDO.GR12Language;
            composit.HomeLanguage = compositBDO.HomeLanguage.ToString();
            composit.ID_Type = compositBDO.ID_Type;
            composit.Initials = compositBDO.Initials;
            composit.MatCode = compositBDO.MatCode;
            composit.MatLanguage = compositBDO.MatLanguage;
            composit.MATLevel = compositBDO.MATLevel;
            composit.MATScore = compositBDO.MATScore;
            composit.Name = compositBDO.Name;
            composit.QLLevel = compositBDO.QLLevel;
            composit.QLScore = compositBDO.QLScore;
            composit.RefNo = compositBDO.RefNo;
            composit.RowGuid = compositBDO.RowGuid;
            composit.RowVersion = compositBDO.RowVersion;
            composit.SAID = compositBDO.SAID;
            composit.Surname = compositBDO.Surname;
            composit.VenueName = compositBDO.VenueName;
            composit.VenueCode = compositBDO.VenueCode;
            composit.WroteAL = compositBDO.WroteAL;
            composit.WroteQL = compositBDO.WroteQL;
            composit.WroteMat = compositBDO.WroteMat;
        }
        static void CompositDALToCompositBDO(CompositBDO compositBDO, Composit composit)
        {
            compositBDO.ALScore = composit.ALScore;
            compositBDO.AQLCode = composit.AQLCode;
            compositBDO.AQLLanguage = composit.AQLLanguage;
            compositBDO.Barcode = composit.Barcode;
            compositBDO.Citizenship = composit.Citizenship;
            compositBDO.Classification = composit.Classification;
            compositBDO.DOB = composit.DOB;
            compositBDO.DOT = composit.DOT;
            compositBDO.ALLevel = composit.ALLevel;
            compositBDO.Faculty = composit.Faculty;
            compositBDO.Faculty2 = composit.Faculty2;
            compositBDO.Faculty3 = composit.Faculty3;
            compositBDO.ForeignID = composit.ForeignID;
            compositBDO.Gender = composit.Gender;
            compositBDO.GR12Language = composit.GR12Language;
            compositBDO.HomeLanguage = (int?)Convert.ToInt32(composit.HomeLanguage);
            compositBDO.ID_Type = composit.ID_Type;
            compositBDO.Initials = composit.Initials;
            compositBDO.MatCode = composit.MatCode;
            compositBDO.MatLanguage = composit.MatLanguage;
            compositBDO.MATLevel = composit.MATLevel;
            compositBDO.MATScore = composit.MATScore;
            compositBDO.Name = composit.Name;
            compositBDO.QLLevel = composit.QLLevel;
            compositBDO.QLScore = composit.QLScore;
            compositBDO.RefNo = composit.RefNo;
            compositBDO.RowGuid = composit.RowGuid;
            compositBDO.RowVersion = composit.RowVersion;
            compositBDO.SAID = composit.SAID;
            compositBDO.Surname = composit.Surname;
            compositBDO.VenueName = composit.VenueName;
            compositBDO.VenueCode = composit.VenueCode;
            compositBDO.WroteAL = composit.WroteAL;
            compositBDO.WroteQL = composit.WroteQL;
            compositBDO.WroteMat = composit.WroteMat;
            compositBDO.DateModified = composit.DateModified;
            compositBDO.Batch = composit.Batch;

        }
        static void ProvinceToProvinceBDO(ProvinceBDO provinceBDO, Province province)
        {
            provinceBDO.Code = province.Code;
            provinceBDO.Id = province.Id;
            provinceBDO.Name = province.Name;
            provinceBDO.RowVersion = province.RowVersion;
        }
        static void provinceBDOtoprovinceDAL(ProvinceBDO provinceBDO, Province province)
        {
            province.Code = provinceBDO.Code;
            province.Id = provinceBDO.Id;
            province.Name = provinceBDO.Name;
            province.RowVersion = provinceBDO.RowVersion;

        }
        static void TestVenueToVenueBDO(VenueBDO venueBDO, TestVenue testVenue)
        {
            venueBDO.Available = testVenue.Available;
            venueBDO.Capacity = testVenue.Capacity;
            venueBDO.Description = testVenue.Comments;
            venueBDO.Place = testVenue.Place;
            venueBDO.ProvinceID = testVenue.ProvinceID;
            venueBDO.Room = testVenue.Room;
            venueBDO.ShortName = testVenue.ShortName;
            venueBDO.VenueCode = testVenue.VenueCode;
            venueBDO.VenueName = testVenue.VenueName;
            venueBDO.VenueType = testVenue.VenueType;
            venueBDO.RowGuid = testVenue.RowGuid;
            venueBDO.WebSiteName = testVenue.WebsiteName;
            //ProvinceBDO m = new ProvinceBDO();
            //venueBDO.Province = m;
        }
        #endregion
    }
}
