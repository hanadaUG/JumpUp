using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Stairs : MonoBehaviour
{
    [SerializeField] public int Floor = 20; // 階層設定
    private List<Transform> _stairs = new List<Transform>(); // 階段の配置を記録（ゲームオブジェクトの座標情報をまるごと格納）

    [SerializeField] private Transform stairSprite; // 階段描画に使用するスプライトをGUIから指定

    // Start is called before the first frame update
    void Start()
    {
        CreateStairs();
    }

    private void CreateStairs()
    {
        if (stairSprite)
        {
            // 階段のx座標（初期位置はプレイヤーの位置を原点とする）
            int x = (int)Mathf.Round(GameObject.Find("Player").transform.position.x);
            List<int> lr = new List<int>() { -1, 1 }; // 階段をランダムに左右に配置するため使用
            for (int y = 0; y < Floor; y++)
            {
                x += lr[Random.Range(0, 2)]; // 0, 1がランダムに選ばれ、x座標を-1か+1する

                // 階段のゲームオブジェクトを生成
                Transform clone = Instantiate(stairSprite, new Vector3(x, y, 0), Quaternion.identity);
                clone.SetParent(transform); // Backgroundを親プロジェクトに設定し一つの階層にまとめる
                _stairs.Add(clone); // 履歴として登録
            }
        }
        else
        {
            Debug.Log("Sprite is not set!");
        }
    }

    // 該当座標に階段が存在するか
    public bool Exists(int x, int y)
    {
        return (int)_stairs[y].position.x == x;
    }

    // リセット
    public void Reset()
    {
        _stairs = new List<Transform>(); //履歴削除
        foreach ( Transform child in transform ) // Stairs オブジェクトの配下の子オブジェクトを一掃
        {
            Destroy(child.gameObject);
        }
        CreateStairs(); // 階段再作成
    }
}