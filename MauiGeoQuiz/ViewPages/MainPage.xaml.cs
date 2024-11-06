using MauiGeoQuiz.MainMenu.ViewModels;
using ReactiveUI.Maui;

namespace MauiGeoQuiz.ViewPages;

public partial class MainPage : ReactiveContentPage<MainMenuViewModel>
{
	public MainPage(MainMenuViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
    }
}