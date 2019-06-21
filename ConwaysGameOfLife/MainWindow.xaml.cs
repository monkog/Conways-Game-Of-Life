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
		private int _boardSize;

		private GameCell[,] _gameBoard;

		private readonly DispatcherTimer _timer = new DispatcherTimer();

		public MainWindow()
		{
			InitializeComponent();
			_timer.Tick += TimerTick;
			_timer.Interval = new TimeSpan(0, 0, 0, 0, 80);
		}

		private void TimerTick(object sender, EventArgs e)
		{
			CalculateNextGeneration();

			for (var i = 0; i < _boardSize; i++)
				for (var j = 0; j < _boardSize; j++)
				{
					var cell = _gameBoard[i, j];
					cell.ApplyNewColor();
					var gridCell = (Rectangle) UniformGrid.Children[cell.Row * _boardSize + cell.Column];
					gridCell.Fill = new SolidColorBrush(cell.Color);
				}
		}

		private void CalculateNextGeneration()
		{
			for (var i = 0; i < _boardSize; i++)
			{
				for (var j = 0; j < _boardSize; j++)
				{
					var rt = _gameBoard[i, j];

					var aliveNeighbors = 0;

					if (i + 1 < _boardSize)
					{
						var neighbor = _gameBoard[i + 1, j];
						if (neighbor.Color == GameCell.Alive) aliveNeighbors++;
					}

					if (i + 1 < _boardSize && j + 1 < _boardSize)
					{
						var neighbor = _gameBoard[i + 1, j + 1];
						if (neighbor.Color == GameCell.Alive) aliveNeighbors++;
					}

					if (j + 1 < _boardSize)
					{
						var neighbor = _gameBoard[i, j + 1];
						if (neighbor.Color == GameCell.Alive) aliveNeighbors++;
					}

					if (i - 1 >= 0 && j + 1 < _boardSize)
					{
						var neighbor = _gameBoard[i - 1, j + 1];
						if (neighbor.Color == GameCell.Alive) aliveNeighbors++;
					}

					if (i - 1 >= 0)
					{
						var neighbor = _gameBoard[i - 1, j];
						if (neighbor.Color == GameCell.Alive) aliveNeighbors++;
					}

					if (j - 1 >= 0 && i - 1 >= 0)
					{
						var neighbor = _gameBoard[i - 1, j - 1];
						if (neighbor.Color == GameCell.Alive) aliveNeighbors++;
					}

					if (j - 1 >= 0)
					{
						var neighbor = _gameBoard[i, j - 1];
						if (neighbor.Color == GameCell.Alive) aliveNeighbors++;
					}

					if (i + 1 < _boardSize && j - 1 >= 0)
					{
						var neighbor = _gameBoard[i + 1, j - 1];
						if (neighbor.Color == GameCell.Alive) aliveNeighbors++;
					}

					if (rt.Color == GameCell.Dead)
					{
						rt.NewColor = aliveNeighbors == 3 ? GameCell.Alive : GameCell.Dead;
					}
					else
					{
						if (aliveNeighbors < 2 || aliveNeighbors > 3)
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

			_gameBoard = new GameCell[cellNumber, cellNumber];
			var brush = new SolidColorBrush(GameCell.Dead);

			for (var i = 0; i < cellNumber; i++)
				for (var j = 0; j < cellNumber; j++)
				{
					var rect = new Rectangle()
					{
						Fill = brush,
						Margin = new Thickness(1),
						Tag = new GameCell(i, j, GameCell.Dead) { NewColor = GameCell.Dead }
					};

					_gameBoard[i, j] = (GameCell) rect.Tag;
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
			_boardSize = (int)ValueSlider.Value;
			InitializeGame(_boardSize);
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
