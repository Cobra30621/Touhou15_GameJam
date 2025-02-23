using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class addbullet_effect : MonoBehaviour
{
    public Image uiImage;        // ���w�Ϥ�
    public TextMeshProUGUI uiText; // ���w��r
    public float moveSpeed = 50f; // �V�W���ʳt��
    public float fadeDuration = 10f; // �z�����ܲH�ɶ�

    
    void Start()
    {
        StartCoroutine(FadeAndMove());
    }

    public void Setup(Sprite sprite)
    {
        uiImage.sprite = sprite;
    }

    IEnumerator FadeAndMove()
    {
        float elapsedTime = 0f;
        RectTransform rectTransform = GetComponent<RectTransform>();
        Color imageColor = uiImage.color;
        Color textColor = uiText.color;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // �z���׻���
            uiImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, alpha);
            uiText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);

            rectTransform.anchoredPosition += new Vector2(0, moveSpeed * Time.deltaTime); // �V�W����
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject); // �ʵe������R������
    }
}
