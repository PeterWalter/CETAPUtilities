using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CETAPUtilities.BDO;
using CETAPUtilities.Database;
using System.Data.Entity;

namespace CETAPUtilities.Model
{
    public class CompositeService : ICompositeService
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public async Task<ObservableCollection<CompositBDO>> GetAllNBTScoresAsync(IntakeYearsBDO period)
        {
            var AllScores = new ObservableCollection<CompositBDO>();
            using (var context = new NBT_ProductionEntities())
            {
                var scores = await context.Composits.Where(x => x.DOT >= period.yearStart && x.DOT <= period.yearEnd).Select(m => m).ToListAsync();
                if (scores != null)
                {
                    // isloaded = true;
                    foreach (var score in scores)
                    {
                        var NBTScore = new CompositBDO();
                        CompositDALToCompositBDO(NBTScore, score);
                        AllScores.Add(NBTScore);
                    }

                }
                else
                {
                    string msg = "No such intake year " + period.Year;
                    log.Error(msg);
                }
            }

            return AllScores;
        }

        static void IntakeYearDalToIntakeYearBDO(IntakeYear intakeDAL, IntakeYearsBDO intakeBDO)
        {
            intakeBDO.Year = intakeDAL.Year;
            intakeBDO.yearID = intakeDAL.yearID;
            intakeBDO.yearStart = intakeDAL.yearStart;
            intakeBDO.yearEnd = intakeDAL.yearEnd;
            intakeBDO.DateModified = intakeDAL.DateModified;
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
    }
}
