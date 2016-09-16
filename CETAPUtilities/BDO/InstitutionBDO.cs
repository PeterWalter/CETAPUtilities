using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CETAPUtilities.BDO
	{
	public class InstitutionBDO
		{
		public int InstID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public byte[ ] Logo { get; set; }
		public System.Guid GUID { get; set; }
		public System.DateTime DateModified { get; set; }
		}
	}
