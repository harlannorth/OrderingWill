using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


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

		private List<Tuple<int,int,IndexEntry>> unOrderedBook = new List<Tuple<int,int,IndexEntry>>();

        public Book(IndexIterator iterator)
		{

			IndexEntry entry;
			while((entry = iterator.GetNext()) != null)

			unOrderedBook.Add(Tuple.Create<int,int,IndexEntry>(entry.Page,entry.Position, entry));
		}

		/// <summary>
		/// returns a page in a book
		/// </summary>
		/// <param name="page">w</param>
		/// <returns></returns>
        public string GetPage(int page)
		{
			return string.Concat( unOrderedBook.Where(t => t.Item1 == page).OrderBy(t => t.Item2).Select(t => t.Item3.Word))
		}

	}

	public class IndexEntry
	{
		public IndexEntry(int page, int position, string word)
		{
			Page = page;
			Position = position;
			Word = word;
		}

		public string Word;
		public int Page;
		public int Position;
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
					
			}

			return entry;

			
		}
	}
}
