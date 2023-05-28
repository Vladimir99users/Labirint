using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private int Width;
    [SerializeField] private int Height;
    
    public GameObject CellPrefabs;

    private Maze _maze;
    private MaseGenerator generator;

    private List<Cell> _cellsGameBoard;

    private void Start()
    {
        _cellsGameBoard = new List<Cell>();
        generator = new MaseGenerator();
        generator.Initialize(Width,Height);
        GenerateMaze();
    }


    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            foreach (var item in _cellsGameBoard)
            {
                Destroy(item.gameObject);
            }

            _cellsGameBoard.Clear();
            GenerateMaze();
        }
    }


    private void GenerateMaze()
    {
        _maze = generator.GenerateMaze();

        for (int x = 0; x < _maze.Cells.GetLength(0); x++)
        {
            for (int y = 0; y < _maze.Cells.GetLength(1); y++)
            {
                Cell cell = Instantiate(CellPrefabs, new Vector2(x,y), Quaternion.identity, transform).GetComponent<Cell>();

                cell.WallLeft.SetActive(_maze.Cells[x,y].WallLeft);
                cell.WallBottom.SetActive(_maze.Cells[x,y].WallBottom);

                _cellsGameBoard.Add(cell);

            }
        }
    }
    
}
