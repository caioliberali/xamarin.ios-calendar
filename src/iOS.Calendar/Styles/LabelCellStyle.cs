using System;
using UIKit;

namespace iOS.Calendar.Styles
{
    public class LabelCellStyle
    {
        public UIColor BackgroundColor { get; set; } = UIColor.Clear;
        public UIColor BorderColor { get; set; } = UIColor.Clear;
        public nfloat BorderWidth { get; set; } = 0.0f;
        public UIFont Font { get; set; } = UIFont.BoldSystemFontOfSize(16);
        public UIColor TextColor { get; set; } = UIColor.Black;
        public UITextAlignment TextAlignment { get; set; } = UITextAlignment.Center;
    }
}
