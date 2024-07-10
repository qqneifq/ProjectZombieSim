using UnityEngine;
using TMPro;
using System.Collections;

public class TextFadeEffect : MonoBehaviour
{
    public TextMeshProUGUI questName;
    public TextMeshProUGUI questDescription;
    public float fadeDuration = 1.0f;
    public float delayBeforeNewText = 2.0f;

    private void Awake()
    {
        QuestController.OnQuestStart += OnQuestTextStart;
    }

    void OnQuestTextStart(string name, string description)
    {
        Debug.Log($"Loading quest {name}/{description}");
        StartCoroutine(ChangeTextWithFade(questName, name));
        StartCoroutine(ChangeTextWithFade(questDescription, description));
    }

    public IEnumerator ChangeTextWithFade(TextMeshProUGUI textObject, string newText)
    {
        // ????????? fade-out
        yield return StartCoroutine(FadeTextToZeroAlpha(fadeDuration, textObject));

        // ????????? ????? ?????? ??????
        yield return new WaitForSeconds(delayBeforeNewText);

        // ??????? ?????
        textObject.text = newText;

        // ????????? fade-in
        yield return StartCoroutine(FadeTextToFullAlpha(fadeDuration, textObject));
    }

    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI textObject)
    {
        textObject.color = new Color(textObject.color.r, textObject.color.g, textObject.color.b, 0);
        while (textObject.color.a < 1.0f)
        {
            textObject.color = new Color(textObject.color.r, textObject.color.g, textObject.color.b, textObject.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI textObject)
    {
        textObject.color = new Color(textObject.color.r, textObject.color.g, textObject.color.b, 1);
        while (textObject.color.a > 0.0f)
        {
            textObject.color = new Color(textObject.color.r, textObject.color.g, textObject.color.b, textObject.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
