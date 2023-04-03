using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : GSingleton<LoadingManager>
{
    AsyncOperation op = default;
    public string nextSceneName = string.Empty;

    public void StartLoading()
    {
        StartCoroutine(LoadScene());
    }

    public void LoadLoadingScene(string sceneName)
    {
        nextSceneName = sceneName;
        SceneManager.LoadScene("03.LoadingScene", LoadSceneMode.Additive);
    }

    IEnumerator LoadScene()
    {
        op = SceneManager.LoadSceneAsync(nextSceneName);

        while (!op.isDone)
        {
            yield return null;

            // 로딩 상황이 progress로 0 ~ 1의 숫자로 표시됨 
            if (op.progress < 1f) 
            {
                /* Do nothing */
            }
            else
            {
                // progress >= 1 즉, 준비가 다된 상황
                if (op.progress >= 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break; 
                }
            }
        }
    }
}
