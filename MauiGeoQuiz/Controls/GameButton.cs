using MauiGeoQuiz.Core.Enums;
using Bindables.Maui;

namespace MauiGeoQuiz.Controls;

public partial class GameButton : Button
{
    private const string PositiveColorString = "Positive";
    private const string NegativeColorString = "Negative";
    private const string LightAccentColorString = "LightAccent";
    private const string DarkAccentColorString = "DarkAccent";

    [BindableProperty(typeof(Validity), OnPropertyChanged = nameof(OnIsValidPropertyChanged))]
    public static readonly BindableProperty IsValidProperty;

    protected override void OnPropertyChanged(string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
    }

    private static void OnIsValidPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is GameButton button && App.Current is not null)
        {
            switch (newValue)
            {
                case Validity.Valid:
                    ApplyBackgroundColor(App.Current, button, PositiveColorString);
                    ApplyTextColor(App.Current, button, LightAccentColorString);
                    break;
                case Validity.Invalid:
                    ApplyBackgroundColor(App.Current, button, NegativeColorString);
                    ApplyTextColor(App.Current, button, LightAccentColorString);
                    break;
                case Validity.Idle:
                default:
                    ApplyBackgroundColor(App.Current, button, LightAccentColorString);
                    ApplyTextColor(App.Current, button, DarkAccentColorString);
                    break;
            }
        }
    }

    private static void ApplyBackgroundColor(Application app, GameButton button, string colorName)
    {
        if (app.Resources.TryGetValue(colorName, out var color))
        {
            button.BackgroundColor = (Color)color;
        }
    }

    private static void ApplyTextColor(Application app, GameButton button, string colorName)
    {
        if (app.Resources.TryGetValue(colorName, out var color))
        {
            button.TextColor = (Color)color;
        }
    }
}
