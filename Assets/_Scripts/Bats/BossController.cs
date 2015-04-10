using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    private bool _isAlive = true;
    private bool isStartFire = false;
    public Image bloodImage;
    private Animator animator;
    public GameObject dan;
    public Transform danPos;
    public GameObject coin;
    public Transform healthPos;

    public float speed;

    private float totalHealth = 40;
    private float _health;
    private bool isMoving = true;

    public AudioClip TiengDan;
    public AudioClip TiengChet;
    public AudioClip TiengItem;
    public AudioClip TiengHit;

    public float Health
    {
        get { return _health; }
        set
        {
            _health = value;

            if (_health <= 0)
                Chet();
            else bloodImage.fillAmount = _health / totalHealth;

        }
    }

    void Awake()
    {
        Transform ui = GameObject.FindObjectOfType<Canvas>().gameObject.transform;
        bloodImage = (Image)Instantiate(bloodImage);
        bloodImage.transform.SetParent(ui);
    }


    void Start()
    {
        animator = this.GetComponent<Animator>();
        StartCoroutine(Fire());
        Health = totalHealth;
        audio.loop = false;
       
    }

    void Update()
    {
        audio.volume = Config.isSound_On ? 1 : 0;
        if (_isAlive && isMoving)
        {
            bloodImage.transform.position = Camera.main.WorldToScreenPoint(healthPos.position);
            if (transform.position.x < 2.6f)
                isStartFire = true;
            if (isStartFire == false)
                transform.Translate(-Vector3.right * speed * Time.deltaTime);
        }
    }

    IEnumerator Fire()
    {
        while (true)
        {
            if (_isAlive && isStartFire)
            {
                audio.clip = TiengDan;
                audio.Play();
                animator.SetTrigger("TANCONG");
            }
            yield return new WaitForSeconds(2f);
        }
    }

    public void BanDan()
    {
        Instantiate(dan, danPos.position, Quaternion.identity);
    }


    void OnMouseDown()
    {
        audio.clip = TiengHit;
        audio.Play();
        Health--;
    }
    void Chet()
    {
        _isAlive = false;
        Destroy(bloodImage);
        collider2D.enabled = false;
        animator.SetTrigger("CHET");
        audio.clip = TiengChet;
        audio.Play();
        StartCoroutine(SinhXu(5));
        Destroy(this.gameObject, 1.7f);
    }

    IEnumerator SinhXu(int soLuong)
    {
        for (int i = 0; i < soLuong; i++)
        {
            Vector3 p = transform.position;
            Instantiate(coin, p, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }

    void OnTriggerEnter2D(Collider2D item)
    {
        print("BOSS");
        if (item.tag == "SET" || item.tag == "BOM" || item.tag == "ANHSANG")
        {
            audio.clip = TiengItem;
            audio.Play();
            Damage(item.tag);
        }
    }

    IEnumerator Wait(float time)
    {
        animator.speed = 0;
        isMoving = false;
        yield return new WaitForSeconds(time);
        isMoving = true;
        animator.speed = 1;
    }

    public void Damage(string itemName)
    {
        if (itemName == "DONGHO")
        {
            float time = 0;
            switch (PlayerController.Instance.GetLevelItem(itemName))
            {
                case 0:
                    time = 1;
                    break;
                case 1:
                    time = 1.5f;
                    break;
                case 2:
                    time = 2;
                    break;
                default:
                    break;
            }
            StartCoroutine(Wait(time));
        }
        else
        {
            switch (PlayerController.Instance.GetLevelItem(itemName))
            {
                case 0:
                    Health = Health - totalHealth * 0.25f;
                    break;
                case 1:
                    Health = Health - totalHealth * 0.5f;
                    break;
                case 2:
                    Health = Health - totalHealth * 0.75f;
                    break;
                default:
                    break;
            }
        }
    }
}
