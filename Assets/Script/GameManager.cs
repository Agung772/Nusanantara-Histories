using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject transisiUI;

    private void Awake()
    {
        if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);

        Application.targetFrameRate = 60;

        if (PlayerPrefs.GetFloat("Default") == 0)
        {
            PlayerPrefs.SetFloat("Default", 1);

            AudioManager.Instance.SetDefaultVolume();
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteAll();
        }
    }
    int deleteInt;
    public void DeletePlayerPrefs()
    {
        deleteInt++;
        if (deleteInt >= 10)
        {
            PlayerPrefs.DeleteAll();
            AudioManager.Instance.ClickButtonSfx();
        }
    }
    public void PindahScene(string value)
    {
        StartCoroutine(Coroutine());
        IEnumerator Coroutine()
        {
            StartTransisi();
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(value);
            yield return new WaitForSeconds(1);
            ExitTransisi();
            yield return new WaitForSeconds(1);
        }

        AudioManager.Instance.ClickButtonSfx();
    }

    public void StartTransisi()
    {
        transisiUI.SetActive(true);
        transisiUI.GetComponent<Animator>().SetBool("Start", true);
        Time.timeScale = 1;
    }
    public void ExitTransisi()
    {
        transisiUI.GetComponent<Animator>().SetBool("Start", false);
    }
}
