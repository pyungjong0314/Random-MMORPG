using System;
using Game.BaseBossMonster;



namespace Game.BossMonsters
{
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
}
