using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BotonesClick : MonoBehaviour {

    public GameObject Jugador; //19 1.6 25

    public void OnClickPrincipiante(BaseEventData eventData){
        Dificultad.setPrincipiante();
        SceneManager.LoadScene("Partida");
    }

    public void OnClickIntermedio(BaseEventData eventData){
        Dificultad.setIntermedio();
        SceneManager.LoadScene("Partida");
    }

    public void OnClickExperto(BaseEventData eventData){
        Dificultad.setExperto();
        SceneManager.LoadScene("Partida");
    }

    public void OnClickVolver(BaseEventData eventData){
        Jugador.transform.position = new Vector3(19f, 1.6f, 25f);
    }

    public void OnClickPersonalizado(BaseEventData eventData){
        Jugador.transform.position = new Vector3(19f,1.6f,35.3f);
    }

    public void OnClickJugar(BaseEventData eventData){
        Jugador.transform.position = new Vector3(1.2f, 1.6f,-4.7f);
    }

    public void OnClickAjustes(BaseEventData eventData){
        Jugador.transform.position = new Vector3(10.2f, 1.6f, 4.3f);
    }

    public void OnClickVolverJuego(BaseEventData eventData){
        TableroScript.Reset();
        SceneManager.LoadScene("Main");
    }

    public void OnClickCreditos(BaseEventData eventData){
        Jugador.transform.position = new Vector3(8.06f, 1.6f, 37.51f);
    }

    public void OnClickFilaUp(BaseEventData eventData){
        if (SeleccionPersonalizadoScript.filas <30){
            SeleccionPersonalizadoScript.filas++;
        }
    }

    public void OnClickColumnaUp(BaseEventData eventData){
        if (SeleccionPersonalizadoScript.columnas < 30){
            SeleccionPersonalizadoScript.columnas++;
        }
    }

    public void OnClickFilaDown(BaseEventData eventData){
        if (SeleccionPersonalizadoScript.filas > 10){
            SeleccionPersonalizadoScript.filas--;
        }
    }

    public void OnClickColumnaDown(BaseEventData eventData){
        if (SeleccionPersonalizadoScript.filas > 10){
            SeleccionPersonalizadoScript.filas--;
        }
    }

    public void OnClickPlayPersonalizado(BaseEventData eventData){
        Dificultad.setPersonalizado(SeleccionPersonalizadoScript.filas, SeleccionPersonalizadoScript.columnas);
        SceneManager.LoadScene("Partida");

    }

    public void OnClickHistorial(BaseEventData eventData){
        Jugador.transform.position = new Vector3(19f,1.6f,12.64f);
        Persistencia.LoadPartidas();
    }

    public void OnClickEvaluanos(){
        Application.OpenURL("http://www.google.com");
    }

}
