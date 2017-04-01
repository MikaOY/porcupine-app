using System;
using System.Collections.Generic;
using System.Diagnostics;
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

		public static readonly BindableProperty CategoryProperty =
			BindableProperty.Create(
				"Category",
				typeof(App.Category),
				typeof(TodoView),
				new App.Category("Personal", Color.Fuchsia),
				propertyChanged: (bindable, oldValue, newValue) =>
				{
			((TodoView)bindable).deadlineLabel.Text = (newValue as App.Category).Name;
					// TODO: display color of category
				});

		public App.Category Category
		{
			set { SetValue(CategoryProperty, value); }
			get { return (App.Category)GetValue(CategoryProperty); }
		}

		public static readonly BindableProperty DeadlineProperty =
			BindableProperty.Create(
				"Deadline",
				typeof(DateTime),
				typeof(TodoView),
				DateTime.Now,
				propertyChanged: (bindable, oldValue, newValue) =>
				{
			((TodoView)bindable).deadlineLabel.Text = (string)newValue;
				});

		public DateTime Deadline
		{
			set { SetValue(DeadlineProperty, value); }
			get { return (DateTime)GetValue(DeadlineProperty); }
		}

		public static readonly BindableProperty PriorityProperty =
			BindableProperty.Create(
				"Priority",
				typeof(App.Priority),
				typeof(TodoView),
				App.Priority.Low,
				propertyChanged: (bindable, oldValue, newValue) =>
				{
					// TODO: display priority level
				});

		public App.Priority Priority
		{
			set { SetValue(PriorityProperty, value); }
			get { return (App.Priority)GetValue(PriorityProperty); }
		}

		public TodoView()
		{
			InitializeComponent();

			// To show metadata

			metadataStack.Scale = 0.1;
			metadataStack.TranslateTo(metadataStack.X, (metadataStack.Y + metadataStack.Height), 10, Easing.Linear);
			metadataStack.IsVisible = false;

			var tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.Tapped += async (s, e) =>
			{
				// Scale stack to show / hide
				if (isExpanded)
				{
					await metadataStack.ScaleTo(0.1, 250, Easing.CubicOut);
					metadataStack.IsVisible = false;
				}
				else
				{
					metadataStack.IsVisible = true;
					await metadataStack.ScaleTo(1, 250, Easing.CubicOut);
				}

				// Invert expanded variable
				isExpanded = !isExpanded;
			};
			todoStack.GestureRecognizers.Add(tapGestureRecognizer);

			// To enable checking 

			var tapGestureRecognizer2 = new TapGestureRecognizer();
			tapGestureRecognizer2.Tapped += (s, e) =>
			{
				IsDone = !IsDone;
			};
			checkbox.GestureRecognizers.Add(tapGestureRecognizer2);
		}
	}
}
