using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CETAPUtilities.BDO
{
    public class IntakeYearsBDO
    {
        public int yearID { get; set; }
        public int Year { get; set; }
        public DateTime yearStart { get; set; }
        public DateTime yearEnd { get; set; }
        public DateTime DateModified { get; set; }
        public override string ToString()
        {
            return Year.ToString();
        }
    }
}
