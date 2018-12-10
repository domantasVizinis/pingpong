using System;
using System.Windows.Forms;

namespace pingpong
{
    public class GameEngine
    {
        public Player Player1;
        public Player Player2;
        public Ball GameBall;

        public GameEngine()
        {
            Player1 = new Player(14, 258);
            Player2 = new Player(745, 258);
            GameBall = new Ball(290, 115);
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
            int bottom = GameRules.GameHeight - GameRules.PlayerHeight;
            if (Player1.MoveUp)
            { int z = Player1.Body.Y <= 0 ? Player1.Body.Y = 0 : Player1.Body.Y -= GameRules.PlayerSpeed; }
            if (Player1.MoveDown)
            { int z = Player1.Body.Y >= bottom ? Player1.Body.Y = bottom : Player1.Body.Y += GameRules.PlayerSpeed; }
            if (Player2.MoveUp)
            { int z = Player2.Body.Y <= 0 ? Player2.Body.Y = 0 : Player2.Body.Y -= GameRules.PlayerSpeed; }
            if (Player2.MoveDown)
            { int z = Player2.Body.Y >= bottom ? Player2.Body.Y = bottom : Player2.Body.Y += GameRules.PlayerSpeed; }
        }


        private void UpdateAI()
        {
            int z = Player2.Body.Bottom - GameRules.PlayerHeight / 2;
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

        private void CheckIfIntersects(Player player)
        {
            if (GameBall.Body.IntersectsWith(player.Body) && !GameBall.Bounced)
            {
                int distance = player.Body.Bottom - GameBall.Body.Y;

                if (player.PowerShot(distance))
                {
                    GameBall.AddPowerShot(player);
                }
                else
                {
                    GameBall.SpeedX = -GameBall.SpeedX;
                }
            }
        }
        private void MoveBall()
        {
            if (GameBall.IntersectsWithBorder())
                GameBall.SpeedY = -GameBall.SpeedY;

            if (GameBall.ScoredGoal())
            {
                if (GameBall.Body.X - GameRules.BallRadius < 1)
                    Player2.Score++;
                else
                {
                    Player1.Score++;
                }

                GameBall.SpawnBall();
            }
            CheckIfIntersects(Player1);
            CheckIfIntersects(Player2);

            GameBall.Body.X += GameBall.SpeedX;
            GameBall.Body.Y += GameBall.SpeedY;
        }
    }
}
