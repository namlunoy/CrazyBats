using UnityEngine;
using System.Collections;

public class Bat2Generation : MonoBehaviour
{
    public GameObject bat_2;
    public Transform up;
    public Transform down;
    void Start()
    {
        StartCoroutine(Generate());
    }

    IEnumerator Generate()
    {
        yield return new WaitForSeconds(7);
        while (PlayerController.Instance.IsAlive)
        {
            yield return new WaitForSeconds(LevelController.Instance.WaitTime2);

            Sinh();
            if (Random.Range(0, 4) == 2)
                Sinh();
        }
    }

    private void Sinh()
    {
        //Vị trí sinh
        Vector3 pos = new Vector3(transform.position.x, 0, 0);
        pos.y = Random.Range(down.position.y, up.position.y);
        GameObject bat = (GameObject)Instantiate(bat_2, pos, Quaternion.identity);


        //tính đường đi
        BatController controller = bat.GetComponent<BatController>();
        int index = Random.Range(0, LevelController.Instance.Paths.Count);
        Vector3[] basePath = LevelController.Instance.Paths[index];
        Vector3[] path = new Vector3[basePath.Length];
        Vector3 offset = pos - basePath[0];
        for (int j = 0; j < basePath.Length; j++)
            path[j] = basePath[j] + offset;
        controller.SetUp(path, LevelController.Instance.Speed, FlyType.THANG, 0);
    }

}
