using UnityEngine;
using UnityEngine.SceneManagement;
public class uimanager : MonoBehaviour
{
  
 public  void exit() {   Application.Quit();   }
   public  void Startgame() {   SceneManager.LoadScene("SampleScene");   }
}
