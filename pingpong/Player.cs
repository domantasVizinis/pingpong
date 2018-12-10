﻿using System.Drawing;

namespace pingpong
{
    public class Player
    {
        public Player(int positionX, int positionY)
        {
            Score = 0;
            MoveUp = false;
            MoveDown = false;
            Body = new Rectangle(positionX, positionY, GameRules.PlayerWidth, GameRules.PlayerHeight);
        }

        public int Score { get; set; }
        public bool MoveUp { get; set; }
        public bool MoveDown { get; set; }
        public Rectangle Body;

        public bool PowerShot(int distance)
        {
            if ((distance < (int) GameRules.PlayerHeight * 0.33 || distance > (int) GameRules.PlayerHeight * 0.66) &&
                (this.MoveUp || this.MoveDown))
                return true;
            return false;
        }

    }
}
