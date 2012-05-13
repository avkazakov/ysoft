using System;
using System.IO;

namespace temp
{
	public interface IInput
	{
		void MoveNext();
		string Current { get; }
	}

	internal sealed class Input : IInput
	{
		public Input(StreamReader reader)
		{
			this.reader = reader;
			currentLine = reader.ReadLine();
		}

		public void MoveNext()
		{
			if (reader.EndOfStream)
				currentLine = null;
			else
				currentLine = reader.ReadLine();
		}

		public string Current { get { return currentLine; } }

		private string currentLine;
		private readonly StreamReader reader;
	}
}