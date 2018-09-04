using UIKit;

namespace iOS.Calendar.Styles
{
    public class CalendarHeaderStyle
    {
        public UIColor BackgroundColor { get; set; } = UIColor.Clear;

        public LabelStyle Title { get; set; } = new LabelStyle();
        public ButtonStyle PrevButton { get; set; } = new ButtonStyle { Text = "<" };
        public ButtonStyle NextButton { get; set; } = new ButtonStyle { Text = ">" };
    }
}
