using UnityEngine.Advertisements;
using UnityEngine;

public class Anuncios : MonoBehaviour {

	private void Start () {
        Anuncios.MostrarAnuncio();
	}
	
    public static void MostrarAnuncio(){
        if (Advertisement.IsReady()){
            Advertisement.Show();
        }
    }

}
