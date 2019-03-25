using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class Persistencia{
    public static List<Partida> partidas = new List<Partida>();

    public static void SavePartidas(Partida partida) {
        if (File.Exists(Application.persistentDataPath + "/partidas.mw")){
            LoadPartidas();
            Persistencia.maxima(partida);
        }else{
            partida.setMaxima(true);
        }
        Persistencia.partidas.Add(partida);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(Application.persistentDataPath + "/partidas.mw",FileMode.Create);
        bf.Serialize(file, partidas);
        file.Close();
    }

    public static void LoadPartidas(){
        if (File.Exists(Application.persistentDataPath + "/partidas.mw")){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/partidas.mw", FileMode.Open);
            Persistencia.partidas = (List<Partida>)bf.Deserialize(file);
            file.Close();
        }
    }

    public static void SaveAjustes(){
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(Application.persistentDataPath + "/ajustes.mw", FileMode.Create);
        bf.Serialize(file, Ajustes.ajustes);
        file.Close();
    }

    public static void LoadAjustes(){
        if (File.Exists(Application.persistentDataPath + "/ajustes.mw")){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/ajustes.mw", FileMode.Open);
            Ajustes.ajustes = (Ajustes)bf.Deserialize(file);
            file.Close();
        }else{
            Ajustes.ajustes = new Ajustes();
        }
    }

    private static void maxima(Partida partida){
        foreach (Partida p in partidas) {
            if (p.isMaxima()) {
                if (p.getCronometro().getMinutos() > partida.getCronometro().getMinutos() || (p.getCronometro().getMinutos() == partida.getCronometro().getMinutos() && p.getCronometro().getSegundos() > partida.getCronometro().getSegundos())){
                    p.setMaxima(false);
                    partida.setMaxima(true);
                    break;
                }
            }
        }
    }
}
