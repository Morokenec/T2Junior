using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace T2JuniorMobile.View.Components
{
    public partial class RegisterEntry : ContentView, INotifyPropertyChanged
    {
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(RegisterEntry), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(RegisterEntry), string.Empty);

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(RegisterEntry), Colors.Transparent, BindingMode.TwoWay, propertyChanged: OnBorderColorChanged);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public RegisterEntry()
        {
            InitializeComponent();
        }

        private static void OnBorderColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is RegisterEntry registerEntry && newValue is Color newColor)
            {
                registerEntry.entry.BackgroundColor = newColor; 
            }
        }

        private void OnEntryFocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(Text))
            {
                entry.PlaceholderColor = Colors.Transparent;
                floatingPlaceholder.TranslateTo(0, -15, 100);
                floatingPlaceholder.FontSize = 10;
                floatingPlaceholder.TextColor = Color.FromArgb("#6b6d80");
            }
        }

        private void OnEntryUnfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(Text))
            {
                entry.PlaceholderColor = Colors.LightGray;
                floatingPlaceholder.TranslateTo(0, 15, 100);
                floatingPlaceholder.FontSize = 14;
                floatingPlaceholder.TextColor = Colors.LightGray;
            }
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                floatingPlaceholder.TranslateTo(0, -15, 100);
                floatingPlaceholder.FontSize = 10;
                floatingPlaceholder.TextColor = Color.FromArgb("#6b6d80");
            }
            else if (string.IsNullOrEmpty(e.NewTextValue) && !entry.IsFocused)
            {
                floatingPlaceholder.TranslateTo(0, 15, 100);
                floatingPlaceholder.FontSize = 14;
                floatingPlaceholder.TextColor = Colors.LightGray;
            }
        }
    }
}
