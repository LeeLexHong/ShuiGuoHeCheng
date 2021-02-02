using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrl : MonoBehaviour
{
    public int lv;
    public bool Coled = false; //已碰撞
    // 碰撞开始
    void OnCollisionEnter2D(Collision2D collision)
    {
        ItemCtrl otherItm = collision.collider.gameObject.GetComponent<ItemCtrl>();
        if (otherItm == null)
            return;
        if (!Coled && otherItm != null && otherItm.lv == lv) {
            if (Center.instance != null && Center.instance.Items.Length > lv +1)
            {
                otherItm.Coled = true;
                this.Coled = true;
                Vector3 pos = (this.transform.localPosition + collision.collider.transform.localPosition) / 2;
                Center.instance.GenerateItem(pos, lv + 1);
                Destroy(this.gameObject);
                Destroy(collision.collider.gameObject);
            }
              
        }
    }
}
