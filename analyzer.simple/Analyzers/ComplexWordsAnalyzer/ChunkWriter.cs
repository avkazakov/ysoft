using System;
using System.IO;
using System.Text;

namespace Analyzer.Simple.Analyzers.ComplexWordsAnalyzer
{
	internal class ChunkWriter : IChunkWriter, IDisposable
	{
		public ChunkWriter(FileStream output)
		{
			this.output = output;
		}

		public void WriteLine(string line)
		{
			int count = Encoding.UTF8.GetBytes(line, 0, line.Length, buf, 0);
			output.Write(buf, 0, count);
			output.WriteByte(Chars.EOL);
		}

		public void Dispose()
		{
			output.Dispose();
		}

		public static ChunkWriter Create(string fileName)
		{
			var output = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read, 16 * 1024 * 1024);
			return new ChunkWriter(output);
		}

		private readonly byte[] buf = new byte[Const.MAX_WORD_LEN];
		private readonly FileStream output;
	}
}