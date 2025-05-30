using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 일반 몬스터 기본 클래스
public class Monster
{
    public string MonsterName;
    public string MonsterId; // mid
    public int MonsterLevel;
    public int MonsterCoinValue;
    public int MonsterMapId;
    public (int x, int y) MonsterLocation;
    public int MonsterHp;
    public int MonsterAttackAbility;
    public int MonsterDefenseAbility;

    public Monster(string name, string id, int level, int coinValue, int mapId, (int x, int y) location, int hp, int attack, int defense)
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

    public virtual void MonsterCreate() { }
    public virtual void MonsterSave() { }
    public virtual void MonsterLoad() { }
    public virtual void MonsterGetAttack() { }
    public virtual void MonsterGetHp() { }
    public virtual void MonsterMoveMap(int newMapId) => MonsterMapId = newMapId;
    public virtual void MonsterMoveLocation(int x, int y) => MonsterLocation = (x, y);
    public virtual void MonsterAttackEnemy() { }
    public virtual void MonsterDefend() { }
    public virtual void MonsterDie() { }
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
// LunaCrab 
public class LunaCrab : BossMonster
{
    public LunaCrab()
        : base(
            name: "LunaCrab",
            id: "101",
            level: 5,
            coinValue: 90,
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

// GoblinKing 
public class GoblinKing : BossMonster
{
    public GoblinKing()
        : base(
            name: "GoblinKing",
            id: "102",
            level: 5,
            coinValue: 90,
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

// DarkKnight 
public class DarkKnight : BossMonster
{
    public DarkKnight()
        : base(
            name: "DarkKnight",
            id: "103",
            level: 5,
            coinValue: 90,
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




// 일반 몬스터 리스트
// Goblin 
public class Goblin : Monster
{
    public Goblin()
        : base(
            name: "Goblin",
            id: "001",
            level: 5,
            coinValue: 90,
            mapId: 10,
            location: (4, 2),
            hp: 2700,
            attack: 55,
            defense: 30)
    { }

    public void Slash() { Console.WriteLine("고블린이 베기를 사용했다!"); }
}

// Slime 
public class Slime : Monster
{
    public Slime()
        : base(
            name: "Slime",
            id: "002",
            level: 5,
            coinValue: 90,
            mapId: 10,
            location: (4, 2),
            hp: 2700,
            attack: 55,
            defense: 30)
    { }

    public void Spit() { Console.WriteLine("슬라임이 침 뱉기를 사용했다!"); }
}

// Scorpion 
public class Scorpion : Monster
{
    public Scorpion()
        : base(
            name: "Scorpion",
            id: "003",
            level: 5,
            coinValue: 90,
            mapId: 10,
            location: (4, 2),
            hp: 2700,
            attack: 55,
            defense: 30)
    { }

    public void Sting() { Console.WriteLine("전갈이 침쏘기를 사용했다!"); }
}

// Witch 
public class Witch : Monster
{
    public Witch()
        : base(
            name: "Witch",
            id: "004",
            level: 5,
            coinValue: 90,
            mapId: 10,
            location: (4, 2),
            hp: 2700,
            attack: 55,
            defense: 30)
    { }

    public void CastSpell() { Console.WriteLine("마녀가 마법을 시전했다!"); }
}

// Basilisk 
public class Basilisk : Monster
{
    public Basilisk()
        : base(
            name: "Basilisk",
            id: "005",
            level: 5,
            coinValue: 90,
            mapId: 10,
            location: (4, 2),
            hp: 2700,
            attack: 55,
            defense: 30)
    { }

    public void Petrify() { Console.WriteLine("바실리스크가 석화 시선을 사용했다!"); }
}

// Orc 
public class Orc : Monster
{
    public Orc()
        : base(
            name: "Orc",
            id: "006",
            level: 5,
            coinValue: 90,
            mapId: 10,
            location: (4, 2),
            hp: 2700,
            attack: 55,
            defense: 30)
    { }

    public void Rush() { Console.WriteLine("오크가 돌진을 사용했다!"); }
}
