using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsacTimeTableCore2.Data.DBModels
{
    public class Lecturer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ChairId { get; set; }
        public Chair Chair { get; set; }

        //public ICollection<Record> Records { get; set; }
    }
}
