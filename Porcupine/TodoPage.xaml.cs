using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Porcupine
{
	public partial class TodoPage : ContentPage
	{
		public TodoPage()
		{
			InitializeComponent();

		}

		bool triggered;

		void RevealMenu(object sender, EventArgs e)
		{
			sideMenu.TranslateTo(sideMenu.Width, 0, 300, Easing.CubicInOut);
			toggleButton.IsVisible = false;
			triggered = true;
		}

		void HideMenu(object sender, EventArgs e)
		{
			if (triggered)
			{
				sideMenu.TranslateTo(-sideMenu.Width, 0, 300, Easing.CubicIn);
				toggleButton.IsVisible = true;
				triggered = false;
			}
		}


	}
}
