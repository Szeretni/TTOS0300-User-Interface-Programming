using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SnakeGame
{
    class Bonus : IGameObject
    {
        public Point Position { get; set; }
        public int Radius { get; set; }

        public void Draw(DrawingContext drawingContext)
        {
            drawingContext.DrawEllipse(Brushes.Red, null, Position, Radius, Radius);
        }

        public void Update()
        {  
        }
    }
}