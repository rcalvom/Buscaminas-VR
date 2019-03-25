using UnityEngine;

public class ActualizarVolumenes : MonoBehaviour {

    public GameObject Reproductor;

	private void Start () {
        Persistencia.LoadAjustes();
        Reproductor.GetComponent<AudioSource>().volume = Ajustes.ajustes.volumenes.VolumenMusica;
    }

    private void Update(){
        if (TableroScript.exploto){
            Destroy(Reproductor);
        }
    }


}
