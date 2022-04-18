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
        private Player _player = new Player(0,0);
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            var brush = new SolidBrush(Color.Blue);
            graphics.FillRectangle(brush,_player.x,_player.y,30,30);
        }

        
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            
            switch (e.KeyChar)
            {
                case 'w':
                    Player.Move(Directrion.Up);
                    break;
                case 'd':
                    Player.Move(Directrion.Right);
                    break;
                case 'a':
                    Player.Move(Directrion.Left);
                    break;
                case 's':
                    Player.Move(Directrion.Down);
                    break;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            var move = new creat
            switch (Keys)
            {
                
            }
        }
    }
}