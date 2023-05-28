using UnityEngine;

public class MazeGeneratorCell
{
    public int X;
    public int Y;

    public bool WallLeft = true;
    public bool WallBottom = true;

    public bool IsVisited =false;

    public bool IsFinished = false;
    public int DistanceFromStart;
}

public class Maze
{
    public MazeGeneratorCell[,] Cells;
    public Vector2Int FinishPosition;
    public MazeGeneratorCell FinishedCell;
}
