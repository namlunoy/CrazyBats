using UnityEngine;
using System.Collections;

//KEEP IT SIMPLE
/**
 * Wave chỉ là nơi để lưu vị trí bắt đầu của những con dơi.
 * Tiếp theo nó sẽ nạp đường đi và tốc độ cho bày dơi này.
 * 
 * 
 * */
public class WaveController : MonoBehaviour
{
    private GameObject[] bats;
    private BatController[] controllers;

    void Awake()
    {
        bats = new GameObject[transform.childCount];
        controllers = new BatController[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            bats[i] = transform.GetChild(i).gameObject;
            controllers[i] = bats[i].GetComponent<BatController>();
        }
    }

    void Start()
    {
        float speed = LevelController.Instance.Speed;
        int index = Random.Range(0, LevelController.Instance.Paths.Count);
        Vector3[] basePath = LevelController.Instance.Paths[index];
        if (index == 1) speed -= 3;
        WaitType waitType;
        if (Random.Range(0, 2) == 1 || index == 1 || LevelController.Instance.Level < 4)
            waitType = WaitType.YES;
        else waitType = WaitType.NO;
        for (int i = 0; i < bats.Length; i++)
        {
            Vector3[] path = new Vector3[basePath.Length];
            Vector3 offset = bats[i].transform.position - basePath[0];

            for (int j = 0; j < basePath.Length; j++)
                path[j] = basePath[j] + offset;

            switch (waitType)
            {
                case WaitType.YES:
                    controllers[i].SetUp(path, speed, FlyType.THANG, i * 1f / 2);
                    break;
                default:
                    controllers[i].SetUp(path, speed, FlyType.THANG, 0);
                    break;
            }
        }
        StartCoroutine(CheckAlive());
    }


    IEnumerator CheckAlive()
    {
        while (true)
        {
            if (transform.childCount == 0)
                Destroy(this.gameObject);
            yield return new WaitForSeconds(2);
        }
    }

    public bool isStillAlive()
    {
        foreach (GameObject bat in bats)
            if (bat.activeSelf)
                return true;
        return false;
    }

    // Update is called once per frame

}
