using System;
using System.Globalization;
using CoreGraphics;
using iOS.Calendar.Delegates;
using iOS.Calendar.Styles;
using UIKit;

namespace iOS.Calendar.Views
{
    class CalendarHeaderView : UIView
    {
        private DateTime _currentDate;
        private DateTime _maxDate;
        private DateTime _minDate;
        private readonly UILabel _labelTitle = new UILabel();
        private readonly UIButton _buttonPrev = new UIButton();
        private readonly UIButton _buttonNext = new UIButton();
        private CalendarHeaderStyle _headerStyle;
        private WeakReference<ICalendarHeaderViewDelegate> _calendarHeaderViewDelegate;


        public ICalendarHeaderViewDelegate CalendarHeaderViewDelegate
        {
            get
            {
                ICalendarHeaderViewDelegate _delegate;
                return _calendarHeaderViewDelegate.TryGetTarget(out _delegate) ? _delegate : null;
            }
            set
            {
                _calendarHeaderViewDelegate = new WeakReference<ICalendarHeaderViewDelegate>(value);
            }
        }


        public CalendarHeaderView(CGRect frame)
            : base(frame)
        {
            SetupControls();

            StartDate(DateTime.Now);
            SetStyle(new CalendarHeaderStyle());

            SetMinDate(new DateTime(2000, 1, 1));
            SetMaxDate(new DateTime(2050, 1, 1));

            AddControlsToView();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _buttonPrev?.RemoveTarget(OnPrevClicked, UIControlEvent.TouchUpInside);
                _buttonNext?.RemoveTarget(OnNextClicked, UIControlEvent.TouchUpInside);
            }

            base.Dispose(disposing);
        }


        public void StartDate(DateTime dateTime)
        {
            UpdateHeader(dateTime);
        }


        public void SetStyle(CalendarHeaderStyle headerStyle)
        {
            _headerStyle = headerStyle;

            BackgroundColor = _headerStyle.BackgroundColor;

            UpdateLabelStyle(_labelTitle, _headerStyle.Title);
            UpdateButtonStyle(_buttonPrev, _headerStyle.PrevButton);
            UpdateButtonStyle(_buttonNext, _headerStyle.NextButton);
        }


        public void SetMinDate(DateTime dateTime)
        {
            _minDate = CreateValidDate(dateTime);
            _buttonPrev.Enabled = _currentDate > _minDate;
        }


        public void SetMaxDate(DateTime dateTime)
        {
            _maxDate = CreateValidDate(dateTime);
            _buttonNext.Enabled = _currentDate < _maxDate;
        }


        private void SetupControls()
        {
            _labelTitle.TranslatesAutoresizingMaskIntoConstraints = false;

            _buttonPrev.TranslatesAutoresizingMaskIntoConstraints = false;
            _buttonPrev.AddTarget(OnPrevClicked, UIControlEvent.TouchUpInside);

            _buttonNext.TranslatesAutoresizingMaskIntoConstraints = false;
            _buttonNext.AddTarget(OnNextClicked, UIControlEvent.TouchUpInside);
        }


        private void UpdateLabelStyle(UILabel label, LabelStyle labelStyle)
        {
            label.BackgroundColor = labelStyle.BackgroundColor;
            label.Font = labelStyle.Font;
            label.TextColor = labelStyle.TextColor;
            label.TextAlignment = labelStyle.TextAlignment;
        }


        private void UpdateButtonStyle(UIButton button, ButtonStyle buttonStyle)
        {
            button.BackgroundColor = buttonStyle.BackgroundColor;
            button.Font = buttonStyle.Font;
            button.SetTitle(buttonStyle.Text, UIControlState.Normal);
            button.SetTitleColor(buttonStyle.NormalColor, UIControlState.Normal);
            button.SetTitleColor(buttonStyle.DisabledColor, UIControlState.Disabled);
        }


        private void AddControlsToView()
        {
            AddSubview(_buttonPrev);
            AddSubview(_labelTitle);
            AddSubview(_buttonNext);

            _buttonPrev.TopAnchor.ConstraintEqualTo(TopAnchor).Active = true;
            _buttonPrev.LeftAnchor.ConstraintEqualTo(LeftAnchor).Active = true;
            _buttonPrev.WidthAnchor.ConstraintEqualTo(50).Active = true;
            _buttonPrev.HeightAnchor.ConstraintEqualTo(HeightAnchor).Active = true;

            _buttonNext.TopAnchor.ConstraintEqualTo(TopAnchor).Active = true;
            _buttonNext.RightAnchor.ConstraintEqualTo(RightAnchor).Active = true;
            _buttonNext.WidthAnchor.ConstraintEqualTo(50).Active = true;
            _buttonNext.HeightAnchor.ConstraintEqualTo(HeightAnchor).Active = true;

            _labelTitle.TopAnchor.ConstraintEqualTo(TopAnchor).Active = true;
            _labelTitle.LeftAnchor.ConstraintEqualTo(_buttonPrev.RightAnchor).Active = true;
            _labelTitle.RightAnchor.ConstraintEqualTo(_buttonNext.LeftAnchor).Active = true;
            _labelTitle.HeightAnchor.ConstraintEqualTo(HeightAnchor).Active = true;
        }


        private void UpdateHeader(DateTime dateTime)
        {
            _currentDate = CreateValidDate(dateTime);

            _labelTitle.Text = CreateHeaderTitle(_currentDate);
            _buttonPrev.Enabled = _currentDate > _minDate;
            _buttonNext.Enabled = _currentDate < _maxDate;
        }


        private DateTime CreateValidDate(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }


        private string CreateHeaderTitle(DateTime dateTime)
        {
            var cultureInfo = new CultureInfo(CultureInfo.CurrentCulture.Name);
            var dateFormatted = dateTime.ToString("MMMM yyyy");

            return cultureInfo.TextInfo.ToTitleCase(dateFormatted);
        }


        private void OnPrevClicked(object sender, EventArgs e)
        {
            var updatedDate = _currentDate.AddMonths(-1);

            if (updatedDate < _minDate)
                return;

            UpdateHeader(updatedDate);

            if (_calendarHeaderViewDelegate != null)
                CalendarHeaderViewDelegate?.DidMonthChanged(_currentDate);
        }


        private void OnNextClicked(object sender, EventArgs e)
        {
            var updatedDate = _currentDate.AddMonths(1);

            if (updatedDate > _maxDate)
                return;

            UpdateHeader(updatedDate);

            if (_calendarHeaderViewDelegate != null)
                CalendarHeaderViewDelegate?.DidMonthChanged(_currentDate);
        }
    }
}
