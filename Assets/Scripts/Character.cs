using UnityEngine;

[System.Serializable]
public class Character
{
    public int id;
    public string name;
    public Sprite sprite;
    public Color color;
    public string type; //"player" or "computer"
    public GameStat gameStat = new GameStat();

    public Character(int id, string name, Sprite sprite, Color color)
    {
        this.id = id;
        this.name = name;
        this.sprite = sprite;
        this.color = color;
    }

}

[System.Serializable]
public class GameStat
{
    public int score;
    public int bubbleCounter;
    public int footprintsCounter;
    public int clawsCounter;

    public GameStat()
    {
        this.score = 0;
        this.bubbleCounter = 0;
        this.footprintsCounter = 0;
        this.clawsCounter = 0;
    }

    public GameStat(int score, int bubbleCounter, int footprintsCounter, int clawsCounter)
    {
        this.score = score;
        this.bubbleCounter = bubbleCounter;
        this.footprintsCounter = footprintsCounter;
        this.clawsCounter = clawsCounter;
    }

    public void AddDiceFace(int faceIndex)
    {
        if (faceIndex == 0) bubbleCounter++;
        if (faceIndex == 1) footprintsCounter++;
        if (faceIndex == 2) clawsCounter++;
    }
}