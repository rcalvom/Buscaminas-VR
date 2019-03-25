using UnityEngine;

public class SalirX : MonoBehaviour {
    private void Start(){
        Input.backButtonLeavesApp = true;
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }
}
