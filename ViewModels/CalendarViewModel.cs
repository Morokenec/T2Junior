using MauiApp1.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModels
{
    class CalendarViewModel : BindableObject
    {
        private string _eventName;
        private Color _colorOfAButton;

        public string EventName
        {
            get => _eventName;
            set
            {
                if (_eventName != value)
                {
                    _eventName = value;
                    OnPropertyChanged(nameof(EventName));
                }
            }
        }
        public Color ColorOfAButton
        {
            get => _colorOfAButton;
            set
            {
                if (_colorOfAButton != value)
                {
                    _colorOfAButton = value;
                    OnPropertyChanged(nameof(ColorOfAButton));
                }
            }
        }
        public ObservableCollection<DayOfCalendar> Days { get; set; }
        public DateTime CurrentDate { get; set; }
        public CalendarViewModel()
        {
            CurrentDate = DateTime.Now;
            Days = new ObservableCollection<DayOfCalendar>();
            LoadDays();
        }
        private void LoadDays()
        {
            Days.Clear();
            var firstDayOfMonth = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(0);

            var startDate = firstDayOfMonth.AddDays(-(int)firstDayOfMonth.DayOfWeek + 1);
            var endDate = lastDayOfMonth.AddDays(6 - (int)lastDayOfMonth.DayOfWeek + 1);

            int dayCount = 0;

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var day = new DayOfCalendar
                {
                    Date = date,
                    IsCurrentMonth = date.Month == CurrentDate.Month
                };

                Days.Add(day);

                dayCount++;

                if (dayCount % 7 == 0)
                {
                    day.IsSeparate = true;
                }
            }
        }
    }
}
