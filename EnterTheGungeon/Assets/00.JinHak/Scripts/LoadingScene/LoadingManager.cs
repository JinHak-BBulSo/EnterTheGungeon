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
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;
        op = SceneManager.LoadSceneAsync(nextSceneName);

        while (!op.isDone)
        {
            yield return null;

            // 로딩 상황이 progress로 0 ~ 1의 숫자로 표시됨 
            if (op.progress < 0.9f) 
            { 
                /* Do nothing */
            }
            else
            {
                // progress >= 1 즉, 준비가 다된 상황
                if (op.progress >= 1.0f)
                { 
                    yield break; 
                }
            }
        }

        StartCoroutine(NextSceneLoad());
    }

    IEnumerator NextSceneLoad()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerManager.Instance.player.enabled = true;
        PlayerManager.Instance.player.OnHitAndStatusEvent();
        // 초기 무기 셋팅 호출
        InventoryManager.Instance.inventoryControl.AddFirstItem();
        yield return null;
        op.allowSceneActivation = true;
    }
}
