using UIKit;

namespace iOS.Calendar.Styles
{
    public class CalendarWeekdayStyle
    {
        public UIColor BackgroundColor { get; set; } = UIColor.Clear;

        public LabelStyle Day { get; set; } = new LabelStyle();
        public LabelStyle Weekend { get; set; } = new LabelStyle();
    }
}
