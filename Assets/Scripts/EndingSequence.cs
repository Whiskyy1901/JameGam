using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndingSequence : MonoBehaviour
{
    public CanvasGroup gameUI;
    public CanvasGroup endPanel;
    public TMP_Text endText;
    public Transform guy;
    public GrowthStages growth;

    bool played;

    public SoundManager sound;

    public void Play()
    {
        if (played) return;
        played = true;
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        sound.PlayEnding();
        gameUI.interactable = false;
        yield return new WaitForSeconds(2f);

        growth.enabled = false;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 0.5f;
            gameUI.alpha = 1f - t;
            guy.localScale *= 1f + Time.deltaTime * 3f;
            guy.Rotate(0, 0, 90f * Time.deltaTime);
            endPanel.alpha = t;
            yield return null;
        }

        endPanel.alpha = 1f;
        endPanel.interactable = true;
        endPanel.blocksRaycasts = true;

        string line = "You swallowed everything.\n...still just a little guy?";
        endText.text = "";
        foreach (char c in line)
        {
            endText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void Restart() =>
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}