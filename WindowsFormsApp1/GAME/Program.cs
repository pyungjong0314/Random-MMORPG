using System;
using System.Collections.Generic;
using System.Linq;
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
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());*/

            Character c1 = CharaceterFactory.CharacterCreate("짱구");
            Character c2 = CharaceterFactory.CharacterCreate("철수");
            Character c3 = CharaceterFactory.CharacterCreate("훈이");

            
            Console.WriteLine(c1.ToString());
            Console.WriteLine(c2.ToString());
            Console.WriteLine(c3.ToString());
            Console.ReadLine();
        }
    }
}
