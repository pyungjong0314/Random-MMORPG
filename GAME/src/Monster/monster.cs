using Game.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Maps;



namespace Game.Monsters
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
            MapRef?.RemoveMonster(this);
            MapRef?.RequestRespawn(this.GetType(), MonsterLocation, 5);
            return MonsterCoinValue;
        }



        public virtual void MonsterDropWeapon() { }
    }



    // 보스몹 기본 클래스
    public class BossMonster : Monster
    {
        public BossMonster(string name, string id, int level, int coinValue, int mapId, (int x, int y) location, int hp, int attack, int defense)
            : base(name, id, level, coinValue, mapId, location, hp, attack, defense) { }

        public virtual void UltimateSkill() { }
    }




    // 보스몹 리스트
    // GoblinKing 
    public class GoblinKing : BossMonster
    {
        public GoblinKing()
            : base(
                name: "GoblinKing",
                id: "1000",
                level: 5,
                coinValue: 1000,
                mapId: 10,
                location: (4, 2),
                hp: 2700,
                attack: 55,
                defense: 30)
        { }

        public void BladeDance() { Console.WriteLine("고블린왕이 칼춤을 사용했다!"); }
        public void DiceCarnage() { Console.WriteLine("고블린왕이 주사위 학살을 사용했다!"); }

        public override void UltimateSkill()
        {
            BladeDance();
            DiceCarnage();
        }
    }

    // LunaCrab 
    public class LunaCrab : BossMonster
    {
        public LunaCrab()
            : base(
                name: "LunaCrab",
                id: "2000",
                level: 5,
                coinValue: 1000,
                mapId: 10,
                location: (4, 2),
                hp: 2700,
                attack: 55,
                defense: 30)
        { }

        public void ShellGuard() { Console.WriteLine("루나크랩이 껍질 방어를 사용했다!"); }
        public void TidalSmash() { Console.WriteLine("루나크랩이 해일 강타를 사용했다!"); }

        public override void UltimateSkill()
        {
            ShellGuard();
            TidalSmash();
        }
    }


    // DarkKnight
    public class DarkKnight : BossMonster
    {
        public DarkKnight()
            : base(
                name: "DarkKnight",
                id: "3000",
                level: 5,
                coinValue: 1000,
                mapId: 10,
                location: (4, 2),
                hp: 2700,
                attack: 55,
                defense: 30)
        { }

        public void DarkSlash() { Console.WriteLine("다크나이트가 암흑 베기를 사용했다!"); }
        public void ShadowStrike() { Console.WriteLine("다크나이트가 그림자 타격을 사용했다!"); }

        public override void UltimateSkill()
        {
            DarkSlash();
            ShadowStrike();
        }
    }


    // 몬스터 팩토리 클래스
    public static class BossMonsterFactory
    {

        //  공통 부분 수정
        private static void BaseBossMonster(BossMonster bossMonster)
        {
            // 여기서 공통 부분 설정
            bossMonster.setLevel(200);
            bossMonster.SetMapId(3);
        }


        private static LunaCrab LunaCrabCreate()
        {
            LunaCrab lunaCrab = new LunaCrab();
            BaseBossMonster(lunaCrab);
            
            
            // 고블린 설정
            lunaCrab.setName("lunacrab");
            lunaCrab.SetMapId(100);

            return lunaCrab;
        }


        private static GoblinKing GoblinKingCreate()
        {
            GoblinKing goblinKing = new GoblinKing();
            BaseBossMonster(goblinKing);
            // 고블린 설정
            goblinKing.setName("goblinking");
            goblinKing.SetMapId(100);


            return goblinKing;
        }


        private static DarkKnight DarkKnightCreate()
        {
            DarkKnight darkKnight = new DarkKnight();
            BaseBossMonster(darkKnight);
            // 고블린 설정
            darkKnight.setName("darkknight");
            darkKnight.SetMapId(100);

            return darkKnight;
        }

        public static BossMonster CreateBossMonster(int type)
        {
            switch (type)
            {
                case 0:
                    return LunaCrabCreate();
                case 1:
                    return GoblinKingCreate();
                case 2:
                    return DarkKnightCreate();
                default:
                    throw new ArgumentException("Invalid monster type");
            }
        }
    }








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
        private static int  witchCount = 0;

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