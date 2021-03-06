﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implements
{
    public class Data
    {
        public static double Percentile(List<double> list, double percentile)
        {
            try
            {
                // null and empty check

                var listCount = list.Count;
                list.Sort();
                double[] array = list.ToArray();

                var ordinalRank = (int)Math.Round(((percentile / 100) * listCount), 0, MidpointRounding.AwayFromZero);

                return array[ordinalRank];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static void StandardDeviation(List<double> population)
        {
            //null and empty check

            var min = population.Min();
            var max = population.Max();
            var range = max - min;
            var sum = population.Sum();

            //medium
            //mode

            var avg = population.Average();

            var variance = population.Select(x => Math.Round(Math.Sqrt(x - avg))).ToList();

            var varAvg = variance.Average();

            var standardDeviation = Math.Sqrt(varAvg);

        }
    }
}
