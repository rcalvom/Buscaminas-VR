using UnityEngine;

public class PartidaScript : MonoBehaviour {

    public GameObject casilla;

    private void Start() {
        
        TableroScript.ICasillas = new GameObject[Dificultad.filas,Dificultad.columnas];
        for (int i = 0; i < Dificultad.filas; i++){
            for (int j = 0; j < Dificultad.columnas; j++){
                TableroScript.ICasillas[i, j] = Instantiate(casilla);
                TableroScript.ICasillas[i, j].transform.position = new Vector3(j-Dificultad.columnas/2.0f+0.5f, (float) Dificultad.filas - i,  (float) Mathf.Max(Dificultad.filas,Dificultad.columnas) *0.4f+5f);
                TableroScript.ICasillas[i, j].GetComponent<CasillaScript>().fila = i;
                TableroScript.ICasillas[i, j].GetComponent<CasillaScript>().columna = j;
            }
        }
    }

}
