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
			var state = GameCell.Dead;

			var unitUnderTest = new GameCell(row, column, state);

			Assert.AreEqual(row, unitUnderTest.Row);
			Assert.AreEqual(column, unitUnderTest.Column);
			Assert.AreEqual(state, unitUnderTest.State);
		}

		[TestMethod]
		public void ChangeState_DeadCell_AliveCell()
		{
			var unitUnderTest = new GameCell(7, 2, GameCell.Dead);
			unitUnderTest.ChangeState();

			Assert.AreEqual(GameCell.Alive, unitUnderTest.State);
		}

		[TestMethod]
		public void ChangeState_AliveCell_DeadCell()
		{
			var unitUnderTest = new GameCell(3, 6, GameCell.Alive);
			unitUnderTest.ChangeState();

			Assert.AreEqual(GameCell.Dead, unitUnderTest.State);
		}

		[DataTestMethod]
		[DataRow(0, true)]
		[DataRow(1, true)]
		[DataRow(2, true)]
		[DataRow(3, false)]
		[DataRow(4, true)]
		public void DetermineCellSurvival_DeadCell_ExpectedSurvival(int aliveNeighbors, bool isDead)
		{
			var unitUnderTest = new GameCell(3, 6, GameCell.Dead);

			unitUnderTest.DetermineCellSurvival(aliveNeighbors);
			unitUnderTest.NextGeneration();
			var expectedState = isDead ? GameCell.Dead : GameCell.Alive;

			Assert.AreEqual(expectedState, unitUnderTest.State);
		}

		[DataTestMethod]
		[DataRow(0, true)]
		[DataRow(1, true)]
		[DataRow(2, false)]
		[DataRow(3, false)]
		[DataRow(4, true)]
		public void DetermineCellSurvival_AliveCell_ExpectedSurvival(int aliveNeighbors, bool isDead)
		{
			var unitUnderTest = new GameCell(3, 6, GameCell.Alive);

			unitUnderTest.DetermineCellSurvival(aliveNeighbors);
			unitUnderTest.NextGeneration();
			var expectedState = isDead ? GameCell.Dead : GameCell.Alive;

			Assert.AreEqual(expectedState, unitUnderTest.State);
		}
	}
}
