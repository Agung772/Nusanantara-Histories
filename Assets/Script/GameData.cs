using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    public string namaKerajaan;

    //
    public string _NamaKerajaan = "_NamaKerajaan";

    private void Awake()
    {
        if (instance == null) instance = this;
        LoadData();
    }

    private void Start()
    {

    }

    public void LoadData()
    {
        namaKerajaan = PlayerPrefs.GetString(_NamaKerajaan);
    }

    public void SaveKerajaan(string value)
    {
        PlayerPrefs.SetString(_NamaKerajaan, value);
        LoadData();
    }
}
