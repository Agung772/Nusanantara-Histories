using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIGameplay : MonoBehaviour
{
    public static UIGameplay instance;

    public GameObject deathUI, victoryUI, pauseUI;
    public Text gameTime;
    public TextMeshProUGUI totalBenarWin, textTittleWin, totalBenarDefeat;

    public GameObject canvaMateri;

    public GameObject notifikasiText;
    public Transform spawnNotif;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        AudioManager.Instance.audioSourceBGM.clip = AudioManager.Instance.gameplayBGM;
        AudioManager.Instance.audioSourceBGM.Play();
    }


    bool pause;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUI();

            AudioManager.Instance.ClickButtonSfx();
        }
    }
    public void SpawnNotifikasi(string jawaban)
    {
        GameObject damageTextObject = Instantiate(notifikasiText, spawnNotif);
        damageTextObject.GetComponent<DamageText>().damageText.text = "Jawaban yang benar adalah " + jawaban;
        damageTextObject.GetComponent<DamageText>().transform.position = new Vector3(0, 0, 0);
    }

    public void DeathUI()
    {
        deathUI.SetActive(true);

        AudioManager.Instance.DefeatSfx();
        totalBenarDefeat.text = "Jawaban yang Benar = " + GameplayManager.instance.totalBenar;
    }
    public void VictoryUI()
    {
        victoryUI.SetActive(true);

        AudioManager.Instance.VictorySfx();
        totalBenarWin.text = "Jawaban yang Benar = " + GameplayManager.instance.totalBenar;

        textTittleWin.text = "Kerajaan " + GameData.instance.namaKerajaan + " Menang Berhasil Mengalahkan Musuh";
    }

    public void PauseUI()
    {
        if (!pause)
        {
            pause = true;
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pause = false;
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void RestratGame()
    {
        GameManager.instance.PindahScene(GameData.instance.namaKerajaan);
    }

    public void SelectMap()
    {
        GameManager.instance.PindahScene("SelectMap");
    }
    public void Mainmenu()
    {
        GameManager.instance.PindahScene("Mainmenu");
    }


}
