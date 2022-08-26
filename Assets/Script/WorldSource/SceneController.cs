using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    //Will change our scene to string passed in
    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    // Reloads the current scene we are in
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    //Load out Title scene. Must be called Title exactly
    public void TotitleScene()
    {
        GameController.instance.controlType = ControlType.Normal;
        SceneManager.LoadScene("Title");
    }

    //Get out active scenes name
    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    private void ToTiteScene()
    {
        GameController.instance.controlType = ControlType.Normal;
        SceneManager.LoadScene("Title");
    }

    //Quits our game
    public void QuitGame()
    {
        Application.Quit();
    }
}
