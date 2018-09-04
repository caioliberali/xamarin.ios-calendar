using UIKit;

namespace iOS.Calendar.Styles
{
    public class ButtonStyle
    {
        public UIColor BackgroundColor { get; set; } = UIColor.Clear;
        public UIFont Font { get; set; } = UIFont.BoldSystemFontOfSize(16);
        public string Text { get; set; } = "";
        public UIColor NormalColor { get; set; } = UIColor.Black;
        public UIColor DisabledColor { get; set; } = UIColor.LightGray;
    }
}
