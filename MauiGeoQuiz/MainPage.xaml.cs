using MauiGeoQuiz.Presentation.MainMenu;
using ReactiveUI;
using ReactiveUI.Maui;

namespace MauiGeoQuiz;

public partial class MainPage : ReactiveContentPage<MainMenuViewModel>
{
	public MainPage(MainMenuViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
        this.WhenActivated(_ => { });
    }
}