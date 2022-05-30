using System;
using System.Drawing;
using System.Windows.Forms;
using Model;
using System.Media;

namespace FutureGame
{
    public partial class OuterRoom : Form
    {
        private Image playerImage;
        private Player player = new Player(30, 30);

        private World world;
//        private SoundPlayer mediaPlayer = new SoundPlayer("music/background.wav");


        public OuterRoom()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint,
                true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //  mediaPlayer.Play();
            WindowState = FormWindowState.Maximized;
            var sizeForm = Screen.PrimaryScreen.WorkingArea.Size;
            world = new World(sizeForm.Height - 60, sizeForm.Width);
            world.CreateObjectWorld();
            Application.Idle += delegate { Invalidate(); };
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Update();

            e.Graphics.DrawString("Где же выход?", new Font("Arial", 16), Brushes.Black,
                new Point(world.RightSide / 2 - 80, world.Ground / 3));
            playerImage = Image.FromFile("Image/PlayerInMove.png");
            var graphics = e.Graphics;
            DrawWorld(graphics);
            Invalidate();
        }

        private void Level(Graphics e)
        {
        }

        private void DrawWorld(Graphics graphics)
        {
            graphics.DrawImage(playerImage,
                new RectangleF(player.x, player.y, player.Width,
                    player.Height)); // вроде этим я хочу разделить изображение от просто рисованых штук
            graphics.DrawLine(Pens.Green, 0, world.Ground, Width, world.Ground);
            graphics.FillRectangle(Brushes.Green, world.RightSide / 2 - 150, world.Ground - 130, 300, 130);
            graphics.FillRectangle(Brushes.Green, world.RightSide / 2 - 250, world.Ground - 50, 100, 50);
            graphics.FillRectangle(Brushes.Green, world.RightSide / 2 + 150, world.Ground - 50, 100, 50);
            graphics.FillRectangle(Brushes.Green, 0, world.Ground / 1.6f, world.RightSide / 4f, 30);
            graphics.FillRectangle(Brushes.Green, world.RightSide - world.RightSide / 4, world.Ground / 1.6f,
                world.RightSide / 4f, 30);
            graphics.FillRectangle(Brushes.Red, world.RightSide - 10, world.Ground - 90, 10, 90);
        }

        private DateTime lastUpdate = DateTime.MinValue;

        new void Update()
        {
            var now = DateTime.Now;
            var dt = (float) (now - lastUpdate).TotalMilliseconds / 100f;

            if (lastUpdate != DateTime.MinValue)
                player.Update(dt, player, world);

            lastUpdate = now;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (Keys.Space == e.KeyCode)
                player.Jump(world);

            if (Keys.A == e.KeyCode)
                player.Move(Directrion.Left);

            if (Keys.D == e.KeyCode)
                player.Move(Directrion.Right);
        }
    }
}