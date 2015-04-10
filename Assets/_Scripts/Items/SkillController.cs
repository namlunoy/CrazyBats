using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillController : MonoBehaviour
{
    public AudioClip soundClick;
    public Text txtHighScore1;
    public Text txtHighScore2;

    public void clickBack()
    {
        audio.volume = Config.isSound_On ? 1 : 0;
        audio.clip = soundClick;
        audio.Play();
        Application.LoadLevel(0);
    }


    //Giá
    private int[] bomCoinUpgrate = new int[4];
    private int[] setCoinUpgrate = new int[4];
    private int[] dongHoCoinUpgrate = new int[4];
    private int[] anhSangCoinUpgrate = new int[4];

    public Text txtGiaUpSET;
    public Text txtGiaUpBOM;
    public Text txtGiaUpANHSANG;
    public Text txtGiaUpDONGHO;

    public Image[] bom;
    public Image[] sett;
    public Image[] dongho;
    public Image[] anhsang;

    public Image img_bom;
    public Image img_sett;
    public Image img_dongho;
    public Image img_anhsang;

    public Button bt_bom;
    public Button bt_set;
    public Button bt_dongho;
    public Button bt_anhsang;

    public Text coin;
    private PlayerController player;
    public Sprite ok;
    public Sprite no;


    public Text sl_bom;
    public Text sl_set;
    public Text sl_dongho;
    public Text sl_anhsang;

    //Mua số lượng
    private int[] giaBom = new int[3];
    private int[] giaSet = new int[3];
    private int[] giaDongHo = new int[3];
    private int[] giaAnhSang = new int[3];

    public Text txtGiaBom;
    public Text txtGiaSet;
    public Text txtGiaDongHo;
    public Text txtGiaAnhSang;

    public Button btMuaBom;
    public Button btMuaSet;
    public Button btMuaDongHo;
    public Button btMuaAnhSang;

    private Color32 MauXam = new Color32(255, 255, 255, 50);
    private Color32 MauSang = new Color32(255, 255, 255, 255);

    public Button bt_Trai;
    public Button bt_Phai;
    public RectTransform UpdateGratePanel;
    public RectTransform ReloadPanel;
    public Transform BenTrai;
    public Transform BenPhai;
    public Transform Center;

    public AudioClip click;
    public AudioClip clickUpgarate;
    public AudioClip clickAdd;


    void Start()
    {
#if UNITY_METRO
        txtHighScore2.text = PlayerController.Instance.HightScore.ToString();
#else
        txtHighScore1.gameObject.SetActive(false);
        txtHighScore2.gameObject.SetActive(false);
#endif

        //------------- Giá upgrate ---------------
        bomCoinUpgrate[1] =  200;
        bomCoinUpgrate[2] = 400;

        setCoinUpgrate[1] = 200;
        setCoinUpgrate[2] = 400;

        dongHoCoinUpgrate[1] = 200;
        dongHoCoinUpgrate[2] = 400;

        anhSangCoinUpgrate[1] = 200;
        anhSangCoinUpgrate[2] = 400;
        //-------------------------------------------

        //------------- Giá mua số lượng ---------------
        giaBom[0] = 10;
        giaBom[1] = 15;
        giaBom[2] = 20;

        giaSet[0] = 10;
        giaSet[1] = 15;
        giaSet[2] = 20;

        giaDongHo[0] = 10;
        giaDongHo[1] = 15;
        giaDongHo[2] = 20;

        giaAnhSang[0] = 10;
        giaAnhSang[1] = 15;
        giaAnhSang[2] = 20;

        //------------- Giá mua số lượng ---------------

        //Cập nhật các skill hiện tại
        player = PlayerController.Instance;
        coin.text = player.TotalCoin.ToString();


        UpdateTrangThai();
    }


    public void UpdateTrangThai()
    {
        //Update trạng thái các nút và level,tiền
        coin.text = player.TotalCoin.ToString();

        //update level hiện tại
        for (int i = 0; i < 3; i++)
        {
            bom[i].color = MauXam;
            sett[i].color = MauXam;
            dongho[i].color = MauXam;
            anhsang[i].color = MauXam;
        }

        bom[player.BomLevel].color = MauSang;
        sett[player.SetLevel].color = MauSang;
        print("level dong ho: " + player.DongHoLevel);
        dongho[player.DongHoLevel].color = MauSang;
        anhsang[player.AnhSangLevel].color = MauSang;

        txtGiaUpANHSANG.text = anhSangCoinUpgrate[player.AnhSangLevel + 1].ToString();
        txtGiaUpBOM.text = bomCoinUpgrate[player.BomLevel + 1].ToString();
        txtGiaUpSET.text = setCoinUpgrate[player.SetLevel + 1].ToString();
        txtGiaUpDONGHO.text = dongHoCoinUpgrate[player.DongHoLevel + 1].ToString();


        //update level bên kia
        img_bom.sprite = bom[player.BomLevel].sprite;
        img_sett.sprite = sett[player.SetLevel].sprite;
        img_anhsang.sprite = anhsang[player.AnhSangLevel].sprite;
        img_dongho.sprite = dongho[player.DongHoLevel].sprite;

        //update số lượng
        sl_anhsang.text = player.SoAnhSang.ToString();
        sl_bom.text = player.SoBoom.ToString();
        sl_set.text = player.SoSet.ToString();
        sl_dongho.text = player.SoDongHoCat.ToString();

        //Update giá
        txtGiaBom.text = giaBom[player.BomLevel].ToString();
        txtGiaSet.text = giaSet[player.SetLevel].ToString();
        txtGiaAnhSang.text = giaAnhSang[player.AnhSangLevel].ToString();
        txtGiaDongHo.text = giaDongHo[player.DongHoLevel].ToString();

        //update button mua
        if (player.TotalCoin >= giaBom[player.BomLevel])
        {
            btMuaBom.enabled = true;
            btMuaBom.image.sprite = ok;
        }
        else
        {
            btMuaBom.enabled = false;
            btMuaBom.image.sprite = no;
        }

        if (player.TotalCoin >= giaSet[player.SetLevel])
        {
            btMuaSet.enabled = true;
            btMuaSet.image.sprite = ok;
        }
        else
        {
            btMuaSet.enabled = false;
            btMuaSet.image.sprite = no;
        }

        if (player.TotalCoin >=giaDongHo[player.DongHoLevel])
        {
            btMuaDongHo.enabled = true;
            btMuaDongHo.image.sprite = ok;
        }
        else
        {
            btMuaDongHo.enabled = false;
            btMuaDongHo.image.sprite = no;
        }

        if (player.TotalCoin >= giaAnhSang[player.AnhSangLevel])
        {
            btMuaAnhSang.enabled = true;
            btMuaAnhSang.image.sprite = ok;
        }
        else
        {
            btMuaAnhSang.enabled = false;
            btMuaAnhSang.image.sprite = no;
        }

        //Update nút button upgrate
        //BOM
        if (player.BomLevel < 2 && player.TotalCoin >= bomCoinUpgrate[player.BomLevel + 1])
        {
            bt_bom.enabled = true;
            bt_bom.image.sprite = ok;
        }
        else
        {
            bt_bom.enabled = false;
            bt_bom.image.sprite = no;
        }

        //SET
        if (player.SetLevel < 2 && player.TotalCoin >= setCoinUpgrate[player.SetLevel + 1])
        {
            bt_set.enabled = true;
            bt_set.image.sprite = ok;
        }
        else
        {
            bt_set.enabled = false;
            bt_set.image.sprite = no;
        }

        //Dongho
        if (player.DongHoLevel < 2 && player.TotalCoin >= dongHoCoinUpgrate[player.DongHoLevel + 1])
        {
            bt_dongho.enabled = true;
            bt_dongho.image.sprite = ok;
        }
        else
        {
            bt_dongho.enabled = false;
            bt_dongho.image.sprite = no;
        }


        //Anh sagn
        if (player.AnhSangLevel < 2 && player.TotalCoin >= bomCoinUpgrate[player.AnhSangLevel + 1])
        {
            bt_anhsang.enabled = true;
            bt_anhsang.image.sprite = ok;
        }
        else
        {
            bt_anhsang.enabled = false;
            bt_anhsang.image.sprite = no;
        }
    }
    public void Click_Add_Set()
    {
        audio.clip = clickUpgarate;
        audio.Play();
        player.SetLevel++;
        player.TotalCoin -= setCoinUpgrate[player.SetLevel];
        UpdateTrangThai();
    }
    public void Click_Add_DongHo()
    {
        audio.clip = clickUpgarate;
        audio.Play();
        int a = player.DongHoLevel;
        player.DongHoLevel = a + 1;
        player.TotalCoin -= setCoinUpgrate[player.DongHoLevel];
        UpdateTrangThai();
        print("Đã chạy vào!");
    }
    public void Click_Add_Bom()
    {
        audio.clip = clickUpgarate;
        audio.Play();
        player.BomLevel++;
        player.TotalCoin -= setCoinUpgrate[player.BomLevel];
        UpdateTrangThai();
    }
    public void Click_Add_AnhSang()
    {
        audio.clip = clickUpgarate;
        audio.Play();
        player.AnhSangLevel++;
        player.TotalCoin -= setCoinUpgrate[player.AnhSangLevel];
        UpdateTrangThai();
    }


    public void Click_Them(string name)
    {
        audio.clip = clickAdd;
        audio.Play();
        switch (name)
        {
            case "SET":
                player.SoSet++;
                player.TotalCoin -= giaSet[player.SetLevel];
                break;

            case "BOM":
                player.SoBoom++;
                player.TotalCoin -= giaBom[player.BomLevel];
                break;

            case "DONGHO":
                player.SoDongHoCat++;
                player.TotalCoin -= giaDongHo[player.DongHoLevel];
                break;

            case "ANHSANG":
                player.SoAnhSang++;
                player.TotalCoin -= giaAnhSang[player.AnhSangLevel];
                break;

            default:
                break;
        }
        UpdateTrangThai();
    }


    public void Click_Button_TraiPhai(string s)
    {
        audio.clip = click;
        audio.Play();
        switch (s)
        {
            case "TRAI":
                iTween.MoveTo(UpdateGratePanel.gameObject, iTween.Hash("position",BenTrai.position));
                iTween.MoveTo(ReloadPanel.gameObject, iTween.Hash("position", Center.position));
                break;
            default:
                   iTween.MoveTo(UpdateGratePanel.gameObject, iTween.Hash("position",Center.position));
                iTween.MoveTo(ReloadPanel.gameObject, iTween.Hash("position", BenPhai.position));
                break;
        }
    }
}
