using System;

namespace Porcupine
{
	public class Todo
	{
		public string Title;
		public bool IsDone; 
		public App.Category Category;
		public DateTime Deadline;
		public App.Priority Priority;

		public Todo()
		{
		}
	}
}
