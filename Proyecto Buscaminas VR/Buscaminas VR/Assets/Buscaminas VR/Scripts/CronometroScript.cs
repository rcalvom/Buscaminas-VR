using UnityEngine;

public class CronometroScript : MonoBehaviour {

    public static Cronometro cronometro;
    private float time;

	private void Start () {
        cronometro = new Cronometro();
        gameObject.GetComponent<TextMesh>().text = "00:00";
    }
	
	private void Update () {
        if(TableroScript.tablero!=null)
            if (!TableroScript.tablero.isFinalizado()){
            time += Time.deltaTime;
            if (time >= 1.0f){
                time = 0;
                cronometro.SumarSegundo();
                gameObject.GetComponent<TextMesh>().text = cronometro.Cadena();
            }
        }    
	}
}
