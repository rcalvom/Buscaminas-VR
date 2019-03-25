using UnityEngine;

public class Tablero{

    private int filas;
    private int columnas;
    private int minas;
    private int minasRestantes;
    private int casillasLimpias;
    private bool finalizado;
    private Casilla[,] casillas;

    public Tablero(int filas, int columnas, int minas){
        this.filas = filas;
        this.columnas = columnas;
        this.minas = minas;
        this.casillasLimpias = filas * columnas - minas;
        this.finalizado = false;
        this.minasRestantes = minas;
        this.casillas = new Casilla[filas,columnas];
        for (int i = 0; i < filas; i++) { 
            for (int j = 0; j < columnas; j++){
                this.casillas[i,j] = new Casilla(false);
            }
        }
        for (int i = 0; i < minas; i++){
            int f = Random.Range(0, filas);
            int c = Random.Range(0, columnas);
            if (!casillas[f,c].isMina()){
                casillas[f,c].setMina(true);
            }else{
                i--;
            }
        }
        for (int i = 0; i < filas; i++){
            for (int j = 0; j < columnas; j++){
                casillas[i,j].setVecinos(vecinos(i, j));
            }
        }

    }

    public Tablero(int filas, int columnas, int minas, int fila, int columna){
        this.filas = filas;
        this.columnas = columnas;
        this.minas = minas;
        this.casillasLimpias = filas * columnas - minas;
        this.finalizado = false;
        this.minasRestantes = minas;
        this.casillas = new Casilla[filas,columnas];
        for (int i = 0; i < filas; i++){
            for (int j = 0; j < columnas; j++){
                this.casillas[i,j] = new Casilla(false);
            }
        }
        for (int i = 0; i < minas; i++){
            int f = Random.Range(0,filas);
            int c = Random.Range(0,columnas);
            if (!casillas[f,c].isMina() && f != fila && c != columna){
                casillas[f,c].setMina(true);
            }else{
                i--;
            }
        }
        for (int i = 0; i < filas; i++){
            for (int j = 0; j < columnas; j++){
                casillas[i,j].setVecinos(vecinos(i, j));
            }
        }
        descubrirCasilla(fila, columna);
    }

    public int vecinos(int i, int j){
        int vecinos = 0;
        if ((i == 0 || i == filas - 1) && (j == 0 || j == columnas - 1)){
            int df = (i == 0) ? 1 : -1, dc = (j == 0) ? 1 : -1;
            if (casillas[i, j + dc].isMina())
                vecinos++;
            if (casillas[i + df, j].isMina())
                vecinos++;
            if (casillas[i + df, j + dc].isMina())
                vecinos++;
            return vecinos;
        }
        if (j == 0 || j == columnas - 1){
            int dc = (j == 0) ? 1 : -1;
            for (int k = -1; k < 2; k++){
                if (casillas[i + k,j].isMina())
                    vecinos++;
                if (casillas[i + k,j + dc].isMina())
                    vecinos++;
            }
            return vecinos;
        }
        if (i == 0 || i == filas - 1){
            int df = (i == 0) ? 1 : -1;
            for (int k = -1; k < 2; k++){
                if (casillas[i,j + k].isMina())
                    vecinos++;
                if (casillas[i + df,j + k].isMina())
                    vecinos++;
            }
            return vecinos;
        }

        for (int k = -1; k < 2; k++){
            for (int s = -1; s < 2; s++){
                if (casillas[i + k,j + s].isMina())
                    vecinos++;
            }
        }
        return vecinos;
    }

    public void descubrirCasilla(int fila, int columna){
        if (!casillas[fila,columna].isBandera() && !casillas[fila,columna].isInterrogacion()){
            if (casillas[fila,columna].isMina()){
                casillas[fila,columna].setDescubierta(true);
                finalizado = true;
                //descubrirTodo();
                //Explosion, resultados juego-Partida;
            }else if (casillas[fila,columna].getVecinos() == 0 && !casillas[fila,columna].isDescubierta()){
                descubrirRecursivo(fila, columna);
            }else if (banderasVecinas(fila, columna) == casillas[fila,columna].getVecinos() && casillas[fila,columna].isDescubierta()){
                descubrirVecinos(fila, columna);
            }else if (!casillas[fila,columna].isDescubierta()){
                casillas[fila,columna].setDescubierta(true);
            }
        }
        if (contarCasillasDescubiertas() == casillasLimpias){
            finalizado = true;
            //partida ganada 
        }
    }

