using Xamarin.Forms;

namespace Porcupine
{
	public partial class App : Application
	{
		Color transPurple = new Color(227, 221, 255, 0.5);
		string testText = "Hi There";

		public class Category
		{
			public string Name;
			public Color Tint;

			public Category(string name, Color tint)
			{
				Name = name;
				Tint = tint;
			}
		}

		public enum Priority
		{
			Low, Medium, High, JustDoIt
		};

		public App()
		{
			InitializeComponent();

			MainPage = new PorcupinePage();

			Resources.Add("transPurple", transPurple);
			Resources.Add("testText", testText);
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
