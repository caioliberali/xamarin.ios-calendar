using UIKit;

namespace iOS.Calendar.Styles
{
    public class LabelStyle
    {
        public UIColor BackgroundColor { get; set; } = UIColor.Clear;
        public UIFont Font { get; set; } = UIFont.BoldSystemFontOfSize(16);
        public UIColor TextColor { get; set; } = UIColor.Black;
        public UITextAlignment TextAlignment { get; set; } = UITextAlignment.Center;
    }
}
