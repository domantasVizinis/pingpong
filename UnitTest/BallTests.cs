using Microsoft.VisualStudio.TestTools.UnitTesting;
using pingpong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pingpong.Tests
{
    [TestClass()]
    public class BallTests
    {
        [TestMethod()]
        public void IntersectsWithBorderTest_Valid_Return()
        {
            Ball test = new Ball(100, 30);
            var intersects = test.IntersectsWithBorder();
            Assert.AreEqual(false, intersects);
        }

        [TestMethod()]
        public void ScoredGoalTest_scored_right_side()
        {
            Ball test = new Ball(0, 30);
            var scored = test.ScoredGoal();
            Assert.AreEqual(true, scored);
        }
        [TestMethod()]
        public void ScoredGoalTest_scored_left_side()
        {
            Ball test = new Ball(900, 30);
            var scored = test.ScoredGoal();
            Assert.AreEqual(true, scored);
        }
    }
}