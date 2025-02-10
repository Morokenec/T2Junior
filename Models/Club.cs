using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.DataModel
{
    public class Club
    {
        public int IdClub { get; set; } = 0;
        public string Name { get; set; } = "КофеКлуб";

        public string Rules { get; set; } = "";

        public string Target { get; set; } = "Клуб кофеманов";

        public string Avatar { get; set; } = "club_placeholder.png";

        public string SubImageSource => IsSubscribed ? "already_subbed.png" : "add_a_new.png";

        public int SubCount { get; set; } = 13;

        public int Rating { get; set; } = 105;

        public string SubValue
        {
            get
            {
                return CountFormat(SubCount);
            }
        }

        private static string CountFormat(int subCount)
        {
            if (subCount < 1000)
            {
                return subCount.ToString();
            }
            else if (subCount < 1000000)
            {
                return (subCount / 1000.0).ToString("0.##") + "к";
            }
            else
            {
                return (subCount / 1000000.0).ToString("0.##") + "M";
            }
        }

        private bool _isSubscribed;
        public bool IsSubscribed
        {
            get => _isSubscribed;
            set
            {
                _isSubscribed = value;
                OnPropertyChanged(nameof(IsSubscribed));
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
