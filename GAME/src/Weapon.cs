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
        protected int weaponAttackAbility;
        protected int weaponDefenseAbility;

        protected Weapon() { }

        public int GetWeaponId() { return weaponId; }
        public string GetWeaponName() { return weaponName; }
        public int GetWeaponLevel() { return weaponLevel; }
        public int GetWeaponAttackAbility() { return weaponAttackAbility; }
        public int GetWeaponDefenseAbility() { return weaponDefenseAbility; }

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
            weaponAttackAbility *= 2;
            weaponDefenseAbility *= 2;
            return this;
        }

        public Weapon UpgradeFail()
        {
            weaponLevel = 0;
            weaponAttackAbility = 0;
            weaponDefenseAbility = 0;
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
            weaponAttackAbility = attack;
            weaponDefenseAbility = defense;
        }

        public override string ToString()
        {
            return $"[Sword {weaponId}] Name: {weaponName}, Level: {weaponLevel}, Attack: {weaponAttackAbility}, Defense: {weaponDefenseAbility}";
        }
    }
    public class Shield : Weapon
    {
        public Shield(int id, string name, int level, int attack, int defense)
        {
            weaponId = id;
            weaponName = name;
            weaponLevel = level;
            weaponAttackAbility = attack;
            weaponDefenseAbility = defense;
        }

        public override string ToString()
        {
            return $"[Shield {weaponId}] Name: {weaponName}, Level: {weaponLevel}, Attack: {weaponAttackAbility}, Defense: {weaponDefenseAbility}";
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
