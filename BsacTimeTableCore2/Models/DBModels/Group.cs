using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsacTimeTableCore2.Models.DBModels
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdFlow { get; set; }
        public int IdFaculty { get; set; }
        public int EduLevel { get; set; }
    }
}
