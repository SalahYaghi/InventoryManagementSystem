using System;
using System.Windows.Forms;

namespace UI.Shared.Helpers.UI_Helpers
{
    public static class NumericInputHelper
    {
        public static void SetValue(NumericUpDown control, decimal value)
        {
            if (control == null)
                return;

            control.Value = Clamp(control, value);
        }

        public static void SetValue(NumericUpDown control, decimal? value)
        {
            SetValue(control, value ?? 0m);
        }

        public static decimal Clamp(NumericUpDown control, decimal value)
        {
            if (control == null)
                return value;

            if (value < control.Minimum)
                return control.Minimum;

            if (value > control.Maximum)
                return control.Maximum;

            return value;
        }

        public static void ConfigureMoney(NumericUpDown control, decimal maximum = 999999999m)
        {
            if (control == null)
                return;

            control.DecimalPlaces = 2;
            control.Increment = 1m;
            control.Minimum = 0m;
            control.Maximum = maximum;
            control.ThousandsSeparator = true;
            control.TextAlign = HorizontalAlignment.Right;
        }

        public static void ConfigureQuantity(NumericUpDown control, decimal maximum = 999999999m)
        {
            if (control == null)
                return;

            control.DecimalPlaces = 2;
            control.Increment = 1m;
            control.Minimum = 0m;
            control.Maximum = maximum;
            control.ThousandsSeparator = true;
            control.TextAlign = HorizontalAlignment.Right;
        }
    }
}
