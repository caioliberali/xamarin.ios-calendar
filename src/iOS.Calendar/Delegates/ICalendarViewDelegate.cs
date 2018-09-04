using System;

namespace iOS.Calendar.Delegates
{
    public interface ICalendarViewDelegate
    {
        void DidDatePicked(DateTime dateTime);
    }
}
