using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   // Acessar a camada de Modelo (PerguntaSO)
   [SerializeField] private PerguntaSO perguntaAtual;
   
   // Acessar a camada de Visão 
   [SerializeField] private TextMeshProUGUI textoEnunciado;
   [SerializeField] private GameObject[] alternativasTMP;

   public void Start()
   {
      // Popular o texto do enunciado
      textoEnunciado.SetText(perguntaAtual.GetEnunciado());
      
      // Popular os textos para as 4 alternativas
      string[] alternativas = perguntaAtual.GetAlternativas();
      
      for (int i = 0; i < alternativasTMP.Length; i++)
      {
         // Capturar cada caixa de texto que encontra-se "dentro" dos botões
         // Cada botão está sendo tratado como um GameObject 
         TextMeshProUGUI ta = alternativasTMP[i].GetComponentInChildren<TextMeshProUGUI>();
         // Alterar o texto de cada "caixa de texto" que encontra-se dentro dos botões
         ta.SetText(alternativas[i]);
      }
   }

   public void HandleOption(int alternativaSelecionada)
   {
      if (alternativaSelecionada == perguntaAtual.GetAlternativaCorreta())
      {
         // a alternativa selecionada está correta
         Debug.Log("Show... parabéns!");   
      }
      else
      {
         // a alternativa selecionada está correta
         Debug.Log("Erroouuuuuuuuu (Fausto Silva)");
      }
   }
}




