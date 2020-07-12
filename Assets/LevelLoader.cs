using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private SceneAsset[] scenes = new SceneAsset[0];
    private Fader fader = default;

    private void Awake()
    {
        fader = GetComponentInChildren<Fader>();

        for (int i = 0; i < scenes.Length; i++)
            LoadScene(scenes[i].name, LoadSceneMode.Additive);
    }

    private void Start() => fader.FadeIn();

    public void LoadSingleScene(SceneAsset scene) => StartCoroutine(LoadSingleSceneAfterFade(scene));

    private IEnumerator LoadSingleSceneAfterFade(SceneAsset scene)
    {
        fader.FadeOut();

        while (fader.Fading)
            yield return null;

        LoadScene(scene.name, LoadSceneMode.Single);
    }

    private void LoadScene(string name, LoadSceneMode mode) => SceneManager.LoadScene(name, mode);
}
