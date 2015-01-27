using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySolution.Web.Core.Models
{
    public class StorageViewModel
    {
        public string MedicamentName { get; set; }
        public string PharmacyNumber { get; set; }
        public int MedicamentId { get; set; }
        public int PharmacyId { get; set; }
        public int Count { get; set; }
    }
}
