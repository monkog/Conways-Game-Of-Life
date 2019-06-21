using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ConwaysGameOfLife
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Rectangle[,] _rectangleTab;
		private readonly DispatcherTimer _timer = new DispatcherTimer();

		public MainWindow()
		{
			InitializeComponent();
			_timer.Tick += TimerTick;
			_timer.Interval = new TimeSpan(0, 0, 0, 0, 80);
		}

		private void TimerTick(object sender, EventArgs e)
		{
			Check();
			for (var i = 0; i < _rectangleTab.GetLength(0); i++)
				for (var j = 0; j < _rectangleTab.GetLength(0); j++)
				{
					var rect = _rectangleTab[i, j];
					var rt = (GameCell)rect.Tag;
					rt.ApplyNewColor();
					rect.Fill = new SolidColorBrush(rt.Color);
				}
		}

		private void Check()
		{
			for (var i = 0; i < _rectangleTab.GetLength(0); i++)
			{
				for (var j = 0; j < _rectangleTab.GetLength(0); j++)
				{
					var rt = (GameCell)_rectangleTab[i, j].Tag;

					var dziki_dzik = 0;

					if (i + 1 < _rectangleTab.GetLength(0))
					{
						var tmp = (GameCell)_rectangleTab[i + 1, j].Tag;
						if (tmp.Color == GameCell.Alive) dziki_dzik++;
					}

					if (i + 1 < _rectangleTab.GetLength(0) && j + 1 < _rectangleTab.GetLength(0))
					{
						var tmp = (GameCell)_rectangleTab[i + 1, j + 1].Tag;
						if (tmp.Color == GameCell.Alive) dziki_dzik++;
					}

					if (j + 1 < _rectangleTab.GetLength(0))
					{
						var tmp = (GameCell)_rectangleTab[i, j + 1].Tag;
						if (tmp.Color == GameCell.Alive) dziki_dzik++;
					}

					if (i - 1 >= 0 && j + 1 < _rectangleTab.GetLength(0))
					{
						var tmp = (GameCell)_rectangleTab[i - 1, j + 1].Tag;
						if (tmp.Color == GameCell.Alive) dziki_dzik++;
					}

					if (i - 1 >= 0)
					{
						var tmp = (GameCell)_rectangleTab[i - 1, j].Tag;
						if (tmp.Color == GameCell.Alive) dziki_dzik++;
					}

					if (j - 1 >= 0 && i - 1 >= 0)
					{
						var tmp = (GameCell)_rectangleTab[i - 1, j - 1].Tag;
						if (tmp.Color == GameCell.Alive) dziki_dzik++;
					}

					if (j - 1 >= 0)
					{
						var tmp = (GameCell)_rectangleTab[i, j - 1].Tag;
						if (tmp.Color == GameCell.Alive) dziki_dzik++;
					}

					if (i + 1 < _rectangleTab.GetLength(0) && j - 1 >= 0)
					{
						var tmp = (GameCell)_rectangleTab[i + 1, j - 1].Tag;
						if (tmp.Color == GameCell.Alive) dziki_dzik++;
					}
					if (rt.Color == GameCell.Dead)
					{
						rt.NewColor = dziki_dzik == 3 ? GameCell.Alive : GameCell.Dead;
					}
					else
					{
						if (dziki_dzik < 2 || dziki_dzik > 3)
							rt.NewColor = GameCell.Dead;
						else
							rt.NewColor = GameCell.Alive;

					}
				}
			}

		}

		private void InitializeGame(int cellNumber)
		{
			UniformGrid.Children.Clear();
			UniformGrid.Columns = cellNumber;
			UniformGrid.Rows = cellNumber;

			_rectangleTab = new Rectangle[cellNumber, cellNumber];

			for (var i = 0; i < cellNumber; i++)
				for (var j = 0; j < cellNumber; j++)
				{
					var brush = new SolidColorBrush(SystemColors.ControlColor);
					var rect = new Rectangle()
					{
						Fill = brush,
						Margin = new Thickness(1, 1, 1, 1),
						Tag = new GameCell(i, j, GameCell.Dead) { NewColor = GameCell.Dead }
					};
					_rectangleTab[i, j] = rect;
					rect.MouseDown += RectMouseDown;
					UniformGrid.Children.Add(rect);
				}

		}

		private static void RectMouseDown(object sender, MouseButtonEventArgs e)
		{
			var cell = sender as Rectangle;
			var gameCell = (GameCell)cell.Tag;
			gameCell.ChangeColor();
			cell.Fill = new SolidColorBrush(gameCell.Color);
		}

		private void GameSizeChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			var cellNumber = (int)ValueSlider.Value;
			InitializeGame(cellNumber);
		}

		private void IsGameRunningChecked(object sender, RoutedEventArgs e)
		{
			_timer.Start();
		}

		private void IsGameRunningUnchecked(object sender, RoutedEventArgs e)
		{
			_timer.Stop();
		}
	}
}
