using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsacTimeTableCore2.Data.DBModels
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }

        public ICollection<Record> Records { get; set; }
    }
}
