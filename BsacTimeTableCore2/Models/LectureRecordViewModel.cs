using System;

namespace BsacTimeTableCore2.Models
{
    public class LectureRecordViewModel
    {
        public int SubjOrdinalNumber { get; set; }
        public DateTime Date { get; set; }
        public string ClassroomName { get; set; }
        public string GroupName { get; set; }
        public string SubjectName { get; set; }
        public string SubjectTypeName { get; set; }
    }
}