    public void ponerBandera(int fila, int columna){
        if (!casillas[fila,columna].isBandera() && !casillas[fila,columna].isInterrogacion() && !casillas[fila,columna].isDescubierta()){
            casillas[fila,columna].setBandera(true);
            minasRestantes--;
        }else if (casillas[fila,columna].isBandera() && !casillas[fila,columna].isDescubierta()){
            casillas[fila,columna].setBandera(false);
            casillas[fila,columna].setInterrogacion(true);
            minasRestantes++;
        }else if (casillas[fila,columna].isInterrogacion() && !casillas[fila,columna].isDescubierta()){
            casillas[fila,columna].setInterrogacion(false);
        }
        //actualizar HUD MINAS
    }

    public int contarCasillasDescubiertas(){
        int descubiertas = 0;
        for (int i = 0; i < filas; i++){
            for (int j = 0; j < columnas; j++){
                if (casillas[i,j].isDescubierta())
                    descubiertas++;
            }
        }
        return descubiertas;
    }
    
    public void descubrirVecinos(int fila, int columna){
        if ((fila == 0 || fila == filas - 1) && (columna == 0 || columna == columnas - 1)){
            int df = (fila == 0) ? 1 : -1, dc = (columna == 0) ? 1 : -1;
            if (!casillas[fila,columna + dc].isDescubierta()) { descubrirCasilla(fila, columna + dc); }
            if (!casillas[fila + df,columna].isDescubierta()) { descubrirCasilla(fila + df, columna); }
            if (!casillas[fila + df,columna + dc].isDescubierta()) { descubrirCasilla(fila + df, columna + dc); }
        }else if (columna == 0 || columna == columnas - 1){
            int dc = (columna == 0) ? 1 : -1;
            for (int k = -1; k < 2; k++){
                if (!casillas[fila + k,columna].isDescubierta()) { descubrirCasilla(fila + k, columna); }
                if (!casillas[fila + k,columna + dc].isDescubierta()) { descubrirCasilla(fila + k, columna + dc); }
            }
        }else if (fila == 0 || fila == filas - 1){
            int df = (fila == 0) ? 1 : -1;
            for (int k = -1; k < 2; k++){
                if (!casillas[fila,columna + k].isDescubierta()) { descubrirCasilla(fila, columna + k); }
                if (!casillas[fila + df,columna + k].isDescubierta()) { descubrirCasilla(fila + df, columna + k); }
            }
        }else{
            for (int k = -1; k < 2; k++){
                for (int s = -1; s < 2; s++){
                    if (!casillas[fila + k,columna + s].isDescubierta()) { descubrirCasilla(fila + k, columna + s); }
                }
            }
        }
    }

