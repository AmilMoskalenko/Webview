using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private Transform _progress;

    private void Start()
    {
        LoadSceneAsync("Menu");
    }

    private async void LoadSceneAsync(string sceneName)
    {
        var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            _progress.Rotate(0, 0, -200 * Time.deltaTime);
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            await Task.Yield();
        }
    }
}
