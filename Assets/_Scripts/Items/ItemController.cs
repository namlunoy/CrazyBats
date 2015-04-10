using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
 * Quản lý số lượng item.
 * Khả năng click
 * Di chuyển item đi và trở về
 */
public class ItemController : MonoBehaviour
{
    //Tên dùng để phân lại item để thực hiện các công việc tương ứng
    public string itemName;
    //vị trí ban đầu để khi thực hiện xong thì nó về vị trí ban đầu
    private Vector3 originalPosition;
    //Hình ảnh 3 level của item
    public Sprite[] LevelImages;
    //Điều khiển việc thực hiện aciton

    public GameObject vong;
    private Vector3 vong_pos;

    public GameObject item;

    //Lấy thuôc tính button của nó
    private Button button;

    private bool isMoving = false;

    public Text txt;
    private int SoLuong { get { return PlayerController.Instance.GetSoLuongItem(itemName); } }
    private bool vanCon = true;
    private PlayerController player { get { return PlayerController.Instance; } }
    private int Level;

    protected void Start()
    {
        button = GetComponent<Button>();
        originalPosition = transform.position;
        Level = GetLevel(itemName);
        button.image.sprite = LevelImages[Level];
        vong_pos = vong.transform.position;
        UpdateItem();
        //  button.transform.position = new Vector3(800, 480);  
    }

    private void UpdateItem()
    {
        int s = SoLuong;
        if (s <= 0)
        {
            button.enabled = false;
            vanCon = false;
        }

        txt.text = s.ToString();
    }
    private int GetLevel(string itemName) { return PlayerController.Instance.GetLevelItem(itemName); }

    //Kiểm tra các điều kiện và đánh dấu việc bắt đầu di chuyển
    public void Down()
    {
        if (vanCon)
        {
            isMoving = true;
            vong.transform.position = Camera.main.ScreenToWorldPoint(transform.position);
        }
    }

    //Kiểm tra điều kiện và thực hiện action của item
    public void Up()
    {
        if (vanCon)
        {
            isMoving = false;
            //Boom!
            Vector3 p = vong.transform.position;
            GameObject action = (GameObject)Instantiate(item);
            if (action.name.Contains("ActionSetDanh"))
                action.name = "ActionSetDanh";
            action.GetComponent<ItemAction>().BatDau(p);


            //Update số lượng
            PlayerController.Instance.UseItem(itemName);
            UpdateItem();

            //Riêng trường hợp của tia sét sẽ được cài đặt level ở đây
            //Do tự nó không thể tự khởi tạo chính nó để tạo thêm 1 cái nữa được
            if (itemName == "SET" && Level > 0)
            {
                //Khởi tạo thêm 1 tia sét nữa
                p.x -= 2;
                p.y += 1.1f;
                action = (GameObject)Instantiate(item);
                action.name = "ActionSetDanh_1";
                action.GetComponent<ItemAction>().BatDau(p);
                p.x += 4;
                p.y -= 0.5f;
                if (Level == 2)
                {
                    action = (GameObject)Instantiate(item);
                    action.name = "ActionSetDanh_2";
                    action.GetComponent<ItemAction>().BatDau(p);
                }
            }


            //Dưa item icon trở về vị trí ban đầu
            vong.transform.position = vong_pos;
        }
    }

    void Update()
    {
        if (isMoving)
        {
            Vector3 p;
            if (Input.touchCount > 0)
            {
                Vector2 v = Input.GetTouch(0).position;
                p = new Vector3(v.x, v.y, 0);
            }
            else
                p = Input.mousePosition;
            p = Camera.main.ScreenToWorldPoint(p);
            p.z = 0;
            vong.transform.position = p;
        }
    }
}