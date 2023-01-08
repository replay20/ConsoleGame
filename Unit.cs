using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        /*public int Move1ID { get; set; }
        public int Move2ID { get; set; }
        public int Move3ID { get; set; }
        public int Move4ID { get; set; }*/
        public List<Move> UnitMoves { get; set; }
        public double Attack { get; set; }
        public double Defense { get; set; }
        public double MaxHitPoints { get; set; }
        public double CurrentHitpoints { get; set; }
        public int MaxManaPoints { get; set; }
        public int CurrentManaPoints { get; set; }
        public int Speed { get; set; }
    }
}
