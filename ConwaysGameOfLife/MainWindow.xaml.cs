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
        Rectangle[,] rectangleTab;
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 80);
            is_game_running.Visibility = Visibility.Hidden;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            check();
            for (int i = 0; i < rectangleTab.GetLength(0); i++)
                for (int j = 0; j < rectangleTab.GetLength(0); j++)
                {
                    Rectangle rect = rectangleTab[i, j];
                    rectTag rt = (rectTag)rect.Tag;
                    rt.color = rt.new_color;

                    SolidColorBrush brush;

                    if (rt.color == 1)
                        brush = new SolidColorBrush(Colors.Blue);
                    else
                        brush = new SolidColorBrush(SystemColors.ControlColor);
                    rect.Fill = brush;
                }
        }

        void check()
        {
            for (int i = 0; i < rectangleTab.GetLength(0); i++)
            {
                for (int j = 0; j < rectangleTab.GetLength(0); j++)
                {
                    rectTag rt = (rectTag)rectangleTab[i, j].Tag;

                    int dziki_dzik = 0;

                    if (i + 1 < rectangleTab.GetLength(0))
                    {
                        rectTag tmp = (rectTag)rectangleTab[i + 1, j].Tag;
                        if (tmp.color == 1) dziki_dzik++;
                    }

                    if (i + 1 < rectangleTab.GetLength(0) && j + 1 < rectangleTab.GetLength(0))
                    {
                        rectTag tmp = (rectTag)rectangleTab[i + 1, j + 1].Tag;
                        if (tmp.color == 1) dziki_dzik++;
                    }

                    if (j + 1 < rectangleTab.GetLength(0))
                    {
                        rectTag tmp = (rectTag)rectangleTab[i, j + 1].Tag;
                        if (tmp.color == 1) dziki_dzik++;
                    }

                    if (i - 1 >= 0 && j + 1 < rectangleTab.GetLength(0))
                    {
                        rectTag tmp = (rectTag)rectangleTab[i - 1, j + 1].Tag;
                        if (tmp.color == 1) dziki_dzik++;
                    }

                    if (i - 1 >= 0)
                    {
                        rectTag tmp = (rectTag)rectangleTab[i - 1, j].Tag;
                        if (tmp.color == 1) dziki_dzik++;
                    }

                    if (j - 1 >= 0 && i - 1 >= 0)
                    {
                        rectTag tmp = (rectTag)rectangleTab[i - 1, j - 1].Tag;
                        if (tmp.color == 1) dziki_dzik++;
                    }

                    if (j - 1 >= 0)
                    {
                        rectTag tmp = (rectTag)rectangleTab[i, j - 1].Tag;
                        if (tmp.color == 1) dziki_dzik++;
                    }

                    if (i + 1 < rectangleTab.GetLength(0) && j - 1 >= 0)
                    {
                        rectTag tmp = (rectTag)rectangleTab[i + 1, j - 1].Tag;
                        if (tmp.color == 1) dziki_dzik++;
                    }
                    if (rt.color == 0)
                    {

                        if (dziki_dzik == 3)
                            rt.new_color = 1;
                        else
                            rt.new_color = 0;
                    }
                    else
                    {
                        if (dziki_dzik < 2 || dziki_dzik > 3)
                            rt.new_color = 0;
                        else
                            rt.new_color = 1;

                    }
                }
            }

        }

        void draw(int howMany)
        {
            uniform_grid.Children.Clear();
            uniform_grid.Columns = howMany;
            uniform_grid.Rows = howMany;

            rectangleTab = new Rectangle[howMany, howMany];

            for (int i = 0; i < howMany; i++)
                for (int j = 0; j < howMany; j++)
                {
                    SolidColorBrush brush = new SolidColorBrush(SystemColors.ControlColor);
                    Rectangle rect = new Rectangle()
                    {
                        Fill = brush,
                        Margin = new Thickness(1, 1, 1, 1),
                        Tag = new rectTag { color = 0, x = j, y = i, new_color = 0 }
                    };
                    rectangleTab[i, j] = rect;
                    rect.MouseDown += rect_MouseDown;
                    uniform_grid.Children.Add(rect);
                }

        }

        void rect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            SolidColorBrush brush;
            rectTag rt = (rectTag)rect.Tag;
            if (rt.color == 1)
                brush = new SolidColorBrush(SystemColors.ControlColor);
            else
                brush = new SolidColorBrush(Colors.Blue);
            rt.color = (rt.color + 1) % 2;
            rect.Fill = brush;

        }

        private void Valueslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int howMany = (int)Valueslider.Value;
            draw(howMany);
        }

        public class rectTag
        {
            public int color { get; set; }
            public int x { get; set; }
            public int y { get; set; }
            public int new_color { get; set; }
        }

        private void class_version_Click(object sender, RoutedEventArgs e)
        {
            if (class_version.IsChecked == true)

                is_game_running.Visibility = Visibility.Hidden;
            else
                is_game_running.Visibility = Visibility.Visible;
        }

        private void is_game_running_Checked(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void is_game_running_Unchecked(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void class_version_Checked(object sender, RoutedEventArgs e)
        {
            class_version.Margin = new Thickness(2, 0, 401, 8);
            Valueslider.Margin = new Thickness(125, 0, 0, 8);
            Value_label.Margin = new Thickness(100, 0, 0, 2);
        }

        private void class_version_Unchecked(object sender, RoutedEventArgs e)
        {
            class_version.Margin = new Thickness(125, 0, 297, 8);
            is_game_running.Margin = new Thickness(2, 0, 401, 8);
            Valueslider.Margin = new Thickness(250, 0, 0, 8);
            Value_label.Margin = new Thickness(220, 0, 0, 2);
        }
    }
}





