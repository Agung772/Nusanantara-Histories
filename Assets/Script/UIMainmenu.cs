using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainmenu : MonoBehaviour
{
    public GameObject pengaturanUI;
    public Image barBGM, barSFX;
    public Text barBGMText, barSFXText;

    private void Awake()
    {

    }
    private void Start()
    {
        AudioManager.Instance.barBGM = barBGM;
        AudioManager.Instance.barSFX = barSFX;
        AudioManager.Instance.barBGMText = barBGMText;
        AudioManager.Instance.barSFXText = barSFXText;

        AudioManager.Instance.audioSourceBGM.clip = AudioManager.Instance.mainmenuBGM;
        AudioManager.Instance.audioSourceBGM.Play();

        AudioManager.Instance.UpdateVolume();


        StartCoroutine(Coroutine());
        IEnumerator Coroutine()
        {
            GameManager.instance.transisiUI.SetActive(true);
            yield return new WaitForSeconds(1);
            GameManager.instance.ExitTransisi();
        }
    }
    public void PengaturanUI(bool value)
    {
        if (value)
        {
            pengaturanUI.SetActive(true);
        }
        else
        {
            pengaturanUI.SetActive(false);
        }

        AudioManager.Instance.ClickButtonSfx();
    }

    public void PindahScene(string value)
    {
        GameManager.instance.PindahScene(value);
    }

    public void PlusBGM()
    {
        AudioManager.Instance.PlusBGM();
        AudioManager.Instance.ClickButtonSfx();
    }
    public void MinusBGM()
    {
        AudioManager.Instance.MinusBGM();
        AudioManager.Instance.ClickButtonSfx();
    }
    public void PlusSFX()
    {
        AudioManager.Instance.PlusSFX();
        AudioManager.Instance.ClickButtonSfx();
    }
    public void MinusSFX()
    {
        AudioManager.Instance.MinusSFX();
        AudioManager.Instance.ClickButtonSfx();
    }
    
    public void Quit()
    {
        Application.Quit();
        AudioManager.Instance.ClickButtonSfx();
    }
}
