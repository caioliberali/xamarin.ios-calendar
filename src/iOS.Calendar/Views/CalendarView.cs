using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using iOS.Calendar.Delegates;
using iOS.Calendar.Styles;
using iOS.Calendar.ViewCells;
using UIKit;

namespace iOS.Calendar.Views
{
    public class CalendarView : UIView, IUICollectionViewDelegate,
        IUICollectionViewDataSource, IUICollectionViewDelegateFlowLayout,
        ICalendarHeaderViewDelegate
    {
        private int _dayOfWeek;
        private DateTime _currentDate;
        private DateTime _maxDate;
        private DateTime _minDate;
        private readonly DateTime _dateToday = DateTime.Now.Date;
        private readonly List<DateTime> _blockedDays = new List<DateTime>();
        private UICollectionView _viewDays;
        private CalendarWeekdayView _viewWeekday;
        private CalendarHeaderView _viewHeader;
        private CalendarDaysStyle _daysStyle;
        private WeakReference<ICalendarViewDelegate> _calendarViewDelegate;


        public DateTime MinDate
        {
            set
            {
                _minDate = value.Date;
                _viewHeader.SetMinDate(_minDate);
            }
        }

        public DateTime MaxDate
        {
            set
            {
                _maxDate = value.Date;
                _viewHeader.SetMaxDate(_maxDate);
            }
        }

        public List<DateTime> BlockedDays
        {
            set
            {
                _blockedDays.Clear();
                _blockedDays.AddRange(value.Select(x => x.Date));
            }
        }

        public CalendarHeaderStyle StyleHeader
        {
            set => _viewHeader.SetStyle(value);
        }

        public CalendarWeekdayStyle StyleWeekday
        {
            set => _viewWeekday.SetStyle(value);
        }

        public CalendarDaysStyle StyleDays
        {
            set => SetStyle(value);
        }

        public ICalendarViewDelegate CalendarViewDelegate
        {
            get
            {
                ICalendarViewDelegate _delegate;
                return _calendarViewDelegate.TryGetTarget(out _delegate) ? _delegate : null;
            }
            set
            {
                _calendarViewDelegate = new WeakReference<ICalendarViewDelegate>(value);
            }
        }


        public CalendarView(CGRect frame)
            : base(frame)
        {
            SetupControls(frame);

            StartDate(_dateToday);
            SetStyle(new CalendarDaysStyle());

            AddControlsToView();

            SetupDaysCollection();
        }


        public void StartDate(DateTime dateTime)
        {
            _currentDate = dateTime.Date;
            _dayOfWeek = GetFirstDayOfWeek(_currentDate);

            _viewHeader.StartDate(_currentDate);
        }


        private void SetupControls(CGRect frame)
        {
            _viewHeader = new CalendarHeaderView(frame);
            _viewHeader.TranslatesAutoresizingMaskIntoConstraints = false;
            _viewHeader.CalendarHeaderViewDelegate = this;

            _viewWeekday = new CalendarWeekdayView(frame);
            _viewWeekday.TranslatesAutoresizingMaskIntoConstraints = false;

            var layoutFlow = new UICollectionViewFlowLayout();
            layoutFlow.SectionInset = UIEdgeInsets.Zero;

            _viewDays = new UICollectionView(CGRect.Empty, layoutFlow);
            _viewDays.AllowsMultipleSelection = false;
            _viewDays.ShowsHorizontalScrollIndicator = false;
            _viewDays.TranslatesAutoresizingMaskIntoConstraints = false;
        }


        private void SetStyle(CalendarDaysStyle daysStyle)
        {
            _daysStyle = daysStyle;

            _viewDays.BackgroundColor = _daysStyle.BackgroundColor;
        }


        private int GetFirstDayOfWeek(DateTime date)
        {
            return (int)new DateTime(date.Year, date.Month, 1).DayOfWeek;
        }


        private void AddControlsToView()
        {
            AddSubview(_viewHeader);
            AddSubview(_viewWeekday);
            AddSubview(_viewDays);

            _viewHeader.TopAnchor.ConstraintEqualTo(TopAnchor).Active = true;
            _viewHeader.LeftAnchor.ConstraintEqualTo(LeftAnchor).Active = true;
            _viewHeader.RightAnchor.ConstraintEqualTo(RightAnchor).Active = true;
            _viewHeader.HeightAnchor.ConstraintEqualTo(45).Active = true;

            _viewWeekday.TopAnchor.ConstraintEqualTo(_viewHeader.BottomAnchor).Active = true;
            _viewWeekday.LeftAnchor.ConstraintEqualTo(LeftAnchor).Active = true;
            _viewWeekday.RightAnchor.ConstraintEqualTo(RightAnchor).Active = true;
            _viewWeekday.HeightAnchor.ConstraintEqualTo(40).Active = true;

            _viewDays.TopAnchor.ConstraintEqualTo(_viewWeekday.BottomAnchor).Active = true;
            _viewDays.LeftAnchor.ConstraintEqualTo(LeftAnchor).Active = true;
            _viewDays.RightAnchor.ConstraintEqualTo(RightAnchor).Active = true;
            _viewDays.BottomAnchor.ConstraintEqualTo(BottomAnchor).Active = true;
        }


        private void SetupDaysCollection()
        {
            _viewDays.Delegate = this;
            _viewDays.DataSource = this;
            _viewDays.RegisterClassForCell(typeof(DateCollectionViewCell), DateCollectionViewCell.Key);
        }


        private bool IsDateEnabled(DateTime dateTime)
        {
            return dateTime >= _minDate && dateTime <= _maxDate &&
                !_blockedDays.Any(x => x == dateTime);
        }


        private LabelCellStyle GetCellStyle(bool isEnabled, DateTime dateTime)
        {
            if (dateTime == _dateToday)
                return isEnabled ? _daysStyle.Today : _daysStyle.BlockedToday;

            return isEnabled ? _daysStyle.Day : _daysStyle.BlockedDay;
        }


        // IUICollectionViewDataSource
        //[Export("collectionView:numberOfItemsInSection:")]
        public nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return DateTime.DaysInMonth(_currentDate.Year, _currentDate.Month) + _dayOfWeek;
        }


