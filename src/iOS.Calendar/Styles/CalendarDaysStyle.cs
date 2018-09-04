using UIKit;

namespace iOS.Calendar.Styles
{
    public class CalendarDaysStyle
    {
        public UIColor BackgroundColor { get; set; } = UIColor.Clear;

        public LabelCellStyle Day { get; set; } = new LabelCellStyle();
        public LabelCellStyle BlockedDay { get; set; } = new LabelCellStyle();
        public LabelCellStyle Today { get; set; } = new LabelCellStyle();
        public LabelCellStyle BlockedToday { get; set; } = new LabelCellStyle();
    }
}
