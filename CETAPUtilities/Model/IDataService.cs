using CETAPUtilities.BDO;
using CETAPUtilities.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CETAPUtilities.Model
{
    public interface IDataService
    { 

        Task<bool> SaveLargeExcelAsync(string folder, ObservableCollection<CompositBDO> composite, IntakeYearsBDO intake);
        Task<bool> SaveLargeExcel2Async(string folder, ObservableCollection<CompositBDO> composite, IntakeYearsBDO intake);
        Task<bool> GetAllNBTScoresAsync(IntakeYear period);
        IntakeYear GetIntakeRecord(int year);
        List<IntakeYearsBDO> GetAllIntakeYears();
    }
}
