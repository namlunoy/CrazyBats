using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour
{
    private static LevelController _instance = null;
    public static LevelController Instance
    {
        get
        {
            return _instance;
        }
    }
    public int Level = 0;

    private float maxSpeed = 6;
    private float minSpeed = 5;
    private float kcSpeed = 0.5f;
    public float Speed { get { return Random.Range(minSpeed, maxSpeed); } }

    private float minWaitTime = 8f;
    private float maxWaitTime = 10;
    private float kcWaitTime = 0.7f;
    public float WaitTime { get { return Random.Range(minWaitTime, maxWaitTime); } }
    public float WaitTime2 { get; set; }
    private float kcWaitTime2 = 0.5f;

    public List<Vector3[]> Paths;

    void Awake()
    {
        _instance = this;
        Paths = new List<Vector3[]>();
        Paths.Add(iTweenPath.GetPath("THANG"));
    }
    void Start()
    {
        WaitTime2 = 10;
        StartCoroutine(Counter());
    }

    IEnumerator Counter()
    {
        while (true)
        {
            //Cập nhật các thông số theo từng level
            Level++;
            WaitTime2 -= kcWaitTime2;
            print("GameLevel : " + Level);
            minSpeed += kcSpeed;
            maxSpeed += kcSpeed;

            minWaitTime -= kcWaitTime;
            maxWaitTime -= kcWaitTime;

            switch (Level)
            {
                case 5:
                    minSpeed -= 1;
                    maxSpeed -= 1;

                    kcSpeed = 0.4f;
                    kcWaitTime = 0.4f;

                    Paths.Add(iTweenPath.GetPath("ZICZAC"));
                    break;
                case 7:
                    minSpeed -= 1;
                    maxSpeed -= 1;

                    kcSpeed = 0.3f;
                    kcWaitTime = 0.2f;
                    break;
                case 10:
                    minSpeed -= 1;
                    maxSpeed -= 1;

                    //Khoảng cách chờ đợi nhỏ đi
                    kcWaitTime = 0.1f;
                    kcWaitTime2 = 0.2f;
                    break;

                case 15:
                    kcWaitTime = 0;
                    kcWaitTime2 = 0;
                    break;
            }
            yield return new WaitForSeconds(20);
        }
    }

}
