using System;
using Foundation;
using iOS.Calendar.Styles;
using UIKit;

namespace iOS.Calendar.ViewCells
{
    class DateCollectionViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("DateCollectionViewCell");

        private DateTime _date;
        private LabelCellStyle _cellStyle;
        private readonly UILabel _label = new UILabel();


        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value.Date;
                _label.Text = value.ToString("dd");
            }
        }


        protected internal DateCollectionViewCell(IntPtr handle)
            : base(handle)
        {
            SetupControl();

            SetStyle(new LabelCellStyle());

            AddControlToView();
        }


        public void SetStyle(LabelCellStyle cellStyle)
        {
            _cellStyle = cellStyle;

            BackgroundColor = _cellStyle.BackgroundColor;

            UpdateBorderStyle(_cellStyle);
            UpdateLabelStyle(_label, _cellStyle);
        }


        private void SetupControl()
        {
            _label.TranslatesAutoresizingMaskIntoConstraints = false;
        }


        private void UpdateBorderStyle(LabelCellStyle cellStyle)
        {
            if (cellStyle.BorderWidth < 0.1f)
                return;

            Layer.BorderWidth = cellStyle.BorderWidth;
            Layer.BorderColor = cellStyle.BorderColor.CGColor;
        }


        private void UpdateLabelStyle(UILabel label, LabelCellStyle cellStyle)
        {
            label.BackgroundColor = cellStyle.BackgroundColor;
            label.Font = cellStyle.Font;
            label.TextColor = cellStyle.TextColor;
            label.TextAlignment = cellStyle.TextAlignment;
        }


        private void AddControlToView()
        {
            AddSubview(_label);

            _label.TopAnchor.ConstraintEqualTo(TopAnchor).Active = true;
            _label.LeftAnchor.ConstraintEqualTo(LeftAnchor).Active = true;
            _label.RightAnchor.ConstraintEqualTo(RightAnchor).Active = true;
            _label.BottomAnchor.ConstraintEqualTo(BottomAnchor).Active = true;
        }
    }
}
