using System.Windows;
using System.Windows.Media;

namespace ConwaysGameOfLife
{
	/// <summary>
	/// Describes a single cell displayed in the game.
	/// </summary>
	public class GameCell
	{
		public static readonly Color Dead = SystemColors.ControlColor;

		public static readonly Color Alive = Colors.Blue;

		private Color _newColor;

		/// <summary>
		/// Gets the cell state.
		/// </summary>
		public Color State { get; private set; }

		/// <summary>
		/// Gets the column index.
		/// </summary>
		public int Column { get; }

		/// <summary>
		/// Gets the row index.
		/// </summary>
		public int Row { get; }

		public GameCell(int row, int column, Color state)
		{
			Column = column;
			Row = row;
			State = state;
		}

		/// <summary>
		/// Changes the state of the control to the opposite value.
		/// </summary>
		public void ChangeState()
		{
			State = State == Dead ? Alive : Dead;
		}

		/// <summary>
		/// Calculates whether this cell will survive to the next generation.
		/// </summary>
		public void DetermineCellSurvival(int aliveNeighbors)
		{
			if (State == Dead)
			{
				_newColor = aliveNeighbors == 3 ? Alive : Dead;
			}
			else
			{
				if (aliveNeighbors < 2 || aliveNeighbors > 3)
					_newColor = Dead;
				else
					_newColor = Alive;
			}
		}

		/// <summary>
		/// Moves the cell to the next generation.
		/// </summary>
		public void NextGeneration()
		{
			State = _newColor;
		}
	}
}
