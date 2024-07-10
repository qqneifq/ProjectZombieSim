using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject bar;
    [SerializeField]
    Transform[] slots;
    [SerializeField]
    Transform activeSlot;

    [SerializeField]
    int childCount = 100;
    // Start is called before the first frame update
    void Start()
    {
        slots = new Transform[9];
        foreach(Transform child in bar.transform)
        {
            //Debug.Log($"{child.gameObject.name}");
        }
        WeaponController.OnWeaponChange += WeaponChangeHandler;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void WeaponChangeHandler(int i)
    {
        Debug.Log($"Weapon change {i}");
        //ActivateSlot(i);
    }
    void ActivateSlot(int i)
    {
        activeSlot.localScale = Vector3.one;
        activeSlot = slots[i];
        activeSlot.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }
}
