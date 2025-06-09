using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
// 배포시 폰트 불러오는 파일
// 아직 수정 필요
namespace WindowsFormsApp1
{
    public static class FontManager
    {
        private static readonly PrivateFontCollection FontCollection = new PrivateFontCollection();
        private static FontFamily RegularFamily;
        private static FontFamily BoldFamily;

        public static Font Galmuri11_9 { get; private set; }
        public static Font Galmuri11_10 { get; private set; }
        public static Font Galmuri11Bold_23 { get; private set; }

        static FontManager()
        {
            AddFont(Properties.Resources.Galmuri11, out RegularFamily);
            AddFont(Properties.Resources.Galmuri11Bold, out BoldFamily);

            Galmuri11_9 = new Font(RegularFamily, 9f, FontStyle.Regular);
            Galmuri11_10 = new Font(RegularFamily, 10f, FontStyle.Regular);
            Galmuri11Bold_23 = new Font(BoldFamily, 23f, FontStyle.Regular);
        }

        private static void AddFont(byte[] FontData, out FontFamily family)
        {
            IntPtr ptr = Marshal.AllocCoTaskMem(FontData.Length);
            Marshal.Copy(FontData, 0, ptr, FontData.Length);
            FontCollection.AddMemoryFont(ptr, FontData.Length);
            Marshal.FreeCoTaskMem(ptr);
            family = FontCollection.Families[FontCollection.Families.Length - 1];
        }

        public static Font GetMainFont() => Galmuri11Bold_23;
        public static Font GetStoryFont() => Galmuri11_9;
        public static Font GetNextButtonFont() => Galmuri11_10;


    }
}
