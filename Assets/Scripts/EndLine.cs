using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    float EndTime = 0;
    void OnTriggerEnter2D(Collider2D collision)
    {
        ItemCtrl tc = collision.GetComponent<ItemCtrl>();
        if (tc != null)
            EndTime = Time.time;
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        ItemCtrl tc = collision.GetComponent<ItemCtrl>();
        if (tc != null && Time.time - EndTime >= 3 && Center.instance != null)
            Center.instance.GameOver();
    }
}
