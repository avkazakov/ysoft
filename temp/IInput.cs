using System;
using System.IO;

namespace temp
{
	public interface IInput
	{
		string MoveNext();
		string Current { get; }
	}

	internal sealed class Input : IInput
	{
		public Input(StreamReader reader)
		{
			this.reader = reader;
			currentLine = reader.ReadLine();
		}

		public string MoveNext()
		{
			if (reader.EndOfStream)
				currentLine = null;
			else
				currentLine = reader.ReadLine();
			return currentLine;
		}

		public string Current { get { return currentLine; } }

		private string currentLine;
		private readonly StreamReader reader;
	}
}