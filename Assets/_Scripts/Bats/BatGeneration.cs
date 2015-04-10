using UnityEngine;
using System.Collections;


/*
 * Lớp này thực hiện việc sinh các con dơi,
 * Tăng dần các level
 * 
 * Các level đầu sẽ chỉ có các con dơi bay thẳng
 * Tiếp theo sẽ thêm các con dơi bay dích dắc
 * Tiếp đến là những con bay tròn
 * 
 * 
 * Chú ý đến thành phần tốc độ của dơi
 * 
 * - Có 2 loại: 
 * 
 * */
public class BatGeneration : MonoBehaviour
{
    public GameObject[] waves;
    
    void Awake()
    { }

    void Start()
    {
        StartCoroutine(Generate());
    }

    IEnumerator Generate()
    {
        while (PlayerController.Instance.IsAlive)
        {
            GameObject g = waves[Random.Range(0, waves.Length)];
            Instantiate(g, transform.position, Quaternion.identity);
            float time = LevelController.Instance.WaitTime + g.transform.childCount * 0.1f;
            yield return new WaitForSeconds(time);
        }
    }
}