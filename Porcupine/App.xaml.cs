using Xamarin.Forms;

namespace Porcupine
{
	public partial class App : Application
	{
		public class Category
		{
			public string Name;
			public Color Tint;
		}

		public enum PriorityLevel
		{
			Low, Medium, High, JustDoIt
		};

		public App()
		{
			InitializeComponent();

			MainPage = new PorcupinePage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
