using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Game.Weapons
{
    public class Weapon
    {
        private static Random random = new Random();

        public int weaponId;
        public string weaponName;
        public int weaponLevel;
        public int weaponAttack;
        public int weaponDefense;
        public int weaponValue;

        public Weapon() { }

        public void setWeaponId(int id) {  weaponId = id; }
        public void setWeaponName(string name) { weaponName = name; }
        public void setWeaponLevel(int level) { weaponLevel = level; }
        public void setWeaponAttack(int attack) { weaponAttack = attack; }
        public void setWeaponDefense(int defense) { weaponDefense = defense; }
        public void setWeaponValue(int value) { weaponValue = value; }

        public int GetWeaponId() { return weaponId; }
        public string GetWeaponName() { return weaponName; }
        public int GetWeaponLevel() { return weaponLevel; }
        public int GetWeaponAttack() { return weaponAttack; }
        public int GetWeaponDefense() { return weaponDefense; }
        public int GetWeaponValue() { return weaponValue; }

        public Weapon UpgradeWeapon() {
            if(random.Next(0, 2) == 1)
            {
                UpgradeSuccess();
            }
            else
            {
                UpgradeFail();
            }

            return this;
        }

        public Weapon UpgradeSuccess()
        {
            if (weaponLevel == 0)
                return this;

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
        public override string ToString()
        {
            return $"[Sword {weaponId}] Name: {weaponName}, Level: {weaponLevel}, Attack: {weaponAttack}, Defense: {weaponDefense}";
        }
    }

    public class Shield : Weapon
    {

        public override string ToString()
        {
            return $"[Shield {weaponId}] Name: {weaponName}, Level: {weaponLevel}, Attack: {weaponAttack}, Defense: {weaponDefense}";
        }
    }


    public static class WeaponFactory
    {
        private static int id = 1;
        private static Random random = new Random();

        private static void InitWeaponBase(Weapon weapon)
        {
            weapon.setWeaponId(id++);
            weapon.setWeaponLevel(1);
        }


        private static Sword SwordCreate()
        {
            Sword sword = new Sword();

            InitWeaponBase(sword);
            
            sword.setWeaponName("용사의 검");
            sword.setWeaponAttack(random.Next(1, 30));
            sword.setWeaponDefense(0);
            sword.setWeaponValue(100);

            return sword;
        }

        private static Shield ShieldCreate()
        {
            Shield shield = new Shield();

            InitWeaponBase(shield);

            shield.setWeaponName("방패");
            shield.setWeaponAttack(0);
            shield.setWeaponDefense(random.Next(1, 100));
            shield.setWeaponValue(50);

            return shield;
        }

        public static Weapon WeaponCreate(int type) // 0: Sword / 1: Shield
        {
            switch (type)
            {
                case 0:
                    return SwordCreate();
                case 1:
                    return ShieldCreate();
                default:
                    throw new ArgumentException("Invalid weapon type");
            }
        }
    }
}
