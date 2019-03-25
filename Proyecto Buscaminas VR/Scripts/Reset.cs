using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Reset : MonoBehaviour {

    public Material TexturaFeliz;
    public Material TexturaTriste;
    public Material TexturaVictoria;
    public AudioClip SonidoOn;
    public AudioClip SonidoClick;

    private Renderer myRenderer;
    private AudioSource source;

    private void Start(){
        myRenderer = GetComponent<Renderer>();
    }

    private void Awake(){
        source = GetComponent<AudioSource>();
    }

    private void Update(){
        if (TableroScript.isCreated){
            if (TableroScript.tablero.isFinalizado() && TableroScript.exploto){
                myRenderer.material = TexturaTriste;
            }else if (TableroScript.tablero.isFinalizado() && !TableroScript.exploto){
                myRenderer.material = TexturaVictoria;
            }else{
                myRenderer.material = TexturaFeliz;
            }
        }
    }

    public void EnterPointer(){
        source.PlayOneShot(SonidoOn, Ajustes.ajustes.volumenes.VolumenEfectos);
    }

    public void ExitPointer(){

    }

    public void OnClick(BaseEventData eventData){
        TableroScript.Reset();
        source.PlayOneShot(SonidoClick, Ajustes.ajustes.volumenes.VolumenEfectos);
        SceneManager.LoadScene("Partida");
    }

}
