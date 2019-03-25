using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casilla {

    private bool mina;
    private bool descubierta;
    private int vecinos;
    private bool bandera;
    private bool interrogacion;

    public Casilla(bool mina){
        this.mina = mina;
        this.descubierta = false;
        this.interrogacion = false;
        this.bandera = false;
    }

    public bool isMina(){
        return this.mina;
    }

    public void setMina(bool mina){
        this.mina = mina;
    }

    public bool isDescubierta(){
        return this.descubierta;
    }

    public void setDescubierta(bool descubierta){
        this.descubierta = descubierta;
    }

    public int getVecinos(){
        return this.vecinos;
    }

    public void setVecinos(int vecinos){
        this.vecinos = vecinos;
    }

    public bool isBandera(){
        return bandera;
    }
    
    public void setBandera(bool bandera){
        this.bandera = bandera;
    }

    public bool isInterrogacion(){
        return interrogacion;
    }

    public void setInterrogacion(bool interrogacion){
        this.interrogacion = interrogacion;
    }

}
