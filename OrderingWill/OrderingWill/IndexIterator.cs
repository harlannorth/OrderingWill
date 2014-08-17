using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace OrderingWill
{
	public class IndexIterator
	{

		private string _uri;
		private int position;

		private string[] _fileLines;
		private Func<string, IndexEntry> _entryParser;

		public IndexIterator(string uri, Func<string, IndexEntry> createEntryParser)
			: this(uri)
		{
			_entryParser = createEntryParser;
		}

		public IndexIterator(string uri)
		{
			position = 0;
			_fileLines = File.ReadAllLines(uri);
		}

		private List<IndexEntry> Entries;
		public IndexEntry GetNext()
		{
			IndexEntry entry = null;

			//make sure there are still lines to read
			if (position <= _fileLines.Count() - 1)
			{
				string[] entryAsStrings = _fileLines[position].Split(";".ToCharArray())
					.Select(str => Regex.Replace(str, @"[""\\]", string.Empty).Trim()).ToArray();

				entry = new IndexEntry(int.Parse(entryAsStrings[0]), entryAsStrings[1], entryAsStrings[3], entryAsStrings[4], entryAsStrings[5]);
			}

			position++;

			return entry;


		}
	}
}
