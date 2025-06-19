// GameWebSocketClient.cs
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Game.Characters;

namespace GameClientLib
{
    #region DTO 정의 (CMD 103)
    public class AllResponse103 { public int status; public Body103 body; }
    public class Body103
    {
        public List<PlayerEntry> players { get; set; }
        public List<bool> monsters { get; set; }
        //public List<ItemEntry> items { get; set; }
    }
    public class PlayerEntry
    {
        public string uid;
        public int characterId;
        public string characterName;
        public int characterLevel;
        public int characterExp;
        public int characterMoney;
        public int characterMapId;
        public Location characterLocation;
        public int characterHp;
        public int characterAttack;
    }

    public class MonsterEntry
    {
        public string mid;
        public string monsterId;
        public string monsterName;
        public int monsterLevel;
        public int monsterCoinValue;
        public int monsterMapId;
        public Location monsterLocation;
        public int monsterHp;
        public int monsterAttackAbility;
        public int monsterDefenseAbility;
        public int monsterExperience;
    }

    public class ItemEntry
    {
        public string iid;
        public double x;
        public double y;
    }

    public class Location { public int x; public int y; }

    public class CombatResponse
    {
        public int status { get; set; }
        public int cmd { get; set; }
        public int my_damage { get; set; }
        public int target_damage { get; set; }
        public string from { get; set; }
        public string message { get; set; }
        public string uid1 { get; set; }
        public string uid2 { get; set; }
    }
    #endregion

    public class GameWebSocketClient : IDisposable
    {
        private ClientWebSocket ws;
        private Uri serverUri = new Uri("ws://14.42.12.49:25565/ws/");

        public async Task ConnectAsync()
        {
            ws = new ClientWebSocket();
            await ws.ConnectAsync(serverUri, CancellationToken.None);
        }

        public async Task<string> CmdConnectAsync()
        {
            var json = await SendAndReceive(new { cmd = 100, uid = "" });
            dynamic o100 = JsonConvert.DeserializeObject(json);
            return o100.uid;
        }

        public async Task CmdMoveAsync(string uid, int mapId, int dx, int dy)
        {
            await SendAndReceive(new { cmd = 101, uid = uid, map_id = mapId, dx = dx, dy = dy });
        }

        public async Task CmdPositionAsync(string uid)
        {
            await SendAndReceive(new { cmd = 102, uid = uid });
        }

        public async Task<AllResponse103> CmdAllAsync(int mapId)
        {
            var json = await SendAndReceive(new { cmd = 103, map_id = mapId });
            return JsonConvert.DeserializeObject<AllResponse103>(json);
        }

        public async Task CmdMonsterInfoAsync(string mid, int mapId)
        {
            await SendAndReceive(new { cmd = 105, mid = mid, map_id = mapId });
        }

        public async Task CmdRemoveAsync(string uid)
        {
            await SendAndReceive(new { cmd = 104, uid = uid });
        }

        public async Task CmdMonsterKillAsync(int mapId, int mIndex)
        {
            await SendAndReceive(new
            {
                cmd = 117,
                map_id = mapId,
                mid = mIndex
            });
        }

        public async Task CmdMonsterRespwanAsync(int mapId, int mIndex)
        {
            await SendAndReceive(new
            {
                cmd = 118,
                map_id = mapId,
                mid = mIndex
            });
        }

        public async Task CmdSendCharacterAsync(Character character)
        {
            var message = new
            {
                cmd = 119,
                uid = character.characterId,
                characterName = character.characterName,
                characterLevel = character.characterLevel,
                characterExp = character.characterExp,
                characterMoney = character.characterMoney,
                characterMapId = character.characterMapId,
                characterLocation = new
                {
                    x = character.characterLocation.x,
                    y = character.characterLocation.y
                },
                characterHp = character.characterHp,
                characterAttack = character.characterAttack
            };

            await SendAndReceive(message);
        }

        public async Task CmdPvPRequest(string uid1, string uid2)
        {
            await SendOnly(new { cmd = 106, uid1, uid2 });
        }

        public async Task CmdPvPAccept(string uid1, string uid2)
        {
            await SendAndReceive(new { cmd = 115, uid1, uid2 });
        }

        public async Task CmdPvPAttack(int damage, int modifier, string targetUid)
        {
            await SendAndReceive(new { cmd = 108, damage, modifier, target_uid = targetUid });
        }

        public async Task CmdPvPDefense(int modifier, string targetUid)
        {
            await SendAndReceive(new { cmd = 109, modifier, target_uid = targetUid });
        }

        public async Task<string> WaitForCmd(int expectedCmd)
        {
            while (true)
            {
                var msg = await ReceiveOnce();
                var res = JsonConvert.DeserializeObject<CombatResponse>(msg);
                if (res.cmd == expectedCmd) return msg;
            }
        }

        public async Task WaitForStatuses(params int[] statuses)
        {
            while (true)
            {
                var msg = await ReceiveOnce();
                var res = JsonConvert.DeserializeObject<CombatResponse>(msg);
                foreach (var s in statuses)
                    if (res.status == s)
                        return;
            }
        }
        public async Task SendOnly(object message)
        {
            var json = JsonConvert.SerializeObject(message);
            var bytes = Encoding.UTF8.GetBytes(json);
            await ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        private async Task<string> SendAndReceive(object message)
        {
            var json = JsonConvert.SerializeObject(message);
            var bytes = Encoding.UTF8.GetBytes(json);
            await ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);

            var buffer = new ArraySegment<byte>(new byte[8192]);
            var result = await ws.ReceiveAsync(buffer, CancellationToken.None);
            return Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
        }

        public async Task<string> ReceiveOnce()
        {
            var buffer = new ArraySegment<byte>(new byte[8192]);
            var res = await ws.ReceiveAsync(buffer, CancellationToken.None);
            return Encoding.UTF8.GetString(buffer.Array, 0, res.Count);
        }


        public void Dispose()
        {
            if (ws != null)
            {
                ws.Dispose();
                ws = null;
            }
        }
    }
}