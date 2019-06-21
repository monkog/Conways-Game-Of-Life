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

		public Color Color { get; private set; }

		/// <summary>
		/// Gets the column index.
		/// </summary>
		public int Column { get; }

		/// <summary>
		/// Gets the row index.
		/// </summary>
		public int Row { get; }

		public Color NewColor { get; set; }

		public GameCell(int row, int column, Color color)
		{
			Column = column;
			Row = row;
			Color = color;
		}

		/// <summary>
		/// Changes the color of the control to the opposite value.
		/// </summary>
		public void ChangeColor()
		{
			Color = Color == Dead ? Alive : Dead;
		}

		/// <summary>
		/// Changes the color of the control to the new color.
		/// </summary>
		public void ApplyNewColor()
		{
			Color = NewColor;
		}
	}
}
