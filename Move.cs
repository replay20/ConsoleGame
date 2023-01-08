using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class Move
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ManaCost { get; set; }
        public double Power { get; set; }
        public int Accuracy { get; set; }
        public double AttackModifier { get; set; }
        public double DefenseModifier { get; set; }
        public int SpeedModifier { get; set; }
        public double OwnHitPointsModifier { get; set; }
    }
}
