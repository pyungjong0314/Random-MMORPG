using System;
using System.ComponentModel;
using Game.BaseMonster;



namespace Game.Monsters
{
    public class Goblin : Monster
    {
        public Goblin()
            : base(
                name: "Goblin",
                id: 0,
                level: 4,
                coinValue: new Random().Next(10, 20),
                mapId: 1,
                location: (4, 2),
                hp: new Random().Next(55, 70),
                attack: new Random().Next(15, 30),
                defense: 30,
                exp: new Random().Next(15, 30))
        { }

        public void Slash() { Console.WriteLine("고블린이 베기를 사용했다!"); }
    }

    // Slime 
    public class Slime : Monster
    {
        private static int slimeCount = 0;

        public Slime()
            : base(
                name: "Slime",
                id: 0,
                level: 1,
                coinValue: new Random().Next(5, 10),
                mapId: 10,
                location: (4, 2),
                hp: new Random().Next(40, 60),
                attack: new Random().Next(5, 10),
                defense: 30,
                exp: new Random().Next(10, 20))
        { }

        public void Spit() { Console.WriteLine("슬라임이 침 뱉기를 사용했다!"); }
    }

    // Scorpion 
    public class Scorpion : Monster
    {
        private static int scorpionCount = 0;

        public Scorpion()
            : base(
                name: "Scorpion",
                id: 0,
                level: 5,
                coinValue: 200,
                mapId: 10,
                location: (4, 2),
                hp: 500,
                attack: 55,
                defense: 30,
                exp:100)
        { }

        public void Sting() { Console.WriteLine("전갈이 침쏘기를 사용했다!"); }
    }

    // Witch 
    public class Witch : Monster
    {
        private static int witchCount = 0;

        public Witch()
            : base(
                name: "Witch",
                id: 0,
                level: 5,
                coinValue: 150,
                mapId: 10,
                location: (4, 2),
                hp: 10,
                attack: 55,
                defense: 30,
                exp:5)
        { }

        public void CastSpell() { Console.WriteLine("마녀가 마법을 시전했다!"); }
    }

    // Basilisk 
    public class Basilisk : Monster
    {
        private static int basiliskCount = 0;


        public Basilisk()
            : base(
                name: "Basilisk",
                id: 0,
                level: 5,
                coinValue: 90,
                mapId: 10,
                location: (4, 2),
                hp: 500,
                attack: 55,
                defense: 30,
                exp:2
               )
        { }

        public void Petrify() { Console.WriteLine("바실리스크가 석화 시선을 사용했다!"); }
    }

    // Orc 
    public class Orc : Monster
    {
        private static int orcCount = 0;

        public Orc()
            : base(
                name: "Orc",
                id: 0,
                level: 5,
                coinValue: 90,
                mapId: 10,
                location: (4, 2),
                hp: 500,
                attack: 55,
                defense: 30,
                exp:14)
        { }

        public void Rush() { Console.WriteLine("오크가 돌진을 사용했다!"); }
    }
}
