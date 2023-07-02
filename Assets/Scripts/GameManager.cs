using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject _player;
    private GameObject _camera;
    private Text _counter;
    private int _count = 0;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _camera = GameObject.Find("Main Camera");
        _counter = GameObject.Find("Counter").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        int px = (int)_player.transform.position.x;
        int py = (int)_player.transform.position.y;
        int dir = (int)_player.transform.rotation.z; // キャラクターの向き 左 = 0 右 = 180

        int cx = (int)_camera.transform.position.x;
        int cy = (int)_camera.transform.position.y;
        int cz = (int)_camera.transform.position.z;

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (dir == 0)
            {
                px -= 1;
                cx -= 1;
            }
            else
            {
                px += 1;
                cx += 1;
            }
            _player.transform.position = new Vector3(px, py + 1, 0);
            _camera.transform.position = new Vector3(cx, cy + 1, cz);
            _count++;
            _counter.text = _count.ToString();
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (dir == 0)
            {
                px += 1;
                cx += 1;
            }
            else
            {
                px -= 1;
                cx -= 1;
            }
            _player.transform.position = new Vector3(px, py + 1, 0);
            _player.transform.Rotate(0, 0, 180); // キャラクターの向きを変更
            _camera.transform.position = new Vector3(cx, cy + 1, cz);
            _count++;
            _counter.text = _count.ToString();
        }
    }
}
