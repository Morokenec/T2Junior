using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace T2JuniorMobile.View.Components
{
    public partial class RegisterEntry : ContentView
    {
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(RegisterEntry), string.Empty,
                BindingMode.TwoWay, propertyChanged: OnTextChanged);

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(RegisterEntry), Colors.Gray,
                BindingMode.Default, propertyChanged: OnBorderColorChanged);

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(RegisterEntry), string.Empty,
                BindingMode.Default, propertyChanged: OnPlaceholderChanged);

        public static readonly BindableProperty ShadowColorProperty =
            BindableProperty.Create(nameof(ShadowColor), typeof(Color), typeof(RegisterEntry), Colors.Transparent);

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(RegisterEntry), 10.0);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public Color ShadowColor
        {
            get => (Color)GetValue(ShadowColorProperty);
            set => SetValue(ShadowColorProperty, value);
        }

        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var registerEntry = (RegisterEntry)bindable;
            registerEntry.entry.Text = (string)newValue;
        }

        private static void OnBorderColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var registerEntry = (RegisterEntry)bindable;
            registerEntry.frame.BorderColor = (Color)newValue;
        }

        private static void OnPlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var registerEntry = (RegisterEntry)bindable;
            registerEntry.entry.Placeholder = (string)newValue;
        }

        public RegisterEntry()
        {
            InitializeComponent();

        }

    }
}
