using UnityEngine;
using UnityEngine.EventSystems;

public class BotonesVolumen : MonoBehaviour {

    public GameObject Jugador;
    public GameObject Reproductor;
    public GameObject NumeroVolumen;
    public GameObject NumeroMúsica;

    public void OnclickMusicaDown(BaseEventData eventData){
        Volumenes.bajarVolumenMusica();
        Reproductor.GetComponent<AudioSource>().volume = Ajustes.ajustes.volumenes.VolumenMusica;
    }

    public void OnclickMusicaUp(BaseEventData eventData){
        Volumenes.subirVolumenMusica();
        Reproductor.GetComponent<AudioSource>().volume = Ajustes.ajustes.volumenes.VolumenMusica;
    }

    public void OnclickEfectosDown(BaseEventData eventData){
        Volumenes.bajarVolumenEfectos();
    }

    public void OnclickEfectosUp(BaseEventData eventData){
        Volumenes.subirVolumenEfectos();
    }

    public void OnClickVovler(BaseEventData eventData){
        Jugador.transform.position = new Vector3(19f, 1.6f, 25f);
        Persistencia.SaveAjustes();
    }

    public void UpdateNumeros(BaseEventData eventData){
        NumeroVolumen.GetComponent<TextMesh>().text = (Mathf.RoundToInt(Ajustes.ajustes.volumenes.VolumenEfectos*100.0f))+"%";
        NumeroMúsica.GetComponent<TextMesh>().text = (Mathf.RoundToInt(Ajustes.ajustes.volumenes.VolumenMusica * 100.0f)) + "%";
    }

}
