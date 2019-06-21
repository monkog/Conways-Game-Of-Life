namespace ConwaysGameOfLife
{
	/// <summary>
	/// Describes a single cell displayed in the game.
	/// </summary>
	public class GameCell
	{
		public int Color { get; set; }

		/// <summary>
		/// Gets the column index.
		/// </summary>
		public int Column { get; }

		/// <summary>
		/// Gets the row index.
		/// </summary>
		public int Row { get; }

		public int NewColor { get; set; }

		public GameCell(int row, int column)
		{
			Column = column;
			Row = row;
		}
	}
}
