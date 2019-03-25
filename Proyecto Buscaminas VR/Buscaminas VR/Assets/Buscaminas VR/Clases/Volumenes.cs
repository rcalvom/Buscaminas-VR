
[System.Serializable]
public class Volumenes{
    public float VolumenMusica;
    public float VolumenEfectos;

    public Volumenes(){
        this.VolumenEfectos = 1.0f;
        this.VolumenMusica = 1.0f;
    }

    public static void bajarVolumenMusica(){
        if (Ajustes.ajustes.volumenes.VolumenMusica >= 0f){
            Ajustes.ajustes.volumenes.VolumenMusica -= 0.1f;
        }
    }

    public static void subirVolumenMusica(){
        if (Ajustes.ajustes.volumenes.VolumenMusica < 1f){
            Ajustes.ajustes.volumenes.VolumenMusica += 0.1f;
        }
    }

    public static void bajarVolumenEfectos(){
        if (Ajustes.ajustes.volumenes.VolumenEfectos >= 0f){
            Ajustes.ajustes.volumenes.VolumenEfectos -= 0.1f;
        }
    }

    public static void subirVolumenEfectos(){
        if(Ajustes.ajustes.volumenes.VolumenEfectos < 1f){
            Ajustes.ajustes.volumenes.VolumenEfectos += 0.1f;
        }
    }

}