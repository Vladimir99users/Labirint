using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class HintRenderer : MonoBehaviour
{
    [SerializeField] private MazeSpawner3D _mazeSpawner;
    [SerializeField] private LineRenderer componentLineRenderer;

    public void DrawPath()
    {
        Maze maze = _mazeSpawner.maze;
        int x = maze.FinishPosition.x;
        int y = maze.FinishPosition.y;
        List<Vector3> positions = new List<Vector3>();

        while ((x != 0 || y != 0) && positions.Count < 10000)
        {
            positions.Add(new Vector3(x * _mazeSpawner.CellSize.x, y * _mazeSpawner.CellSize.y, y * _mazeSpawner.CellSize.z));

            MazeGeneratorCell currentCell = maze.Cells[x, y];

            if (x > 0 &&
                !currentCell.WallLeft &&
                maze.Cells[x - 1, y].DistanceFromStart < currentCell.DistanceFromStart)
            {
                x--;
            }
            else if (y > 0 &&
                !currentCell.WallBottom &&
                maze.Cells[x, y - 1].DistanceFromStart < currentCell.DistanceFromStart)
            {
                y--;
            }
            else if (x < maze.Cells.GetLength(0) - 1 &&
                !maze.Cells[x + 1, y].WallLeft &&
                maze.Cells[x + 1, y].DistanceFromStart < currentCell.DistanceFromStart)
            {
                x++;
            }
            else if (y < maze.Cells.GetLength(1) - 1 &&
                !maze.Cells[x, y + 1].WallBottom &&
                maze.Cells[x, y + 1].DistanceFromStart < currentCell.DistanceFromStart)
            {
                y++;
            }
        }

        positions.Add(Vector3.zero);
        componentLineRenderer.positionCount = positions.Count;
        componentLineRenderer.SetPositions(positions.ToArray());
    }
}


