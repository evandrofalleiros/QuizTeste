using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temporizador : MonoBehaviour
{
    
    [SerializeField] private float tempoMaximo;
    [SerializeField] private Slider slider;
    private float tempoAtual;

    
    void Start()
    {
        tempoAtual = 0f;
        slider.maxValue = tempoMaximo;
        slider.value = tempoAtual;
    }
    void Update()
    {
        tempoAtual += 1 * Time.deltaTime;
        slider.value = tempoAtual;
        if (tempoAtual > tempoMaximo)
        {
            Debug.Log("Chegou no fim");
            Debug.Log(tempoAtual);
            tempoAtual = 0f;
            slider.value = tempoAtual;
        }
    }
}