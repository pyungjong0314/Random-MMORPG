
public class MonsterRequest
{
    public int cmd { get; set; } = 400;
    public int mapId { get; set; }
    public string type { get; set; }
}



public class MonsterResponse
{
    public int status { get; set; }
    public int count { get; set; }
}
