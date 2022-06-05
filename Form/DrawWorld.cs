using System.Drawing;
using System.Windows.Forms;
using Model;

namespace FutureGame
{
    public partial class OuterRoom
    {
        private void PaintWorld()
        {
            var ground = new PictureBox();
            var floor = new PictureBox();
            var rightUpFloor = new PictureBox();
            var leftUpFloor = new PictureBox();
            CreateElement(rightUpFloor, new Point(0, (int) (_currentLevel.Ground / 1.6)), 
                _currentLevel.RightSide / 4, 60);
            CreateElement(ground, new Point(0, _currentLevel.Ground), _currentLevel.RightSide, 60);
            CreateElement(floor, new Point(_currentLevel.RightSide / 2 - 150,
                _currentLevel.Ground - 130), 300, 130);
            CreateElement(leftUpFloor, new Point(_currentLevel.RightSide - _currentLevel.RightSide / 4,
                (int) (_currentLevel.Ground / 1.6)), _currentLevel.RightSide / 4, 60);
            rightUpFloor.Paint += PainterUpFloor;
            ground.Paint += PainterGround;
            floor.Paint += PaintFloor;
            leftUpFloor.Paint += PainterUpFloor;
        }
        
        private void PainterUpFloor(object obj, PaintEventArgs e)
            => e.Graphics.DrawImage(_floorImage, new RectangleF(0, 0, _currentLevel.RightSide / 4f, 60));
        
        private void PainterGround(object obj, PaintEventArgs e)
        {
            var x = 0;
            for (var j = 0; j < 21; j++)
            {
                e.Graphics.DrawImage(_floorImage,
                    new RectangleF(x, 0, _currentLevel.RightSide / 20,
                        60));
                x += _currentLevel.RightSide / 20;
            }
        }

        private void PaintFloor(object obj, PaintEventArgs e)
        {
            e.Graphics.DrawImage(_floorImage,
                new RectangleF(0, 0, 150, 130));
            e.Graphics.DrawImage(_floorImage,
                new RectangleF(150, 0, 150, 130));
        }
        
        private void CreateElement(Control elementWorld, Point location, int width, int height)
        {
            elementWorld.Location = location;
            elementWorld.Width = width;
            elementWorld.Height = height;
            Controls.Add(elementWorld);
        }
    }
}