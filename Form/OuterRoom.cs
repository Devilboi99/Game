using System;
using System.Collections.Generic;
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
        private Game Levels = new Game();
        private Monster monster = new Monster(1000, 30);

        private World currentWorld;
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
            Levels.CreateLevels(sizeForm.Height - 60, sizeForm.Width);
            currentWorld = Levels[Levels.CurrentLevelNunber];
            Level(Levels);
            Application.Idle += delegate { Invalidate(); };
        }

        public void Level(Game levels)
        {
            if (currentWorld.IsCompleted && currentWorld.door.isOpen)
                currentWorld = levels[++levels.CurrentLevelNunber];
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Update();
            Level(Levels);
            if (Levels.CurrentLevelNunber == numlevel.second)
            {
                e.Graphics.DrawImage(playerImage,
                    new RectangleF(monster.x, monster.y, monster.Width,
                        monster.Height));
            }
            e.Graphics.DrawString(currentWorld.TextLevel, new Font("Arial", 16), Brushes.Black,
                new Point(currentWorld.RightSide / 2 - 80, currentWorld.Ground / 3));
            playerImage = Image.FromFile("Image/PlayerInMove.png");
            var graphics = e.Graphics;
            DrawWorld(graphics);
            Invalidate();
        }
        

        private void DrawWorld(Graphics graphics)
        {
            graphics.DrawImage(playerImage,
                new RectangleF(player.x, player.y, player.Width,
                    player.Height)); // вроде этим я хочу разделить изображение от просто рисованых штук
            graphics.DrawLine(Pens.Green, 0, currentWorld.Ground, Width, currentWorld.Ground);
            
            graphics.FillRectangle(Brushes.Green, currentWorld.RightSide / 2 - 150, currentWorld.Ground - 130, 300, 130);
            graphics.FillRectangle(Brushes.Green, currentWorld.RightSide / 2 - 250, currentWorld.Ground - 50, 100, 50);
            graphics.FillRectangle(Brushes.Green, currentWorld.RightSide / 2 + 150, currentWorld.Ground - 50, 100, 50);
            graphics.FillRectangle(Brushes.Green, 0, currentWorld.Ground / 1.6f, currentWorld.RightSide / 4f, 30);
            graphics.FillRectangle(Brushes.Green, currentWorld.RightSide - currentWorld.RightSide / 4, currentWorld.Ground / 1.6f,
                currentWorld.RightSide / 4f, 30);
            graphics.FillRectangle(Brushes.Red, currentWorld.RightSide - 10, currentWorld.Ground - 90, 10, 90);
        }

        private DateTime lastUpdate = DateTime.MinValue;

        new void Update()
        {
            var now = DateTime.Now;
            var dt = (float) (now - lastUpdate).TotalMilliseconds / 100f;

            if (lastUpdate != DateTime.MinValue)
                player.Update(dt, player, currentWorld);
            if (Levels.CurrentLevelNunber == numlevel.second)
                monster.GoTo(player);

            lastUpdate = now;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (Keys.Space == e.KeyCode)
                player.Jump(currentWorld);

            if (Keys.A == e.KeyCode)
                player.Move(Direction.Left);

            if (Keys.D == e.KeyCode)
                player.Move(Direction.Right);
        }
    }
}