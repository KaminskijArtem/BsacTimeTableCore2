using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsacTimeTableCore2.Data.DBModels
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Group> Group { get; set; }
    }
}
