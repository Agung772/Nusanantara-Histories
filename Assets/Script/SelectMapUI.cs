using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMapUI : MonoBehaviour
{
    public void PindahGameplay(string value)
    {
        GameManager.instance.PindahScene(value);
        GameData.instance.SaveKerajaan(value);

    }

    public void Mainmenu()
    {
        GameManager.instance.PindahScene("Mainmenu");
    }
}
