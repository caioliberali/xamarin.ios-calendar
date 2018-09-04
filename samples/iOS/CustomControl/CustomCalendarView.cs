using System;
using System.Collections.Generic;
using System.Linq;
using iOS.Calendar.Delegates;
using iOS.Calendar.Styles;
using iOS.Calendar.Views;
using UIKit;

namespace iOS.CalendarSample.iOS.CustomControl
{
    public class CustomCalendarView
    {
        private static UIColor DarkGray = UIColor.FromRGB(0.37f, 0.36f, 0.36f);

        public CustomCalendarView(UIView view, ICalendarViewDelegate iDelegate)
        {
            var calendarView = new CalendarView(view.Bounds)
            {
                BlockedDays = BlockedDays(),
                CalendarViewDelegate = iDelegate,
                MaxDate = DateTime.Now.AddMonths(1),
                MinDate = DateTime.Now.AddMonths(-1),
                StyleHeader = CreateHeaderStyle(),
                StyleWeekday = CreateWeekdayStyle(),
                StyleDays = CreateDaysStyle()
            };

            calendarView.StartDate(DateTime.Now);

            view.AddSubview(calendarView);
        }


        private List<DateTime> BlockedDays()
        {
            var blockedDays = new List<DateTime>();

            var maxDate = DateTime.Now.AddMonths(1);
            var minDate = DateTime.Now.AddMonths(-1);
            var currentDate = minDate;

            while (currentDate.Date < maxDate.Date)
            {
                currentDate = currentDate.AddDays(1);

                blockedDays.Add(currentDate);
            }

            return blockedDays.Where(x => x.Day % 2 == 1).ToList();
        }


        private CalendarHeaderStyle CreateHeaderStyle()
        {
            return new CalendarHeaderStyle
            {
                BackgroundColor = DarkGray,
                Title = new LabelStyle
                {
                    Font = UIFont.SystemFontOfSize(18),
                    TextColor = UIColor.White
                },
                PrevButton = new ButtonStyle
                {
                    Font = UIFont.SystemFontOfSize(18),
                    NormalColor = UIColor.White,
                    DisabledColor = DarkGray,
                    Text = "<"
                },
                NextButton = new ButtonStyle
                {
                    Font = UIFont.SystemFontOfSize(18),
                    NormalColor = UIColor.White,
                    DisabledColor = DarkGray,
                    Text = ">"
                }
            };
        }


        private CalendarWeekdayStyle CreateWeekdayStyle()
        {
            return new CalendarWeekdayStyle
            {
                BackgroundColor = DarkGray,
                Day = new LabelStyle
                {
                    Font = UIFont.SystemFontOfSize(16),
                    TextColor = UIColor.White
                },
                Weekend = new LabelStyle
                {
                    Font = UIFont.SystemFontOfSize(16),
                    TextColor = UIColor.White
                }
            };
        }


        private CalendarDaysStyle CreateDaysStyle()
        {
            var backgroundEnabled = UIColor.FromRGB(54, 104, 150);
            var backgroundEnabledToday = UIColor.FromRGB(41, 78, 112);
            var backgroundBlocked = UIColor.FromRGB(205, 203, 203);
            var backgroundBlockedToday = UIColor.FromRGB(179, 178, 178);

            return new CalendarDaysStyle
            {
                BackgroundColor = backgroundBlocked,
                Day = new LabelCellStyle
                {
                    BackgroundColor = backgroundEnabled,
                    Font = UIFont.SystemFontOfSize(16),
                    TextColor = UIColor.White
                },
                Today = new LabelCellStyle
                {
                    BackgroundColor = backgroundEnabledToday,
                    Font = UIFont.SystemFontOfSize(16),
                    TextColor = UIColor.White
                },
                BlockedDay = new LabelCellStyle
                {
                    BackgroundColor = backgroundBlocked,
                    Font = UIFont.SystemFontOfSize(16),
                    TextColor = UIColor.Black
                },
                BlockedToday = new LabelCellStyle
                {
                    BackgroundColor = backgroundBlockedToday,
                    Font = UIFont.SystemFontOfSize(16),
                    TextColor = UIColor.Black
                },
            };
        }
    }
}
