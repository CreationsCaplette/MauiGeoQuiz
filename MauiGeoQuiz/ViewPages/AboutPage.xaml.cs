using MauiGeoQuiz.About.ViewModels;
using ReactiveUI;
using ReactiveUI.Maui;

namespace MauiGeoQuiz.ViewPages;

public partial class AboutPage : ReactiveContentPage<AboutViewModel>
{
    public AboutPage(AboutViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
        this.WhenActivated(_ => { });
    }
}