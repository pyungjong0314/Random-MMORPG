    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Game.Characters;
    using Game.Monsters;
    using Game.Maps;
    using System.Data;

    namespace WindowsFormsApp1
    {
        internal static class Program
        {
            [STAThread]

            static void Main()
            {
                Map m = MapFactory.CreateMap(1);

                // 초기 상태 출력 및 1000 데미지
                foreach (var monster in m.monsters.ToList()) // ToList()로 복사본 사용
                {
                    Console.WriteLine($"{monster.MonsterName}{monster.MonsterLocation}{monster.MonsterHp}");
                    monster.MonsterGetAttack(1000);
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();

                // 3000 추가 데미지
                foreach (var monster in m.monsters.ToList())
                {
                    Console.WriteLine($"{monster.MonsterName}{monster.MonsterLocation}{monster.MonsterHp}");
                    monster.MonsterGetAttack(3000);
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();


                // 최종 생존자 출력
                foreach (var monster in m.monsters)
                {
                    Console.WriteLine($"{monster.MonsterName}{monster.MonsterLocation}{monster.MonsterHp}");
                }
            }
        }
    }
