using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Button = UnityEngine.UI.Button;

public class GameManager : MonoBehaviour
{
   
   [Header("Perguntas")]
   // Acessar a camada de Modelo (PerguntaSO)
   [SerializeField] private PerguntaSO perguntaAtual;
   
   // Acessar a camada de Visão 
   [SerializeField] private TextMeshProUGUI textoEnunciado;
   [SerializeField] private GameObject[] alternativasTMP;
   
   [Header("Sprites")]
   [SerializeField] private Sprite spriteRespostaCorreta;
   [SerializeField] private Sprite spriteRespostaIncorreta;

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
      // Desabilitar os botões de resposta para que novas respostas não sejam registradas
      DisableOptionsButtons();
      
      Image imgRespostaSelecionada = alternativasTMP[alternativaSelecionada].GetComponent<Image>();
      
      if (alternativaSelecionada == perguntaAtual.GetAlternativaCorreta())
      {
         // a alternativa selecionada está correta
         
         // alterar o sprite do botão selecionado pelo jogador, assumindo que esse botão representa
         // a alternativa correta para a questão avaliada
         changeButtonSprite(imgRespostaSelecionada, spriteRespostaCorreta);
      }
      else
      {
         // a alternativa selecionada está incorreta
         Image imgRespostaCorreta = alternativasTMP[perguntaAtual.GetAlternativaCorreta()].GetComponent<Image>();
         
         changeButtonSprite(imgRespostaSelecionada, spriteRespostaIncorreta);
         changeButtonSprite(imgRespostaCorreta, spriteRespostaCorreta);
      }
   }

   // Função utilizada para alterar os sprites de um botão de alternativa
   public void changeButtonSprite(Image img, Sprite sprite)
   {
      img.sprite = sprite;
   }

   // Função utilizada para desabilitar os botões de alternativas;
   public void DisableOptionsButtons()
   {
      for (int i = 0; i < alternativasTMP.Length; i++)
      {
         Button btn = alternativasTMP[i].GetComponent<Button>();
         btn.enabled = false;
      }
   }

  
}




