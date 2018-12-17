using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pingpong
{
    public class SuperPlayer:Player
    {
        public SuperPlayer(int positionX, int positionY) : base(positionX,  positionY)
        {
            this.Score = 0;
            this.MoveUp = false;
            this.MoveDown = false;
            this.Body = new Rectangle(positionX, positionY, GameRules.PlayerWidth, GameRules.PlayerHeight+20);
        }

        public override bool PowerShot(int distance)
        {
            if ((distance < (int)this.Body.Height*45 || distance > (int)this.Body.Height * 0.55) &&
                (this.MoveUp || this.MoveDown))
                return true;
            return false;
        }
    }
}
