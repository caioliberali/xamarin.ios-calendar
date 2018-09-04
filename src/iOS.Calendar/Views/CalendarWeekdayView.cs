using System;
using System.Collections.Generic;
using CoreGraphics;
using iOS.Calendar.Styles;
using UIKit;

namespace iOS.Calendar.Views
{
    class CalendarWeekdayView : UIView
    {
        private readonly UIStackView _stackView = new UIStackView();
        private readonly List<UILabel> _labelDays = new List<UILabel>();
        private CalendarWeekdayStyle _weekdayStyle;


        public CalendarWeekdayView(CGRect frame)
            : base(frame)
        {
            SetupControls();

            SetStyle(new CalendarWeekdayStyle());

            AddControlsToView();
        }


        public void SetStyle(CalendarWeekdayStyle weekdayStyle)
        {
            _weekdayStyle = weekdayStyle;

            BackgroundColor = _weekdayStyle.BackgroundColor;

            UpdateWeekdayStyle();
        }


        private void SetupControls()
        {
            _stackView.Distribution = UIStackViewDistribution.FillEqually;
            _stackView.TranslatesAutoresizingMaskIntoConstraints = false;

            for (var day = 1; day <= 7; day++)
            {
                var label = new UILabel
                {
                    Text = new DateTime(2017, 1, day).ToString("ddd")
                };

                _labelDays.Add(label);
            }
        }


        private void UpdateWeekdayStyle()
        {
            for (var day = 1; day <= 7; day++)
            {
                var label = _labelDays[day - 1];
                var labelStyle = day > 1 && day < 7 ? _weekdayStyle.Day : _weekdayStyle.Weekend;

                UpdateLabelStyle(label, labelStyle);
            }
        }


        private void UpdateLabelStyle(UILabel label, LabelStyle labelStyle)
        {
            label.BackgroundColor = labelStyle.BackgroundColor;
            label.Font = labelStyle.Font;
            label.TextColor = labelStyle.TextColor;
            label.TextAlignment = labelStyle.TextAlignment;
        }


        private void AddControlsToView()
        {
            AddSubview(_stackView);

            _stackView.TopAnchor.ConstraintEqualTo(TopAnchor).Active = true;
            _stackView.LeftAnchor.ConstraintEqualTo(LeftAnchor).Active = true;
            _stackView.RightAnchor.ConstraintEqualTo(RightAnchor).Active = true;
            _stackView.BottomAnchor.ConstraintEqualTo(BottomAnchor).Active = true;

            foreach (var label in _labelDays)
            {
                _stackView.AddArrangedSubview(label);
            }
        }
    }
}
