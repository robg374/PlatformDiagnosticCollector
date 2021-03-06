﻿namespace Sensor
{
    using System;
    using System.Collections.Generic;
    using Kiroku;

    public class SensorManager
    {
        private static bool _configOnline = false;

        private static bool _appConfigStatus = false;

        private static bool _logConfigStatus = false;

        /// <summary>
        /// Initialize Sensor application.
        /// </summary>
        /// <param name="sensorConfig"></param>
        /// <param name="kirokuConfig"></param>
        /// <returns></returns>
        public static bool Initialize(List<KeyValuePair<string, string>> sensorConfig, List<KeyValuePair<string, string>> kirokuConfig)
        {
            // Null checks
            if (sensorConfig != null)
            {
                _appConfigStatus = true;
            }
            if (kirokuConfig != null)
            {
                _logConfigStatus = true;
            }

            // Push config packages to Configuration logic
            try
            {
                return _configOnline = Configuration.SetConfigs(sensorConfig, kirokuConfig);
            }
            catch
            {
                return _configOnline = false;
            }
        }

        /// <summary>
        /// Execute Sensor application.
        /// </summary>
        /// <returns></returns>
        public static bool Execute()
        {

            DateTime _session = DateTime.Now.ToUniversalTime();

            if (!_configOnline)
            {
                return false;
            }

            KManager.Open();

            using (KLog klog = new KLog("ExecutionStack"))
            {
                try
                {
                    Capsule capsule = new Capsule
                    {
                        Session = _session,
                        Source = Configuration.Source
                    };


                    capsule.DNSRecords = GetArticles.Execute();

                    GetIPAddress.Execute(ref capsule);

                    TagIPAddress.Execute(ref capsule);

                    GetTCPLatency.Execute(ref capsule);

                    UploadCapsule.Execute(capsule);
                }

                catch (Exception ex)
                {
                    klog.Error(ex.ToString());

                    throw new Exception($"SENSOR EXCEPTION: {ex}");
                }
            }

            KManager.Close();

            return true;
        }

        /// <summary>
        /// Execute Sensor data retention.
        /// </summary>
        /// <returns></returns>
        public static bool Retention()
        {
             using (KLog klog = new KLog("RetentionProcess"))
            {
                if (Configuration.Worker)
                {
                    klog.Trace($"Worker Detected.");

                    DataRetention.Execute();
                }
            }

            return true;
        }
    }
}
