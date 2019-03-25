using UnityEngine.EventSystems;
using UnityEngine;

public class BotonModo : MonoBehaviour {

    public Material textureON;
    public Material textureOFF;
    public AudioClip SonidoOn;
    public AudioClip SonidoClick;
    public GameObject bandera;

    private Renderer myRenderer;
    private AudioSource source;
    private GameObject Ibandera;

    private void Start(){
        myRenderer = GetComponent<Renderer>();
        source = GetComponent<AudioSource>();
    }

    public void EnterPointer(){
        source.PlayOneShot(SonidoOn, Ajustes.ajustes.volumenes.VolumenEfectos);
    }

    public void ExitPointer(){

    }

    public void OnClick(BaseEventData baseEventData){
        if (TableroScript.isModoBandera) {
            TableroScript.isModoBandera = false;
            Destroy(Ibandera);
            myRenderer.material = textureON;
        }else{
            TableroScript.isModoBandera = true;
            myRenderer.material = textureOFF;
            Ibandera = Instantiate(bandera);
            Ibandera.transform.parent = gameObject.transform;
            Ibandera.transform.position = gameObject.transform.position;
            Ibandera.transform.Rotate(0f, 90f, 0f);
        }
        source.PlayOneShot(SonidoClick, Ajustes.ajustes.volumenes.VolumenEfectos);
    }
}
