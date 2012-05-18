using System;
using System.IO;
using System.Text;

namespace Analyzer.Core.Analyzers.ComplexWordsAnalyzer
{
	public sealed class ChunkReader : IChunkReader, IDisposable
	{
		private ChunkReader(StreamReader reader)
		{
			this.reader = reader;
		}

		public string MoveNextLine()
		{
			if(reader.EndOfStream)
				currentLineLine = null;
			else
				currentLineLine = reader.ReadLine();
			return currentLineLine;
		}

		public string CurrentLine { get { return currentLineLine; } }

		public void Dispose()
		{
			reader.Dispose();
		}

		public static ChunkReader Open(string file)
		{
			var streamReader = new StreamReader(file, Encoding.UTF8, false, Const.MAX_WORD_LEN);
			return new ChunkReader(streamReader);
		}

		private string currentLineLine;
		private readonly StreamReader reader;
	}
}