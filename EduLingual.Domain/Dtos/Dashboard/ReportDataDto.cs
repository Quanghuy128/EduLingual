using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.Dashboard;

public class ReportDataDto
{
    public double DataInMonday { get; set; }
    public double DataInTuesday { get; set; }
    public double DataInWednesday { get; set; }
    public double DataInThursday { get; set; }
    public double DataInFriday { get; set; }
    public double DataInSaturday { get; set; }
    public double DataInSunday { get; set; }
}
