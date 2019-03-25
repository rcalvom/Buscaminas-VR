using UnityEngine;
using UnityEngine.SceneManagement;

public class SetDificultadScript : MonoBehaviour {
	private void Start () {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Juego")){
            Dificultad.setPrincipiante();
        }/*else if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Juego")){

        }else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Juego")){

        }*/
	}
}
