using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FutureGame
{
    public partial class Form1 : Form
    {
        private Player player = new Player(0, 0);
        private World world;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DoubleBuffered = true;
            var graphics = e.Graphics;
            var pen = new Pen(Color.Black);
            var brush = new SolidBrush(Color.Blue);
            graphics.FillRectangle(brush, Player.x, Player.y, 30, 30);
            world = new World(Size.Height - 90);
            graphics.DrawLine(pen, 0, world.ground, Size.Width, world.ground + 30);
            CheckFall(world);
            Invalidate();
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    Player.Move(Directrion.Up);
                    break;
                case Keys.D:
                    Player.Move(Directrion.Right);
                    break;
                case Keys.A:
                    Player.Move(Directrion.Left);
                    break;
                case Keys.S:
                    Player.Move(Directrion.Down);
                    break;
            }
        }

        private void CheckFall(World earth)
        {
            if (Player.y <= earth.ground)
                Player.fall();
        }
    }
}