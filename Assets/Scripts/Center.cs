using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Center : MonoBehaviour
{
    public Transform ItemsParent;
    public Item[] Items;
    public Text GNum, Enum;
    public GameObject ItemMod,StartCanvas,GameCanvas,OverCanvas;
    float ItemPosY = 470;
    GameObject NextItem = null;
    bool gaming = false;
    public static Center instance = null;
    int score = 0;
    void Start()
    {
        GameCanvas.SetActive(false);
        OverCanvas.SetActive(false);
        instance = this;
        ShowNextItem();
    }
    void Update()
    {
        if (gaming && NextItem != null) {
            if (Input.GetMouseButtonUp(0))
            {
                NextItem.GetComponent<Rigidbody2D>().simulated = true;
                NextItem = null;
                ShowNextItem();
            }
            if (Input.GetMouseButton(0)) {
                float x = Input.mousePosition.x - Screen.width / 2f;
                NextItem.transform.localPosition = new Vector3(x, ItemPosY, 0);
            }
        }
    }

    void ShowNextItem() {
        int idx = Random.Range(0, 4);
        GameObject go = GameObject.Instantiate<GameObject>(ItemMod);
        go.transform.SetParent(ItemsParent);
        go.transform.localPosition = new Vector3(0, ItemPosY, 0);
        go.transform.localScale = Vector3.one * Items[idx].Scale;
        go.GetComponent<Rigidbody2D>().mass = Items[idx].Mass;
        go.GetComponent<Image>().color = Items[idx].color;
        ItemCtrl itmctrl = go.AddComponent<ItemCtrl>();
        itmctrl.lv = idx;
        NextItem = go;
    }

    public void GenerateItem(Vector3 pos,int lv)
    {
        GameObject go = GameObject.Instantiate<GameObject>(ItemMod);
        go.transform.SetParent(ItemsParent);
        go.transform.localPosition = pos;
        go.transform.localScale = Vector3.one * Items[lv].Scale;
        go.GetComponent<Rigidbody2D>().mass = Items[lv].Mass;
        go.GetComponent<Rigidbody2D>().simulated = true;
        go.GetComponent<Image>().color = Items[lv].color;
        ItemCtrl itmctrl = go.AddComponent<ItemCtrl>();
        itmctrl.lv = lv;

        score += lv;
        GNum.text = score.ToString() ;
    }

    public void Play() {
        gaming = true;
        score = 0;
        GNum.text = "0";
        for (int i = 0; i < ItemsParent.childCount; i++)
            Destroy(ItemsParent.GetChild(i).gameObject);
        ShowNextItem();
        StartCanvas.SetActive(false);
        GameCanvas.SetActive(true);
        OverCanvas.SetActive(false);
    }

    public void BackHall() {
        StartCanvas.SetActive(true);
        GameCanvas.SetActive(false);
        OverCanvas.SetActive(false);
    }

    public void GameOver() {
        gaming = false;
        Enum.text = score.ToString();
        OverCanvas.SetActive(true);
    }
}