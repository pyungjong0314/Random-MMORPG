using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Characters
{
    public class Character
    {
        private int characterId;
        private string characterName;
        private int characterLevel;
        private int characterExp;
        private int characterMoney;
        private int characterMapId;
        private Tuple<int, int> characterLocation;
        private int characterHp;
        private int characterAttack;

        private Character() { }

        public int GetCharacterId() => characterId;
        public string GetCharacterName() => characterName;
        public int GetCharacterLevel() => characterLevel;
        public int GetCharacterExp() => characterExp;
        public int GetCharacterMoney() => characterMoney;
        public int GetCharacterMapId() => characterMapId;
        public Tuple<int, int> GetCharacterLocation() => characterLocation;
        public int GetCharacterHp() => characterHp;
        public int GetCharacterAttack() => characterAttack;

        public override string ToString()
        {
            return "Id: " + characterId
                + "\nName: " + characterName
                + "\nLevel: " + characterLevel
                + " (exp: " + characterExp + ")"
                + "\nAttack: " + characterAttack
                + "\nHP: " + characterHp
                + "\nMoney: " + characterMoney + "\n";
        }

        public class Builder
        {
            private readonly Character c = new Character();
            public Builder SetCharacterId(int id) { c.characterId = id; return this; }
            public Builder SetCharacterName(string name) { c.characterName = name; return this; }
            public Builder SetCharacterLevel(int level) { c.characterLevel = level; return this; }
            public Builder SetCharacterExp(int exp) { c.characterExp = exp; return this; }
            public Builder SetCharacterMoney(int money) { c.characterMoney = money; return this; }
            public Builder SetCharacterMapId(int mapId) { c.characterMapId = mapId; return this; }
            public Builder SetCharacterLocation(int x, int y) { c.characterLocation = new Tuple<int, int>(x, y); return this; }
            public Builder SetCharacterHp(int hp) { c.characterHp = hp; return this; }
            public Builder SetCharacterAttack(int atk) { c.characterAttack = atk; return this; }

            public Character Build() => c;
        }
    }


    public static class CharacterFactory
    {
        static int id = 1;
        static Random rand = new Random();
        public static Character CharacterCreate(string name)
        {
            return new Character.Builder()
            .SetCharacterId(id++)
            .SetCharacterName(name)
            .SetCharacterLevel(1)
            .SetCharacterExp(0)
            .SetCharacterMoney(0)
            .SetCharacterMapId(0)
            .SetCharacterLocation(0, 0)
            .SetCharacterHp(rand.Next(100, 200))
            .SetCharacterAttack(rand.Next(30, 50))
            .Build();
        }
    }
}
