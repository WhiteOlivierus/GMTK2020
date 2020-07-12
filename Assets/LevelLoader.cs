using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private SceneAsset[] scenes = new SceneAsset[0];

    private void Awake()
    {
        for (int i = 0; i < scenes.Length; i++)
            LoadScene(scenes[i].name, LoadSceneMode.Additive);
    }

    public void LoadSingleScene(SceneAsset scene) => LoadScene(scene.name, LoadSceneMode.Single);

    private void LoadScene(string name, LoadSceneMode mode) => SceneManager.LoadScene(name, mode);
}
