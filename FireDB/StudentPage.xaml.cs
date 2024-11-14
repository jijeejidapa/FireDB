using FireDB.Services;
using FireDB.ViewModels;

namespace FireDB;

public partial class StudentPage : ContentPage
{
	public StudentPage()
	{
		InitializeComponent();
		var firestoreService = new FireDBService();
		BindingContext = new StudentViewModel(firestoreService);
	}
}