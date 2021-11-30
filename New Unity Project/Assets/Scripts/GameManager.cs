using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Dictionary<GameObject, Boom> Contain;

    private int _countDestroy;

    public int CountDestroy
    {
        get
        {
            return _countDestroy;
        }
        set
        {
            _countDestroy = value;
            _slifer.maxValue = Contain.Count;
            _slifer.value = _countDestroy;
            _percent = _countDestroy / (float)Contain.Count * 100;
            _text.text = _percent.ToString("0") + "%";
            _timer = 0;
        }
    }

    public Slider _slifer;

    public Text _text;

    public Image _imageWin, _imageLouse;

    private float _timer;

    public int _countBoom = 3;

    private float _percent;

    private void Awake()
    {
        Instance = this;
        Contain = new Dictionary<GameObject, Boom>();
    }

    private void Start()
    {
        _countDestroy = Contain.Count;
        _slifer.maxValue = Contain.Count;
        _slifer.value = _countDestroy;
        _percent = _countDestroy / (float)Contain.Count * 100;
        _text.text = _percent.ToString("0") + "%";
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > 5 && _countBoom <= 0)
        {
            if (_percent <= 30)
                _imageWin.gameObject.SetActive(true);
            else
                _imageLouse.gameObject.SetActive(true);
        }
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
