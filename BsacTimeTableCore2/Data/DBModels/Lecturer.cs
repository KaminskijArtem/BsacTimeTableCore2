using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BsacTimeTableCore2.Data.DBModels
{
    public class Lecturer
    {
        public int Id { get; set; }
        [Display(Name = "Имя")]
        public string Name { get; set; }

        public ICollection<Record> Records { get; set; }
    }
}
