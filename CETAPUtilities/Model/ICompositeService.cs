using CETAPUtilities.BDO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CETAPUtilities.Model
{
   public interface ICompositeService
    {
        Task<ObservableCollection<CompositBDO>> GetAllNBTScoresAsync(IntakeYearsBDO period);
		Task<ObservableCollection<InstitutionBDO>> GetAllInstitutions();

	}
}
