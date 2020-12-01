using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Application.AIs
{
    public interface IDatePickerService
    {
        List<DateTime> PickDate(List<DateTime> selectedDates);
    }
}
