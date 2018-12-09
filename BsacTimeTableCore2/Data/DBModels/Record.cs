using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BsacTimeTableCore2.Data.DBModels
{
    public class Record
    {
        public int Id { get; set; }
        public int SubjOrdinalNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public bool? IsChanged { get; set; }
        public bool? IsDeleted { get; set; }

        public int ClassroomId { get; set; }
        public Classroom Classroom { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }

        public int SubjectForId { get; set; }
        public SubjectFor SubjectFor { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int SubjectTypeId { get; set; }
        public SubjectType SubjectType { get; set; }
    }
}
