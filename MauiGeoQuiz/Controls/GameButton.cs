using MauiGeoQuiz.Core.Enums;
using Bindables.Maui;

namespace MauiGeoQuiz.Controls;

public partial class GameButton : Button
{
    private const string DisabledColorString = "Disabled";
    private const string PositiveColorString = "Positive";
    private const string NegativeColorString = "Negative";
    private const string LightAccentColorString = "LightAccent";
    private const string DarkAccentColorString = "DarkAccent";

    [BindableProperty(typeof(GameButtonStates), OnPropertyChanged = nameof(OnStatePropertyChanged))]
    public static readonly BindableProperty StateProperty;

    protected override void OnPropertyChanged(string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
    }

    private static void OnStatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is GameButton button && App.Current is not null)
        {
            switch (newValue)
            {
                case GameButtonStates.Positive:
                    ApplyBackgroundColor(App.Current, button, PositiveColorString);
                    ApplyTextColor(App.Current, button, LightAccentColorString);
                    break;
                case GameButtonStates.Negative:
                    ApplyBackgroundColor(App.Current, button, NegativeColorString);
                    ApplyTextColor(App.Current, button, LightAccentColorString);
                    break;
                case GameButtonStates.Disabled:
                    ApplyBackgroundColor(App.Current, button, DisabledColorString);
                    ApplyTextColor(App.Current, button, LightAccentColorString);
                    break;
                case GameButtonStates.Idle:
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
