using UnityEngine;

public class MinasColocadasScript : MonoBehaviour {

    private void Update(){
        if (TableroScript.tablero != null){
            gameObject.GetComponent<TextMesh>().text = "" + (Dificultad.minas-TableroScript.tablero.getMinasRestantes());
        }else{
            gameObject.GetComponent<TextMesh>().text = "" + 0;
        }
    }
}
