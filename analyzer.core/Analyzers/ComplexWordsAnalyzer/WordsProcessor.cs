using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Analyzer.Core.Analyzers.ComplexWordsAnalyzer
{
	public class WordsProcessor
	{
		public WordsProcessor()
		{
			ChunkFiles = new List<string>();
		}

		public void Add(char[] wordChars)
		{
			Add(wordChars, wordChars.Length);
		}

		public void Add(char[] wordChars, int len)
		{
			//Do not count stop words in statistic, it's not interesting...
			if (StopWords.Contains(wordChars, len))
				return;
			byte[] word = Encoding.UTF8.GetBytes(wordChars, 0, len);
			if(!words.ContainsKey(word))
			{
				words.Add(word, 1);
				estimatedFileSize += word.Length + 1 /*for space*/+ 4 /*for count, 10 is max, 2 is most expected, 4 is for reserve*/;
			}
			else
				words[word]++;
			if(estimatedFileSize > CHUNK_SIZE)
				Flush();
		}

		public void Flush()
		{
			if(chunksDir == null)
				chunksDir = CreateChunksDir();
			string chunkFileName = GetChunkFileName();
			using(var fs = new FileStream(chunkFileName, FileMode.Create, FileAccess.Write, FileShare.Read, 1 * 1024 * 1024))
			{
				foreach(var word in words)
				{
					fs.Write(word.Key, 0, word.Key.Length);
					fs.WriteByte(Chars.SP);
					string wcount = word.Value.ToString(CultureInfo.InvariantCulture);
					int count = Encoding.UTF8.GetBytes(wcount, 0, wcount.Length, buf, 0);
					fs.Write(buf, 0, count);
					fs.WriteByte(Chars.EOL);
				}
			}
			words.Clear();
			estimatedFileSize = 0;
			Log.Debug("Chunk flushed to file: " + chunkFileName);
		}

		public List<string> ChunkFiles { get; set; }
		public const int CHUNK_SIZE = 128 * 1024 * 1024;

		private string GetChunkFileName()
		{
			chunkNumber++;
			var chunk = Path.Combine(chunksDir.FullName, "chunk_" + chunkNumber);
			ChunkFiles.Add(chunk);
			return chunk;
		}

		private DirectoryInfo CreateChunksDir()
		{
			var dir =
				new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "workspace/chunks_" + DateTime.UtcNow.Ticks));
			if(!dir.Exists)
				dir.Create();
			Log.Debug("Chunks directory craeted: " + dir.FullName);
			return dir;
		}

		private readonly byte[] buf = new byte[16]; // for converting int-string to bytes

		private readonly SortedDictionary<byte[], int> words = new SortedDictionary<byte[], int>(new Comparer());
		private int estimatedFileSize;
		private int chunkNumber;
		private DirectoryInfo chunksDir;
	}
}