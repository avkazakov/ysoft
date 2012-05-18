using System.Collections.Generic;
using NUnit.Framework;

namespace Analyzer.Core.Analyzers.ComplexWordsAnalyzer
{
	[TestFixture]
	public class ChunksMerger_Tests
	{
		[Test]
		public void Test()
		{
			var readers = new []
                    	{
                    		new ChunkReader("word1 12", "word2 12", "word3 zas"), 
                    		new ChunkReader("word3 12", "word4 12", "word5 zzz"), 
                    		new ChunkReader("word5 15", "word6 12", "word7 asdfasdf"), 
                    		new ChunkReader("word7 15"), 
                    		new ChunkReader("word8 15", "word8 15"), 
                    	};
			var writer = new ChunkWriter();
			var merger = new ChunksMerger();
			merger.Merge(readers, writer);
			
			Assert.AreEqual(12, writer.Lines.Count);
			int i = -1;
			Assert.IsTrue(writer.Lines[++i].StartsWith("word1"));
			Assert.IsTrue(writer.Lines[++i].StartsWith("word2"));
			Assert.IsTrue(writer.Lines[++i].StartsWith("word3"));
			Assert.IsTrue(writer.Lines[++i].StartsWith("word3"));
			Assert.IsTrue(writer.Lines[++i].StartsWith("word4"));
			Assert.IsTrue(writer.Lines[++i].StartsWith("word5"));
			Assert.IsTrue(writer.Lines[++i].StartsWith("word5"));
			Assert.IsTrue(writer.Lines[++i].StartsWith("word6"));
			Assert.IsTrue(writer.Lines[++i].StartsWith("word7"));
			Assert.IsTrue(writer.Lines[++i].StartsWith("word7"));
			Assert.IsTrue(writer.Lines[++i].StartsWith("word8"));
			Assert.IsTrue(writer.Lines[++i].StartsWith("word8"));
		}

		class ChunkReader : IChunkReader
		{
			private readonly string[] lines;

			public ChunkReader(params string[] lines)
			{
				this.lines = lines;
			}

			public string MoveNextLine()
			{
				if (curr >= lines.Length)
				{
					CurrentLine = null;
				}
				else
				{
					CurrentLine = lines[curr];
					curr++;
				}
				return CurrentLine;
			}

			public string CurrentLine { get; set; }
			private int curr = 0;
		}

		internal class ChunkWriter : IChunkWriter
		{
			public void WriteLine(string line)
			{
				Lines.Add(line);
			}

			public List<string> Lines = new List<string>();
		}
	}
}