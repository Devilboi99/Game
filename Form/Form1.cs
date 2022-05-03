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
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
 
            Size = new Size(600, 600);
 
           
 
            Application.Idle += delegate { Invalidate(); };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Update();
            DoubleBuffered = true;
            world = new World(Size.Height - 60);
            var graphics = e.Graphics;
            e.Graphics.DrawLine(Pens.Green, 0, world.ground, Width, world.ground);
            var brush = new SolidBrush(Color.Blue);
            graphics.FillRectangle(brush, player.x, player.y, 30,  30);
            Invalidate();   
        }

        private DateTime lastUpdate = DateTime.MinValue;
 
        new void Update()
        {
            var now = DateTime.Now;
            var dt = (float)(now - lastUpdate).TotalMilliseconds / 100f;
            //
            if (lastUpdate != DateTime.MinValue)
            {
                player.Update(dt,player,world);
            }
            //
            lastUpdate = now;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    player.jump(player,world);
                    break;
                case Keys.W:
                    player.Move(Directrion.Up, player);
                    break;
                case Keys.D:
                    player.Move(Directrion.Right, player);
                    break;
                case Keys.A:
                    player.Move(Directrion.Left, player);
                    break;
                case Keys.S:
                    player.Move(Directrion.Down, player);
                    break;
            }
        }
    }
}