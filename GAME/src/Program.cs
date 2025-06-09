using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Game.Monsters;
using Game.Maps;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game.Characters;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            // cmd 103 = 지도 모든 몬스터의 위치 표시 (예시 : map_id = 1)
            Map m = MapFactory.CreateMap(2);

            Console.WriteLine("=== 지도 모든 몬스터의 위치 표시 (cmd 103) ===");
            foreach (var monster in m.monsters)
            {
                Console.WriteLine($"MID : {monster.MonsterId}, Name : {monster.MonsterName}, Pos : {monster.MonsterLocation}, HP : {monster.MonsterHp}  Coin : {monster.MonsterCoinValue}");
                Console.WriteLine();
            }


            // cmd 502 = 몬스터 피해가 피해 받았을때 (예시 : damage = 4000)
            Console.WriteLine("\n=== 몬스터 피해가 피해 받았을때 (cmd 502) ===");
            foreach (var monster in m.monsters.ToList())
            {
                int coins = monster.MonsterGetAttack(4000);
                Console.WriteLine();
            }

            // 리스폰 테스트를 위해 6초간 매초 Update 호출
            Console.WriteLine("\n=== 몬스터 리스폰 대기중... (6초) ===");
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine($"[Time: {i + 1}s] 업데이트 호출");
                m.Update();
                Thread.Sleep(1000); // 1초 대기
            }

            // 몬스터 리스폰 결과 출력
            Console.WriteLine("\n=== 몬스터가 다시 생성됨 ===");
            foreach (var monster in m.monsters.ToList())
            {
                Console.WriteLine($"MID : {monster.MonsterId}, Name : {monster.MonsterName}, Pos : {monster.MonsterLocation}, HP : {monster.MonsterHp}");
            }
        }
    }
}