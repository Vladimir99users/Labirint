using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner3D : MonoBehaviour
{
    [Header("Высота и длина поля")]
    [SerializeField] private int Width;
    [SerializeField] private int Height;


    [Header("Настройка поля")]
    [SerializeField] private Cell CellPrefabs;
    [SerializeField] public Vector3 CellSize = new Vector3(1,1,0);

    [Header("Объект для поиска пути")]
    [SerializeField] private HintRenderer _hintRenderer;




    public Maze maze;
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
        maze = generator.GenerateMaze();

        for (int x = 0; x < maze.Cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.Cells.GetLength(1); y++)
            {
                Cell c = Instantiate(CellPrefabs, new Vector3(x * CellSize.x, y * CellSize.y, y * CellSize.z), Quaternion.identity,transform);

                c.WallLeft.SetActive(maze.Cells[x, y].WallLeft);
                c.WallBottom.SetActive(maze.Cells[x, y].WallBottom);
                c.FinishedCollider.enabled =  maze.Cells[x,y].IsFinished;

                _cellsGameBoard.Add(c);
            }
        }

        _hintRenderer.DrawPath();
    }
    
}
