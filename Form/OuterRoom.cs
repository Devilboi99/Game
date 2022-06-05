using System;
using System.Drawing;
using System.Windows.Forms;
using Model;
using Timer = System.Windows.Forms.Timer;

namespace FutureGame
{
    public partial class OuterRoom : Form
    {
        private Image _playerImage;
        private Image _floorImage;
        private Image _doorImage;
        private Image backGround;
        private readonly Player _player = new Player(30, 30);
        private Game _gameMap;
        private World _currentLevel;


        //        private SoundPlayer mediaPlayer = new SoundPlayer("music/background.wav");

        public OuterRoom()
        {
            InitializeComponent();
            CreateMap();
            TakeTexture();
            PaintWorld();
            SettingSmoothness(5);
        }

        private void CreateMap()
        {
            var sizeForm = SettingsSizeForm();
            _gameMap = new Game(sizeForm.Height - 60, sizeForm.Width);
            _currentLevel = _gameMap[_gameMap.CurrentLevelNumber];
        }

        private void SettingSmoothness(int interval)
        {
            var timer = new Timer();
            timer.Interval = interval;
            timer.Tick += Update;
            timer.Start();
        }

        private Size SettingsSizeForm()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint, true);
            //mediaPlayer.Play();
            WindowState = FormWindowState.Maximized;
            var sizeForm = Screen.PrimaryScreen.WorkingArea.Size;
            return sizeForm;
        }

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

        private void CreateElement(Control elementWorld, Point location, int width, int height)
        {
            elementWorld.Location = location;
            elementWorld.Width = width;
            elementWorld.Height = height;
            Controls.Add(elementWorld);
        }

        private void CheckCompletedLevel(Game gameMap)
        {
            if (_currentLevel.IsCompleted && _currentLevel.door.isOpen)
                _currentLevel = gameMap.NextLevel;
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


        protected override void OnPaint(PaintEventArgs args)
        {
            CheckCompletedLevel(_gameMap);
            var e = args.Graphics;
            if (_gameMap.CurrentLevelNumber == numlevel.Second)
            {
                e.DrawImage(_playerImage,
                    new RectangleF(_currentLevel.Monster.x, _currentLevel.Monster.y, _currentLevel.Monster.Width,
                        _currentLevel.Monster.Height));
            }

            _currentLevel.PlayerInDoor(_player, () => _gameMap.ActionWithCloseDoor[_gameMap.CurrentLevelNumber]());
            e.DrawString(_currentLevel.TextLevel, new Font("Arial", 16), Brushes.Black,
                new Point(_currentLevel.RightSide / 2 - 80, _currentLevel.Ground / 3));
            DrawWorld(e);
        }


        private void TakeTexture()
        {
            _playerImage = Image.FromFile("Image/PlayerInMove.png");
            _floorImage = Image.FromFile("Image/blocks/Floor.jpg");
            _doorImage = Image.FromFile("Image/door/door.png");
            backGround = Image.FromFile("Image/BackGround/back.jpg");
        }


        private void DrawWorld(Graphics graphics)
        {
            graphics.DrawImage(_floorImage,
                new RectangleF(_currentLevel.RightSide / 2 - 250, _currentLevel.Ground - 50, 100, 50));
            graphics.DrawImage(_floorImage,
                new RectangleF(_currentLevel.RightSide / 2 + 150, _currentLevel.Ground - 50, 100, 50));
            graphics.DrawImage(_doorImage,
                new RectangleF(_currentLevel.RightSide - 40, _currentLevel.Ground - 100, 80, 120));
            graphics.DrawImage(_playerImage,
                new RectangleF(_player.x, _player.y, _player.Width,
                    _player.Height));
            // вроде этим я хочу разделить изображение от просто рисованых штук
        }


        private DateTime _lastUpdate = DateTime.MinValue;

        void Update(object sender, EventArgs eventArgs)
        {
            var now = DateTime.Now;
            var dt = (float) (now - _lastUpdate).TotalMilliseconds / 100f;

            if (_lastUpdate != DateTime.MinValue)
                _player.Update(dt, _player, _currentLevel);
            if (_gameMap.CurrentLevelNumber == numlevel.Second)
                _currentLevel.Monster.GoTo(_player);

            CheckCompletedLevel(_gameMap);
            _lastUpdate = now;
            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (Keys.Space == e.KeyCode)
                _player.Jump(_currentLevel);

            if (Keys.A == e.KeyCode)
                _player.Move(Direction.Left);

            if (Keys.D == e.KeyCode)
                _player.Move(Direction.Right);
        }
    }
}