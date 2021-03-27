using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticeHackerRank
{
    public class SchedulePlay
    {
        [Test]//fail...
        public void a()
        {
            List<int> firstDay = new List<int>() { 1, 10, 11 };
            List<int> lastDay = new List<int>() { 11, 10, 11 };

            List<Tuple<int, int>> totalInt = new List<Tuple<int, int>>();

            for (int investor = 0; investor < firstDay.Count(); investor++)
            {
                List<Tuple<int, int>> thisInvestorAvailableDay = new List<Tuple<int, int>>();

                for (int day = firstDay[investor]; day <= lastDay[investor]; day++)
                {
                    thisInvestorAvailableDay.Add(new Tuple<int, int>(day, investor));
                }

                totalInt.AddRange(thisInvestorAvailableDay);
            }

            int sm = firstDay.Min();

            int bg = lastDay.Max();

            bool[] schedule = new bool[bg];

            List<int> alreadyScheduled = new List<int>();

            totalInt = totalInt.OrderBy(x => x.Item2).ToList();

            foreach (var item in totalInt)
            {
                if (alreadyScheduled.Contains(item.Item2))
                {
                    continue;
                }
                else
                {
                    if (schedule[item.Item1 - 1] == true)
                    {
                        continue;
                    }
                    schedule[item.Item1 - 1] = true;
                    alreadyScheduled.Add(item.Item2);
                }
            }

            var returnInt = alreadyScheduled.Count(); ;
        }
    }
}