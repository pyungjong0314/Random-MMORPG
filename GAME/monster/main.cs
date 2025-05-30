using MonsterServer; // TcpMonsterServer가 선언된 네임스페이스

namespace MonsterClass
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpMonsterServer server = new TcpMonsterServer();
            server.Start(); // 서버 구동
        }
    }
}
