using UnityEngine;
using System.Collections;

public class ItemAction : MonoBehaviour
{
    public string itemName;
    private Animator animator;
    private int level;
    private PlayerController player { get { return PlayerController.Instance; } }
    public GameObject child;

    void Start()
    {
        audio.volume = Config.isSound_On ? 1 : 0;
        audio.Play();
        animator = GetComponent<Animator>();
        level = PlayerController.Instance.GetLevelItem(itemName);
        print("item name: " + itemName);
    }


    void Update()
    {
        audio.volume = Config.isSound_On ? 1 : 0;
    }
    public void BatDau(Vector3 pos)
    {
        level = PlayerController.Instance.GetLevelItem(itemName);
        print("ItemAction.BatDau");

        //Chuyển sang vị trí hiện tại mà icon đang ở
        pos.z = 0;


        if (gameObject.name == "ActionSetDanh")
            pos.y = 0.5f;
        else if (gameObject.name == "ActionSetDanh_1")
            pos.y = 1f;
        else if (gameObject.name == "ActionSetDanh_2")
            pos.y = 1.3f;

        if (itemName == "ANHSANG")
            pos.y = 1;

        transform.position = pos;

        //TH_SET:
        if (itemName == "SET" || itemName == "ANHSANG")
            collider2D.enabled = true;
        if (animator == null)
            animator = GetComponent<Animator>();
        animator.SetTrigger("START");
        switch (itemName)
        {
            case "SET":
                XuLySetDanh();
                break;
            case "BOM":
                XuLyBomNo();
                break;
            case "DONGHO":
                XuLyDongHo();
                break;
            case "ANHSANG":
                StartCoroutine(XuLyAnhSang());
                break;
        }
    }

    public void KetThuc()
    {
        switch (itemName)
        {
            case "SET":
                collider2D.enabled = false;
                Destroy(gameObject, 2);
                break;
            case "BOM":
                Destroy(gameObject, 2);
                break;
            case "ANHSANG":
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
    //Có Set và Ánh sáng sẽ sử dụng cái này
    void OnTriggerEnter2D(Collider2D bat)
    {
        if (bat.tag == "BAT")
        {
            if (itemName == "ANHSANG")
                bat.GetComponent<BatController>().Kill(KillType.ANH_SANG);
            else bat.GetComponent<BatController>().Kill(KillType.SET_DANH);
        }
    }
    private void XuLySetDanh()
    {
        //Với tia sét thì nó sử dụng va trạm vật lý để quyết định xem có dính ko
    }

    private void XuLyBomNo()
    {
        //Với boom thì ta chỉ cần xử lý khoảng cách của nó
        child.GetComponent<Animator>().SetTrigger("NO");
        float d = 0;
        switch (level)
        {
            case 0:
                d = 3;
                break;
            case 1:
                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                d = 5f;
                break;
            case 2:
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                d = 7;
                break;
        }

        GameObject[] b = GameObject.FindGameObjectsWithTag("BAT");
        for (int i = 0; i < b.Length; i++)
            if (Vector3.Distance(this.transform.position, b[i].transform.position) <= d)
                b[i].GetComponent<BatController>().Kill(KillType.BOM_NO);

        b = GameObject.FindGameObjectsWithTag("BOSS");
        for (int i = 0; i < b.Length; i++)
            if (Vector3.Distance(this.transform.position, b[i].transform.position) <= d)
                b[i].GetComponent<BossController>().Damage("BOM");

    }

    private void XuLyDongHo()
    {
        //Với đồng hồ chỉ cần thay đổi thời gian bọn nó đứng yên
        float waitTime = 0;
        print("level dong ho: " + level);
        switch (level)
        {
            case 0:
                waitTime = 2;
                break;
            case 1:
                waitTime = 3.5f;
                break;
            case 2:
                waitTime = 5f;
                break;
            default:
                waitTime = 2f;
                break;
        }
        GameObject[] bats = GameObject.FindGameObjectsWithTag("BAT");
        for (int i = 0; i < bats.Length; i++)
            if (bats[i] != null)
                StartCoroutine(bats[i].GetComponent<BatController>().Pause(waitTime));

        GameObject[] bosses = GameObject.FindGameObjectsWithTag("BOSS");
        for (int i = 0; i < bosses.Length; i++)
            if (bosses[i] != null)
            {
                BossController c = bosses[i].GetComponent<BossController>();
                if (c != null)
                    c.Damage("DONGHO");
            }

        Destroy(gameObject, waitTime + 0.25f);
    }

    IEnumerator XuLyAnhSang()
    {
        float waitTime = 0;
        switch (level)
        {
            case 0:
                waitTime = 2;
                break;
            case 1:
                waitTime = 3f;
                break;
            case 3:
            default:
                waitTime = 4f;
                break;
        }

        yield return new WaitForSeconds(waitTime);
        KetThuc();
        animator.SetTrigger("KETTHUC");
    }
}
