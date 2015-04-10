using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ThanhController : MonoBehaviour
{
    public Text bloodText;
    public Image bloodImage;
    public int total;
    private int _health;
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            bloodText.text = _health.ToString();
            bloodImage.fillAmount = _health * 1f / total;
            if (_health <= 0)
                PlayerController.Instance.IsAlive = false;
        }
    }

    void Start()
    {
        Health = total;
    }

    // Update is called once per frame
    void Update()
    {
        audio.volume = Config.isSound_On ? 1 : 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        audio.Play();
        switch (other.tag)
        {
            case "BAT":
                Health -= 2;
                break;

            case "DAN":
                Health -= 5;
                break;
            default:
                break;
        }

        if(other.gameObject.name.Contains("Bat_2"))
        {
            Health -= 2;
        }
    }
}
