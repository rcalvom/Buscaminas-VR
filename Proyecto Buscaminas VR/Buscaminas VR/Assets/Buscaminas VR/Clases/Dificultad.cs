
public static class Dificultad {
    public static int filas;
    public static int columnas;
    public static int minas;
    public static string dificultad;

    public static void setPrincipiante(){
        Dificultad.filas = 8;
        Dificultad.columnas = 8;
        Dificultad.minas = 10;
        Dificultad.dificultad = "Principiante.";
    }

    public static void setIntermedio(){
        Dificultad.filas = 16;
        Dificultad.columnas = 16;
        Dificultad.minas = 40;
        Dificultad.dificultad = "Intermedio.";
    }

    public static void setExperto(){
        Dificultad.filas = 16;
        Dificultad.columnas = 30;
        Dificultad.minas = 99;
        Dificultad.dificultad = "Experto.";
    }

    public static void setPersonalizado(int filas, int columnas){
        Dificultad.filas = filas;
        Dificultad.columnas = columnas;
        Dificultad.minas = (int)((double) filas * columnas * 0.15);
        Dificultad.dificultad = "Personalizado " + filas + " x " + columnas + ".";
    }

}
