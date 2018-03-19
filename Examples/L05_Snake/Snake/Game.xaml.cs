using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Snake
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    
    //enum for movement directions
    public enum Direction
    {
        Up,Right,Down,Left
    }
    public enum ChangeColor
    {
        Blue,Green,Yellow,Red,White,Orange,Violet
    }

    public partial class Game : Window
    {
        DispatcherTimer timer;
        private Random rnd = new Random();
        private int difficulty = 1;
        private int score = 0;
        private Point startPosition = new Point(100, 100);
        private Point currentPosition = new Point();
        private Size snakeSize = new Size(10, 10);
        private List<Point> snakeParts = new List<Point>();
        private Direction lastDirection = Direction.Right;
        private Direction currentDirection = Direction.Right;
        private int snakeLengt = 100;
        private List<char> colorList = new List<char>();
        private int numberOfColors = 0;
        private int colorCounter = 4;
        private char fillColor = 'g';

        public Game()
        {
            InitializeComponent();

            //init fill colors
            colorList.Add('b');
            colorList.Add('r');
            colorList.Add('y');
            colorList.Add('w');
            colorList.Add('g');

            numberOfColors = colorList.Count();

            //init game timer
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(GameTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, difficulty);
            
            //get keypresses to our OnKeyDown method
            KeyDown += new KeyEventHandler(OnKeyDown);
            timer.Start();

            //init position

            currentPosition = startPosition;
        }

        private void GameTick(object sender, EventArgs e)
        {
            int drawStep = 7;
            switch (currentDirection)
            {
            case Direction.Up:
                currentPosition.Y = currentPosition.Y - drawStep;
                break;
            case Direction.Right:
                currentPosition.X = currentPosition.X + drawStep;
                break;
            case Direction.Down:
                    currentPosition.Y = currentPosition.Y + drawStep;
                    break;
            case Direction.Left:
                    currentPosition.X = currentPosition.X - drawStep;
                    break;
            /*
            case Direction.Up:
                currentPosition.Y--;
                break;
            case Direction.Right:
                currentPosition.X++;
                break;
            case Direction.Down:
                currentPosition.Y++;
                break;
            case Direction.Left:
                currentPosition.X--;
                break;
                */
        }
            //check hits to game area
            /*
            if (currentPosition.X < 0 || currentPosition.X > gameCanvas.ActualWidth || currentPosition.Y < 0 || currentPosition.Y > gameCanvas.ActualHeight)
            {
                GameOver();
            }
            */

            //circumspect game area
            if (currentPosition.X < 0)
            {
                currentPosition.X = gameCanvas.ActualWidth - 1;
                if (colorCounter == numberOfColors-1)
                {
                    colorCounter = 0;
                    ColorChanger(ref colorCounter);
                }
                else
                {
                    ColorChanger(ref colorCounter);
                }
                
            }
            if (currentPosition.X > gameCanvas.ActualWidth)
            {
                currentPosition.X = 1;
                if (colorCounter == numberOfColors - 1)
                {
                    colorCounter = 0;
                    ColorChanger(ref colorCounter);
                }
                else
                {
                    ColorChanger(ref colorCounter);
                }
            }
            if (currentPosition.Y < 0)
            {
                currentPosition.Y = gameCanvas.ActualHeight - 1;
                if (colorCounter == numberOfColors - 1)
                {
                    colorCounter = 0;
                    ColorChanger(ref colorCounter);
                }
                else
                {
                    ColorChanger(ref colorCounter);
                }
            }
            if (currentPosition.Y > gameCanvas.ActualHeight)
            {
                currentPosition.Y = 1;
                if (colorCounter == numberOfColors - 1)
                {
                    colorCounter = 0;
                    ColorChanger(ref colorCounter);
                }
                else
                {
                    ColorChanger(ref colorCounter);
                }
            }

            //check if snake hits itself
            for(int i = 0; i <(snakeParts.Count - snakeSize.Width*2);i++)
            {
                Point point = new Point(snakeParts[i].X, snakeParts[i].Y);
                if (Math.Abs(point.X - currentPosition.X) < snakeSize.Width && Math.Abs(point.Y - currentPosition.Y) < snakeSize.Height)
                {
                    GameOver();
                    break;
                }
            }

            DrawSnake(currentPosition,fillColor);
        }

        private void DrawSnake(Point position,char color)
        {
            Ellipse ellipse = new Ellipse();

            //fill color
            switch(color)
            {
                case 'b':
                    ellipse.Fill = Brushes.Blue;
                    break;
                case 'r':
                    ellipse.Fill = Brushes.Red;
                    break;
                case 'y':
                    ellipse.Fill = Brushes.Yellow;
                    break;
                case 'w':
                    ellipse.Fill = Brushes.White;
                    break;
                case 'g':
                    ellipse.Fill = Brushes.Green;
                    break;
            }

            ellipse.Width = snakeSize.Width;
            ellipse.Height = snakeSize.Height;

            Canvas.SetTop(ellipse, position.Y);
            Canvas.SetLeft(ellipse, position.X);

            int count = gameCanvas.Children.Count;
            gameCanvas.Children.Add(ellipse);
            snakeParts.Add(position);

            if(count > snakeLengt)
            {
                gameCanvas.Children.RemoveAt(count - snakeLengt - 1);
                snakeParts.RemoveAt(count - snakeLengt - 1);
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    if (lastDirection != Direction.Down)
                    {
                        currentDirection = Direction.Up;
                    }
                    break;
                case Key.Right:
                    if (lastDirection != Direction.Left)
                    {
                        currentDirection = Direction.Right;
                    }
                    break;
                case Key.Down:
                    if (lastDirection != Direction.Up)
                    {
                        currentDirection = Direction.Down;
                    }
                    break;
                case Key.Left:
                    if (lastDirection != Direction.Right)
                    {
                        currentDirection = Direction.Left;
                    }
                    break;
                case Key.Escape:
                    Close();
                    break;
            }
            lastDirection = currentDirection;
        }

        private void GameOver()
        {
            timer.Stop();
            MessageBox.Show("Your score: " + score);
            Close();
        }

        private void ColorChanger(ref int colorCounter)
        {
            fillColor = colorList[colorCounter];
            colorCounter++;
        }
    }
}