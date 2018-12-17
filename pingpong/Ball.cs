using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace pingpong
{
    public class Ball
    {
        public Ball(int x, int y)
        {
            Body = new Rectangle(x, y, GameRules.BallRadius, GameRules.BallRadius);
            SpeedY = GameRules.BallSpeed;
            SpeedX = GameRules.BallSpeed;
            Bounced = false;
        }

        public int SpeedX;
        public int SpeedY;
        public bool Bounced;

        public Rectangle Body;

        public void SpawnBall()
        {
            this.Body.Y = GameRules.GameHeight / 2;
            this.Body.X = GameRules.GameWidth / 2;
            this.SpeedX = new Random().Next(2) == 0 ? -5 : 5;
            this.SpeedY = new Random().Next(-5, 5);
        }

        public bool IntersectsWithBorder()
        {
            if (this.Body.Y > GameRules.GameHeight - GameRules.BallRadius || this.Body.Y - GameRules.BallRadius <= 0)
                return true;
            return false;
        }

        public void AddPowerShot(Player player)
        {
            this.SpeedX = -this.SpeedX;
            BouncedTimer();

            this.SpeedX += this.SpeedX > 0 ? GameRules.PowerShot : -GameRules.PowerShot;
            if (Math.Abs(this.SpeedX) > GameRules.BallMaxSpeed)
              this.SpeedX = this.SpeedX > 0 ? GameRules.BallMaxSpeed : -GameRules.BallMaxSpeed;

            if (player.MoveUp)
                this.SpeedY = new Random().Next(-5, -2);

            if (player.MoveDown)
                this.SpeedY = new Random().Next(2, 5);
            
        }

        public bool ScoredGoal()
        {
            if (this.Body.X > GameRules.GameWidth || this.Body.X < -10)
                return true;
            return false;
        }

        public void BouncedTimer()
        {
            Bounced = true;
            var aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += ResetBouncedTimer;
            aTimer.AutoReset = false;
            aTimer.Enabled = true;
        }

        private void ResetBouncedTimer(Object source, ElapsedEventArgs e)
        {
            this.Bounced = false;
        }


    }
}
