using System.Collections.Generic;

namespace BsacTimeTableCore2.Data.DBModels
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EduLevel { get; set; }
        public string AbnameSubject { get; set; }

        public ICollection<Record> Records { get; set; }
    }
}