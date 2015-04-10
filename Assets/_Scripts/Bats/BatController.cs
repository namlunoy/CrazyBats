using UnityEngine;
using System.Collections;


/*
 * Lớp này thực hiện việc điều khiển các con dơi.
 * - Dơi bay thẳng: Thực hiện theo move.
 * 
 * Để đồng bộ và dễ quản lý ta chuyển sang tất cả các con dơi đều sử dung iTween để là công cụ di chuyển!
 * Vấn đề quản lý tốc độ của iTween lại chuyển sang quản lý khoảng thời gian của chúng.
 * 
 * Chú ý việc đánh dấu các con dơi đã chết
 * */

public class BatController : MonoBehaviour
{

    public GameObject coin;
    public int type;
    public AudioClip HitSound;
    public AudioClip Sound_BatNo;
    public bool IsNotDead { get { return !_isDead; } }
    private bool _isDead;
    public bool IsDead
    {
        get { return _isDead; }
        set
        {
            _isDead = value;

        }
    }
    private float speed;
    private int health;

    private Vector3[] path = null;
    private Animator _animator = null;
    private Animator Animator
    {
        get
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();
            return _animator;
        }
    }

    void Start()
    {
        if (type == 1)
            health = 3;
        else health = 1;

     
    }

    void Update()
    { audio.volume = Config.isSound_On ? 1 : 0; }

    //Hàn set up này cần được gọi trước khi chạy vào start
    public void SetUp(Vector3[] pPath, float pSpeed, FlyType flytype, float delay)
    {

        this.path = pPath;
        switch (flytype)
        {
            case FlyType.THANG:
                this.speed = pSpeed;
                break;
            default:
                this.speed = pSpeed - 7;
                break;
        }
        IsDead = false;

        iTween.MoveTo(gameObject, iTween.Hash("path", path, "easetype", iTween.EaseType.linear, "speed", speed, "delay", delay));
    }

    #region Xử lý các hành vi của con dơi
    //Quản lý việc giết con dơi ntn
    public void Kill(KillType flytype)
    {
        if (IsNotDead)
        {
            IsDead = true;
            switch (flytype)
            {
                case KillType.BINH_THUONG:
                    Kill_BinhThuong();
                    break;
                case KillType.SET_DANH:
                    Kill_SetDanh();
                    break;
                case KillType.BOM_NO:
                    Kill_BomNo();
                    break;
                case KillType.ANH_SANG:
                    Kill_AnhSang();
                    break;
                default:
                    break;
            }
            //Đánh dấu đã chết

            //tăng điểm cho người chơi
            PlayerController.Instance.UpScore();

            //Sinh ra đồng xu
            if (type == 1)
                StartCoroutine(SinhXu(2));
            else if(Random.Range(0, 5) == 1 )
                 Instantiate(coin, transform.position, Quaternion.identity);
        }
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

    private void Kill_BinhThuong()
    {
        //Đối với các loại dơi sử dụng iTween
        iTween.Stop(gameObject);

        //Chạy animation
        Animator.SetBool("bat_die", true);

        //Cho nó chịu tác dụng của trọng lực
        rigidbody2D.isKinematic = false;
        rigidbody2D.AddForce(Vector2.right * Random.Range(10, 50));
        iTween.ScaleTo(gameObject, new Vector3(0.7f, 0.7f), 4f);

        //Xóa sau 1s
        Destroy(this.gameObject, 2);
    }

    private void Kill_BomNo()
    {
       
        NoTung();
    }

    private void Kill_SetDanh()
    {
        NoTung();
    }

    private void Kill_AnhSang()
    {
        audio.clip = Sound_BatNo;
        audio.Play();
        NoTung();
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag.Equals("THANH"))
        {
            NoTung();
        }
        else if (c.tag.Equals("BACKGROUND"))
        {
            IsDead = true;
            Destroy(this.gameObject);
        }
    }


    //Sự kiện khi chạm tay vào con dơi
    void OnMouseDown()
    {
        if (IsNotDead)
        {
            audio.clip = HitSound;
            audio.Play();
            health--;
            if (health <= 0)
                Kill(KillType.BINH_THUONG);
        }
    }

    public void NoTung()
    {
        //Đánh dấu đã chết
        IsDead = true;
        iTween.Stop(gameObject);

        //Loại bỏ 3 cái đuôi trail phía sau
        foreach (Transform trail in transform)
            Destroy(trail.gameObject);
        Animator.SetBool("boom", true);
        Animator.speed = 1;
        Destroy(this.gameObject, 3);
    }

    public IEnumerator Pause(float time)
    {
        if (this != null && this.gameObject != null && IsNotDead)
        {
            iTween.Pause(this.gameObject);
            if (Animator == null)
                print("Animator is null");
            else Animator.speed = 0;
            yield return new WaitForSeconds(time);
            if (this != null && this.gameObject != null && IsNotDead)
            {
                iTween.Resume(this.gameObject);
                if (Animator != null)
                    Animator.speed = 1;
            }
        }
    }
    #endregion
}