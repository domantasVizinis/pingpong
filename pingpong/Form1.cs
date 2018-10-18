using System;
using System.Drawing;
using System.Windows.Forms;

namespace pingpong
{
    public partial class Form1 : Form
    {
        //Konstatnots
        Graphics Canvas;
        SolidBrush sb = new SolidBrush(Color.White);
        static Font DrawFont = new Font("Arial", 40);

   

        private const int playerHeight = 100;
        private const int playerWidth = 20;
        private const int playerSpeed = 10;
        private const int ballRadius = 16;
        private const int ballMaxSpeed = 15;
        private int player1Score = 0, player2Score = 0;
        private bool p1moveUp, p1moveDown, p2moveUp, p2moveDown;

        private Rectangle player1 = new Rectangle(14, 258, playerWidth, playerHeight);
        private Rectangle player2 = new Rectangle(745, 258, playerWidth, playerHeight);
        private Rectangle ball = new Rectangle(290, 115, ballRadius, ballRadius);

        private int ballSpeedX = 5, ballSpeedY = 5;      
        private int RandoMin = 1, RandoMax = 10;
        

        public Form1()
        {
            InitializeComponent();
            canvas.BackColor = Color.Black;
            Canvas = canvas.CreateGraphics();
            SpawnBall();
        }


        private void IncreaseSpeed()
        {
            RandoMin += 1;
            RandoMax += 1;
            ballSpeedX = ballSpeedX < 0 ? ballSpeedX -= 1 : ballSpeedX += 1;
        }

        public void DrawIt()
        {    
            Canvas.Clear(Color.Black);
            Canvas.FillRectangle(sb, player1);
            Canvas.FillRectangle(sb, player2);
            Canvas.FillRectangle(sb, ball);

            Canvas.DrawString(player1Score.ToString(), DrawFont, sb, (int)(this.ClientRectangle.Width*0.2), 10);
            Canvas.DrawString(player2Score.ToString(), DrawFont, sb, (int)(this.ClientRectangle.Width*0.8), 10);
            Canvas.DrawString(String.Format("Speed= {0}", Math.Abs(ballSpeedX).ToString()) ,new Font("Arial",20), sb, (int)(this.ClientRectangle.Width/2), 10);
        }

        private void SpawnBall()
        {
            ball.Y = this.ClientRectangle.Height / 2;
            ball.X = this.ClientRectangle.Width / 2;
            ballSpeedX = new Random().Next(2) == 0 ? -5 : 5;
            ballSpeedY = new Random().Next(-5,5);
        }

        private void MoveBall()
        {
            ball.X += ballSpeedX;
            ball.Y += ballSpeedY;
            if (ball.Y > canvas.Height-ballRadius || ball.Y-ballRadius<=0)
                ballSpeedY = -ballSpeedY;

            if (ball.X > canvas.Width-ballRadius || ball.X-ballRadius < 1)
            {
                if (ball.X -ballRadius < 1)
                    player2Score++;
                else
                {
                    player1Score++;
                }

                SpawnBall();
            }               
            if (ball.IntersectsWith(player1))
            {
                int distance = player1.Bottom - ball.Y;
                if ((distance < (int) playerHeight * 0.33 || distance > (int) playerHeight * 0.66) &&
                    (p1moveUp || p1moveDown))
                {
                    ballSpeedX = (-ballSpeedX) + playerSpeed;
                    if (Math.Abs(ballSpeedX) > ballMaxSpeed)
                        ballSpeedX = ballMaxSpeed;
                    if(p1moveUp)
                        ballSpeedY+=new Random().Next(3,5);
                    if (p1moveDown)
                        ballSpeedY -= new Random().Next(3, 5);
                }
                else
                {
                    ballSpeedX = -ballSpeedX;
                }

            }
            else if (ball.IntersectsWith(player2))
            {
                int distance = player2.Bottom - ball.Y;
                if ((distance < playerHeight * 0.33 || distance > playerHeight * 0.66) &&
                    (p2moveUp || p2moveDown))
                {
                    ballSpeedX = -(ballSpeedX + playerSpeed);
                    if (Math.Abs(ballSpeedX) > ballMaxSpeed)
                        ballSpeedX = -ballMaxSpeed;

                    if (p2moveUp)
                        ballSpeedY += new Random().Next(3, 5);
                    if (p2moveDown)
                        ballSpeedY -= new Random().Next(3, 5);

                }
                else
                {
                    ballSpeedX = -ballSpeedX;
                }

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            IncreaseSpeed();
        }

        private void Randomize()
        {
            Random r = new Random();
            int s = r.Next(RandoMin, RandoMax);
            ballSpeedX = ball.IntersectsWith(player1) ? ballSpeedX = s : ballSpeedX = -s;

            if (ballSpeedY < 0)
            {
                int t = r.Next(RandoMin, RandoMax);
                ballSpeedY = -t;
            }

            else
            {
                ballSpeedY = r.Next(RandoMin, RandoMax);
            }
        }
        private void updatePlayersMovement()
        {
            int bottom = this.ClientRectangle.Height - playerHeight;
            if (p1moveUp)
            { int z = player1.Y <= 0 ? player1.Y = 0 : player1.Y -= playerSpeed; }
            if (p1moveDown)
            { int z = player1.Y >= bottom? player1.Y = bottom : player1.Y += playerSpeed; }
            if (p2moveUp)
            { int z = player2.Y <= 0 ? player2.Y = 0 : player2.Y -= playerSpeed; }
            if (p2moveDown)
            { int z = player2.Y >= bottom ? player2.Y = bottom : player2.Y += playerSpeed; }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    p1moveUp = true;
                    break;
                case Keys.S:
                    p1moveDown = true;
                    break;
                case Keys.I:
                    p2moveUp = true;
                    break;
                case Keys.K:
                    p2moveDown = true;
                    break;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    p1moveUp = false;
                    break;
                case Keys.S:
                    p1moveDown = false;
                    break;
                case Keys.I:
                    p2moveUp = false;
                    break;
                case Keys.K:
                    p2moveDown = false;
                    break;
            }
        }

        private void updateAI()
        {
            int z = player2.Bottom - playerHeight / 2;
            if (ballSpeedX > 0 && ball.X>this.ClientRectangle.Width/2)
            {
                if (ball.Y > player2.Y+30 && ball.Y < player2.Bottom-30)
                {
                    p2moveUp = false;
                    p2moveDown = false;
                }
                else if (ball.Y<=z)
                {
                    p2moveDown = false;
                    p2moveUp = true;
                }
                else if (ball.Y>=z)
                {
                    p2moveUp = false;
                    p2moveDown = true;
                }
            }
            else
            {
                p2moveUp = false;
                p2moveDown = false;
            }
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            DrawIt();
            updatePlayersMovement();
            updateAI();
            MoveBall();
                        
        }
    }
}