        // IUICollectionViewDataSource
        //[Export ("collectionView:cellForItemAtIndexPath:")]
        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (DateCollectionViewCell)collectionView.DequeueReusableCell(DateCollectionViewCell.Key, indexPath);
            var cellGreaterThanDayOfWeek = indexPath.Item > _dayOfWeek - 1;

            if (cellGreaterThanDayOfWeek)
            {
                var day = indexPath.Row - _dayOfWeek + 1;
                var date = new DateTime(_currentDate.Year, _currentDate.Month, day);
                var isEnabled = IsDateEnabled(date);
                var cellStyle = GetCellStyle(isEnabled, date);

                cell.Date = date;
                cell.SetStyle(cellStyle);
                cell.UserInteractionEnabled = isEnabled;
            }

            cell.Hidden = !cellGreaterThanDayOfWeek;

            return cell;
        }


        // IUICollectionViewDelegate
        [Export("collectionView:didSelectItemAtIndexPath:")]
        public void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (DateCollectionViewCell)collectionView.CellForItem(indexPath);

            if (_calendarViewDelegate != null)
                CalendarViewDelegate?.DidDatePicked(cell.Date);
        }


        // IUICollectionViewDelegate
        [Export("collectionView:didDeselectItemAtIndexPath:")]
        public void ItemDeselected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.CellForItem(indexPath);
        }


        // IUICollectionViewDelegateFlowLayout
        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            var width = collectionView.Frame.Width / 7;
            var height = width;

            return new CGSize(width, height);
        }


        // IUICollectionViewDelegateFlowLayout
        [Export("collectionView:layout:minimumLineSpacingForSectionAtIndex:")]
        public nfloat GetMinimumLineSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return 0f;
        }


        // IUICollectionViewDelegateFlowLayout
        [Export("collectionView:layout:minimumInteritemSpacingForSectionAtIndex:")]
        public nfloat GetMinimumInteritemSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return 0f;
        }


        // INavigationHeaderViewDelegate
        public void DidMonthChanged(DateTime dateTime)
        {
            _currentDate = dateTime.Date;
            _dayOfWeek = GetFirstDayOfWeek(_currentDate);

            _viewDays.ReloadData();
        }
    }
}
