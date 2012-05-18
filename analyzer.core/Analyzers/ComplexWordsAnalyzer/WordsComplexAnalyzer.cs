using System.Collections.Generic;
using System.IO;
using Analyzer.Core.Text;

namespace Analyzer.Core.Analyzers.ComplexWordsAnalyzer
{
	public sealed partial class WordsComplexAnalyzer : IAnalyzer
	{
		public void TreatChar(char c)
		{
			if(char.IsLetter(c))
			{
				currWord[currWordLen] = char.ToLower(c);
				currWordLen++;
			}
			else if(currWordLen > 0)
			{
				wordsProcessor.Add(currWord, currWordLen);
				wordsCount++;
				currWordLen = 0;
			}
		}

		public IAnalysisResult Finish()
		{
			if(currWordLen > 0)
			{
				wordsProcessor.Add(currWord, currWordLen);
				wordsCount++;
				currWordLen = 0;
			}
			wordsProcessor.Flush();

			if (wordsProcessor.ChunkFiles.Count == 0)
				return new ComplexWordsAnalysisResult(0, new List<WordsFrequency>());

			string mergedChunkFile = Path.Combine(Path.GetDirectoryName(wordsProcessor.ChunkFiles[0]), "merged");
			var chunkReaders = new ChunkReader[wordsProcessor.ChunkFiles.Count];
			try
			{
				for(int i = 0; i < chunkReaders.Length; i++)
					chunkReaders[i] = ChunkReader.Open(wordsProcessor.ChunkFiles[i]);
				using(ChunkWriter chunkWriter = ChunkWriter.Create(mergedChunkFile))
				{
					var merger = new ChunksMerger();
					merger.Merge(chunkReaders, chunkWriter);
				}
			}
			finally
			{
				for(int i = 0; i < chunkReaders.Length; i++)
					chunkReaders[i].Dispose();
			}
			List<WordsFrequency> wordsTop;
			using(ChunkReader chunkReder = ChunkReader.Open(mergedChunkFile))
				wordsTop = new TopSelector().CalcTop(chunkReder);
			return new ComplexWordsAnalysisResult(wordsCount, wordsTop);
		}

		private int currWordLen;
		private readonly char[] currWord = new char[Const.MAX_WORD_LEN];

		private long wordsCount;

		private readonly WordsProcessor wordsProcessor = new WordsProcessor();
	}
}