using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CETAPUtilities.BDO;
using CETAPUtilities.Database;

namespace CETAPUtilities.Model
{
    public class DesignDataService : IDataService
    {
        public List<IntakeYearsBDO> GetAllIntakeYears()
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetAllNBTScoresAsync(IntakeYear period)
        {
            throw new NotImplementedException();
        }

        public List<UserBDO> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public IntakeYear GetIntakeRecord(int year)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveLargeExcel2Async(string folder, ObservableCollection<CompositBDO> composite, IntakeYearsBDO intake)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveLargeExcelAsync(string folder, ObservableCollection<CompositBDO> composite, IntakeYearsBDO intake)
        {
            throw new NotImplementedException();
        }
    }
}
