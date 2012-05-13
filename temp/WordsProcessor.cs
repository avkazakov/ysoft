using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace temp
{
	public class WordsProcessor
	{
		public void Add(char[] wordChars)
		{
			Add(wordChars, 0, wordChars.Length);
		}

		public void Add(char[] wordChars, int index, int len)
		{
			var word = Encoding.UTF8.GetBytes(wordChars, index, len);
			if (!words.ContainsKey(word))
			{
				words.Add(word, 1);
				estimatedFileSize += word.Length + 1/*for space*/ + 4 /*for count, 10 is max, 2 is most expected, 4 is for reserve*/;
			}
			else 
				words[word]++;

			if (estimatedFileSize > Consts.CHUNK_SIZE)
			{
				Flush();
			}
		}

		public void Flush()
		{
			if (chunksDir == null)
				chunksDir = CreateChunksDir();
			string chunkFileName = GetChunkFileName();
			using(var fs = new FileStream(chunkFileName, FileMode.Create, FileAccess.Write, FileShare.Read, 1 * 1024 * 1024))
			{
				foreach (var word in words)
				{
					fs.Write(word.Key, 0, word.Key.Length);
					fs.WriteByte(Chars.SP);
					string wcount = word.Value.ToString(CultureInfo.InvariantCulture);
					var count = Encoding.UTF8.GetBytes(wcount, 0, wcount.Length, buf, 0);
					fs.Write(buf, 0, count);
					//fs.Write(Chars.EOL, 0, Chars.EOL.Length);
					fs.WriteByte(Chars.EOL);
				}
			}
			words.Clear();
			estimatedFileSize = 0;
			Log.Info("Chunk flushed to file: " + chunkFileName);
		}

		private string GetChunkFileName()
		{
			chunkNumber++;
			return Path.Combine(chunksDir.FullName, "chunk_" + chunkNumber);
		}

		private DirectoryInfo CreateChunksDir()
		{
			var dir =
				new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "workspace/chunks_" + DateTime.UtcNow.Ticks));
			if (!dir.Exists)
				dir.Create();
			Log.Info("Chunks directory craeted: " + dir.FullName);
			return dir;
		}

		private byte[] buf = new byte[1 * 1024 * 1024]; // in the real life there is no words longer then 200 KB (© wikipedia)

		private readonly SortedDictionary<byte[], int> words = new SortedDictionary<byte[], int>(new Comparer());
		private int estimatedFileSize = 0;
		private int chunkNumber = 0;
		private DirectoryInfo chunksDir;
	}
}