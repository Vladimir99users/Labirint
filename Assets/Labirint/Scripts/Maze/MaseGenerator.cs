using System;
using System.Collections.Generic;
using UnityEngine;
public class MaseGenerator
{
    public int Width;
    public int Height;

    public MazeGeneratorCell FinishedCell;

    public void Initialize(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public Maze GenerateMaze()
    {
        Maze maze = new Maze();
        MazeGeneratorCell[,] cells = new MazeGeneratorCell[Width,Height];

        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                cells[x,y] = new MazeGeneratorCell{X = x,Y = y};
            }
        }

        for (int x = 0; x < cells.GetLength(0); x++)
        {
            cells[x,Height -1].WallLeft = false;
        }

        for (int y = 0; y < cells.GetLength(1); y++)
        {
            cells[Width - 1,y].WallBottom = false;
        }

        RemoveWallWithBackTracker(cells);

        maze.Cells = cells;

        MazeGeneratorCell finishedCell = new MazeGeneratorCell();
        maze.FinishPosition = PlaceMazeExit(cells, ref finishedCell);
        maze.FinishedCell = finishedCell;

        return maze;
    }



    private void RemoveWallWithBackTracker(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell current = maze[0, 0];
        current.IsVisited = true;
        current.DistanceFromStart = 0;

        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
        do
        {
            List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();

            int x = current.X;
            int y = current.Y;

            if (x > 0 && !maze[x - 1, y].IsVisited) unvisitedNeighbours.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].IsVisited) unvisitedNeighbours.Add(maze[x, y - 1]);
            if (x < Width - 2 && !maze[x + 1, y].IsVisited) unvisitedNeighbours.Add(maze[x + 1, y]);
            if (y < Height - 2 && !maze[x, y + 1].IsVisited) unvisitedNeighbours.Add(maze[x, y + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeGeneratorCell chosen = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(current, chosen);

                chosen.IsVisited = true;
                stack.Push(chosen);
                chosen.DistanceFromStart = current.DistanceFromStart + 1;
                current = chosen;
            }
            else
            {
                current = stack.Pop();
            }
        } while (stack.Count > 0);
    }

    private void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
    {
        if (a.X == b.X)
        {
            if (a.Y > b.Y) a.WallBottom = false;
            else b.WallBottom = false;
        }
        else
        {
            if (a.X > b.X) a.WallLeft = false;
            else b.WallLeft = false;
        }
    }

    private UnityEngine.Vector2Int PlaceMazeExit(MazeGeneratorCell[,] maze, ref MazeGeneratorCell finishedCell)
    {
        MazeGeneratorCell furthest = maze[0, 0];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            if (maze[x, Height - 2].DistanceFromStart > furthest.DistanceFromStart) 
            {
                furthest = maze[x, Height - 2];
                furthest.IsFinished = false;
            }
            if (maze[x, 0].DistanceFromStart > furthest.DistanceFromStart) 
            {
                furthest = maze[x, 0];
                furthest.IsFinished = false;
            }
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            if (maze[Width - 2, y].DistanceFromStart > furthest.DistanceFromStart) 
            {
                furthest = maze[Width - 2, y];
                furthest.IsFinished = false;
            }
            if (maze[0, y].DistanceFromStart > furthest.DistanceFromStart) 
            {
                furthest = maze[0, y];
                furthest.IsFinished = false;
            }
        }

        if (furthest.X == 0) furthest.WallLeft = false;
        else if (furthest.Y == 0) furthest.WallBottom = false;
        else if (furthest.X == Width - 2) maze[furthest.X + 1, furthest.Y].WallLeft = false;
        else if (furthest.Y == Height - 2) maze[furthest.X, furthest.Y + 1].WallBottom = false;

        Debug.Log(new UnityEngine.Vector2Int(furthest.X, furthest.Y));
        furthest.IsFinished = true;
        finishedCell = furthest;

        return new UnityEngine.Vector2Int(furthest.X, furthest.Y);
    }
}
