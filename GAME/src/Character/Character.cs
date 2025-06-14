using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Characters
{
    public class Character
    {
        Random rand = new Random();

        private int characterId;
        private string characterName;
        private int characterLevel;
        private int characterExp;
        private int characterMoney;
        private int characterMapId;
        private (int x, int y) characterLocation;
        private int characterHp;
        private int characterAttack;
        private Weapon characterSword;
        private Weapon characterShiled;
        private List<Weapon> characterWeapons;

        private Character() { }

        public int GetCharacterId() => characterId;
        public string GetCharacterName() => characterName;
        public int GetCharacterLevel() => characterLevel;
        public int GetCharacterExp() => characterExp;
        public int GetCharacterMoney() => characterMoney;
        public int GetCharacterMapId() => characterMapId;
        public (int x, int y) GetCharacterLocation() => characterLocation;
        public int GetCharacterHp() => characterHp;
        public int GetCharacterAttack() => characterAttack;

        // 저장
        // 불러오기

        public void AquireMoney(int money) // 돈 획득
        {
            characterMoney += money;
        }

        public void AquireExp(int exp) // 경험치 획득
        {
            characterExp += exp;

            while (characterExp > characterLevel * 100)
            {
                characterExp -= characterLevel * 100;
                characterLevel++;
            }
        }

        public void AquireWeapon(Weapon weapon)
        {
            characterWeapons.Add(weapon);
        }

        public void MoveMap(int mapId) // Map 이동
        {
            characterMapId = mapId;
            // 이동 위치
            characterLocation = (0, 0);
        }

        // dx, dy 만큼 이동
        public void MoveLocation(int dx, int dy)
        {
            characterLocation = (characterLocation.x + dx, characterLocation.y + dy);
        }

        // 공격력 반환
        public int Attack()
        {
            return characterAttack
                + (characterSword?.GetWeaponAttack() ?? 0)
                + (characterShiled?.GetWeaponAttack() ?? 0);
        }

        // 공격력이 방어력 보다 높은 경우 HP 감소, 방어력이 더 높으면 공격 무시
        public void Defense(int attack)
        {
            if((characterShiled?.GetWeaponDefense() ?? 0) < attack)
            {
                characterHp -= attack;
            }
        }

        // 무기 장착
        public void EquipWeapon(Weapon weapon)
        {
            if (weapon is Sword)
            {
                characterSword = weapon;
            }
            else if (weapon is Shield)
            {
                characterShiled = weapon;
            }
        }

        public void RemoveWeapon(Weapon weapon)
        {
            if (weapon == null)
                return;

            if (weapon == characterSword) {
                characterSword = null;
            }
            else if(weapon == characterShiled) {
                characterShiled = null;
            }
        }
        
        public void DropWeapon(Weapon weapon)
        {
            if (weapon == null)
                return;

            characterWeapons.Remove(weapon);
        }

        public void Die()
        {
            int option = rand.Next(0, 2);
            Weapon lostWeapon = null;

            switch (option)
            {
                case 0:
                    lostWeapon = characterSword;
                    break;

                case 1:
                    lostWeapon = characterShiled;
                    break;
            }

            RemoveWeapon(lostWeapon);
            DropWeapon(lostWeapon);

            // 죽은 다음처리
            Console.WriteLine($"{characterName}이(가) 죽어서 {lostWeapon.GetWeaponName()}을 잃었습니다.");
        }

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
            public Builder SetCharacterLocation(int x, int y) { c.characterLocation = (x, y); return this; }
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
            // HP 100 ~ 200
            .SetCharacterHp(rand.Next(100, 200))
            // Attack 30 ~ 50
            .SetCharacterAttack(rand.Next(30, 50))
            .Build();
        }
    }
}