using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.Dashboard
{
    public class ReportDataTodayDto
    {
        public double Revenue { get; set; }
        public int TotalPayments {  get; set; }
        public double Profit { get; set; }
    }
}
