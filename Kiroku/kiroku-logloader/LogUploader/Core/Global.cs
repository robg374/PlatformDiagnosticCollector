﻿using System;
using System.Configuration;

namespace KLOGLoader
{
    class Global
    {
        public static readonly String Debug = ConfigurationManager.AppSettings["debug"].ToString();
        public static readonly String AzureContainer = ConfigurationManager.AppSettings["azureContainer"].ToString();
        public static readonly String SqlConnetionString = ConfigurationManager.AppSettings["sql"].ToString();
        static string _retentionDays = ConfigurationManager.AppSettings["retentionDays"].ToString();
        public static readonly Double RetentionDays = Convert.ToDouble(_retentionDays);

        public static void CheckDebug()
        {
            if (Debug == "1")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\tDEBUG DETECTED, PRESS ANY KEY");
                Console.ReadKey();
            }
        }
    }
}
