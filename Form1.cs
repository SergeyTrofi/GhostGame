using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GAME.Classes;
/*
Phisics refactoring / Fix
На гите убрать мусор (obj, bin)
*/
namespace GAME
{
    public partial class Form1 : Form
    {
        Player player;
        Timer timer;
        public static Form1 Instance;
        public Form1()
        {
            InitializeComponent();
            Init();
            timer = new Timer();
            timer.Interval = 15;
            timer.Tick += new EventHandler(Update);
            timer.Start();
            this.KeyDown += new KeyEventHandler(OnKeyboardPressed);
            this.KeyUp += new KeyEventHandler(OnKeyboardUp);
            this.Height = 400; // Изменение высоты формы
            this.Width = 600; // Изменение ширины формы
            this.Paint += new PaintEventHandler(OnRepaint);
            Instance = this;
        }
        public void Init()
        {
            PipeController.Init();
            PlatformController.Init();
            RoadController.Init();
            RivalController.Init();
            player = new Player();
        }
        private void OnKeyboardUp(object sender, KeyEventArgs e)
        {
            player.Physics.XMotion = 0;
        }

        private void OnKeyboardPressed(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    player.Physics.XMotion = 6;
                    player.Sprite = Properties.Resources.manR;
                    break;
                case "Left":
                    player.Physics.XMotion = -6;
                    player.Sprite = Properties.Resources.manL;
                    break;
                case "Up":
                    player.Physics.ApplyJump();
                    break;
            }
        }
        private void Update(object sender, EventArgs e)
        {
            this.Text = "GhostGame";
            player.Physics.ApplyPhysics();
            player.Physics.CalculateJump();
            Invalidate();
            FollowPlayer();
        }

        public void FollowPlayer()
        {
            int offset = 50 - (int)player.Physics.Transform.Position.X;
            
            player.Physics.Transform.Position = new PointF(player.Physics.Transform.Position.X + offset,
                                                                    player.Physics.Transform.Position.Y);

            foreach (var platform in PlatformController.Platforms)
            {
                platform.Transform.Position = new PointF(platform.Transform.Position.X + offset, platform.Transform.Position.Y);
            }

            foreach (var road in RoadController.Roads)
            {
                road.Transform.Position = new PointF(road.Transform.Position.X + offset, road.Transform.Position.Y);
            }

            foreach (var pipe in PipeController.Pipes)
            {
                pipe.Transform.Position = new PointF(pipe.Transform.Position.X + offset, pipe.Transform.Position.Y);
            }

            foreach (var rival in RivalController.Rivals)
            {
                rival.Transform.Position = new PointF(rival.Transform.Position.X + offset, rival.Transform.Position.Y);
            }
        }
        private void OnRepaint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            foreach (var platform in PlatformController.Platforms)
            {
                platform.DrawSprite(graphics);
            }

            foreach (var road in RoadController.Roads)
            {
                road.DrawSprite(graphics);
            }

            foreach (var pipe in PipeController.Pipes)
            {
                pipe.DrawSprite(graphics);
            }

            foreach (var rival in RivalController.Rivals)
            {
                rival.DrawSprite(graphics);
            }

            player.DrawSprite(graphics);
        }
        public static void RestartGame()
        {
            Instance.timer.Stop();
            Instance.Controls.Clear();
            Instance.InitializeComponent();
            Instance.Init();
            Instance.timer.Start();

        }
    }
}
