using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace northboundsnails.tools
{
	//public interface ITree<T>
	//{
	//	IEnumerable<T> GetItems();

	//	T GetItem();

	//	void AddItem(T item);

	//	void RemoveItem(T item);

	//}

}

namespace OrderingWill
{
	public class Book
	{
		private IndexIterator _iterator;

		private List<Tuple<int,int,int,IndexEntry>> unOrderedBook = new List<Tuple<int,int,int,IndexEntry>>();

        public Book(IndexIterator iterator)
		{
			IndexEntry entry;
			while((entry = iterator.GetNext()) != null)

			unOrderedBook.Add(Tuple.Create<int,int,int,IndexEntry>(entry.LineNumber.Act, entry.LineNumber.Scene,entry.LineNumber.Line, entry));
		}

		/// <summary>
		/// returns a page in a book
		/// </summary>
		/// <param name="page">w</param>
		/// <returns></returns>
        public string GetScene(int scene)
		{
			return string.Concat(unOrderedBook.Where(t => t.Item2 == scene)
				.OrderBy(t => t.Item3)
				.Select(t => t.Item4.Text));
		}

	}

	/*
	 * 
	 *         <table name="bill_play_text">
            <column name="line_id">1</column>
            <column name="play_name">Henry IV</column>
            <column name="speech_number"/>
            <column name="line_number"/>
            <column name="speaker"/>
            <column name="text_entry">ACT I</column>
        </table>*/

	public class IndexEntry
	{
		public IndexEntry(int line, string playName, string lineNumber, string speaker, string text)
		{
			Line = line;
			PlayName = playName;
			LineNumber = new PlayLineNumber(lineNumber);
			Speaker = speaker;
			Text = text;

		}

		public int Line;
		public string Text;
		public string Speaker;
		public int Speech;
		public string PlayName;
		public PlayLineNumber LineNumber;

		
		public class PlayLineNumber
		{
			public PlayLineNumber(string playLineNumber)
			{
				LineNumber = playLineNumber;
			}

			private void splitLine()
			{

				if (!string.IsNullOrEmpty(LineNumber))
				{
					var splitLineNumber = LineNumber.Split(".".ToCharArray()).Select(value => int.Parse(value)).ToArray();

					if (splitLineNumber.Count() == 3)
					{
						_act = splitLineNumber[0];
						_scene = splitLineNumber[1];
						_line = splitLineNumber[2];
					}
					else
					{
						_act = 0;
						_scene = 0;
						_line = 0;
					}
				}
			}

			public string LineNumber;

			public int Act { get { if (_act == null) { splitLine(); } return _act; } }
			public int Scene { get { if (_scene == null) { splitLine(); } return _scene; } }
			public int Line { get { if (_line == null) { splitLine(); } return _line; } }

			private int _act;
			private int _scene;
			private int _line;
		}
			
	}

	public class IndexIterator
	{

		private string _uri;
		private int position;

		private string[] _fileLines;
		private Func<string, IndexEntry> _entryParser;

		public IndexIterator(string uri, Func<string,IndexEntry> createEntryParser):this(uri)
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
