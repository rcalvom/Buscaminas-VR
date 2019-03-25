using UnityEngine.EventSystems;
using UnityEngine;

public class SeleccionPersonalizadoScript : MonoBehaviour {

    public static int filas = 10;
    public static int columnas = 10;

    public GameObject TextoFila;
    public GameObject TextoColumna;

    public void Start(){
        filas = 10;
        columnas = 10;
        UpdateSeleccion(null);
    }

    public void UpdateSeleccion(BaseEventData eventData){
        TextoFila.GetComponent<TextMesh>().text = SeleccionPersonalizadoScript.filas + "";
        TextoColumna.GetComponent<TextMesh>().text = SeleccionPersonalizadoScript.columnas + "";
    }
    
}
