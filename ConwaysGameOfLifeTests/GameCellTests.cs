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
			var color = GameCell.Dead;

			var unitUnderTest = new GameCell(row, column, color);

			Assert.AreEqual(row, unitUnderTest.Row);
			Assert.AreEqual(column, unitUnderTest.Column);
			Assert.AreEqual(color, unitUnderTest.Color);
		}

		[TestMethod]
		public void ChangeColor_DeadCell_AliveCell()
		{
			var unitUnderTest = new GameCell(7, 2, GameCell.Dead);
			unitUnderTest.ChangeColor();

			Assert.AreEqual(GameCell.Alive, unitUnderTest.Color);
		}

		[TestMethod]
		public void ChangeColor_AliveCell_DeadCell()
		{
			var unitUnderTest = new GameCell(3, 6, GameCell.Alive);
			unitUnderTest.ChangeColor();

			Assert.AreEqual(GameCell.Dead, unitUnderTest.Color);
		}
	}
}
