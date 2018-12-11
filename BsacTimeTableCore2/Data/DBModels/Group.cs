using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BsacTimeTableCore2.Data.DBModels
{
    public class Group
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Факультет")]
        public int FacultyId { get; set; }
        [Display(Name = "Факультет")]
        public Faculty Faculty { get; set; }

        public List<Record> Records { get; set; }
    }
}
