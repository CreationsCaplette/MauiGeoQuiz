using MauiGeoQuiz.Presentation.About;
using ReactiveUI;
using ReactiveUI.Maui;

namespace MauiGeoQuiz.About;

public partial class AboutPage : ReactiveContentPage<AboutViewModel>
{
    public AboutPage(AboutViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
        this.WhenActivated(_ => { });
    }
}