using UnityEngine;
using System.Collections;

public class DanController : MonoBehaviour
{
    private Animator animator;
    private float speed = 5;
    void Start()
    {
       
        animator = this.GetComponent<Animator>();
        audio.volume = Config.isSound_On ? 1 : 0;
        audio.Play();
        if (animator == null)
            print("animator is null");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D thanh)
    {
        if(thanh.tag == "THANH")
        {
            speed = 0;
            animator.SetTrigger("NO");
            Destroy(this.gameObject,1.3f);
        }
        else if(thanh.tag == "BACKGROUND")
        {
            Destroy(this.gameObject);
        }
    }

}