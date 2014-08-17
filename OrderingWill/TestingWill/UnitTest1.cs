using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text.RegularExpressions;

namespace TestingWill
{
	[TestClass]
	public class TestingWill
	{

		//sample from http://datahub.io/dataset/william-shakespeare-plays/resource/514d3c17-8469-4ae8-b83f-57678af50735
		[TestMethod]
		public void CanLoadAndIterate()
		{
			
			bool matches = true;
			

			OrderingWill.IndexIterator iterator = new OrderingWill.IndexIterator(@"C:\Users\hnorth\Documents\GitHub\OrderingWill\OrderingWill\TestingWill\Data\HenryIV.csv");

			//assert that each GetLine in iterator is the same as the one I do

			string[] allLines = System.IO.File.ReadAllLines(@"C:\Users\hnorth\Documents\GitHub\OrderingWill\OrderingWill\TestingWill\Data\HenryIV.csv");

			int i = 0;
			do
			{
				string text = Regex.Replace(allLines[i].Split(";".ToCharArray())[5],@"[""\\]", string.Empty).Trim();
				string iteratorText = iterator.GetNext().Text;

				matches = iteratorText.Equals(text);
				i++;

			} while (matches && i < allLines.Count());

			Assert.IsTrue(matches);

		}
	}
}
