using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Porcupine
{
	public partial class TodoView : ContentView
	{
		bool isExpanded = false;

		public static readonly BindableProperty TitleProperty =
			BindableProperty.Create(
				"Title",
				typeof(string),
				typeof(TodoView),
				"Do something",
				propertyChanged: (bindable, oldValue, newValue) =>
				{
					((TodoView)bindable).todoTitle.Text = (string)newValue;
				});

		public string Title
		{
			set { SetValue(TitleProperty, value); }
			get { return (string)GetValue(TitleProperty); }
		}

		public static readonly BindableProperty IsDoneProperty =
			BindableProperty.Create(
				"IsDone",
				typeof(bool),
				typeof(TodoView),
				false,
				propertyChanged: (bindable, oldValue, newValue) =>
				{
					((TodoView)bindable).checkbox.Color = (bool)newValue ? Color.Fuchsia : Color.Silver;
				});

		public bool IsDone
		{
			set { SetValue(IsDoneProperty, value); }
			get { return (bool)GetValue(IsDoneProperty); }
		}


		public TodoView()
		{
			InitializeComponent();

			// To show metadata

			metadataStack.IsVisible = true;
			double fullHeight = metadataStack.Height;
			metadataStack.IsVisible = false;

			var tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.Tapped += (s, e) =>
			{
				Device.StartTimer(TimeSpan.FromMilliseconds(15), () =>
				{
					metadataStack.HeightRequest += !isExpanded ? 2 : -2;
					double targetHeight = !isExpanded ? fullHeight : 0;
					return metadataStack.Height == targetHeight;
				});
				isExpanded = !isExpanded;
			};
			todoStack.GestureRecognizers.Add(tapGestureRecognizer);
		}
	}
}
