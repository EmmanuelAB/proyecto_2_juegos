using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenaryController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame() {

        GameData.gameRunning = true;
        SceneManager.LoadScene("City", LoadSceneMode.Single);
    }

    public void exitGame()
    {
        Application.Quit();
    }

}
