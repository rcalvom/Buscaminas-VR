using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class CasillaScript : MonoBehaviour{

    public int fila; //número de fila de la casilla.
    public int columna; //número de columna de la casilla.
    public Material CasillaAbierta; //Textura casilla abierta.
    public Material CasillaAbiertaOn; //Textura casilla abierta cuando el jugador la está apuntando.
    public Material CasillaTapada; //Textura casilla tapada.
    public Material CasillaTapadaON; //Textura casilla tapada cuando el jugador la está apuntando.
    public GameObject[] Numeros3D; //Modelos 3D de los numeros, la interrogación y la bandera.
    public GameObject[] explosion; //Efecto explosión y sus componentes visuales.
    public GameObject[] fireworks; //Efectos de una victoria. 
    public AudioClip On; //Sonido al mirar una casilla.
    public AudioClip Click; // Sonido al clickear una casilla.
    public GameObject[] LetreroVictoria; // Letrero de nueva puntuación maxima.

    private GameObject[] IExplosion; //Instancias de la explosión.
    private static GameObject[,] INumero; //Instancias de los números.
    private GameObject[] IFireworks; //Instancias de los efectos de victoria.
    private Renderer myRenderer; //Textura de la casilla.
    private AudioSource source; //Recurso de audio.
    private GameObject[] ILetreroVictoria; // Instancia del letrero de nueva puntuación maxima.
    

    //Método que se ejecuta una sola vez al ser instanciado el objeto.
    private void Start(){
        //Reservar espacio en memoria para instancias.
        IExplosion = new GameObject[explosion.Length];
        INumero = new GameObject[Dificultad.filas,Dificultad.columnas];
        IFireworks = new GameObject[5];
        ILetreroVictoria = new GameObject[2];
        source = GetComponent<AudioSource>();
        myRenderer = GetComponent<Renderer>();

        //Crear eventos:
        EventTrigger trigger = GetComponentInParent<EventTrigger>();

        //Evento al dar click.
        EventTrigger.Entry click = new EventTrigger.Entry { eventID = EventTriggerType.PointerClick };
        click.callback.AddListener((eventData) => { Onclick(); });
        trigger.triggers.Add(click);

        //Evento al mirar casilla.
        EventTrigger.Entry enter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        enter.callback.AddListener((eventData) => { EnterPointer(); });
        trigger.triggers.Add(enter);

        //evento al dejar de mirar casilla.
        EventTrigger.Entry exit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
        exit.callback.AddListener((eventData) => { ExitPointer(); });
        trigger.triggers.Add(exit);

    }

    //Método que se ejecuta al clickear una casilla.
    public void Onclick() { 
        if (TableroScript.tablero == null){
            if (TableroScript.isModoBandera) TableroScript.isModoBandera = false; 
            TableroScript.tablero = new Tablero(Dificultad.filas,Dificultad.columnas, Dificultad.minas, fila, columna);
        }else{
            if (TableroScript.isModoBandera && !TableroScript.tablero.getCasillas()[fila, columna].isDescubierta()) { 
                TableroScript.tablero.ponerBandera(fila, columna);
            }else{
                TableroScript.tablero.descubrirCasilla(fila, columna);
            }
        }
        VisualCasilla();
        ComprobarCasilla();
    }

    private void ComprobarCasilla(){
        if (TableroScript.tablero.isFinalizado() && !TableroScript.exploto){
            TableroScript.tablero.setMinasRestantes(0);
            for (int i = 0; i < TableroScript.tablero.getFilas(); i++) {
                for (int j = 0; j < TableroScript.tablero.getColumnas(); j++) {
                    if (TableroScript.tablero.getCasillas()[i, j].isMina() && !TableroScript.tablero.getCasillas()[i, j].isBandera()) {
                        INumero[i, j] = Instantiate(Numeros3D[0]);
                        INumero[i, j].transform.parent = TableroScript.ICasillas[i, j].transform;
                        INumero[i, j].transform.position = new Vector3(TableroScript.ICasillas[i, j].transform.position.x, TableroScript.ICasillas[i, j].transform.position.y, TableroScript.ICasillas[i, j].transform.position.z - 0.52f);
                    }
                }
            }
            TableroScript.ICasillas[fila, columna].GetComponent<Renderer>().material = CasillaAbierta;
            DestruirScript();
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
            if (partida.isMaxima()){
                MostrarLetrero();
            }
        }
    }

    private void MostrarLetrero() {
        ILetreroVictoria[0] = Instantiate(LetreroVictoria[0]);
        ILetreroVictoria[0].transform.localScale = new Vector3(Dificultad.columnas,Dificultad.filas,2f);
        ILetreroVictoria[0].transform.position = new Vector3(0, Dificultad.filas/2.0f + 0.5f, (float)Mathf.Max(Dificultad.filas, Dificultad.columnas) * 0.4f + 5f);
        ILetreroVictoria[1] = Instantiate(LetreroVictoria[1]);
        ILetreroVictoria[1].transform.position = new Vector3(0, Dificultad.filas / 2.0f + 0.5f, (float)Mathf.Max(Dificultad.filas, Dificultad.columnas) * 0.4f + 4f);
    }

    private void MostrarFireworks(){
        IFireworks[0] = Instantiate(fireworks[UnityEngine.Random.Range(0, 3)]);
        IFireworks[0].transform.position = new Vector3(-Dificultad.columnas/2.0f-2.5f, Dificultad.filas/2.0f, (float)Mathf.Max(Dificultad.filas, Dificultad.columnas) * 0.4f + 5f);
        IFireworks[1] = Instantiate(fireworks[UnityEngine.Random.Range(0, 3)]);
        IFireworks[1].transform.position = new Vector3(-(-Dificultad.columnas/2.0f-2.5f), Dificultad.filas/2.0f, (float)Mathf.Max(Dificultad.filas, Dificultad.columnas) * 0.4f + 5f);
        IFireworks[2] = Instantiate(fireworks[UnityEngine.Random.Range(0, 3)]);
        IFireworks[2].transform.position = new Vector3(-Dificultad.columnas / 2.0f - 2.5f, Dificultad.filas +2.5f, (float)Mathf.Max(Dificultad.filas, Dificultad.columnas) * 0.4f + 5f);
        IFireworks[3] = Instantiate(fireworks[UnityEngine.Random.Range(0, 3)]);
        IFireworks[3].transform.position = new Vector3(-(-Dificultad.columnas / 2.0f - 2.5f), Dificultad.filas +2.5f, (float)Mathf.Max(Dificultad.filas, Dificultad.columnas) * 0.4f + 5f);
        IFireworks[4] = Instantiate(fireworks[UnityEngine.Random.Range(0, 3)]);
        IFireworks[4].transform.position = new Vector3(0, Dificultad.filas +2.5f, (float)Mathf.Max(Dificultad.filas, Dificultad.columnas) * 0.4f + 5f);
    }

    //Método que se ejecuta al mirar una casilla.
    public void EnterPointer(){
        if (TableroScript.tablero == null){
            myRenderer.material = CasillaTapadaON;
            source.PlayOneShot(On, Ajustes.ajustes.volumenes.VolumenEfectos);
        }else{
            if (TableroScript.tablero.getCasillas()[fila, columna].isDescubierta()){
                myRenderer.material = CasillaAbiertaOn;
            }else{
                myRenderer.material = CasillaTapadaON;
                source.PlayOneShot(On, Ajustes.ajustes.volumenes.VolumenEfectos);
            }
        }
    }

    //Método que se ejecuta al dejar de mirar una casilla.
    public void ExitPointer(){
        if (TableroScript.tablero == null){
            myRenderer.material = CasillaTapada;
        }else{
            if (TableroScript.tablero.getCasillas()[fila, columna].isDescubierta()){
                myRenderer.material = CasillaAbierta;
            }else{
                myRenderer.material = CasillaTapada;
            }
        }
    }

    private void VisualCasilla(){
        for (int i = 0; i < Dificultad.filas; i++) { 
            for (int j = 0; j < Dificultad.columnas; j++) {
                if (TableroScript.tablero.getCasillas()[i, j].isDescubierta()) {
                    if (TableroScript.tablero.getCasillas()[i, j].isMina()) {
                        Explotar(TableroScript.ICasillas[i, j]);
                        source.PlayOneShot(Click, Ajustes.ajustes.volumenes.VolumenEfectos);
                        DestruirScript();
                    }else{
                        if (INumero[i, j] == null){
                            if (TableroScript.tablero.getCasillas()[i, j].getVecinos() >= 1 && TableroScript.tablero.getCasillas()[i, j].getVecinos() <= 8){
                                INumero[i,j] = Instantiate(Numeros3D[TableroScript.tablero.getCasillas()[i, j].getVecinos()]);
                                INumero[i,j].transform.parent = TableroScript.ICasillas[i,j].transform;
                                INumero[i,j].transform.position = new Vector3(TableroScript.ICasillas[i, j].transform.position.x - 1.4f, TableroScript.ICasillas[i, j].transform.position.y - 0.85f, TableroScript.ICasillas[i, j].transform.position.z + 0.25f);
                            }
                        }
                    }
                    TableroScript.ICasillas[i, j].GetComponent<Renderer>().material = CasillaAbierta;
                }else{
                    if (TableroScript.tablero.getCasillas()[i, j].isBandera()) {
                        if (INumero[i, j] == null) {
                            INumero[i, j] = Instantiate(Numeros3D[0]);
                            INumero[i, j].transform.parent = TableroScript.ICasillas[i, j].transform;
                            INumero[i,j].transform.position = new Vector3(TableroScript.ICasillas[i, j].transform.position.x, TableroScript.ICasillas[i, j].transform.position.y, TableroScript.ICasillas[i, j].transform.position.z - 0.52f);
                        }
                    }else if (TableroScript.tablero.getCasillas()[i, j].isInterrogacion()) {
                        Destroy(INumero[i,j]);
                        INumero[i, j] = Instantiate(Numeros3D[9]);
                        INumero[i, j].transform.parent = TableroScript.ICasillas[i, j].transform;
                        INumero[i,j].transform.position = new Vector3(TableroScript.ICasillas[i, j].transform.position.x - 0.25f, TableroScript.ICasillas[i, j].transform.position.y - 0.05f, TableroScript.ICasillas[i, j].transform.position.z + 0.4f);
                    }else{
                        Destroy(INumero[i,j]);
                    }
                }
            }
        }
        if (!TableroScript.isModoBandera && !TableroScript.tablero.getCasillas()[fila, columna].isBandera())
            TableroScript.ICasillas[fila, columna].GetComponent<Renderer>().material = CasillaAbiertaOn;
        if (TableroScript.isModoBandera && !TableroScript.tablero.getCasillas()[fila, columna].isBandera() && !TableroScript.tablero.getCasillas()[fila, columna].isDescubierta()) 
            TableroScript.ICasillas[fila, columna].GetComponent<Renderer>().material = CasillaTapadaON;
        if (TableroScript.isModoBandera && TableroScript.tablero.getCasillas()[fila, columna].isDescubierta()) 
            TableroScript.ICasillas[fila, columna].GetComponent<Renderer>().material = CasillaAbiertaOn;
        if(!TableroScript.exploto)
        source.PlayOneShot(Click, Ajustes.ajustes.volumenes.VolumenEfectos);
    }

    //Crea la explosión en una casilla dada.
    public void Explotar(GameObject casilla){
        for (int i =0;i<Dificultad.filas;i++){
            for (int j =0;j<Dificultad.columnas;j++){
                TableroScript.ICasillas[i, j].GetComponent<Rigidbody>().useGravity = true;
                TableroScript.ICasillas[i, j].GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        for (int i = 0; i < explosion.Length; i++){
            IExplosion[i] = Instantiate(explosion[i]);
            IExplosion[i].transform.position = new Vector3(casilla.transform.localPosition.x - 2f, casilla.transform.localPosition.y + 1.2f, casilla.transform.localPosition.z + 5f);
        }
        TableroScript.exploto = true;
    }

    //Destruye el script para que el jugador no pueda seguir interactuando con el tablero
    private void DestruirScript(){
        for (int i = 0; i <Dificultad.filas; i++) {
            for (int j = 0; j <Dificultad.columnas; j++) {
                Destroy(TableroScript.ICasillas[i, j].GetComponent<CasillaScript>());
            }
        }
    }


}

