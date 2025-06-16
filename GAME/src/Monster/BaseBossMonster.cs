using System;
using Game.BaseMonster;
using Game.BossMonsters;



namespace Game.BaseBossMonster
{

    // 보스몹 기본 클래스
    public class BossMonster : Monster
    {
        public BossMonster(string name, string id, int level, int coinValue, int mapId, (int x, int y) location, int hp, int attack, int defense, int exp)
            : base(name, id, level, coinValue, mapId, location, hp, attack, defense, exp) { }

        public virtual void UltimateSkill() { }
    }



    // 몬스터 팩토리 클래스
    public static class BossMonsterFactory
    {

        //  공통 부분 수정
        private static void BaseBossMonster(BossMonster bossMonster)
        {
            bossMonster.setLevel(200);
            bossMonster.SetMapId(3);
        }


        // 1. 루나 크랩 생성
        private static LunaCrab LunaCrabCreate()
        {
            LunaCrab lunaCrab = new LunaCrab();
            BaseBossMonster(lunaCrab);


            // 루나 크랩 설정
            lunaCrab.setName("lunacrab");
            lunaCrab.SetMapId(100);

            return lunaCrab;
        }


        // 2. 고블린킹 생성
        private static GoblinKing GoblinKingCreate()
        {
            GoblinKing goblinKing = new GoblinKing();
            BaseBossMonster(goblinKing);

            // 고블린킹 설정
            goblinKing.setName("goblinking");
            goblinKing.SetMapId(100);


            return goblinKing;
        }


        // 3. 다크나잇 생성
        private static DarkKnight DarkKnightCreate()
        {
            DarkKnight darkKnight = new DarkKnight();
            BaseBossMonster(darkKnight);
            
            
            // 다크나잇 설정
            darkKnight.setName("darkknight");
            darkKnight.SetMapId(100);

            return darkKnight;
        }


        // 보스몹 생성 함수
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
