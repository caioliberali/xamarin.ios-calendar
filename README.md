# Calendar for Xamarin.iOS
Simple calendar for Xamarin.iOS.  
Based on [Akhilendra](https://github.com/Akhilendra)'s [Calendar for iOS in Swift 4](https://github.com/Akhilendra/calenderAppiOS)

<img alt="Calendar" src="media/calendar.png" width="320">

## Using

To use Calendar you need to add it to a View:

```cs
var calendarView = new CalendarView(view.Bounds);
view.AddSubview(calendarView);
```

You can extend Calendar features:

```cs
var calendarView = new CalendarView(view.Bounds);
calendarView.BlockedDays = new List<DateTime> { DateTime.Now };
calendarView.MaxDate = DateTime.Now.AddMonths(1);
calendarView.MinDate = DateTime.Now.AddMonths(-1);

view.AddSubview(calendarView);
```

## Event

To handle date selection you must implement <code>ICalendarViewDelegate</code> and add it to <code>CalendarViewDelegate</code> property:

```cs
public partial class ViewController : UIViewController, ICalendarViewDelegate
{
    ...

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        var calendarView = new CalendarView(view.Bounds);
        calendarView.CalendarViewDelegate = this;
        view.AddSubview(calendarView);
    }

    public void DidDatePicked(DateTime dateTime)
    {
        DateLabel.Text = $"Selected date: {dateTime.ToShortDateString()}";
    }
}
```

## Style

You can change the style of the Calendar:  

**Header**  

<img alt="Calendar header" src="media/calendar-header.png" width="320">

```cs
var calendarView = new CalendarView(view.Bounds);
calendarView.StyleHeader = new CalendarHeaderStyle
{
    BackgroundColor = UIColor.DarkGray,
    Title = new LabelStyle
    {
        BackgroundColor = UIColor.LightGray,
        Font = UIFont.SystemFontOfSize(18),
        TextColor = UIColor.White,
        TextAlignment = UITextAlignment.Center
    },
    PrevButton = new ButtonStyle
    {
        Font = UIFont.SystemFontOfSize(18),
        NormalColor = UIColor.White,
        DisabledColor = UIColor.DarkGray,
        Text = "<"
    },
    NextButton = new ButtonStyle
    {
        Font = UIFont.SystemFontOfSize(18),
        NormalColor = UIColor.White,
        DisabledColor = UIColor.DarkGray,
        Text = ">"
    }
};
```  

**Weekday**  

<img alt="Calendar weekday" src="media/calendar-weekday.png" width="320">

```cs
var calendarView = new CalendarView(view.Bounds);
calendarView.StyleWeekday = new CalendarWeekdayStyle
{
    BackgroundColor = UIColor.DarkGray,
    Day = new LabelStyle
    {
        BackgroundColor = UIColor.LightGray,
        Font = UIFont.SystemFontOfSize(16),
        TextColor = UIColor.White,
        TextAlignment = UITextAlignment.Center
    },
    Weekend = new LabelStyle
    {
        BackgroundColor = UIColor.LightGray,
        Font = UIFont.SystemFontOfSize(16),
        TextColor = UIColor.White,
        TextAlignment = UITextAlignment.Center
    }
};
```  

**Days**  

<img alt="Calendar days" src="media/calendar-day.png" width="320">

```cs
var calendarView = new CalendarView(view.Bounds);
calendarView.StyleDays = new CalendarDaysStyle
{
    BackgroundColor = UIColor.FromRGB(205, 203, 203),
    Day = new LabelCellStyle
    {
        BackgroundColor = UIColor.FromRGB(54, 104, 150),
        BorderColor = UIColor.Black,
        BorderWidth = 1f,
        Font = UIFont.SystemFontOfSize(16),
        TextColor = UIColor.White
    },
    Today = new LabelCellStyle
    {
        BackgroundColor = UIColor.FromRGB(41, 78, 112),
        BorderColor = UIColor.Black,
        BorderWidth = 1f,
        Font = UIFont.SystemFontOfSize(16),
        TextColor = UIColor.White,
    },
    BlockedDay = new LabelCellStyle
    {
        BackgroundColor = UIColor.FromRGB(205, 203, 203),
        BorderColor = UIColor.Black,
        BorderWidth = 1f,
        Font = UIFont.SystemFontOfSize(16),
        TextColor = UIColor.Black
    },
    BlockedToday = new LabelCellStyle
    {
        BackgroundColor = UIColor.FromRGB(179, 178, 178),
        BorderColor = UIColor.Black,
        BorderWidth = 1f,
        Font = UIFont.SystemFontOfSize(16),
        TextColor = UIColor.Black
    },
};
```
