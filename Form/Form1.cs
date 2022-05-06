using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;

namespace FutureGame
{
    public partial class Form1 : Form
    {
        private Player player = new Player(30, 30);
        private World world;

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint,
                true);

            Size = new Size(600, 600);
            world = new World(Size.Height - 60, Width);


            Application.Idle += delegate { Invalidate(); };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Update();
            DoubleBuffered = true;
            var graphics = e.Graphics;
            e.Graphics.DrawLine(Pens.Green, 0, world.Ground, Width, world.Ground);
            var brush = new SolidBrush(Color.Blue);
            graphics.FillRectangle(brush, player.x, player.y, player.Width, player.Height);
            Invalidate();
        }

        private DateTime lastUpdate = DateTime.MinValue;

        new void Update()
        {
            var now = DateTime.Now;
            var dt = (float) (now - lastUpdate).TotalMilliseconds / 100f;
            //
            if (lastUpdate != DateTime.MinValue)
            {
                player.Update(dt, player, world);
            }

            //
            lastUpdate = now;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (Keys.Space == e.KeyCode)
                player.jump(player, world);
            if (Keys.A == e.KeyCode)
                player.Move(Directrion.Left, world);
            if (Keys.D == e.KeyCode)
                player.Move(Directrion.Right, world);
        }
    }
}