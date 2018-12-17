using System;
using System.Windows.Forms;
using System.Threading;

namespace pingpong
{
    public class GameEngine
    {

        private static GameEngine _instance;

        public SuperPlayer Player1;
        public Player Player2;
        public Ball GameBall;

        private GameEngine()
        {
            Player1 = new SuperPlayer(14, 258);
            Player2 = new Player(745, 258);
            GameBall = new Ball(290, 115);
        }

        public static GameEngine Instance()
        {
            if (_instance == null)
            {
                _instance = new GameEngine();
            }

            return _instance;
        }

        public void StartGame()
        {
            GameBall.SpawnBall();
        }

        public void GameCycle()
        {
            MoveBall();
            UpdatePlayersMovement();
            UpdateAI();
        }

        public void KeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    Player1.MoveUp = true;
                    break;
                case Keys.S:
                    Player1.MoveDown = true;
                    break;
                case Keys.I:
                    Player2.MoveUp = true;
                    break;
                case Keys.K:
                    Player2.MoveDown = true;
                    break;
            }
        }

        public void KeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    Player1.MoveUp = false;
                    break;
                case Keys.S:
                    Player1.MoveDown = false;
                    break;
                case Keys.I:
                    Player2.MoveUp = false;
                    break;
                case Keys.K:
                    Player2.MoveDown = false;
                    break;
            }
        }

        private void UpdatePlayersMovement()
        {
            int bottom1 = GameRules.GameHeight - Player1.Body.Height;
            int bottom2 = GameRules.GameHeight - Player2.Body.Height;
            if (Player1.MoveUp)
            { int z = Player1.Body.Y <= 0 ? Player1.Body.Y = 0 : Player1.Body.Y -= GameRules.PlayerSpeed; }
            if (Player1.MoveDown)
            { int z = Player1.Body.Y >= bottom1 ? Player1.Body.Y = bottom1 : Player1.Body.Y += GameRules.PlayerSpeed; }
            if (Player2.MoveUp)
            { int z = Player2.Body.Y <= 0 ? Player2.Body.Y = 0 : Player2.Body.Y -= GameRules.PlayerSpeed; }
            if (Player2.MoveDown)
            { int z = Player2.Body.Y >= bottom2 ? Player2.Body.Y = bottom2 : Player2.Body.Y += GameRules.PlayerSpeed; }
        }


        private void UpdateAI()
        {
            int z = Player2.Body.Bottom - Player2.Body.Height / 2;
            if (GameBall.Body.Y > 0 && GameBall.Body.X > GameRules.GameWidth / 2)
            {
                if (GameBall.Body.Y > Player2.Body.Y + 30 && GameBall.Body.Y < Player2.Body.Bottom - 30)
                {
                    Player2.MoveUp = false;
                    Player2.MoveDown = false;
                }
                else if (GameBall.Body.Y <= z)
                {
                    Player2.MoveDown = false;
                    Player2.MoveUp = true;
                }
                else if (GameBall.Body.Y >= z)
                {
                    Player2.MoveUp = false;
                    Player2.MoveDown = true;
                }
            }
            else
            {
                Player2.MoveUp = false;
                Player2.MoveDown = false;
            }
        }
        // normalus inheritance
        // polimorfizmas
        // patterns: creational (factory, singelton), behavior(strategy)


        private void CheckIfIntersects(Player player)
        {
            if (GameBall.Body.IntersectsWith(player.Body) && !GameBall.Bounced) // 
            {   
                int distance = player.Body.Y - GameBall.Body.Y;

                if (player.PowerShot(distance))
                {
                    GameBall.AddPowerShot(player);
                }
                else
                {
                    GameBall.SpeedX = -GameBall.SpeedX;
                    GameBall.BouncedTimer();
                }
            }
        }
        private void MoveBall()
        {
            if (GameBall.IntersectsWithBorder())
                GameBall.SpeedY = -GameBall.SpeedY;

            CheckIfIntersects(Player1);
            CheckIfIntersects(Player2);

            if (GameBall.ScoredGoal())
            {
                if (GameBall.Body.X - GameRules.BallRadius < 1)
                    Player2.Score++;
                else
                {
                    Player1.Score++;
                }
                //Thread.Sleep(9000);
                GameBall.SpawnBall();
            }


            GameBall.Body.X += GameBall.SpeedX;
            GameBall.Body.Y += GameBall.SpeedY;
        }
    }
}
