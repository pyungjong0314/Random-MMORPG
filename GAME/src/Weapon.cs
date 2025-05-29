using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Game.Characters
{
    public class Weapon
    {
        Random random = new Random();

        protected int weaponId;
        protected string weaponName;
        protected int weaponLevel;
        protected int weaponAttack;
        protected int weaponDefense;

        protected Weapon() { }

        public int GetWeaponId() { return weaponId; }
        public string GetWeaponName() { return weaponName; }
        public int GetWeaponLevel() { return weaponLevel; }
        public int GetWeaponAttack() { return weaponAttack; }
        public int GetWeaponDefense() { return weaponDefense; }

        public Weapon UpgradeWeapon() {
            if(random.Next(0, 2) == 0)
            {
                UpgradeFail();
            }
            else
            {
                UpgradeSuccess();
            }
            return this;
        }

        public Weapon UpgradeSuccess()
        {
            weaponLevel++;
            weaponAttack *= 2;
            weaponDefense *= 2;
            return this;
        }

        public Weapon UpgradeFail()
        {
            weaponLevel = 0;
            weaponAttack = 0;
            weaponDefense = 0;
            return this;
        }
    }

    public class Sword : Weapon
    {
        public Sword(int id, string name, int level, int attack, int defense)
        {
            weaponId = id;
            weaponName = name;
            weaponLevel = level;
            weaponAttack = attack;
            weaponDefense = defense;
        }

        public override string ToString()
        {
            return $"[Sword {weaponId}] Name: {weaponName}, Level: {weaponLevel}, Attack: {weaponAttack}, Defense: {weaponDefense}";
        }
    }
    public class Shield : Weapon
    {
        public Shield(int id, string name, int level, int attack, int defense)
        {
            weaponId = id;
            weaponName = name;
            weaponLevel = level;
            weaponAttack = attack;
            weaponDefense = defense;
        }

        public override string ToString()
        {
            return $"[Shield {weaponId}] Name: {weaponName}, Level: {weaponLevel}, Attack: {weaponAttack}, Defense: {weaponDefense}";
        }
    }


    public static class WeaponFactory
    {
        private static int id = 1;
        private static Random random = new Random();

        public static Weapon SwordCreate()
        {
            return new Sword(id++, "검", 1, random.Next(1, 10), 0);
        }

        public static Weapon ShieldCreate()
        {
            return new Shield(id++, "방패", 1, 0, random.Next(1, 10));
        }
    }
}
