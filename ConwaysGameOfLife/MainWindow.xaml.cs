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
					rt.Color = rt.NewColor;

					var brush = rt.Color == 1 ? new SolidColorBrush(Colors.Blue) : new SolidColorBrush(SystemColors.ControlColor);
					rect.Fill = brush;
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
						if (tmp.Color == 1) dziki_dzik++;
					}

					if (i + 1 < _rectangleTab.GetLength(0) && j + 1 < _rectangleTab.GetLength(0))
					{
						var tmp = (GameCell)_rectangleTab[i + 1, j + 1].Tag;
						if (tmp.Color == 1) dziki_dzik++;
					}

					if (j + 1 < _rectangleTab.GetLength(0))
					{
						var tmp = (GameCell)_rectangleTab[i, j + 1].Tag;
						if (tmp.Color == 1) dziki_dzik++;
					}

					if (i - 1 >= 0 && j + 1 < _rectangleTab.GetLength(0))
					{
						var tmp = (GameCell)_rectangleTab[i - 1, j + 1].Tag;
						if (tmp.Color == 1) dziki_dzik++;
					}

					if (i - 1 >= 0)
					{
						var tmp = (GameCell)_rectangleTab[i - 1, j].Tag;
						if (tmp.Color == 1) dziki_dzik++;
					}

					if (j - 1 >= 0 && i - 1 >= 0)
					{
						var tmp = (GameCell)_rectangleTab[i - 1, j - 1].Tag;
						if (tmp.Color == 1) dziki_dzik++;
					}

					if (j - 1 >= 0)
					{
						var tmp = (GameCell)_rectangleTab[i, j - 1].Tag;
						if (tmp.Color == 1) dziki_dzik++;
					}

					if (i + 1 < _rectangleTab.GetLength(0) && j - 1 >= 0)
					{
						var tmp = (GameCell)_rectangleTab[i + 1, j - 1].Tag;
						if (tmp.Color == 1) dziki_dzik++;
					}
					if (rt.Color == 0)
					{
						rt.NewColor = dziki_dzik == 3 ? 1 : 0;
					}
					else
					{
						if (dziki_dzik < 2 || dziki_dzik > 3)
							rt.NewColor = 0;
						else
							rt.NewColor = 1;

					}
				}
			}

		}

		private void Draw(int howMany)
		{
			UniformGrid.Children.Clear();
			UniformGrid.Columns = howMany;
			UniformGrid.Rows = howMany;

			_rectangleTab = new Rectangle[howMany, howMany];

			for (var i = 0; i < howMany; i++)
				for (var j = 0; j < howMany; j++)
				{
					var brush = new SolidColorBrush(SystemColors.ControlColor);
					var rect = new Rectangle()
					{
						Fill = brush,
						Margin = new Thickness(1, 1, 1, 1),
						Tag = new GameCell(i, j) { Color = 0, NewColor = 0 }
					};
					_rectangleTab[i, j] = rect;
					rect.MouseDown += RectMouseDown;
					UniformGrid.Children.Add(rect);
				}

		}

		private static void RectMouseDown(object sender, MouseButtonEventArgs e)
		{
			var rect = sender as Rectangle;
			var rt = (GameCell)rect.Tag;
			var brush = rt.Color == 1 ? new SolidColorBrush(SystemColors.ControlColor) : new SolidColorBrush(Colors.Blue);
			rt.Color = (rt.Color + 1) % 2;
			rect.Fill = brush;

		}

		private void GameSizeChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			var howMany = (int)ValueSlider.Value;
			Draw(howMany);
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
