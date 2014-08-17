using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




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
	/*
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

}
