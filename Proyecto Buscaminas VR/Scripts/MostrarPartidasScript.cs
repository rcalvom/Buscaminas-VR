using UnityEngine;
using UnityEngine.EventSystems;

public class MostrarPartidasScript : MonoBehaviour {

    public GameObject TextoPartidas;

    public void OnClickNext(BaseEventData eventData){
        int size = Persistencia.partidas.Count;
        for (int i = 0; i < size; i++) { 

        }
        
        
    }

    public void OnClickBack(BaseEventData eventData){

    }

}
