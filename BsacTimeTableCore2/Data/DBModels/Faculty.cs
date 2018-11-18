using System;
using System.Collections.Generic;

namespace BsacTimeTableCore2.Data.DBModels
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Group> Groups { get; set; }
    }
}
