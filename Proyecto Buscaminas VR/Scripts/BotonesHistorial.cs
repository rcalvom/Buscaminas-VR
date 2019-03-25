using UnityEngine;
using UnityEngine.EventSystems;

public class BotonesHistorial : MonoBehaviour {
    public GameObject Texto;
    public static int Partida;

    private void Start() {
        Persistencia.LoadPartidas();
        Partida = 0;
        Actualizar();
    }

    private void Actualizar(){
        if (Persistencia.partidas !=null){
            Texto.GetComponent<TextMesh>().text = (Partida + 1) + ". " + Persistencia.partidas[Partida].getDificultad() + "\nTiempo resolucion: ";
            if (Persistencia.partidas[Partida].getCronometro().getMinutos()<10){
                Texto.GetComponent<TextMesh>().text = Texto.GetComponent<TextMesh>().text +"0";
            }
            Texto.GetComponent<TextMesh>().text = Texto.GetComponent<TextMesh>().text + Persistencia.partidas[Partida].getCronometro().getMinutos() + ":";
            if (Persistencia.partidas[Partida].getCronometro().getSegundos()<10){
                Texto.GetComponent<TextMesh>().text = Texto.GetComponent<TextMesh>().text + "0";
            }
            Texto.GetComponent<TextMesh>().text = Texto.GetComponent<TextMesh>().text + Persistencia.partidas[Partida].getCronometro().getSegundos() + "\nFecha: " + Persistencia.partidas[Partida].getFecha()[0] + "/";
            if (Persistencia.partidas[Partida].getFecha()[1]<10){
                Texto.GetComponent<TextMesh>().text = Texto.GetComponent<TextMesh>().text + "0";
            }
            Texto.GetComponent<TextMesh>().text = Texto.GetComponent<TextMesh>().text + Persistencia.partidas[Partida].getFecha()[1] + "/";
            if (Persistencia.partidas[Partida].getFecha()[2] < 10){
                Texto.GetComponent<TextMesh>().text = Texto.GetComponent<TextMesh>().text + "0";
            }
            Texto.GetComponent<TextMesh>().text = Texto.GetComponent<TextMesh>().text + Persistencia.partidas[Partida].getFecha()[2] + " ";
            if (Persistencia.partidas[Partida].getFecha()[3] < 10){
                Texto.GetComponent<TextMesh>().text = Texto.GetComponent<TextMesh>().text + "0";
            }

            Texto.GetComponent<TextMesh>().text = Texto.GetComponent<TextMesh>().text + Persistencia.partidas[Partida].getFecha()[3] + ":";
            if (Persistencia.partidas[Partida].getFecha()[4] < 10){
                Texto.GetComponent<TextMesh>().text = Texto.GetComponent<TextMesh>().text + "0";
            }
            Texto.GetComponent<TextMesh>().text = Texto.GetComponent<TextMesh>().text + Persistencia.partidas[Partida].getFecha()[4];
                
        }
    }

    public void OnClickUp(BaseEventData eventData){
        if (Persistencia.partidas != null){
            if (Partida < Persistencia.partidas.Count-1 ){
                Partida++;
                Actualizar();
            }
        }
    }

    public void OnClickDown(BaseEventData eventData){
        if (Persistencia.partidas != null){
            if (Partida !=0){
                Partida--;
                Actualizar();
            }
        }
    }



}
