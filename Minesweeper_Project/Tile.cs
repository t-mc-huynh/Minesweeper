using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Minesweeper_Project
{
    class Tile
    {
        public Button button = new Button();
        public bool isRevealed;
        public bool isFlagged;
        public int value;

        public Tile(int val)
        {
            value = val;
            button.Content = " ";
            isRevealed = false;
            isFlagged = false;
        }
        public void reveal()
        {
            if (value > 0)
            {
                button.Content = value;
                isRevealed = true;
            }
            else if (value == -1)
            {
                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri("/Users/Brainstorm/Desktop/bomb-512.png", UriKind.Relative));
                button.Background = brush;
                
                //button.Background = Brushes.Black;
            }
            else
                button.IsEnabled = false;

            isRevealed = true;
        }

        public void flag()
        {
            if (isFlagged)
            {
                isFlagged = false;
            }
            else
            {
                isFlagged = true;
            }

            if (isFlagged)
            {
                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri("/Users/Brainstorm/Desktop/red-flag.png", UriKind.Relative));
                button.Background = brush;

                //button.Background = Brushes.DeepPink;
            }
            else
                button.Background = Brushes.LightGray;
        }
    }
}
