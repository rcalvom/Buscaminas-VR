using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cronometro {

    private int minutos;
    private int segundos;

    public Cronometro(){
        this.minutos = 0;
        this.segundos = 0;
    }

    public void setMinutos(int minutos){
        this.minutos = minutos;
    }

    public void setSegundos(int segundos){
        this.segundos = segundos;
    }

    public int getMinutos() {
        return this.minutos;
    }

    public int getSegundos(){
        return this.segundos;
    }

    public void SumarSegundo(){
        if (this.segundos == 59){
            this.minutos++;
            this.segundos = 0;
        }else{
            this.segundos++;
        }
    }

    public string Cadena(){
        if (this.minutos <= 9 && this.segundos<=9){
            return "0" + this.minutos + ":" + "0" + this.segundos;
        }else if (this.minutos > 9 && this.segundos <= 9) {
            return this.minutos + ":" + "0" + this.segundos;
        }else if (this.minutos <= 9 && this.segundos > 9){
            return "0" + this.minutos + ":" + this.segundos;
        }else{
            return this.minutos + ":" + this.segundos;
        }

    }
}
