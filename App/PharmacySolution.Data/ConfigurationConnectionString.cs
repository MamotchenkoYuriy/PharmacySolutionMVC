using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace PharmacySolution.Data
{
    public static class ConfigurationConnectionString
    {
        public static string GetConnectionString(string connectionStringName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            return connectionString;
        }
    }
}
