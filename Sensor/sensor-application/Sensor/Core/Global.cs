﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Sensor
{
    public static class Global
    {
        public static readonly String SensorLocation = ConfigurationManager.AppSettings["sensor"].ToString();
        public static readonly String SQLConnectionString = ConfigurationManager.AppSettings["sql"].ToString();
        public static readonly String SQLConnectionStringv2 = ConfigurationManager.AppSettings["sqlv2"].ToString();
        public static readonly String DebugMode = ConfigurationManager.AppSettings["debug"].ToString();
        public static readonly DateTime SessionDatetime = DateTime.Now.ToUniversalTime();

        // Switch
        public static readonly String Version = ConfigurationManager.AppSettings["version"].ToString();
        public static readonly String Principal = ConfigurationManager.AppSettings["principal"].ToString();
        public static readonly String Agent = ConfigurationManager.AppSettings["agent"].ToString();

        // Constants Strings
        public static readonly string IpAddress = "IpAddress";
        public static readonly string UnknownDataCenter = "UNKNOWN";
        public static readonly string UnknownDataCenterTag = "UNK";
        public static readonly string StatusOnline = "ONLINE";
        public static readonly string StatusNoMatch = "NOMATCH";
    }
}
