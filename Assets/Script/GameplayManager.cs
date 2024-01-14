using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;

    public int totalBenar;

    [Serializable]
    public struct Pertanyaan
    {
        public string pertanyaan;
        public string a, b, c;
        public string jawabanBenar;
    }
    public List<Pertanyaan> pertanyaan;

    public GameObject pilganPrefab;
    public Transform pilganSpawn;
    public int index;

    public GameObject karakter;

    bool start;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        randomBool = new bool[pertanyaan.Count];

        UIGameplay.instance.canvaMateri.SetActive(true);
    }

    public void StartGameplay()
    {
        UIGameplay.instance.canvaMateri.SetActive(false);
        SpawnPilgan();

        AudioManager.Instance.ClickButtonSfx();
    }

    public void NextSoal()
    {
        StartCoroutine(Coroutine());
        IEnumerator Coroutine()
        {
            if (PlayerController.instance.hp <= 0)
            {
                PlayerController.instance.Death();
                yield return new WaitForSeconds(2);
                UIGameplay.instance.DeathUI();
            }
            else if (EnemyController.instance.hp <= 0)
            {
                EnemyController.instance.Death();
                yield return new WaitForSeconds(2);
                UIGameplay.instance.VictoryUI();
            }
            else
            {
                SpawnPilgan();
            }


        }

    }

    public void SpawnPilgan()
    {
        if (!start)
        {
            start = true;
            karakter.SetActive(true);
        }

        RandomIndex();
        GameObject pilgan = Instantiate(pilganPrefab, pilganSpawn);
        pilgan.GetComponent<Pilgan>().IsiPertanyaan
            (pertanyaan[index].pertanyaan, pertanyaan[index].a, pertanyaan[index].b, pertanyaan[index].c, pertanyaan[index].jawabanBenar);
    }

    public bool[] randomBool;
    void RandomIndex()
    {
        int random = UnityEngine.Random.Range(0, pertanyaan.Count);

        //Check true
        int check = 0;
        for (int i = 0; i < randomBool.Length; i++)
        {
            if (randomBool[i]) check++;
        }
        if (check == pertanyaan.Count) randomBool = new bool[pertanyaan.Count];

        for (int i = 0; i < pertanyaan.Count; i++)
        {
            if (!randomBool[i] && random == i)
            {
                randomBool[i] = true;
                index = random;
                print(random);
                break;
            }
            else if (randomBool[i] && random == i)
            {
                RandomIndex();
            }
        }

    }

    public void PlayerAttack()
    {
        StartCoroutine(Coroutine());
        IEnumerator Coroutine()
        {
            yield return new WaitForSeconds(0);
            PlayerController.instance.Attack();
            EnemyController.instance.TerHIT();
        }

        AudioManager.Instance.AttackSfx();
        print("Jawaban benar");
    }
    public void EnemyAttack()
    {
        StartCoroutine(Coroutine());
        IEnumerator Coroutine()
        {
            yield return new WaitForSeconds(0);
            EnemyController.instance.Attack();
            PlayerController.instance.TerHIT();
        }

        AudioManager.Instance.AttackSfx();
        print("Jawaban salah");
    }
}
