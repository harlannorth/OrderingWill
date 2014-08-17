using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingWill
{
	public class Book
	{
		private IndexIterator _iterator;

		private List<Tuple<int, int, int, IndexEntry>> unOrderedBook = new List<Tuple<int, int, int, IndexEntry>>();

		public Book(IndexIterator iterator)
		{
			IndexEntry entry;
			while ((entry = iterator.GetNext()) != null)

				unOrderedBook.Add(Tuple.Create<int, int, int, IndexEntry>(entry.LineNumber.Act, entry.LineNumber.Scene, entry.LineNumber.Line, entry));
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
}
