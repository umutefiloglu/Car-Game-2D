using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewSceneLoader : MonoBehaviour
{
    private float timeRemaining;
    private AsyncOperation asyncLoad;

    [Header("Next Level Panel")]
    [SerializeField] [Tooltip("Holds the panel that is showed before next level.")] private GameObject nextLevelGO;

    // Start is called before the first frame update
    void Start()
    {
        LevelStateManager.Instance.OnGameStateChange.AddListener(OnStateChange);
    }

    private void Update()
    {
        if (LevelStateManager.Instance.CurrentGameState == GameState.s7_Finished)
        {
            timeRemaining -= Time.deltaTime;
            nextLevelGO.transform.GetChild(0).gameObject.GetComponent<Text>().text = "New scene will start in " + Mathf.CeilToInt(timeRemaining);
        }
    }

    private void OnStateChange()
    {
        if (LevelStateManager.Instance.CurrentGameState == GameState.s7_Finished)
        {
            nextLevelGO.SetActive(true);
            timeRemaining = 3f;
            var _currInd = SceneManager.GetActiveScene().buildIndex;

            if (_currInd + 1 < SceneManager.sceneCountInBuildSettings)
            {
                //Load next scene
                StartCoroutine(LoadScene(_currInd + 1));
            }
            else
            {
                //Load same scene
                StartCoroutine(LoadScene(_currInd));
            }
        }
        else
        {
            nextLevelGO.SetActive(false);
        }
    }

    private IEnumerator LoadScene(int sceneIndToLoad)
    {
        if (asyncLoad == null)
        {
            asyncLoad = SceneManager.LoadSceneAsync(sceneIndToLoad);
        }
        while (true)
        {
            if (asyncLoad.progress >= 0.9f && timeRemaining <= 0f)
            {
                asyncLoad.allowSceneActivation = true;
                Debug.Log("true");
            }
            else
            {
                asyncLoad.allowSceneActivation = false;
                Debug.Log("Load Progress: " + asyncLoad.progress);
            }
            yield return null;
        }
    }
}
