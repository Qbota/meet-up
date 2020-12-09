using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Application.AIs
{
    public interface IDatePickerService
    {
        DateTime PickDate(List<DateTime> selectedDates);
    }
}
