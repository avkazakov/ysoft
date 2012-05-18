namespace Analyzer.Core
{
	internal struct CharRange
	{
		public CharRange(char[] buf, int len)
			: this()
		{
			Buf = buf;
			Len = len;
		}

		public char[] Buf { get; private set; }
		public int Len { get; private set; }
	}
}