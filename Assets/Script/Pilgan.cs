using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pilgan : MonoBehaviour
{
    public bool hasButton;
    public float time = 15;

    public Text pertanyaan, jawabanText;
    public Text a3, b3, c3;
    public string jawabanBenar;
    public Text timeText;

    public Animator animator;

    int jawabanInt;
    private void Start()
    {
        jawabanText.text = jawabanBenar;

        AudioManager.Instance.SwitchDialogBoxSfx();
    }

    bool stopDetik;
    bool detikan;
    float reset;
    private void Update()
    {
        if (!stopDetik)
        {
            time -= Time.deltaTime;
        }

        UIGameplay.instance.gameTime.text = time.ToString("F0"); ;
        time = Mathf.Clamp(time, 0, 100);

        if ((int)time == 5 && !detikan)
        {
            detikan = true;
            AudioManager.Instance.StartTimeSfx();
        }
        if (time <= 0 && !hasButton)
        {
            hasButton = true;
            GameplayManager.instance.EnemyAttack();
            UIGameplay.instance.SpawnNotifikasi(jawabanBenar);

            //Exit
            StartCoroutine(Coroutine());
            IEnumerator Coroutine()
            {
                stopDetik = true;
                AudioManager.Instance.StopTimeSfx();
                yield return new WaitForSeconds(1);
                animator.SetTrigger("Exit");
                GameplayManager.instance.NextSoal();
                yield return new WaitForSeconds(1);
                Destroy(gameObject);
            }
        }

        if (reset > 0)
        {
            reset -= Time.deltaTime;
        }
        else
        {
            jawabanInt = 0;
        }
    }

    public void Jawaban()
    {
        reset = 1;
        jawabanInt++;
        if (jawabanInt >= 5)
        {
            jawabanText.gameObject.SetActive(true);
        }
    }

    public void IsiPertanyaan(string Pertanyaan, string A, string B, string C, string JawabanBenar)
    {
        pertanyaan.text = Pertanyaan;
        a3.text = "A. " + A;
        b3.text = "B. " + B;
        c3.text = "C. " + C;
        jawabanBenar = JawabanBenar;
    }

    public void Jawaban(string jawaban)
    {
        if (!hasButton) hasButton = true;
        else return;

        if (jawaban == "A")
        {
            if (jawabanBenar == "A")
            {
                GameplayManager.instance.PlayerAttack();
                GameplayManager.instance.totalBenar++;
            }
            else
            {
                GameplayManager.instance.EnemyAttack();
                UIGameplay.instance.SpawnNotifikasi(jawabanBenar);
            }
        }
        else if (jawaban == "B")
        {
            if (jawabanBenar == "B")
            {
                GameplayManager.instance.PlayerAttack();
                GameplayManager.instance.totalBenar++;
            }
            else
            {
                GameplayManager.instance.EnemyAttack();
                UIGameplay.instance.SpawnNotifikasi(jawabanBenar);
            }
        }
        else if (jawaban == "C")
        {
            if (jawabanBenar == "C")
            {
                GameplayManager.instance.PlayerAttack();
                GameplayManager.instance.totalBenar++;
            }
            else
            {
                GameplayManager.instance.EnemyAttack();
                UIGameplay.instance.SpawnNotifikasi(jawabanBenar);
            }
        }

        //Exit
        StartCoroutine(Coroutine());
        IEnumerator Coroutine()
        {
            stopDetik = true;
            AudioManager.Instance.StopTimeSfx();
            yield return new WaitForSeconds(1);
            animator.SetTrigger("Exit");
            GameplayManager.instance.NextSoal();
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }

        AudioManager.Instance.ClickButtonSfx();
    }
}
