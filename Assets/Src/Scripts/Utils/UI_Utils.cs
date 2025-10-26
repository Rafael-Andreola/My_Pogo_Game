using System.Collections;
using TMPro;
using UnityEngine;

public class UI_Utils
{
    public static IEnumerator StartNFadeText(TextMeshProUGUI text, float duration, float durationAlpha100, float targetAlpha)
    {
        Color originalColor = text.color;
        // Garantir que começa totalmente opaco
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        yield return new WaitForSeconds(durationAlpha100);

        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            float newAlpha = Mathf.Lerp(1f, targetAlpha, t);
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);

            yield return null;
        }

        // Garantir que chega no alpha final
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, targetAlpha);
    }
}
