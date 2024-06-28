using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.Dashboard
{
    public class AllReportDataDto
    {
        public int TotalUser {  get; set; }
        public int TotalTeacher { get; set; }
        public double TotalRevenue { get; set; }
    }
}
