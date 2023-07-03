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
    private Stairs _stairs;

    private bool _isGameOver = false;
    private bool _isGameClear = false;

    private Text _gameOver;
    private Text _gameClear;

    // Start is called before the first frame update
    void Start()
    {
        _stairs = GameObject.FindObjectOfType<Stairs>();
        _player = GameObject.Find("Player");
        _camera = GameObject.Find("Main Camera");
        _counter = GameObject.Find("Counter").GetComponent<Text>();
        _gameOver = GameObject.Find("GameOver").GetComponent<Text>();
        _gameClear = GameObject.Find("GameClear").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameClear && !_gameClear.IsActive())
        {
            _gameClear.enabled = true;
        }
        if (_isGameOver && !_gameOver.IsActive())
        {
            _gameOver.enabled = true;
        }

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
            if(_isGameOver || _isGameClear) return;
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
            _isGameClear = _count == 20;
            if(_isGameClear) return;
            _isGameOver = !_stairs.Exists((int)_player.transform.position.x, _count - 1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(_isGameOver || _isGameClear) return;
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

            _isGameClear = _count == 20;
            if(_isGameClear) return;
            _isGameOver = !_stairs.Exists((int)_player.transform.position.x, _count - 1);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Reset!");
            _isGameOver = false;
            _isGameClear = false;
            _gameOver.enabled = false;
            _gameClear.enabled = false;
            _player.transform.position = new Vector3(4, -1, 0);
            _camera.transform.position = new Vector3(4, 2, -10);
            _count = 0;
            _counter.text = _count.ToString();
            
            _stairs.Reset();
        }
    }
}
