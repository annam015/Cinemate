using Cinemate.ViewModels;

namespace Cinemate.Views;

public partial class MoviesView : ContentPage
{
	public MoviesView(MoviesViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}