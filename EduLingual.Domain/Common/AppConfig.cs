using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Common
{
    public class AppConfig
    {
        public static ConnectionString ConnectionString { get; set; } = null!;
    }
    public class ConnectionString
    {
        public string DefaultConnection { get; set; } = string.Empty;
    }
}
