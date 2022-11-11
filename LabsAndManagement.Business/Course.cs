using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabsAndManagement.Business
{
    public class Course
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int NoCredits { get; set; }
        public Student[] Students { get; set; } = null!;
        public Lab[] Labs { get; set; } = null!;
    }
}
