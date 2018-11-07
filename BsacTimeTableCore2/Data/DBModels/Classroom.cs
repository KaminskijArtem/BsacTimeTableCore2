using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsacTimeTableCore2.Data.DBModels
{
    public class Classroom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Building { get; set; }

        public ICollection<Record> Record { get; set; }
    }
}
