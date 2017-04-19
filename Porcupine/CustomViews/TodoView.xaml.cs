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
					((TodoView)bindable).titleLabel.Text = (string)newValue;
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
					// TODO: render box checking
					// temp
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
					// TODO: display color of category
					// temp
					((TodoView)bindable).categoryLabel.Text = (newValue as App.Category).Name;
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
					// TODO: render priority level
					// temp
					((TodoView)bindable).priorityLabel.Text = ((App.Priority)newValue).ToString();
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

			var tapRecogMeta = new TapGestureRecognizer();
			tapRecogMeta.Tapped += (s, e) =>
			{
				ShowHideMetadata();
			};
			todoStack.GestureRecognizers.Add(tapRecogMeta);

			// To enable checking 

			var tapRecogCheck = new TapGestureRecognizer();
			tapRecogCheck.Tapped += (s, e) =>
			{
				IsDone = !IsDone;
			};
			checkbox.GestureRecognizers.Add(tapRecogCheck);

			// To enable editing
			// Title
			var tapRecogTitle = new TapGestureRecognizer();
			tapRecogTitle.Tapped += (s, e) =>
			{
				if (isExpanded)
				{
					Entry editable = new Entry();
					editable.Text = titleLabel.Text;
					editable.VerticalOptions = titleLabel.VerticalOptions;
					editable.HorizontalOptions = LayoutOptions.FillAndExpand;
					editable.HeightRequest = titleLabel.Height;
					editable.WidthRequest = titleLabel.Width;

					titleLabel.IsVisible = false;
					StackLayout parent = titleLabel.Parent as StackLayout;

					editable.Completed += (sender, args) =>
					{
						Title = editable.Text;
						parent.Children.RemoveAt((parent.Children.Count - 1));
						titleLabel.IsVisible = true;
					};
					editable.Unfocused += (sender, args) =>
					{
						parent.Children.RemoveAt((parent.Children.Count - 1));
						titleLabel.IsVisible = true;
					};

					parent.Children.Add(editable);

					editable.IsVisible = true;
					editable.Focus();
				}
				else
				{
					ShowHideMetadata();
				}
			};
			titleLabel.GestureRecognizers.Add(tapRecogTitle);

			// Category
			var tapRecogCategory = new TapGestureRecognizer();
			tapRecogCategory.Tapped += (s, e) =>
			{
				if (isExpanded)
				{
					Picker picker = new Picker();
					picker.Title = "Category";
					string[] array = { "Casual", "Ideas", "Bananas", "Grapes", "KYS", "Money" };
					List<string> categories = new List<string>(array);
					foreach (string cat in categories)
					{
						picker.Items.Add(cat);
					}
					// TODO: bind to user defined categories

					StackLayout parent = todoLayout.Parent as StackLayout;
					parent.Children.Add(picker);

				}
				else
				{
					ShowHideMetadata();
				}
			};
			categoryLabel.GestureRecognizers.Add(tapRecogCategory);
			// Deadline

			// Priority
		}

		async void ShowHideMetadata()
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
		}
	}
}
