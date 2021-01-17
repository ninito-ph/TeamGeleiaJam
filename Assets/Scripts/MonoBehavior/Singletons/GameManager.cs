using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    #region Field Declarations

    private MenuState menuState = new MenuState();
    private GameState gameState = new GameState();
    private BaseGameState currentState;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        // Enters the menu state immediately
        SwitchState(menuState);
    }

    // Update is called once per frame
    void Update()
    {
        // Runs the current state's update function
        currentState.Update(this);
    }

    #endregion

    #region Custom Methods

    // Loads a scene through a loading screen
    public void LoadScene(string sceneName)
    {
        // Loads the load scene
        SceneManager.LoadScene("LoadingScene");

        // Loads the next scene asynchronously
        SceneManager.LoadSceneAsync(sceneName);
    }

    // Exits the application
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Switches the game state to the new selected state
    private void SwitchState(BaseGameState newState)
    {
        // Leaves the current state
        currentState.LeaveState(this);

        // Changes the current state and trigger its enter method
        currentState = newState;
        currentState.EnterState(this);
    }

    #endregion
}
