using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class Battle
    {
        public List<Move> Moves { get; set; }
        public List<Unit> Units { get; set; }
        public Player Player { get; set; }
        public Battle()
        {
            LoadMovesFromLines("Moves.txt");
            LoadUnitsFromLines("Units.txt");
        }
        public void LoadMovesFromLines(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var counter = 0;

            Moves = new List<Move>();
            var currentMove = new Move();
            var moveId = 0;

            foreach (var line in lines)
            {
                if (counter == 8)
                {
                    counter = 0;
                }

                if (counter == 0)
                {
                    currentMove.Name = line;
                }

                if (counter == 1)
                {
                    currentMove.ManaCost = int.Parse(line.ToString());
                }
                if (counter == 2)
                {
                    currentMove.Power = int.Parse(line.ToString());
                }
                if (counter == 3)
                {
                    currentMove.Accuracy = int.Parse(line.ToString());
                }
                if (counter == 4)
                {
                    currentMove.AttackModifier = int.Parse(line.ToString());
                }
                if (counter == 5)
                {
                    currentMove.DefenseModifier = int.Parse(line.ToString());
                }
                if (counter == 6)
                {
                    currentMove.SpeedModifier = int.Parse(line.ToString());
                }
                if (counter == 7)
                {
                    currentMove.OwnHitPointsModifier = int.Parse(line.ToString());

                    var newMove = new Move
                    {
                        Id = moveId,
                        Name = currentMove.Name,
                        ManaCost = currentMove.ManaCost,
                        Power = currentMove.Power,
                        Accuracy = currentMove.Accuracy,
                        AttackModifier = currentMove.AttackModifier,
                        DefenseModifier = currentMove.DefenseModifier,
                        SpeedModifier = currentMove.SpeedModifier,
                        OwnHitPointsModifier = currentMove.OwnHitPointsModifier
                    };
                    Moves.Add(newMove);
                    moveId++;
                }
                counter++;
            }
        }
        public void LoadUnitsFromLines(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var counter = 0;

            Units = new List<Unit>();
            var currentUnit = new Unit();
            var unitId = 0;

            foreach (var line in lines)
            {
                if (counter == 11)
                {
                    counter = 0;
                }
                if (counter == 0)
                {
                    currentUnit.Name = line.ToString();
                    currentUnit.UnitMoves = new List<Move>();
                }
                if (counter == 1)
                {
                    currentUnit.Type = line.ToString();
                }
                if (counter == 2)
                {

                    currentUnit.UnitMoves.Add(Moves[int.Parse(line.ToString())]);
                    //currentUnit.UnitMoves.Add(Moves[0]);
                }
                if (counter == 3)
                {
                    currentUnit.UnitMoves.Add(Moves[int.Parse(line.ToString())]);
                    //currentUnit.UnitMoves.Add(Moves[1]);
                }
                if (counter == 4)
                {
                    currentUnit.UnitMoves.Add(Moves[int.Parse(line.ToString())]);
                    //currentUnit.UnitMoves.Add(Moves[2]);
                }
                if (counter == 5)
                {
                    currentUnit.UnitMoves.Add(Moves[int.Parse(line.ToString())]);
                    //currentUnit.UnitMoves.Add(Moves[3]);
                }
                if (counter == 6)
                {
                    currentUnit.Attack = int.Parse(line.ToString());
                }
                if (counter == 7)
                {
                    currentUnit.Defense = int.Parse(line.ToString());
                }
                if (counter == 8)
                {
                    currentUnit.MaxHitPoints = int.Parse(line.ToString());
                    currentUnit.CurrentHitpoints = currentUnit.MaxHitPoints;
                }
                if (counter == 9)
                {
                    currentUnit.MaxManaPoints = int.Parse(line.ToString());
                    currentUnit.CurrentManaPoints = currentUnit.MaxManaPoints;
                }
                if (counter == 10)
                {
                    currentUnit.Speed = int.Parse(line.ToString());

                    var newUnit = new Unit
                    {
                        Id = unitId,
                        Name = currentUnit.Name,
                        Type = currentUnit.Type,
                        UnitMoves = currentUnit.UnitMoves,
                        Attack = currentUnit.Attack,
                        Defense = currentUnit.Defense,
                        MaxHitPoints = currentUnit.MaxHitPoints,
                        CurrentHitpoints = currentUnit.CurrentHitpoints,
                        MaxManaPoints = currentUnit.MaxManaPoints,
                        CurrentManaPoints = currentUnit.CurrentManaPoints,
                        Speed = currentUnit.Speed
                    };
                    Units.Add(newUnit);
                    unitId++;
                }
                counter++;
            }
        }
        public void Start()
        {
            Player player = new();
            Console.WriteLine("Tell me your name: ");
            player.Name = Console.ReadLine();
            Console.WriteLine("Welcome {0}! Choose your unit by writing its number. ", player.Name);

            var loopCounter = 1;
            foreach (var unit in Units)
            {
                Console.WriteLine(loopCounter + " " + unit.Name);
                loopCounter++;
            }

            var correctAnswer = false;
            Unit chosenUnit = Units[0];
            do
            {
                var playerResponse = int.Parse(Console.ReadLine());
                if (0 < playerResponse && playerResponse <= Units.Count())
                {
                    correctAnswer = true;
                    chosenUnit = Units[playerResponse-1];
                    Console.WriteLine("Name of your chosen character is {0}.",chosenUnit.Name);
                    Console.WriteLine("Their details: ");
                    Console.WriteLine("Type: " + chosenUnit.Type);
                    Console.WriteLine("Attack: " + chosenUnit.Attack);
                    Console.WriteLine("Defense: " + chosenUnit.Defense);
                    Console.WriteLine("Hit Points: " + chosenUnit.MaxHitPoints);
                    Console.WriteLine("Mana Points: " + chosenUnit.MaxManaPoints);
                    Console.WriteLine("Speed: " + chosenUnit.Speed);
                    Console.WriteLine("Moves: ");
                    foreach (var move in chosenUnit.UnitMoves)
                    {
                        Console.WriteLine(move.Name + ", ");
                    }
                }
                else
                {
                    Console.WriteLine("Something gone wrong. Please try again.");
                }
            }while(correctAnswer==false);

            correctAnswer = false;
            Console.WriteLine("Your enemy will be {0}. Would you like to fight it now?", Units[1].Name);
            Console.WriteLine("1 Yes");
            Console.WriteLine("2 No");

            while (correctAnswer == false)
            {
                var playerResponse = int.Parse(Console.ReadLine());
                if (playerResponse == 1 || playerResponse == 2)
                {
                    correctAnswer = true;
                }
                else
                {
                    Console.WriteLine("Something gone wrong. Please try again.");
                }
            }

            var winnerUnit = Fight(chosenUnit, Units[1]);
            if (winnerUnit == chosenUnit)
            {
                player.NumberOfWins ++;
                Console.WriteLine("Your unit has won. Your score is now: {0} wins and {1} defeats.", player.NumberOfWins, player.NumberOfLoses);
            }
            else
            {
                player.NumberOfLoses++;
                Console.WriteLine("Your unit has lost. Your score is now: {0} wins and {1} defeats.", player.NumberOfWins, player.NumberOfLoses);
            }
        }

        public static Unit Fight(Unit unit1, Unit unit2)
        {
            while(unit1.CurrentHitpoints >= 0 && unit2.CurrentHitpoints >= 0)
            {

                if(unit1.Speed >= unit2.Speed)
                {
                    var speedMemoryUnit1 = unit1.Speed - unit2.Speed;
                    while (speedMemoryUnit1 > 0 && unit1.CurrentHitpoints >= 0 && unit2.CurrentHitpoints >= 0)
                    {
                        Random random2 = new();
                        int numberOfRandomMove2 = random2.Next(0, 4);
                        var randomedMove2 = unit1.UnitMoves[numberOfRandomMove2];
                        while (unit1.CurrentManaPoints < randomedMove2.ManaCost)
                        {
                            numberOfRandomMove2 = random2.Next(0, 4);
                            randomedMove2 = unit1.UnitMoves[numberOfRandomMove2];
                        }
                        Console.WriteLine("{0} uses {1}", unit1.Name, randomedMove2.Name);
                        double damageDealt2 = (randomedMove2.Power + unit1.Attack) * ((100 - unit2.Defense) / 100);
                        unit1.Attack += randomedMove2.AttackModifier;
                        if(randomedMove2.AttackModifier != 0)
                        {
                            Console.WriteLine("{0} changed value of its attack by {1}, having it total {2}", unit1.Name, randomedMove2.AttackModifier, unit1.Attack);
                        }
                        unit1.Defense += randomedMove2.DefenseModifier;
                        if (randomedMove2.DefenseModifier != 0)
                        {
                            Console.WriteLine("{0} changed value of its defense by {1}, having it total {2}", unit1.Name, randomedMove2.DefenseModifier, unit1.Defense);
                        }
                        unit1.Speed += randomedMove2.SpeedModifier;
                        if (randomedMove2.SpeedModifier != 0)
                        {
                            Console.WriteLine("{0} changed value of its speed by {1}, having it total {2}", unit1.Name, randomedMove2.SpeedModifier, unit1.Speed);
                        }
                        unit1.CurrentManaPoints -= randomedMove2.ManaCost;
                        if (randomedMove2.ManaCost != 0)
                        {
                            Console.WriteLine("{0} uses {1} mana to cast spell, having it total {2}", unit1.Name, randomedMove2.ManaCost, unit1.CurrentManaPoints);
                        }

                        if (unit1.CurrentHitpoints + randomedMove2.OwnHitPointsModifier <= unit1.MaxHitPoints)
                        {
                            unit1.CurrentHitpoints += randomedMove2.OwnHitPointsModifier;
                            if (randomedMove2.OwnHitPointsModifier != 0)
                            {
                                Console.WriteLine("{0} healed itself by {1}, having now {2} hit points.", unit1.Name, randomedMove2.OwnHitPointsModifier, unit1.CurrentHitpoints);
                            }
                        }
                        else
                        {
                            unit1.CurrentHitpoints = unit1.MaxHitPoints;
                            Console.WriteLine("{0} healed itself to maximum, having now {1} hit points.", unit1.Name, unit1.CurrentHitpoints);
                        }

                        int checkIfHit2 = random2.Next(0, 101);
                        if (checkIfHit2 <= randomedMove2.Accuracy)
                        {
                            unit2.CurrentHitpoints -= damageDealt2;
                            Console.WriteLine("{0} lost {1} hit points due to attack of {2}, having now {3} hit points", unit2.Name, damageDealt2, unit1.Name, unit2.CurrentHitpoints);
                        }
                        else
                        {
                            Console.WriteLine("{0} missed its attack.", unit1.Name);
                        }
                        speedMemoryUnit1 -= unit2.Speed;
                    }

                    if (unit1.CurrentHitpoints >= 0 && unit2.CurrentHitpoints >= 0)
                    {
                        Random random = new();
                        int numberOfRandomMove = random.Next(0, 4);
                        var randomedMove = unit2.UnitMoves[numberOfRandomMove];
                        while (unit2.CurrentManaPoints < randomedMove.ManaCost)
                        {
                            numberOfRandomMove = random.Next(0, 4);
                            randomedMove = unit2.UnitMoves[numberOfRandomMove];
                        }
                        Console.WriteLine("{0} uses {1}", unit2.Name, randomedMove.Name);
                        double damageDealt = (randomedMove.Power + unit2.Attack) * ((100 - unit1.Defense) / 100);
                        unit2.Attack += randomedMove.AttackModifier;
                        if (randomedMove.AttackModifier != 0)
                        {
                            Console.WriteLine("{0} changed value of its attack by {1}, having it total {2}", unit2.Name, randomedMove.AttackModifier, unit2.Attack);
                        }
                        unit2.Defense += randomedMove.DefenseModifier;
                        if (randomedMove.DefenseModifier != 0)
                        {
                            Console.WriteLine("{0} changed value of its defense by {1}, having it total {2}", unit2.Name, randomedMove.DefenseModifier, unit2.Defense);
                        }
                        unit2.Speed += randomedMove.SpeedModifier;
                        if (randomedMove.SpeedModifier != 0)
                        {
                            Console.WriteLine("{0} changed value of its speed by {1}, having it total {2}", unit2.Name, randomedMove.SpeedModifier, unit2.Speed);
                        }
                        unit2.CurrentManaPoints -= randomedMove.ManaCost;
                        if (randomedMove.ManaCost != 0)
                        {
                            Console.WriteLine("{0} uses {1} mana to cast spell, having it total {2}", unit2.Name, randomedMove.ManaCost, unit2.CurrentManaPoints);
                        }

                        if (unit2.CurrentHitpoints + randomedMove.OwnHitPointsModifier <= unit2.MaxHitPoints)
                        {
                            unit2.CurrentHitpoints += randomedMove.OwnHitPointsModifier;
                            if (randomedMove.OwnHitPointsModifier != 0)
                            {
                                Console.WriteLine("{0} healed itself by {1}, having now {2} hit points.", unit2.Name, randomedMove.OwnHitPointsModifier, unit2.CurrentHitpoints);
                            }
                        }
                        else
                        {
                            unit2.CurrentHitpoints = unit2.MaxHitPoints;
                            Console.WriteLine("{0} healed itself to maximum, having now {1} hit points.", unit2.Name, unit2.CurrentHitpoints);
                        }

                        int checkIfHit = random.Next(0, 101);
                        if (checkIfHit <= randomedMove.Accuracy)
                        {
                            unit1.CurrentHitpoints -= damageDealt;
                            Console.WriteLine("{0} lost {1} hit points due to attack of {2}, having now {3} hit points", unit1.Name, damageDealt, unit2.Name, unit1.CurrentHitpoints);
                        }
                        else
                        {
                            Console.WriteLine("{0} missed its attack.", unit2.Name);
                        }
                    }
                }
                else //needs fix
                {
                    if (unit1.Speed < unit2.Speed)
                    {
                        var speedMemoryUnit2 = unit2.Speed - unit1.Speed;
                        while (speedMemoryUnit2 > 0 && unit1.CurrentHitpoints >= 0 && unit2.CurrentHitpoints >= 0)
                        {
                            Random random3 = new();
                            int numberOfRandomMove3 = random3.Next(0, 4);
                            var randomedMove3 = unit2.UnitMoves[numberOfRandomMove3];
                            while (unit2.CurrentManaPoints < randomedMove3.ManaCost)
                            {
                                numberOfRandomMove3 = random3.Next(0, 4);
                                randomedMove3 = unit2.UnitMoves[numberOfRandomMove3];
                            }
                            Console.WriteLine("{0} uses {1}", unit2.Name, randomedMove3.Name);
                            double damageDealt = (randomedMove3.Power + unit2.Attack) * ((100 - unit1.Defense) / 100);
                            unit2.Attack += randomedMove3.AttackModifier;
                            if (randomedMove3.AttackModifier != 0)
                            {
                                Console.WriteLine("{0} changed value of its attack by {1}, having it total {2}", unit2.Name, randomedMove3.AttackModifier, unit2.Attack);
                            }
                            unit2.Defense += randomedMove3.DefenseModifier;
                            if (randomedMove3.DefenseModifier != 0)
                            {
                                Console.WriteLine("{0} changed value of its defense by {1}, having it total {2}", unit2.Name, randomedMove3.DefenseModifier, unit2.Defense);
                            }
                            unit2.Speed += randomedMove3.SpeedModifier;
                            if (randomedMove3.SpeedModifier != 0)
                            {
                                Console.WriteLine("{0} changed value of its speed by {1}, having it total {2}", unit2.Name, randomedMove3.SpeedModifier, unit2.Speed);
                            }
                            unit2.CurrentManaPoints -= randomedMove3.ManaCost;
                            if (randomedMove3.ManaCost != 0)
                            {
                                Console.WriteLine("{0} uses {1} mana to cast spell, having it total {2}", unit2.Name, randomedMove3.ManaCost, unit2.CurrentManaPoints);
                            }

                            if (unit2.CurrentHitpoints + randomedMove3.OwnHitPointsModifier <= unit2.MaxHitPoints)
                            {
                                unit2.CurrentHitpoints += randomedMove3.OwnHitPointsModifier;
                                if (randomedMove3.OwnHitPointsModifier != 0)
                                {
                                    Console.WriteLine("{0} healed itself by {1}, having now {2} hit points.", unit2.Name, randomedMove3.OwnHitPointsModifier, unit2.CurrentHitpoints);
                                }
                            }
                            else
                            {
                                unit2.CurrentHitpoints = unit2.MaxHitPoints;
                                Console.WriteLine("{0} healed itself to maximum, having now {1} hit points.", unit2.Name, unit2.CurrentHitpoints);
                            }

                            int checkIfHit = random3.Next(0, 101);
                            if (checkIfHit <= randomedMove3.Accuracy)
                            {
                                unit1.CurrentHitpoints -= damageDealt;
                                Console.WriteLine("{0} lost {1} hit points due to attack of {2}, having now {3} hit points", unit1.Name, damageDealt, unit2.Name, unit1.CurrentHitpoints);
                            }
                            else
                            {
                                Console.WriteLine("{0} missed its attack.", unit2.Name);
                            }
                            speedMemoryUnit2 -= unit1.Speed;
                        }

                        if (unit1.CurrentHitpoints >= 0 && unit2.CurrentHitpoints >= 0)
                        {
                            Random random4 = new();
                            int numberOfRandomMove4 = random4.Next(0, 4);
                            var randomedMove4 = unit1.UnitMoves[numberOfRandomMove4];
                            while (unit1.CurrentManaPoints < randomedMove4.ManaCost)
                            {
                                numberOfRandomMove4 = random4.Next(0, 4);
                                randomedMove4 = unit1.UnitMoves[numberOfRandomMove4];
                            }
                            Console.WriteLine("{0} uses {1}", unit1.Name, randomedMove4.Name);
                            double damageDealt2 = (randomedMove4.Power + unit1.Attack) * ((100 - unit2.Defense) / 100);
                            unit1.Attack += randomedMove4.AttackModifier;
                            if (randomedMove4.AttackModifier != 0)
                            {
                                Console.WriteLine("{0} changed value of its attack by {1}, having it total {2}", unit1.Name, randomedMove4.AttackModifier, unit1.Attack);
                            }
                            unit1.Defense += randomedMove4.DefenseModifier;
                            if (randomedMove4.DefenseModifier != 0)
                            {
                                Console.WriteLine("{0} changed value of its defense by {1}, having it total {2}", unit1.Name, randomedMove4.DefenseModifier, unit1.Defense);
                            }
                            unit1.Speed += randomedMove4.SpeedModifier;
                            if (randomedMove4.SpeedModifier != 0)
                            {
                                Console.WriteLine("{0} changed value of its speed by {1}, having it total {2}", unit1.Name, randomedMove4.SpeedModifier, unit1.Speed);
                            }
                            unit1.CurrentManaPoints -= randomedMove4.ManaCost;
                            if (randomedMove4.ManaCost != 0)
                            {
                                Console.WriteLine("{0} uses {1} mana to cast spell, having it total {2}", unit1.Name, randomedMove4.ManaCost, unit1.CurrentManaPoints);
                            }

                            if (unit1.CurrentHitpoints + randomedMove4.OwnHitPointsModifier <= unit1.MaxHitPoints)
                            {
                                unit1.CurrentHitpoints += randomedMove4.OwnHitPointsModifier;
                                if (randomedMove4.OwnHitPointsModifier != 0)
                                {
                                    Console.WriteLine("{0} healed itself by {1}, having now {2} hit points.", unit1.Name, randomedMove4.OwnHitPointsModifier, unit1.CurrentHitpoints);
                                }
                            }
                            else
                            {
                                unit1.CurrentHitpoints = unit1.MaxHitPoints;
                                Console.WriteLine("{0} healed itself to maximum, having now {1} hit points.", unit1.Name, unit1.CurrentHitpoints);
                            }

                            int checkIfHit2 = random4.Next(0, 101);
                            if (checkIfHit2 <= randomedMove4.Accuracy)
                            {
                                unit2.CurrentHitpoints -= damageDealt2;
                                Console.WriteLine("{0} lost {1} hit points due to attack of {2}, having now {3} hit points", unit2.Name, damageDealt2, unit1.Name, unit2.CurrentHitpoints);
                            }
                            else
                            {
                                Console.WriteLine("{0} missed its attack.", unit1.Name);
                            }
                        }
                    }
                }

            }
            if (unit1.CurrentHitpoints <= 0)
            {
                return unit2;
            }
            else
            {
                return unit1;
            }
        }
    }
}
