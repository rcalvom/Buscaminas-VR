[System.Serializable]
public class Partida {
    private Cronometro tiempoResolucion;
    private string dificultad;
    private int[] fecha;
    private bool ismaxima;

    public Partida(Cronometro tiempoResolucion, string dificultad, int[] fecha) {
        this.tiempoResolucion = tiempoResolucion;
        this.dificultad = dificultad;
        this.fecha = fecha;
        this.ismaxima = false;
    }
    public Partida(){
        this.fecha = new int[5];
    }

    public void setCronometro(Cronometro tiempoResolucion){
        this.tiempoResolucion = tiempoResolucion;
    }

    public Cronometro getCronometro(){
        return this.tiempoResolucion;
    }

    public void setDificultad(string dificultad){
        this.dificultad = dificultad;
    }

    public string getDificultad() { 
        return this.dificultad;
    }

    public void setFecha(int[] fecha){
        this.fecha = fecha;
    }

    public int[] getFecha(){
        return this.fecha;
    }

    public bool isMaxima(){
        return this.ismaxima;
    }

    public void setMaxima(bool ismaxima){
        this.ismaxima = ismaxima;
    }
}
    
