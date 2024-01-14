using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float maxHp, hp;

    public Image barHP;
    public TextMeshProUGUI barHPText;

    public Animator animatorMove, animatorBody;

    public GameObject damageText;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        hp = maxHp;
    }

    public void Attack()
    {
        animatorMove.SetTrigger("Attack");

        float random = Random.Range(1, 3);
        if (random == 1) animatorBody.SetTrigger("Attack1");
        else if (random == 2) animatorBody.SetTrigger("Attack2");

    }
    public void TerHIT()
    {
        animatorBody.SetTrigger("Hit");

        hp -= 34;

        if (hp <= 0)
        {
            hp = 0;
        }
        GameObject damageTextObject = Instantiate(damageText, transform);
        damageTextObject.GetComponent<DamageText>().damageText.text = "-34";
        damageTextObject.GetComponent<DamageText>().transform.localScale = new Vector3(-1, 1, 1);

        UpdateUI();

        AudioManager.Instance.DamageSfx();
    }
    public void UpdateUI()
    {
        barHP.fillAmount = hp / maxHp;
        barHPText.text = hp.ToString();
    }

    public void Death()
    {
        StartCoroutine(Coroutine());
        IEnumerator Coroutine()
        {
            hp = 0;
            UpdateUI();
            animatorBody.SetTrigger("Death");
            yield return new WaitForSeconds(1);
            animatorMove.SetTrigger("Death");
        }
    }
}
