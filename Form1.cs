using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        }
        public void Init()
        {
            PlatformController.platforms = new List<Platform>(); 
            PlatformController.AddPlatform(new PointF(350, 90)); // Изменяем координаты платформы
            PlatformController.startPlatformPosX = 400;
            PlatformController.GenerateStartSequence();

            RoadController.roads = new List<Road>();
            RoadController.AddRoad(new PointF(0, 208)); // Изменяем координаты платформы
            RoadController.startRoadPosX = 0;
            RoadController.GenerateStartSequence();

            PipeController.pipes = new List<Pipe>();
            PipeController.AddPipe(new PointF(700, 168));
            PipeController.startRoadPosX = 400;
            PipeController.GenerateStartSequencePipe();

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
                    break;
                case "Left":
                    player.physics.dx = -6;
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
            Invalidate();
            FollowPlayer();
        }

        public void FollowPlayer()
        {
            int offset = 50 - (int)player.physics.transform.position.X;
            player.physics.transform.position.X += offset;

            foreach (var platform in PlatformController.platforms)
            {
                platform.transform.position.X += offset;
            }

            foreach (var road in RoadController.roads)
            {
                road.transform.position.X += offset;
            }

            foreach (var pipe in PipeController.pipes)
            {
                pipe.transform.position.X += offset;
            }
        }
        private void OnRepaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (var platform in PlatformController.platforms)
            {
                platform.DrawSprite(g);
            }

            foreach (var road in RoadController.roads)
            {
                road.DrawSprite(g);
            }

            foreach (var pipe in PipeController.pipes)
            {
                pipe.DrawSprite(g);
            }

            player.DrawSprite(g);
        }
    }
}
