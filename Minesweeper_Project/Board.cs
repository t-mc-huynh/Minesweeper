using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Minesweeper_Project
{
    class Board
    {
        public Tile[,] game;
        public int rowNum;
        public int numOfBombs;
        MessageBoxResult result;
        public int safeTilesRemaining;

        public Board(int difficulty)
        {
            if (difficulty == 0)
            {
                rowNum = 8;
                numOfBombs = 8;
                safeTilesRemaining = (rowNum * rowNum) - numOfBombs;
                game = new Tile[8, 8];
            }
            else if (difficulty == 1)
            {
                rowNum = 12;
                numOfBombs = 24;
                safeTilesRemaining = (rowNum * rowNum) - numOfBombs;
                game = new Tile[12, 12];
            }
            else
            {
                rowNum = 16;
                numOfBombs = 40;
                safeTilesRemaining = (rowNum * rowNum) - numOfBombs;
                game = new Tile[16, 16];
            }
        }

        public void SeedBoard()
        {
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < rowNum; j++)
                {
                    // default number
                    game[i, j] = new Tile(0);
                }
            }

            Random rnd = new Random();
            int position_x = rnd.Next(rowNum);
            int position_y = rnd.Next(rowNum);

            for (int i = 0; i < numOfBombs; i++)
            {
                while (game[position_x, position_y].value != 0)
                {
                    position_x = rnd.Next(rowNum);
                    position_y = rnd.Next(rowNum);
                }
                game[position_x, position_y].value = -1;
            }
        }
        public void SetButtons(Grid grid)
        {
            Button button;

            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();

            for (int i = 0; i < rowNum; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < rowNum; j++)
                {
                    button = game[i, j].button;
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    button.Width = button.Height = 350 / rowNum;

                    button.Click += Button_Click;
                    button.MouseRightButtonDown += Button_MouseRightButtonDown;

                    grid.Children.Add(button);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UIElement button = (UIElement)sender;

            int x = Grid.GetRow(button);
            int y = Grid.GetColumn(button);

            reveal(x, y);

            if (safeTilesRemaining == 0)
                result = MessageBox.Show("You Win!");
        }

        private void Button_MouseRightButtonDown(object sender, RoutedEventArgs e)
        {
            UIElement button = (UIElement)sender;

            int x = Grid.GetRow(button);
            int y = Grid.GetColumn(button);

            game[x, y].flag();
        }

        public void SetLiveNeighbors()
        {
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < rowNum; j++)
                {
                    if (game[i, j].value != -1)
                    {
                        for (int k = i - 1; k <= i + 1; k++)
                        {
                            for (int l = j - 1; l <= j + 1; l++)
                            {
                                if (l >= 0 && l < rowNum && k >= 0 && k < rowNum)
                                {
                                    if (game[k, l].value == -1)
                                    {
                                        game[i, j].value++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void reveal(int x, int y)
        {
            if (game[x, y].isRevealed == false)
                safeTilesRemaining--;

            game[x, y].reveal();

            if (game[x, y].value == 0)
            {
                for (int i = x - 1; i <= x + 1; i++)
                {
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        if (i >= 0 && i < rowNum && j >= 0 && j < rowNum && game[i, j].isRevealed == false)
                        {
                            reveal(i, j);
                        }
                    }
                }
            }

            if (game[x, y].value == -1)
            {
                lose();
            }
        }

        public void lose()
        {
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < rowNum; j++)
                {
                    game[i, j].reveal();
                }
            }
            result = MessageBox.Show("You Lose");
        }
    }
}
