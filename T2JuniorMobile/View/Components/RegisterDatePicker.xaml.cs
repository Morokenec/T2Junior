using System;

namespace T2JuniorMobile.View.Components
{
    public partial class RegisterDatePicker : ContentView
    {
        public RegisterDatePicker()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public DateTime Today => DateTime.Today;
    }
}
