using ConwaysGameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConwaysGameOfLifeTests
{
	[TestClass]
	public class GameCellTests
	{
		[TestMethod]
		public void Constructor_ProperValues_PropertiesAssigned()
		{
			const int row = 7;
			const int column = 2;


			var unitUnderTest = new GameCell(row, column);

			Assert.AreEqual(row, unitUnderTest.Row);
			Assert.AreEqual(column, unitUnderTest.Column);
		}
	}
}
