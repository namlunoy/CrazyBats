using UnityEngine;
using System.Collections;

public class BossGeneration : MonoBehaviour {
    public GameObject boss;
	void Start () {
        StartCoroutine(Generate());
	}

    IEnumerator Generate()
    {
        yield return new WaitForSeconds(40);
        while(PlayerController.Instance.IsAlive)
        {
            Instantiate(boss, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(50);
        }
    }
	
}
