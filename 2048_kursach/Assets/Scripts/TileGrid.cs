using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public TileRow[] rows { get; private set; }
    public TileCell[] cells { get; private set; }

    public int size => cells.Length;
    public int height => rows.Length;
    public int width => size / height;

    private void Awake()
    {
        rows = GetComponentsInChildren<TileRow>();
        cells= GetComponentsInChildren<TileCell>();
    }

    private void Start()
    {
        for (int y = 0; y < rows.Length; y++)
        {
            for (int x = 0; x < rows[y].cells.Length; x++)
            {
                rows[y].cells[x].coordinates = new Vector2Int(x,y);
            }
        }
    }

    public TileCell GetCell(int x,int y) //получаем ячейку с координатами
    {
        if (x >= 0 && x < width && y>=0 && y < height)
        {
            return rows[y].cells[x];
        }
        else
        {
            return null;
        }
        
    }

    public TileCell GetCell (Vector2Int coordinates)//перегрузочный метод для возврата ячейки
    {
        return GetCell(coordinates.x, coordinates.y);
    }


    public TileCell GetAdjacentCell(TileCell cell,Vector2Int direction) //получение соседней ячейки
    {
        Vector2Int coordinates = cell.coordinates;
        coordinates.x += direction.x;
        coordinates.y -= direction.y;
        return GetCell(coordinates);
    }

    public TileCell GetRandomEmptyCell() //искать пустую ячейку
    {
        int index = Random.Range(0, cells.Length);
        int startingIndex = index;

        while (cells[index].occupied) //если занята прибавляем индекс
        {
            index++;
            if (index >= cells.Length)
            {
                index = 0;
            }
            if (index == startingIndex) //если все ячейки прошли и они заняты
            {
                return null;
            }
        }
        return cells[index];
    }
}
