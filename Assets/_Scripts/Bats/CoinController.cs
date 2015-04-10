using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Đồng xu để nó tự sinh trong đoạn [4,8] giây
//Tốc độ cũng được điều chỉnh theo level

public class CoinController : MonoBehaviour {
	void Start () {
        audio.volume = Config.isSound_On ? 1 : 0;
        audio.Play();
        iTween.MoveTo(gameObject, iTween.Hash("position", GameController.CoinUiPosition, "time", 1.5f, "easetype", iTween.EaseType.easeOutBack));
        iTween.ScaleTo(gameObject, new Vector3(0.5f, 0.5f, 0.5f), 1);
        PlayerController.Instance.UpCoin();
        Destroy(gameObject, 2);
	}
	
    void OnnComplete()
    {
        Destroy(gameObject);
    }

}