    public void descubrirRecursivo(int fila, int columna){
        if (casillas[fila,columna].getVecinos() != 0 && !casillas[fila,columna].isDescubierta()){
            casillas[fila,columna].setDescubierta(true);
        }else if (casillas[fila,columna].getVecinos() == 0 && !casillas[fila,columna].isDescubierta()){
            casillas[fila,columna].setDescubierta(true);
            if ((fila == 0 || fila == filas - 1) && (columna == 0 || columna == columnas - 1)){
                int df = (fila == 0) ? 1 : -1, dc = (columna == 0) ? 1 : -1;
                if (!casillas[fila,columna + dc].isDescubierta()) { descubrirRecursivo(fila, columna + dc); }
                if (!casillas[fila + df,columna].isDescubierta()) { descubrirRecursivo(fila + df, columna); }
                if (!casillas[fila + df,columna + dc].isDescubierta()) { descubrirRecursivo(fila + df, columna + dc); }
            }else if (columna == 0 || columna == columnas - 1){
                int dc = (columna == 0) ? 1 : -1;
                for (int i = -1; i < 2; i++){
                    if (!casillas[fila + i,columna].isDescubierta()) { descubrirRecursivo(fila + i, columna); }
                    if (!casillas[fila + i,columna + dc].isDescubierta()) { descubrirRecursivo(fila + i, columna + dc); }
                }
            }else if (fila == 0 || fila == filas - 1){
                int df = (fila == 0) ? 1 : -1;
                for (int i = -1; i < 2; i++){
                    if (!casillas[fila,columna + i].isDescubierta()) { descubrirRecursivo(fila, columna + i); }
                    if (!casillas[fila + df,columna + i].isDescubierta()) { descubrirRecursivo(fila + df, columna + i); }
                }
            }else{
                for (int i = -1; i < 2; i++){
                    for (int j = -1; j < 2; j++){
                        if (!casillas[fila + i,columna + j].isDescubierta()) { descubrirRecursivo(fila + i, columna + j); }
                    }
                }
            }
        }
    }

    public void descubrirTodo(){
        for (int i = 0; i < filas; i++){
            for (int j = 0; j < columnas; j++){
                casillas[i,j].setDescubierta(true);
            }
        }
    }

    public int banderasVecinas(int i, int j){
        int banderas = 0;
        if ((i == 0 || i == filas - 1) && (j == 0 || j == columnas - 1)){
            int df = (i == 0) ? 1 : -1, dc = (j == 0) ? 1 : -1;
            if (casillas[i,j + dc].isBandera())
                banderas++;
            if (casillas[i + df,j].isBandera())
                banderas++;
            if (casillas[i + df,j + dc].isBandera())
                banderas++;
            return banderas;
        }
        if (j == 0 || j == columnas - 1){
            int dc = (j == 0) ? 1 : -1;
            for (int k = -1; k < 2; k++){
                if (casillas[i + k,j].isBandera())
                    banderas++;
                if (casillas[i + k,j + dc].isBandera())
                    banderas++;
            }
            return banderas;
        }
        if (i == 0 || i == filas - 1){
            int df = (i == 0) ? 1 : -1;
            for (int k = -1; k < 2; k++){
                if (casillas[i,j + k].isBandera())
                    banderas++;
                if (casillas[i + df,j + k].isBandera())
                    banderas++;
            }
            return banderas;
        }
        for (int k = -1; k < 2; k++){
            for (int s = -1; s < 2; s++){
                if (casillas[i + k,j + s].isBandera())
                    banderas++;
            }
        }
        return banderas;
    }

    public void resetearTablero(){
        this.casillasLimpias = filas * columnas - minas;
        this.minasRestantes = minas;
        for (int i = 0; i < filas; i++){
            for (int j = 0; j < columnas; j++){
                casillas[i,j].setDescubierta(false);
            }

        }
    }

    public bool isFinalizado(){
        return this.finalizado;
    }

    public void setFinalizado(bool finalizado){
        this.finalizado = finalizado;
    }

    public int getFilas(){
        return this.filas;
    }

    public void setFilas(int filas){
        this.filas = filas;
    }

    public int getColumnas(){
        return this.columnas;
    }

    public void setColumnas(int columnas){
        this.columnas = columnas;
    }

    public int getMinas(){
        return this.minas;
    }

    public void setMinas(int minas){
        this.minas = minas;
    }

    public int getMinasRestantes(){
        return this.minasRestantes;
    }

    public void setMinasRestantes(int minasRestantes){
        this.minasRestantes = minasRestantes;
    }

    public Casilla[,] getCasillas(){
        return this.casillas;
    }

    public void setCasillas(Casilla[,] casillas){
        this.casillas = casillas;
    }

    public int getCasillasLimpias(){
        return this.casillasLimpias;
    }

    public void setCasillasLimpias(int casillaslimpias){
        this.casillasLimpias = casillaslimpias;
    }
}
