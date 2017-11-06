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
        public static readonly DateTime SessionDatetime = DateTime.Now.ToUniversalTime();
        public static readonly Int32 DebugMode = 1;
    }
}