using MauiGeoQuiz.Presentation.Game.ViewModels;
using ReactiveUI;
using ReactiveUI.Maui;

namespace MauiGeoQuiz.ViewPages;

public partial class GamePage : ReactiveContentPage<GameViewModel>
{
    public GamePage(GameViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
        this.WhenActivated(async _ => { await viewModel.GetQuizData(); });
    }
}