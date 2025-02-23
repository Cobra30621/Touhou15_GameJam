using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class addbullet_effect : MonoBehaviour
{
    public Image uiImage;        // 指定圖片
    public TextMeshProUGUI uiText; // 指定文字
    public float moveSpeed = 50f; // 向上移動速度
    public float fadeDuration = 10f; // 透明度變淡時間

    
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
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // 透明度遞減
            uiImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, alpha);
            uiText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);

            rectTransform.anchoredPosition += new Vector2(0, moveSpeed * Time.deltaTime); // 向上移動
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject); // 動畫結束後刪除物件
    }
}
