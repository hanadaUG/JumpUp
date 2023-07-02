using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // グリッド
    [SerializeField] private int height = 20, width = 9, header = 2, footer = 4;

    // グリッドを表現する2次元配列の作成
    private Transform[,] _grid;

    // 1マス分のスプライト
    [SerializeField] private Transform emptySprite;


    private void Awake()
    {
        _grid = new Transform[width, height];
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        if (emptySprite)
        {
            for (int y = 0; y < height - header - footer; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Transform clone = Instantiate(emptySprite, new Vector3(x, y, 0), Quaternion.identity);
                    clone.transform.parent = transform;
                }
            }
        }
        else
        {
            Debug.Log("emptySprite not set!");
        }
    }
}