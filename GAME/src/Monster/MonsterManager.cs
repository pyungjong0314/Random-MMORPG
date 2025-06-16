using Game.BaseMonster;
using Game.BossMonsters;
using Game.Monsters;
using System;
using System.Drawing;
using System.IO;

namespace Game.MonsterManagers
{
    public static class MonsterManager
    {
        private static readonly string basePath = Path.GetFullPath(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Resources", "Monsters"));

        public static Monster CreateMonsterFromType(Type monsterType)
        {
            if (monsterType == typeof(Goblin)) return new Goblin();
            if (monsterType == typeof(Slime)) return new Slime();
            if (monsterType == typeof(Scorpion)) return new Scorpion();
            if (monsterType == typeof(Witch)) return new Witch();
            if (monsterType == typeof(Basilisk)) return new Basilisk();
            if (monsterType == typeof(Orc)) return new Orc();
            if (monsterType == typeof(LunaCrab)) return new LunaCrab();
            if (monsterType == typeof(GoblinKing)) return new GoblinKing();
            if (monsterType == typeof(DarkKnight)) return new DarkKnight();
            throw new Exception("Unknown monster type");
        }

        public static Size GetMonsterSize(Type monsterType)
        {
            if (monsterType == typeof(Goblin)) return new Size(82, 73);
            if (monsterType == typeof(Slime)) return new Size(68, 73);
            return new Size(64, 64); // default size
        }

        // 👇 상태별 이미지 반환 함수들

        public static Image GetIdleImage(Type monsterType)
            => LoadImage(monsterType, "idle");

        public static Image GetAttackImage(Type monsterType)
            => LoadImage(monsterType, "attack");

        public static Image GetSkillImage(Type monsterType)
            => LoadImage(monsterType, "skill");

        public static Image GetDeadImage(Type monsterType)
            => LoadImage(monsterType, "dead");

        private static Image LoadImage(Type monsterType, string state)
        {
            string fileName = $"{monsterType.Name.ToLower()}.png";

            // fileName = $"{monsterType.Name.ToLower()}_{state}.png";
            string fullPath = Path.Combine(basePath, fileName);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"Monster image not found: {fullPath}");

            return Image.FromFile(fullPath);
        }



        // 몬스터 이미지 반환하는 함수
        public static Image CreateImageFromType(Type monsterType)
        {
            // 몬스터 이미지 경로 설정 (고블린으로 기본 설정)
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string imagePath = Path.Combine(basePath, "..", "..", "Resources", "goblin1.png");

            // 몬스터 이미지 설정
            if (monsterType == typeof(Slime))
                imagePath = Path.Combine(basePath, "..", "..", "Resources", "slime.png");
            else if (monsterType == typeof(Scorpion))
                imagePath = Path.Combine(basePath, "..", "..", "Resources", "scorpion.png");
            else if (monsterType == typeof(Witch))
                imagePath = Path.Combine(basePath, "..", "..", "Resources", "wizard.png");

            // 몬스터 이미지 반환하기
            return Image.FromFile(Path.GetFullPath(imagePath));
        }


    }
}
