using UnityEngine;

public class MinasRestantesScript : MonoBehaviour {
	
	private void Update () {
        if (TableroScript.tablero != null) {
            gameObject.GetComponent<TextMesh>().text = "" + TableroScript.tablero.getMinasRestantes();
        } else {
            gameObject.GetComponent<TextMesh>().text = "" + Dificultad.minas;

        }
    }
}
