using System.Collections.Generic;

namespace BsacTimeTableCore2.Data.DBModels
{
    public class Chair
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Lecturer> Lecturers { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}