using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadKey : MonoBehaviour,IChildBehavior
{


    public void Execute()
    {
        SceneManager.LoadScene("Game");
    }
}
