using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    // Записываем информацию о каждой ячейке внутри лабиринта
    public class Cell
    {
        public bool _visited = false;
        public bool[] _status = new bool[4];
    }

    [SerializeField] private GameObject _room;
    
    private int _startPosition = 0;
    private Vector2 _size;
    private Vector2 _sizeMin = new Vector2();
    private Vector2 _sizeMax = new Vector2();
    private Vector2 _offset = new Vector2(6,6);

    List<Cell> _board;

    void Start()
    {
        RandomizeSize();
        MazeGenerator();
    }

    void RandomizeSize()
    {
        _sizeMin.x = Random.Range(0, 5);
        _sizeMax.x = Random.Range(6, 10);
        _sizeMin.y = Random.Range(0, 2);
        _sizeMax.y = Random.Range(6, 7);

        _size = new Vector2( _sizeMax.x - _sizeMin.x, _sizeMax.y - _sizeMin.y);
    }

    void GeneratorDungeon()
    {
        for (int i = 0; i < _size.x; i++)
        {
            for (int j = 0; j < _size.y; j++)
            {
                Cell _currentCell = _board[Mathf.FloorToInt(i + j * _size.x)];
                if (_currentCell._visited)
                {
                    var _newRoom = Instantiate(_room, new Vector3(i * _offset.x, 0, -j * _offset.y), Quaternion.identity, transform).GetComponent<RoomBehavior>();
                    _newRoom.UpdateRoom(_currentCell._status);

                    _newRoom.name += " " + i + " " + j;
                }
            }
        }
    }

    // Создаем пустой лабиринт
    void MazeGenerator()
    {
        _board = new List<Cell>();

        for (int i = 0; i < _size.x; i++)
        {
            for (int j = 0; j < _size.y; j++)
            {
                _board.Add(new Cell());
            }
        }

        int _currentCell = _startPosition;

        Stack<int> _path = new Stack<int>();

        int _cnt = 0;  // Точка выхода из цикла

        while (_cnt < 1000)
        {
            _cnt++;
            _board[_currentCell]._visited = true;

            if (_currentCell == _board.Count - 1)
            {
                break;
            }

            List<int> _neighbors = CheckNeighbors(_currentCell);

            if (_neighbors.Count == 0)
            {
                if (_path.Count == 0)
                {
                    break;
                }
                else
                {
                    _currentCell = _path.Pop();
                }
            }
            else
            {
                _path.Push(_currentCell);

                int _newCell = _neighbors[Random.Range(0, _neighbors.Count)];

                if (_newCell > _currentCell)
                {
                    // Идем вниз или вправо
                    if (_newCell - 1 == _currentCell)
                    {
                        _board[_currentCell]._status[2] = true;
                        _currentCell = _newCell;
                        _board[_currentCell]._status[3] = true;
                    }
                    else
                    {
                        _board[_currentCell]._status[1] = true;
                        _currentCell = _newCell;
                        _board[_currentCell]._status[0] = true;
                    }
                }
                else
                {
                    // Идем вверх или влево
                    if (_newCell + 1 == _currentCell)
                    {
                        _board[_currentCell]._status[3] = true;
                        _currentCell = _newCell;
                        _board[_currentCell]._status[2] = true;
                    }
                    else
                    {
                        _board[_currentCell]._status[0] = true;
                        _currentCell = _newCell;
                        _board[_currentCell]._status[1] = true;
                    }
                }
            }
        }
        GeneratorDungeon();
    }

    // Проверяем ближайших соседей
    List<int> CheckNeighbors(int _cell)
    {
        List<int> _neighbors = new List<int>();

        // Проверяем соседей 0 - сверху 1 - снизу 2 - справа 3 - слева
        if (_cell - _size.x >= 0 && !_board[Mathf.FloorToInt(_cell - _size.x)]._visited)
        {
            _neighbors.Add(Mathf.FloorToInt(_cell - _size.x));
        }

        if (_cell + _size.x < _board.Count && !_board[Mathf.FloorToInt(_cell + _size.x)]._visited)
        {
            _neighbors.Add(Mathf.FloorToInt(_cell + _size.x));
        }

        if ((_cell + 1) % _size.x != 0 && !_board[Mathf.FloorToInt(_cell + 1)]._visited)
        {
            _neighbors.Add(Mathf.FloorToInt(_cell + 1));
        }

        if (_cell % _size.x != 0 && !_board[Mathf.FloorToInt(_cell - 1)]._visited)
        {
            _neighbors.Add(Mathf.FloorToInt(_cell - 1));
        }

        return _neighbors;
    }
}