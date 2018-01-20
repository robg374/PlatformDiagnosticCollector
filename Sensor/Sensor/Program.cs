﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Sensor
{
	class Program
	{
        static void Main(string[] args)
		{
            #region Target List

            // Get Target List
            var targetList = DataDownload.GetTargetList();
            
            #region console.importcheck

            Console.WriteLine("-- Import Check --");
            foreach (var targetCheck in targetList)
            {
                Console.WriteLine("DNS Name: {0}", targetCheck.DNSName);
                Console.WriteLine("DNS Probe: {0} \r\n", targetCheck.DNSProbe);
            }

            #endregion

            #endregion

            #region Sensor v1

            // Master Sensor collection
            List<Sensor> sensorCollection = new List<Sensor>();

            #region console.connectionstring

            Console.WriteLine("-- Connection String Check --");
            Console.WriteLine("Connection String: {0} \r\n", Global.SQLConnectionString);

            #endregion

            // Loop target list, collect data and load sensor object
            foreach (var target in targetList)
			{
                // Create object and set timer
                // TODO: Add target.DNSName into object creation
                // TODO: Move session and source into object constructor
                // TODO: Move timer into HTTP Request method
                Sensor sensor = new Sensor();
                System.Diagnostics.Stopwatch timer = new Stopwatch();

                // Pre-collect set
                sensor.dt_session = Global.SessionDatetime;
                sensor.nvc_source = Global.SensorLocation;
                sensor.nvc_dns = target.DNSName;

                // Collect IP
                Collection.CollectIP(target, sensor);

                // Collect HTTP Request
                Collection.CollectHTTPRequest(target, sensor, timer);

                // Add to collection
                sensorCollection.Add(sensor);
            }

            // Upsert default collection to SQL
            DataUpload.SQLUpsert(sensorCollection);

            #endregion

            #region Sensor v2 : DNS

            // DNS Sensor Collection
            List<DNSSensor> sensorDnsCollection = new List<DNSSensor>();

            // Loop target list, collect dns data and load sensor object
            foreach (var target in targetList)
            {
                // Create DNS sensor object
                DNSSensor sensorDns = new DNSSensor(target.DNSName);

                // Pre-collect set
                //TODO: remove once confirmed
                //sensorDns.dt_session = Global.SessionDatetime;
                //sensorDns.nvc_source = Global.SensorLocation;
                //sensorDns.nvc_dns = target.DNSName;

                // Collect DNS
                DNSCollector.Execute(target, sensorDns);

                // Add to DNS collection to SQL
                sensorDnsCollection.Add(sensorDns);
            }

            // Upsert default collection to SQL
            DataUpload.DNSUpsert(sensorDnsCollection);

            #endregion

            #region console.debug

            if (Global.DebugMode == "1")
            {
                Console.WriteLine("-- Operation Complete --");
                Console.Read();
            }

            #endregion

            // Close application
            Environment.Exit(0);
		}
    }
}
