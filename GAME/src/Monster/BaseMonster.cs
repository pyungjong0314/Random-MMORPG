using System;
using Game.Maps;
using Game.Monsters;



namespace Game.BaseMonster
{
    // 일반 몬스터 기본 클래스
    public class Monster
    {
        public string MonsterName;
        public string MonsterId; // mid
        public int MonsterLevel;
        public int MonsterCoinValue = 0;
        public int MonsterMapId;
        public (int x, int y) MonsterLocation;
        public int MonsterHp;
        public int MonsterAttackAbility;
        public int MonsterDefenseAbility;

        // 몬스터가 자신이 소속된 맵을 기억함
        public Map MapRef { get; set; }

        public Monster() { } // 이거 꼭 추가!

        public Monster(
            string name,
            string id,
            int level,
            int coinValue,
            int mapId,
            (int x, int y) location,
            int hp,
            int attack,
            int defense)
        {
            MonsterName = name;
            MonsterId = id;
            MonsterLevel = level;
            MonsterCoinValue = coinValue;
            MonsterMapId = mapId;
            MonsterLocation = location;
            MonsterHp = hp;
            MonsterAttackAbility = attack;
            MonsterDefenseAbility = defense;
        }

        public bool IsDead { get; private set; } = false;
        public void setName(string name) => MonsterName = name;
        public void setId(string id) => MonsterId = id;
        public void setLevel(int level) => MonsterLevel = level;
        public void setcoinvalue(int coin) => MonsterCoinValue = coin;
        public void SetMapId(int mapId) => MonsterMapId = mapId;
        public void SetLocation(int x, int y) => MonsterLocation = (x, y);
        public void SetHp(int hp) => MonsterHp = hp;
        public void SetAttack(int atk) => MonsterAttackAbility = atk;
        public void SetDefense(int def) => MonsterDefenseAbility = def;

        public virtual void MonsterCreate() { }
        public virtual void MonsterSave() { }
        public virtual void MonsterLoad() { }

        // 데미지 받은만큼 Hp 감소
        public virtual int MonsterGetAttack(int damage) {
            MonsterHp -= damage;
            if (MonsterHp <= 0)
            {
                return MonsterDie();
            }
            return 0;
        } 

        // HP 회복
        public virtual void MonsterGetHp(int hp) { MonsterHp += hp; }

        public virtual void MonsterMoveMap(int newMapId) => MonsterMapId = newMapId;
        public virtual void MonsterMoveLocation(int x, int y) => MonsterLocation = (x, y);
        public virtual void MonsterAttackEnemy() { }
        public virtual void MonsterDefend() { }

        // 몬스터 죽으면 다시 리스폰
        public virtual int MonsterDie()
        {

            if (IsDead) return 0;

            IsDead = true;

            Console.WriteLine($"{MonsterName} has died.");

            MapRef?.RemoveMonster(this);
            MapRef?.RequestRespawn(this.GetType(), MonsterLocation, 3); // 3초 뒤 같은 자리에 리스폰 요청


            return MonsterCoinValue;
        }




        public virtual void MonsterDropWeapon() { }
    }


    // 몬스터 팩토리 클래스
    public static class MonsterFactory
    {

        //  공통 부분 수정
        private static void BaseMonster(Monster monster)
        {
            // 여기서 공통 부분 설정
            monster.setLevel(10);
            monster.SetMapId(1);
        }


        private static Goblin GoblinCreate()
        {
            Goblin goblin = new Goblin();
            BaseMonster(goblin);
            // 고블린 설정
            goblin.setName("goblin");
            goblin.SetMapId(100);

            return goblin;
        }


        private static Slime SlimeCreate()
        {
            Slime slime = new Slime();
            BaseMonster(slime);


            // 슬라임 설정
            slime.setName("slime");
            slime.SetMapId(200);

            return slime;
        }


        private static Scorpion ScorpionCreate()
        {
            Scorpion scorpion = new Scorpion();
            BaseMonster(scorpion);

            // 스콜피온 설정
            scorpion.setName("scorpion");
            scorpion.SetMapId(300);

            return scorpion;
        }



        private static Witch WitchCreate()
        {
            Witch witch = new Witch();
            BaseMonster(witch);

            // 마녀 설정
            witch.setName("witch");
            witch.SetMapId(400);

            return witch;
        }


        private static Basilisk BasiliskCreate()
        {
            Basilisk basilisk = new Basilisk();

            BaseMonster(basilisk);

            // 바실리스크 설정
            basilisk.setName("basilisk");
            basilisk.SetMapId(500);

            return basilisk;
        }


        private static Orc OrcCreate()
        {
            Orc orc = new Orc();

            BaseMonster(orc);

            // 오크 설정
            orc.setName("orc");
            orc.SetMapId(600);


            return orc;
        }

        public static Monster CreateMonster(int type)
        {
            switch (type)
            {
                case 0:
                    return GoblinCreate();
                case 1:
                    return SlimeCreate();
                case 2:
                    return ScorpionCreate();
                case 3:
                    return WitchCreate();
                case 4:
                    return BasiliskCreate();
                case 5:
                    return OrcCreate();
                default:
                    throw new ArgumentException("Invalid monster type");
            }
        }
    }
}