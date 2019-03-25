using UnityEngine;
using UnityEngine.EventSystems;

public class Boton : MonoBehaviour {

    public Material TexturaEnter;
    public Material TexturaExit;
    public AudioClip SonidoEnter;
    public AudioClip SonidoClick;

    private AudioSource audioSourceComponent;
    private Renderer rendererComponent;

	private void Start () {
        rendererComponent = GetComponent<Renderer>();
	}

    private void Awake(){
        audioSourceComponent = GetComponent<AudioSource>();
    }

    public void EnterPointer(){
        rendererComponent.material = TexturaEnter;
        audioSourceComponent.PlayOneShot(SonidoEnter, Ajustes.ajustes.volumenes.VolumenEfectos);
    }

    public void ExitPointer(){
        rendererComponent.material = TexturaExit;
    }

    public void OnClick(BaseEventData eventData){
        audioSourceComponent.PlayOneShot(SonidoClick, Ajustes.ajustes.volumenes.VolumenEfectos);
    }

}
