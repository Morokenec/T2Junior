using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace T2JuniorMobile.View.Components
{
    public partial class RegisterPicker : ContentView
    {
        public RegisterPicker()
        {
            InitializeComponent();
        }

        // BindableProperty для ItemsSource
        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
            propertyName: nameof(Items),
            returnType: typeof(ObservableCollection<string>),
            declaringType: typeof(RegisterPicker),
            defaultValue: new ObservableCollection<string>(),
            propertyChanged: OnItemsChanged);

        public ObservableCollection<string> Items
        {
            get => (ObservableCollection<string>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        private static void OnItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is RegisterPicker control && newValue is ObservableCollection<string> newItems)
            {
                control.sexPicker.ItemsSource = newItems;
            }
        }

        // BindableProperty для Title
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            propertyName: nameof(Title),
            returnType: typeof(string),
            declaringType: typeof(RegisterPicker),
            defaultValue: "Выберите",
            propertyChanged: OnTitleChanged);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is RegisterPicker control && newValue is string newTitle)
            {
                control.sexPicker.Title = newTitle;
            }
        }
    }
}
