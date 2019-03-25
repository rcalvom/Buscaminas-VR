using UnityEngine;

public static class TableroScript {

    public static Tablero tablero = null;
    public static bool isCreated = false;
    public static bool isModoBandera = false;
    public static bool[,] Isimbolo = new bool[Dificultad.filas, Dificultad.columnas];
    public static bool exploto = false;
    public static int minasrestantes = Dificultad.minas;
    public static GameObject[,] ICasillas;

    public static void Reset(){
        TableroScript.tablero = null;
        TableroScript.isCreated = false;
        TableroScript.exploto = false;
        TableroScript.isModoBandera = false;
    }

}
