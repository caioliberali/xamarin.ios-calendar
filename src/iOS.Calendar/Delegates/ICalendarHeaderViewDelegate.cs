using System;

namespace iOS.Calendar.Delegates
{
    interface ICalendarHeaderViewDelegate
    {
        void DidMonthChanged(DateTime dateTime);
    }
}
