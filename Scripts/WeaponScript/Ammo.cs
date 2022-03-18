using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Ammo : MonoBehaviour
{
    [SerializeField] int ammoTotal = 10;
    [SerializeField] int ammoMagazine = 10;
    [SerializeField] int magazineSize = 10;
    [SerializeField] TextMeshProUGUI ammoTotalTxt;
    [SerializeField] TextMeshProUGUI ammoMagazieTxt;

    private void Update()
    {
        DisplayText();
    }

    // show on the GUI the ammo quantites
    private void DisplayText()
    {
        ammoTotalTxt.text = ammoTotal.ToString();
        ammoMagazieTxt.text = ammoMagazine.ToString();
    }

    //get current ammo in the magazibe
    public int GetCurrentAmmo()
    {
        return ammoMagazine;
    }

    // get ammo in equipment
    public int GetAmmoTotal()
    {
        return ammoTotal;
    }

    // reduce the ammo in the magaizne after shot
    public void ReduceAmmoAmount()
    {
        ammoMagazine--;
    }

    // reload the magazine by transfer bullet from the
    // equipment to the magazine
    public bool ReloadMagazine()
    {
        if(ammoTotal > 0)
        {
            // detrmine how much ammo is need to fill
            int addition = magazineSize - ammoMagazine;
            if(addition == 0) { return false; }
            // if there is enough ammo in the equipment
            if(ammoTotal >= addition)
            {
                ammoMagazine += addition;
                ammoTotal -= addition;
            }

            // if there isnt enough ammo int equipment
            else
            {
                ammoMagazine += ammoTotal;
                ammoTotal = 0;
            }
            return true;

        }
        else
        {
            return false;
        }
    }

    public void AddAmmo(int add)
    {
        ammoTotal += add;
    }
}
