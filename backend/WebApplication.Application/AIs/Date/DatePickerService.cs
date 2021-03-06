﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplication.Application.AIs
{
    public class DatePickerService : IDatePickerService
    {
        public DateTime PickDate(List<DateTime> selectedDates)
        {
            var possibleDates = new Dictionary<DateTime, int>();
            foreach (var date in selectedDates)
            {
                if (possibleDates.ContainsKey(date))
                {
                    possibleDates[date]++;
                }
                else possibleDates.Add(date, 1);
            }
            return possibleDates.Where(x => !possibleDates.Where(y => y.Value > x.Value).Any()).Select(z => z.Key).FirstOrDefault();
        }
    }
}
