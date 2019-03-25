using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class CasillaScripts : MonoBehaviour {

    public int fila; //número de fila de la casilla.
    public int columna; //número de columna de la casilla.
    public Material CasillaAbierta; //Textura casilla abierta.
    public Material CasillaAbiertaOn; //Textura casilla abierta cuando el jugador la está apuntando.
    public Material CasillaTapada; //Textura casilla tapada.
    public Material CasillaTapadaON; //Textura casilla tapada cuando el jugador la está apuntando.
    public GameObject[] Numeros3D; //Modelos 3D de los numeros, la interrogación y la bandera.
    public GameObject[] casillas; //Casillas físicas del escenario.
    public GameObject[] explosion; //Efecto explosión y sus componentes visuales.
    public GameObject[] fireworks; //Efectos de una victoria. 
    public AudioClip On; //Sonido al mirar una casilla.
    public AudioClip Click; // Sonido al clickear una casilla.
    public GameObject LetreroVictria; // Letrero de nueva puntuación maxima.

    private GameObject[] IExplosion; //Instancias de la explosión.
    private static GameObject[] INumero; //Instancias de los números.
    private GameObject[] IFireworks; //Instancias de los efectos de victoria.
    private Renderer myRenderer; //Textura de la casilla.
    private AudioSource source; //Recurso de audio.
    private GameObject ILetreroVictoria; // Instancia del letrero de nueva puntuación maxima.

    //Método que se ejecuta una sola vez al iniciar la escena.
    private void Start() {
        IExplosion = new GameObject[explosion.Length];
        INumero = new GameObject[Dificultad.filas * Dificultad.columnas];
        myRenderer = GetComponent<Renderer>();
        IFireworks = new GameObject[6];

    }

    //Método que se ejecuta el primer frame.
    private void Awake() {
        source = GetComponent<AudioSource>();
    }

    //Método que se ejecuta al clickear una casilla.
    public void Onclick(BaseEventData eventData) {
        if (!TableroScript.isCreated) {
            if (!TableroScript.isModoBandera) {
                TableroScript.tablero = new Tablero(Dificultad.filas, Dificultad.columnas, Dificultad.minas , fila, columna);
                TableroScript.isCreated = true;
                VisualCasilla();
                Comprobarpartida();
            }
        } else {
            if (!TableroScript.isModoBandera) {
                TableroScript.tablero.descubrirCasilla(fila, columna);
            } else {
                if (!TableroScript.tablero.getCasillas()[fila, columna].isDescubierta()) {
                    TableroScript.tablero.ponerBandera(fila, columna);
                } else {
                    TableroScript.tablero.descubrirCasilla(fila, columna);
                }
            }
            VisualCasilla();
            Comprobarpartida();
        }
    }

    // Comprueba si la partida se ha ganado, si es así se ejecuta lo que esta en el interior del if.
    private void Comprobarpartida() {
        if (TableroScript.tablero.isFinalizado() && !TableroScript.exploto) {
            TableroScript.tablero.setMinasRestantes(0);
            for (int i = 0; i < TableroScript.tablero.getFilas(); i++) {
                for (int j = 0; j < TableroScript.tablero.getColumnas(); j++) {
                    if (TableroScript.tablero.getCasillas()[i, j].isMina() && !TableroScript.tablero.getCasillas()[i, j].isBandera()) {
                        INumero[NumeroPos(i, j)] = GameObject.Instantiate(Numeros3D[0]);
                        INumero[NumeroPos(i, j)].transform.parent = casillas[NumeroPos(i, j)].transform;
                        INumero[NumeroPos(i, j)].transform.position = new Vector3(casillas[NumeroPos(i, j)].transform.position.x, casillas[NumeroPos(i, j)].transform.position.y, casillas[NumeroPos(i, j)].transform.position.z - 0.52f);
                    }
                }
            }
            casillas[NumeroPos(fila, columna)].GetComponent<Renderer>().material = CasillaAbierta;
            destruirScript();
            Partida partida = new Partida();
            partida.setDificultad(Dificultad.dificultad);
            partida.setCronometro(CronometroScript.cronometro);
            partida.getFecha()[0] = DateTime.Now.Year;
            partida.getFecha()[1] = DateTime.Now.Month;
            partida.getFecha()[2] = DateTime.Now.Day;
            partida.getFecha()[3] = DateTime.Now.Hour;
            partida.getFecha()[4] = DateTime.Now.Minute;
            Persistencia.SavePartidas(partida);
            MostrarFireworks();
            if (partida.isMaxima()) {
                MostrarLetrero();
            }
        }
    }

    //Convierte la posición en una matriz [n,m] a la posición de un arreglo [n*m].  
    private int NumeroPos(int x, int y) {
        return TableroScript.tablero.getColumnas() * x + y;
    }

    //Refresca la parte visual del tablero.
    public void VisualCasilla() {
        for (int i = 0; i < TableroScript.tablero.getFilas(); i++) {
            for (int j = 0; j < TableroScript.tablero.getColumnas(); j++) {
                if (TableroScript.tablero.getCasillas()[i, j].isDescubierta()) {
                    if (TableroScript.tablero.getCasillas()[i, j].isMina()) {
                        Explotar(casillas[NumeroPos(i, j)]);
                        destruirScript();
                    } else {
                        if (!TableroScript.Isimbolo[i, j]) {
                            if (TableroScript.tablero.getCasillas()[i, j].getVecinos() >= 1 && TableroScript.tablero.getCasillas()[i, j].getVecinos() <= 8) {
                                INumero[NumeroPos(i, j)] = GameObject.Instantiate(Numeros3D[TableroScript.tablero.getCasillas()[i, j].getVecinos()]);
                                TableroScript.Isimbolo[i, j] = true;
                                INumero[NumeroPos(i, j)].transform.parent = casillas[NumeroPos(i, j)].transform;
                                INumero[NumeroPos(i, j)].transform.position = new Vector3(casillas[NumeroPos(i, j)].transform.position.x - 1.4f, casillas[NumeroPos(i, j)].transform.position.y - 0.85f, casillas[NumeroPos(i, j)].transform.position.z + 0.25f);
                            }
                        }
                    }
                    casillas[NumeroPos(i, j)].GetComponent<Renderer>().material = CasillaAbierta;
                } else {
                    if (TableroScript.tablero.getCasillas()[i, j].isBandera()) {
                        if (!TableroScript.Isimbolo[i, j]) {
                            TableroScript.Isimbolo[i, j] = true;
                            INumero[NumeroPos(i, j)] = GameObject.Instantiate(Numeros3D[0]);
                            INumero[NumeroPos(i, j)].transform.parent = casillas[NumeroPos(i, j)].transform;
                            INumero[NumeroPos(i, j)].transform.position = new Vector3(casillas[NumeroPos(i, j)].transform.position.x, casillas[NumeroPos(i, j)].transform.position.y, casillas[NumeroPos(i, j)].transform.position.z - 0.52f);
                        }
                    } else if (TableroScript.tablero.getCasillas()[i, j].isInterrogacion()) {
                        Destroy(INumero[NumeroPos(i, j)]);
                        INumero[NumeroPos(i, j)] = GameObject.Instantiate(Numeros3D[9]);
                        INumero[NumeroPos(i, j)].transform.parent = casillas[NumeroPos(i, j)].transform;
                        INumero[NumeroPos(i, j)].transform.position = new Vector3(casillas[NumeroPos(i, j)].transform.position.x - 0.25f, casillas[NumeroPos(i, j)].transform.position.y - 0.05f, casillas[NumeroPos(i, j)].transform.position.z + 0.4f);
                    } else {
                        TableroScript.Isimbolo[i, j] = false;
                        Destroy(INumero[NumeroPos(i, j)]);
                    }
                }
            }
        }
        if (!TableroScript.isModoBandera && !TableroScript.tablero.getCasillas()[fila, columna].isBandera()) {
            casillas[NumeroPos(fila, columna)].GetComponent<Renderer>().material = CasillaAbiertaOn;
        }
        if (TableroScript.isModoBandera && !TableroScript.tablero.getCasillas()[fila, columna].isBandera() && !TableroScript.tablero.getCasillas()[fila, columna].isDescubierta()) {
            casillas[NumeroPos(fila, columna)].GetComponent<Renderer>().material = CasillaTapadaON;
        }
        if (TableroScript.isModoBandera && TableroScript.tablero.getCasillas()[fila, columna].isDescubierta()) {
            casillas[NumeroPos(fila, columna)].GetComponent<Renderer>().material = CasillaAbiertaOn;
        }
        if (!TableroScript.exploto) {
            source.PlayOneShot(Click, Ajustes.ajustes.volumenes.VolumenEfectos);
        }
    }

    //Crea la explosión en una casilla dada.
    public void Explotar(GameObject casilla) {
        for (int i = 0; i < explosion.Length; i++) {
            IExplosion[i] = GameObject.Instantiate(explosion[i]);
            IExplosion[i].transform.position = new Vector3(casilla.transform.localPosition.x - 2f, casilla.transform.localPosition.y + 1.2f, casilla.transform.localPosition.z + 5f);
            TableroScript.exploto = true;
        }
    }

    private void destruirScript() {
        for (int i = 0; i < TableroScript.tablero.getFilas() * TableroScript.tablero.getColumnas(); i++) {
            Destroy(casillas[i].GetComponent<CasillaScripts>());
        }
    }

    //Método que se ejecuta al mirar una casilla.
    public void EnterPointer() {
        if (TableroScript.tablero != null) {
            if (TableroScript.tablero.getCasillas()[fila, columna].isDescubierta()) {
                myRenderer.material = CasillaAbiertaOn;
            } else {
                myRenderer.material = CasillaTapadaON;
                source.PlayOneShot(On, Ajustes.ajustes.volumenes.VolumenEfectos);
            }
        } else {
            myRenderer.material = CasillaTapadaON;
            source.PlayOneShot(On, Ajustes.ajustes.volumenes.VolumenEfectos);
        }
    }

    //Método que se ejecuta al dejar de mirar una casilla.
    public void ExitPointer() {
        if (TableroScript.tablero != null) {
            if (TableroScript.tablero.getCasillas()[fila, columna].isDescubierta()) {
                myRenderer.material = CasillaAbierta;
            } else {
                myRenderer.material = CasillaTapada;
            }
        } else {
            myRenderer.material = CasillaTapada;
        }
    }

    //Muestra el letrero de nueva puntuación máxima.
    private void MostrarLetrero(){
        if (Dificultad.dificultad.Equals("Principiante")) {
            ILetreroVictoria = GameObject.Instantiate(LetreroVictria);
            //ILetreroVictoria.transform.position = new Vector3(-4.38f, 5f, 4.6f);
        } else if (Dificultad.dificultad.Equals("Intermedio")){

        } else{

        }
    }

    private void MostrarFireworks(){
        if (Dificultad.dificultad.Equals("Principiante")){
            IFireworks[0] = GameObject.Instantiate(fireworks[UnityEngine.Random.Range(0, 3)]);
            IFireworks[0].transform.position = new Vector3(5.8f, 5.8f, 3.8f);
            IFireworks[1] = GameObject.Instantiate(fireworks[UnityEngine.Random.Range(0, 3)]);
            IFireworks[1].transform.position = new Vector3(-4.3f, 5.8f, 3.8f);
            IFireworks[2] = GameObject.Instantiate(fireworks[UnityEngine.Random.Range(0, 3)]);
            IFireworks[2].transform.position = new Vector3(-4.3f, 2.1f, 3.8f);
            IFireworks[3] = GameObject.Instantiate(fireworks[UnityEngine.Random.Range(0, 3)]);
            IFireworks[3].transform.position = new Vector3(5.8f, 2.1f, 3.8f);
            IFireworks[4] = GameObject.Instantiate(fireworks[UnityEngine.Random.Range(0, 3)]);
            IFireworks[4].transform.position = new Vector3(-1.3f, 9.5f, 3.8f);
            IFireworks[5] = GameObject.Instantiate(fireworks[UnityEngine.Random.Range(0, 3)]);
            IFireworks[5].transform.position = new Vector3(2.7f, 9.5f, 3.8f);
        } else if (Dificultad.dificultad.Equals("Intermedio")){

        } else {

        }
    }

}

