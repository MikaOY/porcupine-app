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
		}
	}
}
