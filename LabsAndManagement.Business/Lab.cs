using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabsAndManagement.Business
{
    public class Lab
    {
        public string Id { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
