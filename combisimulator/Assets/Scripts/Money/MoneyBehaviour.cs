using System;
using UnityEngine;

using System.Collections.Generic;
using Random = UnityEngine.Random;

public class MoneyBehaviour : MonoBehaviour
{

    
    List<float> moneyPassenger = new List<float>() {5.0f, 2.0f, 10f, 3.0f , 1.0f};
    List<float> PrecioPassenger = new List<float>() {5.0f, 2.0f, 0.5f, 2.5f, 3.0f , 1.0f};


    void OnEnable()
    {
        float change = ChangeMoney();
        SeleccionarPasajeAleatorio(change);
        Debug.Log(change);
    }

    private void SeleccionarPasajeAleatorio(float change)
    {
        int indexrandom = Random.Range(0, PrecioPassenger.Count);
        
        float price = PrecioPassenger[indexrandom];
        
        Debug.Log($"El pasaje seleccionado al azar es:**${price}**");

        if (price == change)
        {
            Debug.Log("Pasaje exacto");
        }
    }

    float ChangeMoney()
    {
        int indexrandom = Random.Range(0, moneyPassenger.Count);
        
        return moneyPassenger[indexrandom];
    }
}
