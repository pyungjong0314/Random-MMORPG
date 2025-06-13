using System;
using Game.BaseMonster;



namespace Game.Monsters
{
    // 일반 몬스터 리스트
    // Goblin 
    public class Goblin : Monster
    {
        private static int goblinCount = 0;

        public Goblin()
            : base(
                name: "Goblin",
                id: (100 + goblinCount++).ToString(),
                level: 5,
                coinValue: 90,
                mapId: 10,
                location: (4, 2),
                hp: 500,
                attack: 55,
                defense: 30)
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
                id: (200 + slimeCount++).ToString(),
                level: 5,
                coinValue: 90,
                mapId: 10,
                location: (4, 2),
                hp: 500,
                attack: 55,
                defense: 30)
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
                id: (300 + scorpionCount++).ToString(),
                level: 5,
                coinValue: 90,
                mapId: 10,
                location: (4, 2),
                hp: 500,
                attack: 55,
                defense: 30)
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
                id: (400 + witchCount++).ToString(),
                level: 5,
                coinValue: 90,
                mapId: 10,
                location: (4, 2),
                hp: 500,
                attack: 55,
                defense: 30)
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
                id: (500 + basiliskCount++).ToString(),
                level: 5,
                coinValue: 90,
                mapId: 10,
                location: (4, 2),
                hp: 500,
                attack: 55,
                defense: 30)
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
                id: (600 + orcCount++).ToString(),
                level: 5,
                coinValue: 90,
                mapId: 10,
                location: (4, 2),
                hp: 500,
                attack: 55,
                defense: 30)
        { }

        public void Rush() { Console.WriteLine("오크가 돌진을 사용했다!"); }
    }
}
