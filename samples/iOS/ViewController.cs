using System;
using iOS.Calendar.Delegates;
using iOS.CalendarSample.iOS.CustomControl;
using UIKit;

namespace iOS.CalendarSample.iOS
{
    public partial class ViewController : UIViewController, ICalendarViewDelegate
    {
        public ViewController(IntPtr handle) : base(handle)
        {

        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var calendar = new CustomCalendarView(CalendarView, this);
        }


        public void DidDatePicked(DateTime dateTime)
        {
            DateLabel.Text = $"Selected date: {dateTime.ToShortDateString()}";
        }
    }
}
