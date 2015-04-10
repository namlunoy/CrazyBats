using UnityEngine;
using UnityEngine.UI;
using System.IO;
#if UNITY_METRO
#else
using System.Runtime.Serialization.Formatters.Binary;
#endif
using System;

/*
 * Quản lý các thức liên quan đến player
 * Sử dụng sigleton patten
 * */
public class PlayerController : MonoBehaviour
{


    private static PlayerController _instance;
    public static PlayerController Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PlayerController();
            return _instance;
        }

    }

    #region Các thông tin cần lưu trữ
    public int SoBoom
    {
        get { return GetSoLuongItem("BOM"); }
        set { SetSoLuongItem("BOM", value); }
    }
    public int SoSet
    {
        get { return GetSoLuongItem("SET"); }
        set { SetSoLuongItem("SET", value); }
    }
    public int SoDongHoCat
    {
        get { return GetSoLuongItem("DONGHO"); }
        set { SetSoLuongItem("DONGHO", value); }
    }
    public int SoAnhSang
    {
        get { return GetSoLuongItem("ANHSANG"); }
        set { SetSoLuongItem("ANHSANG", value); }
    }
    public int HightScore { get { return PlayerPrefs.GetInt("SCORE"); ; } set { PlayerPrefs.SetInt("SCORE", value); } }
    public int TotalCoin { get { return PlayerPrefs.GetInt("COIN"); ; } set { PlayerPrefs.SetInt("COIN", value); } }
    public string UserName { get { return PlayerPrefs.GetString("NAME"); ; } set { PlayerPrefs.SetString("NAME", value); } }

    public int SetLevel
    {
        get { return PlayerPrefs.GetInt("SET_LEVEL"); }
        set { PlayerPrefs.SetInt("SET_LEVEL", value); }
    }

    public int BomLevel
    {
        get { return PlayerPrefs.GetInt("BOM_LEVEL"); }
        set { PlayerPrefs.SetInt("BOM_LEVEL", value); }
    }

    public int DongHoLevel
    {
        get { return PlayerPrefs.GetInt("DONGHO_LEVEL"); }
        set { PlayerPrefs.SetInt("DONGHO_LEVEL", value); }
    }

    public int AnhSangLevel
    {
        get { return PlayerPrefs.GetInt("ANHSANG_LEVEL"); }
        set { PlayerPrefs.SetInt("ANHSANG_LEVEL", value); }
    }

    #endregion

    #region Các thông tin hiển thị: Score, Coin
    [SerializeField]
    private Text txtCoin;
    private int _currentCoin = 0;
    public int CurrentCoin
    {
        get { return _currentCoin; }
        set { _currentCoin = value; if (txtCoin != null) { txtCoin.text = "x" + _currentCoin.ToString(); } }
    }
    [SerializeField]
    private Text txtScore;
    private int _currentScore = 0;
    public int CurrentScore
    {
        get { return _currentScore; }
        set { _currentScore = value; if (txtScore != null) { txtScore.text = "x" + _currentScore.ToString(); } }
    }
    #endregion


    #region Biến quản lý dòng đời
    private bool _isAlive;
    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            if (_isAlive == false)
            {
                GameController controller = ((GameObject)GameObject.FindGameObjectWithTag("GameController")).GetComponent<GameController>();
                print("Save...");
                Save();
                controller.GameOver();
            }

        }
    }
    #endregion

    //private string filePath { get { return Application.persistentDataPath + "/data.dat"; } }

    void Awake()
    {

        //Nếu là lần đầu tiên tạo
        if (_instance == null)
        {
            Load();
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        //Nếu ko phải lần đầu tiên, thì xóa hết nếu ko phải cái đầu tiên
        //Nếu không, tuy instance nó vẫn chỉ vào cái cũ, nhưng cái mới nó vẫn còn.
        else if (_instance != this)
        {
            Reset();
            print("Chạy lần 2");
            Destroy(gameObject);
        }


    }

    void Start()
    {

    }



    public void UpScore()
    { CurrentScore++; }
    public void UpCoin()
    {
        CurrentCoin++;
        TotalCoin++;
    }
    public void UseItem(string itemName)
    {
        switch (itemName)
        {
            case "SET":
                SoSet--;
                break;
            case "BOM":
                SoBoom--;
                break;
            case "DONGHO":
                SoDongHoCat--;
                break;
            case "ANHSANG":
                SoAnhSang--;
                break;
        }
    }


    //Khởi tạo các giá trị cho "Instance"
    void Reset()
    {
        _instance.txtCoin = this.txtCoin;
        _instance.txtScore = this.txtScore;
        _instance.CurrentCoin = 0;
        _instance.CurrentScore = 0;
        _instance.IsAlive = true;
    }

    public void Load()
    {
        IsAlive = true;
    }


    public void Save()
    {
        int coin = CurrentCoin + PlayerPrefs.GetInt("COIN", 0);
        PlayerPrefs.SetInt("COIN", coin);

        if (CurrentScore > PlayerPrefs.GetInt("SCORE", 0))
        {
            PlayerPrefs.SetInt("SCORE", CurrentScore);
            MyLeaderBoard b = new MyLeaderBoard();
#if UNITY_METRO
#else
            if (UserName.Length > 0)
                b.SaveScore(UserName, CurrentScore);
#endif
        }


    }

    public int GetLevelItem(string itemName)
    {
        switch (itemName)
        {
            case "SET":
                return SetLevel;
            case "BOM":
                return BomLevel;
            case "DONGHO":
                return DongHoLevel;
            case "ANHSANG":
                return AnhSangLevel;
        }
        print("Lỗi: PlayerController.GetLevelItem");
        return 0;
    }
    public void SetLevelItem(string itemName, int value)
    {
        switch (itemName)
        {
            case "SET":
                PlayerPrefs.SetInt("SET_LEVEL", value);
                break;
            case "BOM":
                PlayerPrefs.SetInt("BOM_LEVEL", value);
                break;
            case "DONGHO":
                PlayerPrefs.SetInt("DONGHO_LEVEL", value);
                break;
            case "ANHSANG":
                PlayerPrefs.SetInt("ANHSANG_LEVEL", value);
                break;
        }
    }
    public int GetSoLuongItem(string itemName)
    {
        switch (itemName)
        {
            case "SET":
                return PlayerPrefs.GetInt("SET_SOLUONG");
            case "BOM":
                return PlayerPrefs.GetInt("BOM_SOLUONG");
            case "DONGHO":
                return PlayerPrefs.GetInt("DONGHO_SOLUONG");
            case "ANHSANG":
                return PlayerPrefs.GetInt("ANHSANG_SOLUONG");
        }
        return 0;
    }
    public void SetSoLuongItem(string itemName, int value)
    {
        switch (itemName)
        {
            case "SET":
                PlayerPrefs.SetInt("SET_SOLUONG", value);
                break;
            case "BOM":
                PlayerPrefs.SetInt("BOM_SOLUONG", value);
                break;
            case "DONGHO":
                PlayerPrefs.SetInt("DONGHO_SOLUONG", value);
                break;
            case "ANHSANG":
                PlayerPrefs.SetInt("ANHSANG_SOLUONG", value);
                break;
        }
    }



    // public GameObject set;
    public void Test()
    {
        TotalCoin = 100;

        SoAnhSang = 10;
        SoBoom = 10;
        SoDongHoCat = 10;
        SoSet = 10;

        SetLevel = 2;
        BomLevel = 2;
        AnhSangLevel = 2;
        DongHoLevel = 2;

    }


}
