using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MateriUI : MonoBehaviour
{
    public static MateriUI instance;

    public RectTransform content;
    [SerializeField] TextMeshProUGUI noHalamanText;
    [SerializeField] int totalHalaman;

    public GameObject rightButton, leftButton, startButton;
    public int halamanIndex;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        totalHalaman = content.childCount;

        ConditionButton();

        TextMeshProUGUI[] tempText = new TextMeshProUGUI[content.childCount];
        for (int i = 0; i < tempText.Length; i++)
        {
            tempText[i] = content.GetChild(i).GetChild(content.GetChild(i).childCount - 1).GetComponent<TextMeshProUGUI>();
            tempText[i].text = i + 1 + "/" + totalHalaman;
        }
    }

    float posX;
    private void Update()
    {
        float tempX = (content.GetComponent<HorizontalLayoutGroup>().spacing + content.GetChild(0).GetComponent<RectTransform>().sizeDelta.x) * -halamanIndex;
        posX = Mathf.Lerp(posX, tempX, 5 * Time.deltaTime);

        content.anchoredPosition = new Vector2(posX, content.anchoredPosition.y);


    }

    public void RightButton()
    {
        halamanIndex++;
        ConditionButton();
        AudioManager.Instance.ClickButtonSfx();
    }

    public void LeftButton()
    {
        halamanIndex--;
        ConditionButton();
        AudioManager.Instance.ClickButtonSfx();
    }

    void ConditionButton()
    {
        noHalamanText.text = halamanIndex + 1 + "/" + totalHalaman;

        if (halamanIndex == 0)
        {
            rightButton.SetActive(true);
            leftButton.SetActive(false);

            startButton.SetActive(false);
        }
        else if (halamanIndex == content.childCount - 1)
        {
            rightButton.SetActive(false);
            leftButton.SetActive(true);

            startButton.SetActive(true);
        }
        else
        {
            rightButton.SetActive(true);
            leftButton.SetActive(true);

            startButton.SetActive(false);
        }
    }
}
