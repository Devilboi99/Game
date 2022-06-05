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
        private Image _floorUpImage;
        private Image _backGround;
        private Image _monsterImage;
        private readonly Player _player = new Player(30, 30);
        private Game _gameMap;
        private World _currentLevel;


        //        private SoundPlayer mediaPlayer = new SoundPlayer("music/background.wav");

        public OuterRoom()
        {
            InitializeComponent();
            //mediaPlayer.Play();
            CreateMap();
            TakeTexture();
            PaintWorldForeground();
            SettingSmoothness(5);
        }
        
        protected override void OnPaint(PaintEventArgs args)
        {
            var e = args.Graphics;
            _currentLevel.PlayerInDoor(_player, () => _gameMap.ActionWithDoor[_gameMap.CurrentLevelNumber]());
            DrawWorld(e);
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

        private void TakeTexture()
        {
            _playerImage = Image.FromFile("Image/Player.png");
            _floorImage = Image.FromFile("Image/blocks/Floor.jpg");
            _doorImage = Image.FromFile("Image/door/door.png");
            _backGround = Image.FromFile("Image/BackGround/back.png");// оо, ты здесь. может еще фон добавишь? как видишь все готово ток найти нужно и код немного дописать и оптимизировать сестему и еще анимаций добавить, звуки прыжков, добавить увроней а стоп... прости.
            _floorUpImage = Image.FromFile("Image/blocks/blockUp.png");
            _monsterImage = Image.FromFile("Image/monster/frame-1.r.png");
        }
        
        private void DrawWorld(Graphics graphics)
        {
            graphics.DrawImage(_floorImage,
                new RectangleF(_currentLevel.RightSide / 2 - 150, 
                    _currentLevel.Ground - 130, 150, 130));
            graphics.DrawImage(_floorImage,
                new RectangleF(_currentLevel.RightSide / 2, _currentLevel.Ground - 130, 150, 130));
            graphics.DrawImage(_doorImage,
                new RectangleF(_currentLevel.RightSide - 10, _currentLevel.Ground - 100, 30, 120));
            graphics.DrawImageUnscaled(_playerImage, (int)_player.x,(int)_player.y);
            graphics.DrawString(_currentLevel.TextLevel, new Font("Arial", 14), Brushes.Black,
                new Point(_currentLevel.RightSide / 2 - 80, _currentLevel.Ground / 3));
            if  (_currentLevel.Monster.IsLive)
            {
                graphics.DrawImage(_monsterImage,
                    new RectangleF(_currentLevel.Monster.x, _currentLevel.Monster.y, _currentLevel.Monster.Width,
                        _currentLevel.Monster.Height));
            }

            // вроде этим я хочу разделить изображение от просто рисованых штук
        }


        private DateTime _lastUpdate = DateTime.MinValue;

        private void Update(object sender, EventArgs eventArgs)
        {
            var now = DateTime.Now;
            var dt = (float) (now - _lastUpdate).TotalMilliseconds / 100f;

            if (_lastUpdate != DateTime.MinValue)
                _player.Update(dt, _player, _currentLevel);

            if (_gameMap.CurrentLevelNumber == NumLevel.Second)
            {
                _currentLevel.Monster.GoTo(_player);
                if (World.MonsterInPlayer(_player, _currentLevel.Monster))
                    Application.Restart();
            }
            
            if (_currentLevel.IsLevelCompleted())
                _currentLevel = _gameMap.NextLevel;

            _lastUpdate = now;
            Invalidate();
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
            WindowState = FormWindowState.Maximized;
            var sizeForm = Screen.PrimaryScreen.WorkingArea.Size;
            return sizeForm;
        }
    }
}