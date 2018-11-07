using System.Collections.Generic;

namespace BsacTimeTableCore2.Data.DBModels
{
    public class SubjectType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Record> Records { get; set; }
    }
}