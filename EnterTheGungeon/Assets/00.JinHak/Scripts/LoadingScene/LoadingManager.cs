using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : GSingleton<LoadingManager>
{
    AsyncOperation op = default;
    public string nextSceneName = string.Empty;
    [SerializeField]
    private GameObject playerCamera = default;

    public override void Start()
    {
        playerCamera = GameObject.Find("GameObjs").transform.GetChild(0).gameObject;
    }

    public void StartLoading()
    {
        StartCoroutine(LoadScene());
    }

    public void LoadScene(string sceneName)
    {
        nextSceneName = sceneName;
        SceneManager.LoadScene("LoadingScene");
        PlayerManager.Instance.player.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(false);
        StartLoading();
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
                    op.allowSceneActivation = true; 
                    yield break; 
                }
            }
        }

        playerCamera.SetActive(true);
        PlayerManager.Instance.player.gameObject.SetActive(true);
    }
}
