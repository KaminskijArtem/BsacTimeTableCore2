using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BsacTimeTableCore2.Data.DBModels
{
    public class Subject
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Уровень образования")]
        public int EduLevel { get; set; }
        [Display(Name = "Сокращенное название")]
        public string AbnameSubject { get; set; }

        public ICollection<Record> Records { get; set; }
    }
}