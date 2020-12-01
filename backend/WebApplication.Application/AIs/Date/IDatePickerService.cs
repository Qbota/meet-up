using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Application.AIs
{
    interface IDatePickerService
    {
        List<DateTime> PickDate(List<DateTime> selectedDates);
    }
}
