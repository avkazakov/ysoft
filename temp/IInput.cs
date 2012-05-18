using System;
using System.IO;

namespace temp
{
	public interface IInput
	{
		string MoveNext();
		string Current { get; }
	}

	public sealed class Input : IInput
	{
		public Input(StreamReader reader)
		{
			this.reader = reader;
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