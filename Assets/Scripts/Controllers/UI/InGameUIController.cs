using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class InGameUIController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI ammoText;
    [SerializeField]
    TextMeshProUGUI weaponText;

    private void Start()
    {
        WeaponController.OnAmmoChange += AmmoTextChange;
        WeaponController.OnWeaponChange += WeaponTextChange;
    }

    void AmmoTextChange(int current, int max)
    {
        if(max <= 0)
        {
            ammoText.text = "-/-";
        }
        else
        {
            ammoText.text = $"{current}/{max}";
        }
    }
    void WeaponTextChange(int i)
    {
        WeaponTypes.WeaponType type = (WeaponTypes.WeaponType)i;
        weaponText.text = Enum.GetName(typeof(WeaponTypes.WeaponType), type);
    }
}
