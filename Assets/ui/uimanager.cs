using UnityEngine;
using UnityEngine.SceneManagement;
public class uimanager : MonoBehaviour
{

    [SerializeField] private string name;

    public  void exit() {   Application.Quit();   }
    public  void Startgame() {   SceneManager.LoadScene(name);   }
}
