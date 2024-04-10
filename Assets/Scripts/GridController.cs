using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private Cell _cellViewPrefab;
    
    [SerializeField] private Transform _cellsRoot;
    [SerializeField] private Transform _plane;

    [SerializeField] private float _rowOffset;
    [SerializeField] private float _columnOffset;
    
    [SerializeField] private int _rows;
    [SerializeField] private int _columns;
    
    [SerializeField] private List<Cell> _cellsCollection;
    
    [Button]
    private void GenerateCells()
    {
        Clear();
        
        var cellBounds = _cellViewPrefab.Bounds;
        var cellHeight = cellBounds.extents.z;
        var cellWidth = cellBounds.extents.x;

        var position = _plane.GetComponent<Renderer>().bounds.min + new Vector3(cellWidth, 0f, cellHeight);
        
        for (int i = 0; i < _columns; i++)
        {
            for (int j = 0; j < _rows; j++)
            {
                var instance = Instantiate(_cellViewPrefab, position, Quaternion.identity, _cellsRoot);
                instance.transform.position += Vector3.right * (cellWidth * 2f + _rowOffset) * j + Vector3.forward * (cellHeight * 2 + _columnOffset) * i;
                _cellsCollection.Add(instance);
            }
        }
    }

    [Button]
    private void Clear()
    {
        foreach (var cell in _cellsCollection)
        {
            DestroyImmediate(cell.gameObject);
        }
        _cellsCollection.Clear();
    }

    private void Awake()
    {
        GenerateCells();
    }
}
