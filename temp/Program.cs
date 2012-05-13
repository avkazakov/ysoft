using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace temp
{
	class Program
	{
		static void Main(string[] args)
		{
			var sw = Stopwatch.StartNew();
			int count = 1 * 1000 * 1000;
			int ten = count / 10;

			var processor = new WordsProcessor();
			for (int i = 0; i < count; i++)
			{
				char[] charArray = Guid.NewGuid().ToString().ToCharArray();
				processor.Add(charArray);
				processor.Add(charArray);
				processor.Add(charArray);
				processor.Add(charArray);
				if (i >= ten && i % ten == 0)
				{
					Console.Out.WriteLine(sw.Elapsed);
				}
			}
			processor.Flush();
		}
	}

	public class WordsProcessor
	{
		public void Add(char[] wordChars)
		{
			var word = new string(wordChars);
			if (!words.ContainsKey(word))
			{
				words.Add(word, 1);
				estimatedSize += Encoding.UTF8.GetByteCount(wordChars) + 1/*for space*/ + 4 /*for count, 10 is max, 2 is most expected*/;
			}
			else 
				words[word]++;

			if (estimatedSize > Consts.CHUNK_SIZE)
				Flush();
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
					var count = Encoding.UTF8.GetBytes(word.Key, 0, word.Key.Length, buf, 0);
					fs.Write(buf, 0, count);
					fs.WriteByte(Chars.SP);
					string wcount = word.Value.ToString(CultureInfo.InvariantCulture);
					count = Encoding.UTF8.GetBytes(wcount, 0, wcount.Length, buf, 0);
					fs.Write(buf, 0, count);
					fs.Write(Chars.EOL, 0, Chars.EOL.Length);
				}
			}
			words.Clear();
			estimatedSize = 0;
			Log.Debug("Chunk flushed to file: " + chunkFileName);
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
			Log.Debug("Chunks directory craeted: " + dir.FullName);
			return dir;
		}

		private byte[] buf = new byte[1 * 1024 * 1024]; // in the real life there is no words longer then 200 KB (© wikipedia)

		private SortedDictionary<string, int> words = new SortedDictionary<string, int>();
		private int estimatedSize = 0;
		private int chunkNumber = 0;
		private DirectoryInfo chunksDir;
	}

	public static class Consts
	{
		public const int CHUNK_SIZE = 128*1024*1024;
	}

	public static class Chars
	{
		public static byte SP = 32;
		public static byte[] EOL = new byte[]{13, 10};
	}
}
