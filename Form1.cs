using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GAME.Classes;

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
            player.physics.dx = 0;
        }

        private void OnKeyboardPressed(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    player.physics.dx = 6;
                    player.sprite = Properties.Resources.manR;
                    break;
                case "Left":
                    player.physics.dx = -6;
                    player.sprite = Properties.Resources.manL;
                    break;
                case "Up":
                    player.physics.ApplyJump();
                    break;
            }
        }
        private void Update(object sender, EventArgs e)
        {
            this.Text = "GhostGame";
            player.physics.ApplyPhysics();
            player.physics.CalculateJump();
            Invalidate();
            FollowPlayer();
        }

        public void FollowPlayer()
        {
            int offset = 50 - (int)player.physics.transform.position.X;
            
            player.physics.transform.position = new PointF(player.physics.transform.position.X + offset,
                                                                    player.physics.transform.position.Y);

            foreach (var platform in PlatformController.platforms)
            {
                platform.transform.position = new PointF(platform.transform.position.X + offset, platform.transform.position.Y);
            }

            foreach (var road in RoadController.roads)
            {
                road.transform.position = new PointF(road.transform.position.X + offset, road.transform.position.Y);
            }

            foreach (var pipe in PipeController.pipes)
            {
                pipe.transform.position = new PointF(pipe.transform.position.X + offset, pipe.transform.position.Y);
            }

            foreach (var rival in RivalController.rivals)
            {
                rival.transform.position = new PointF(rival.transform.position.X + offset, rival.transform.position.Y);
            }
        }
        private void OnRepaint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            foreach (var platform in PlatformController.platforms)
            {
                platform.DrawSprite(graphics);
            }

            foreach (var road in RoadController.roads)
            {
                road.DrawSprite(graphics);
            }

            foreach (var pipe in PipeController.pipes)
            {
                pipe.DrawSprite(graphics);
            }

            foreach (var rival in RivalController.rivals)
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
