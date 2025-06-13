using System;
using Game.BaseMonster;
using Game.BossMonsters;



namespace Game.BaseBossMonster
{

    // 보스몹 기본 클래스
    public class BossMonster : Monster
    {
        public BossMonster(string name, string id, int level, int coinValue, int mapId, (int x, int y) location, int hp, int attack, int defense)
            : base(name, id, level, coinValue, mapId, location, hp, attack, defense) { }

        public virtual void UltimateSkill() { }
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

}
