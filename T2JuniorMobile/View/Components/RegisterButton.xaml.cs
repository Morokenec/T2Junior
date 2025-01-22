using System.Windows.Input;
using Microsoft.Maui.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace T2JuniorMobile.View.Components
{
    public partial class RegisterButton : ContentView
    {
        public RegisterButton()
        {
            InitializeComponent();
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            Command?.Execute(CommandParameter);
        }

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(RegisterButton), 90.0,
                propertyChanged: OnCornerRadiusChanged);

        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        private static void OnCornerRadiusChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RegisterButton)bindable;
            control.frame.CornerRadius = (int)(double)newValue;
            control.button.CornerRadius = (int)(double)newValue;
            control.borderBoxView.CornerRadius = new CornerRadius((int)(double)newValue);
        }

        public static readonly BindableProperty FrameBackgroundColorProperty =
            BindableProperty.Create(nameof(FrameBackgroundColor), typeof(Color), typeof(RegisterButton), Colors.Transparent,
                propertyChanged: OnFrameBackgroundColorChanged);

        public Color FrameBackgroundColor
        {
            get => (Color)GetValue(FrameBackgroundColorProperty);
            set => SetValue(FrameBackgroundColorProperty, value);
        }

        private static void OnFrameBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RegisterButton)bindable;
            control.frame.BackgroundColor = (Color)newValue;
        }

        public static readonly BindableProperty ButtonBackgroundColorProperty =
            BindableProperty.Create(nameof(ButtonBackgroundColor), typeof(Color), typeof(RegisterButton), Colors.Blue,
                propertyChanged: OnButtonBackgroundColorChanged);

        public Color ButtonBackgroundColor
        {
            get => (Color)GetValue(ButtonBackgroundColorProperty);
            set => SetValue(ButtonBackgroundColorProperty, value);
        }

        private static void OnButtonBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RegisterButton)bindable;
            control.button.BackgroundColor = (Color)newValue;
        }

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(RegisterButton), Colors.White,
                propertyChanged: OnTextColorChanged);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        private static void OnTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RegisterButton)bindable;
            control.button.TextColor = (Color)newValue;
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(RegisterButton), string.Empty,
                propertyChanged: OnTextChanged);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RegisterButton)bindable;
            control.button.Text = (string)newValue;
        }

        public static readonly BindableProperty HeightProperty =
            BindableProperty.Create(nameof(Height), typeof(double), typeof(RegisterButton), 10.0,
                propertyChanged: OnHeightChanged);

        public double Height
        {
            get => (double)GetValue(HeightProperty);
            set => SetValue(HeightProperty, value);
        }

        private static void OnHeightChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // Здесь можно добавить логику для изменения высоты, если это необходимо
        }

        public static readonly BindableProperty WidthProperty =
            BindableProperty.Create(nameof(Width), typeof(double), typeof(RegisterButton), 50.0,
                propertyChanged: OnWidthChanged);

        public double Width
        {
            get => (double)GetValue(WidthProperty);
            set => SetValue(WidthProperty, value);
        }

        private static void OnWidthChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // Здесь можно добавить логику для изменения ширины, если это необходимо
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(RegisterButton), null);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(RegisterButton), null);

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(RegisterButton), 0.0,
                propertyChanged: OnBorderWidthChanged);

        public double BorderWidth
        {
            get => (double)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        private static void OnBorderWidthChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RegisterButton)bindable;
            control.borderBoxView.HeightRequest = (double)newValue;
            control.borderBoxView.WidthRequest = (double)newValue;
        }

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(RegisterButton), Colors.Transparent,
                propertyChanged: OnBorderColorChanged);

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        private static void OnBorderColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RegisterButton)bindable;
            control.borderBoxView.Color = (Color)newValue;
        }
    }
}