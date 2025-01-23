using Microsoft.Maui.Controls;

namespace T2JuniorMobile.View.Components
{
    public partial class RegisterEntry : ContentView
    {
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(RegisterEntry), string.Empty,
                BindingMode.TwoWay, propertyChanged: OnTextChanged);

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(RegisterEntry), string.Empty,
                BindingMode.Default, propertyChanged: OnPlaceholderChanged);

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

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var registerEntry = (RegisterEntry)bindable;
            if (registerEntry.entry.Text != (string)newValue)
            {
                registerEntry.entry.Text = (string)newValue;
            }
        }

        private static void OnPlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var registerEntry = (RegisterEntry)bindable;
            if (registerEntry.entry.Placeholder != (string)newValue)
            {
                registerEntry.entry.Placeholder = (string)newValue;
            }
        }

        public RegisterEntry()
        {
            InitializeComponent();
        }
    }
}
