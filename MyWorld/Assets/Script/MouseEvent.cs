using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class MouseEvent : MonoBehaviour
{
    [Header("玩家建模方块")]
    public GameObject Wood;

    void DestroyCube()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rhit;
        if (Physics.Raycast(ray, out rhit, 7, LayerMask.GetMask("Cube")))
        {
            Vector3 pos = rhit.point + ray.direction.normalized * 0.1f;
            WorldModify wd = new WorldModify();
            wd.x = Mathf.RoundToInt(pos.x);
            wd.y = Mathf.RoundToInt(pos.y);
            wd.z = Mathf.RoundToInt(pos.z);
            wd.to = 0;
            UserClient.SendMessage(new Message("UpdateWorld", JsonConvert.SerializeObject(wd)));
            Debug.Log("消除一个方块！");
        }
    }

    void PutCube()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rhit;
        if (Physics.Raycast(ray, out rhit, 7, LayerMask.GetMask("Cube")))
        {
            Vector3 pos = rhit.point - ray.direction.normalized * 0.1f;
            WorldModify wd = new WorldModify();
            wd.x = Mathf.RoundToInt(pos.x);
            wd.y = Mathf.RoundToInt(pos.y);
            wd.z = Mathf.RoundToInt(pos.z);
            wd.to = 3;
            if (wd.inside())
            {
                UserClient.SendMessage(new Message("UpdateWorld", JsonConvert.SerializeObject(wd)));
            }
            Debug.Log("创建一个方块！");
        }
    }

    void Update()
    {
        if (Global.pause) return;
        if (Input.GetMouseButtonDown(0))
        {
            DestroyCube();

        }
        if (Input.GetMouseButtonDown(1))
        {
            PutCube();
        }
    }
}
