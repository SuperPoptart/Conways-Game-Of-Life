using ConwaysGameOfLife.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ConwaysGameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int x;
        int y;
        int timerSpeed;
        DispatcherTimer time = new DispatcherTimer();
        Tiles[,] gameTiles = new Tiles[0, 0];
        Random rand = new Random();
        static bool timerRun;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSetSize_Click(object sender, RoutedEventArgs e)
        {
            x = (int)slrXSize.Value;
            y = (int)slrYSize.Value;

            grdGame.Children.Clear();
            grdGame.Rows = x;
            grdGame.Columns = y;

            gameTiles = new Tiles[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int b = 0; b < y; b++)
                {
                    Tiles tile = new Tiles(false, i, b);
                    gameTiles[i, b] = tile;
                }

            }

            foreach (Tiles t in gameTiles)
            {
                t.FindNeighbor(gameTiles);
                grdGame.Children.Add(t.rec);
            }
        }

        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            foreach (Tiles i in gameTiles)
            {
                int d = rand.Next(2);
                switch (d)
                {
                    case 1:
                        if (i.isAliveCell)
                        {
                            i.rec.Fill = Brushes.LightGray;
                            i.isAliveCell = false;
                        }
                        else
                        {
                            i.rec.Fill = Brushes.Blue;
                            i.isAliveCell = true;
                        }
                        break;
                    case 2:
                        break;
                    default:
                        break;
                }

            }
        }

        private void btnStepForward_Click(object sender, RoutedEventArgs e)
        {
            UpdateBoard();
        }

        private void UpdateBoard()
        {
            foreach (Tiles t in gameTiles)
            {
                int alive = t.getAliveNeighbors();
                if (alive < 2 && t.isAliveCell == true)
                {
                    gameTiles[t.xCoor, t.yCoor].isAliveNextGen = false;
                }
                if (alive == 3 && t.isAliveCell == true || alive == 2 && t.isAliveCell == true || t.isAliveCell == false && alive == 3)
                {
                    gameTiles[t.xCoor, t.yCoor].isAliveNextGen = true;
                }
                if (alive > 3 && t.isAliveCell == true)
                {
                    gameTiles[t.xCoor, t.yCoor].isAliveNextGen = false;
                }
            }
            foreach (Tiles j in gameTiles)
            {
                if (j.isAliveNextGen)
                {
                    j.rec.Fill = Brushes.Blue;
                    j.isAliveCell = true;
                    j.isAliveNextGen = false;
                }
                else
                {
                    j.rec.Fill = Brushes.LightGray;
                    j.isAliveCell = false;
                }
            }
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            time.Start();
            timerRun = true;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            time.Stop();
            timerRun = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            x = (int)slrXSize.Value;
            y = (int)slrYSize.Value;
            timerSpeed = 1000/(int)slrSPeed.Value;
            time.Interval = new TimeSpan(0, 0, 0, 0, timerSpeed);
            time.Tick += new EventHandler(tickMethod);

            grdGame.Rows = x;
            grdGame.Columns = y;

            gameTiles = new Tiles[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int b = 0; b < y; b++)
                {
                    Tiles tile = new Tiles(false, i, b);

                    gameTiles[i, b] = tile;
                }

            }
            foreach (Tiles t in gameTiles)
            {
                t.FindNeighbor(gameTiles);
                grdGame.Children.Add(t.rec);
            }
        }

        private void tickMethod(object sender, EventArgs e)
        {
            UpdateBoard();
        }

        private void btnSpeed_Click(object sender, RoutedEventArgs e)
        {
            timerSpeed = 1000/(int)slrSPeed.Value;
            time.Interval = new TimeSpan(0, 0, 0, 0, timerSpeed);

            if(timerRun)
            {
                time.Stop();
                time.Start();
            }
        }
    }
}
