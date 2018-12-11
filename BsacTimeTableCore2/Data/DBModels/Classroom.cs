using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BsacTimeTableCore2.Data.DBModels
{
    public class Classroom
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Корпус")]
        public int Building { get; set; }

        public ICollection<Record> Record { get; set; }
    }
}
