using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ConwaysGameOfLife.Classes
{
    class Tiles
    {
        public Rectangle rec { get => oRec; set => oRec = value; }
        public bool isAliveCell { get => alive; set => alive = value; }
        public int xCoor { get => xc; set => xc = value; }
        public int yCoor { get => yc; set => yc = value; }
        public List<Tiles> neighbors { get => n; set => n = value; }
        public bool isAliveNextGen { get => gen; set => gen = value; }


        private bool alive;
        private Rectangle oRec;
        private int xc;
        private int yc;
        private List<Tiles> n;
        private bool gen;
        public Tiles(bool i, int x, int y)
        {
            isAliveCell = i;
            rec = new Rectangle();
            rec.Fill = Brushes.LightGray;
            rec.StrokeThickness = .25;
            rec.Stroke = Brushes.Gray;
            rec.MouseLeftButtonDown += rec_ChangeLife;
            xCoor = x;
            yCoor = y;
            neighbors = new List<Tiles>();
        }

        public void rec_ChangeLife(object sender, MouseButtonEventArgs e)
        {
            if (isAliveCell)
            {
                rec.Fill = Brushes.LightGray;
                isAliveCell = false;
            }
            else
            {
                rec.Fill = Brushes.Blue;
                isAliveCell = true;
            }
        }

        internal void FindNeighbor(Tiles[,] gameTiles)
        {
            int maxLength = gameTiles.GetLength(0)-1;
            int maxHeight = gameTiles.GetLength(1)-1;
            if(xCoor != maxLength)
            {
                neighbors.Add(gameTiles[xCoor + 1, yCoor]);
            }
            if (xCoor != maxLength && yCoor != maxHeight)
            {
                neighbors.Add(gameTiles[xCoor + 1, yCoor + 1]);
            }
            if (yCoor != maxHeight)
            {
                neighbors.Add(gameTiles[xCoor, yCoor + 1]);
            }
            if (xCoor != 0 && yCoor != maxHeight)
            {
                neighbors.Add(gameTiles[xCoor - 1, yCoor + 1]);
            }
            if (xCoor != 0)
            {
                neighbors.Add(gameTiles[xCoor - 1, yCoor]);
            }
            if (xCoor != 0 && yCoor != 0)
            {
                neighbors.Add(gameTiles[xCoor - 1, yCoor - 1]);
            }
            if (yCoor != 0)
            {
                neighbors.Add(gameTiles[xCoor, yCoor - 1]);
            }
            if (xCoor != maxLength && yCoor != 0)
            {
                neighbors.Add(gameTiles[xCoor + 1, yCoor - 1]);
            }

        }

        internal int getAliveNeighbors()
        {
            int counter = 0;
            foreach (Tiles t in neighbors)
            {
                if (t.isAliveCell)
                {
                    counter++;
                }
            }
            return counter;
        }
    }
}
