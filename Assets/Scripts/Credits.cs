using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public GameObject panel;

    void Start() { if (panel != null) panel.SetActive(false); }

    public void Open() => panel.SetActive(true);
    public void Close() => panel.SetActive(false);
    public void Toggle() => panel.SetActive(!panel.activeSelf);

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
