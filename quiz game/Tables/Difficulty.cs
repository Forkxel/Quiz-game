namespace quiz_game.Tables;

public class Difficulty
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Difficulty(int id, string name)
    {
        Id = id;
        Name = name;
    }
}