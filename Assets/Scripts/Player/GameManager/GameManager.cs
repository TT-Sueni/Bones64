using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private string mainGameplayScene;
    [SerializeField] private string sceneToLoad;

    private SceneReferences main;
    private SceneReferences current;

    protected override void OnAwaken()
    {
        SceneReferences.onLoadedScene += SceneReferences_onLoadedScene;
    }

    protected override void OnDestroyed()
    {
        SceneReferences.onLoadedScene -= SceneReferences_onLoadedScene;
    }

    private void SceneReferences_onLoadedScene(SceneReferences obj)
    {
        if (main == null)
            main = obj;
        else
        {
            current = obj;

            player.transform.position = current.previousState.position;
            player.transform.rotation = current.previousState.rotation;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            Load(sceneToLoad);

        if (Input.GetKeyDown(KeyCode.Q))
            Unload();
    }

    private void Load(string newSceneName)
    {
        main.previousState.position = player.transform.position;
        main.previousState.rotation = player.transform.rotation;

        SceneManager.onLoadedScene += CustomSceneManager_onLoadedScene;
        SceneManager.Instance.ChangeSceneTo(newSceneName);
    }

    private void CustomSceneManager_onLoadedScene()
    {
        SceneManager.onLoadedScene -= CustomSceneManager_onLoadedScene;
        SetAsActiveScene(sceneToLoad);
    }

    private void Unload()
    {
        player.transform.position = main.previousState.position;
        player.transform.rotation = main.previousState.rotation;

        SetAsActiveScene(sceneToLoad);
        main.SetActiveGo(true);

        Scene gameplay = EditorSceneManager.GetSceneByName(mainGameplayScene);
        EditorSceneManager.SetActiveScene(gameplay);
        EditorSceneManager.UnloadSceneAsync(sceneToLoad);
    }

    private void SetAsActiveScene(string sceneName)
    {
        Scene newScene = EditorSceneManager.GetSceneByName(sceneName);
        EditorSceneManager.SetActiveScene(newScene);

        main.SetActiveGo(false);
    }
}
