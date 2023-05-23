using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using Button = UnityEngine.UI.Button;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{
   
   [Header("Perguntas")]
   // Acessar a camada de Modelo (PerguntaSO)
   [SerializeField] private PerguntaSO[] perguntas;
   private PerguntaSO perguntaAtual;
   private int indexQuestaoAtual;
   private int alternativaSelecionada;

   // Acessar a camada de Visão 
   [SerializeField] private TextMeshProUGUI textoEnunciado;
   [SerializeField] private GameObject[] alternativasTMP;
   
   [Header("Sprites")]
   [SerializeField] private Sprite spriteRespostaCorreta;
   [SerializeField] private Sprite spriteRespostaIncorreta;
   [SerializeField] private Sprite spriteRespostaDefault;

   [Header("Overlays")] 
   [SerializeField] private Object overlayResposta;
   [SerializeField] private Object caixaTextoRetorno;
   [SerializeField] private TextMeshProUGUI textoRetornoIntro;
   [SerializeField] private TextMeshProUGUI textoRespostaCorreta;
   [SerializeField] private Sprite spriteFundoRespCorreta;
   [SerializeField] private Sprite spriteFundoRespIncorreta;
   
   private int acertos;
   private bool estaRespondendo;

   private Temporizador temporizador;
   public void Start()
   {
      // Registrando para receber a chamada de volta usando o método RegistrarTempoMaximoAtingido()
      temporizador = GetComponent<Temporizador>();
      temporizador.RegistrarParada(OnParadaTimer);

      indexQuestaoAtual = -1;
      acertos = 0;

      CarregarQuestao();
   }

   public void CarregarQuestao()
   {
      indexQuestaoAtual += 1;
      alternativaSelecionada = -1;
      
      DesabilitarOverlayResposta();
      ReiniciarBotoesResposta();
      
      if (indexQuestaoAtual < perguntas.Length)
      {
         perguntaAtual = perguntas[indexQuestaoAtual];
         temporizador.Zerar();
         estaRespondendo = true;
      
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
      else
      {
         // finalizar jogo
         FinalizarJogo();
      }
      
   }
   public void HandleOption(int alternativa)
   {
      alternativaSelecionada = alternativa;
      
      // Desabilitar os botões de resposta para que novas respostas não sejam registradas
      DesabilitarBotoesResposta();
      PararTimer();
   }

   void FinalizarJogo()
   {
      SairDoQuiz();
   }

   // Função utilizada para alterar os sprites de um botão de alternativa
   public void MudarSpriteBotao(Image img, Sprite sprite)
   {
      img.sprite = sprite;
   }

   // Função utilizada para desabilitar os botões de alternativas;
   public void DesabilitarBotoesResposta()
   {
      for (int i = 0; i < alternativasTMP.Length; i++)
      {
         Button btn = alternativasTMP[i].GetComponent<Button>();
         btn.enabled = false;
      }
   }
   
   // Função utilizada para reinicializar o estado original dos botões de resposta
   public void ReiniciarBotoesResposta()
   {
      for (int i = 0; i < alternativasTMP.Length; i++)
      {
         Button btn = alternativasTMP[i].GetComponent<Button>();
         Image imgBtn = btn.GetComponent<Image>();
         
         btn.enabled = true;
         MudarSpriteBotao(imgBtn, spriteRespostaDefault);
      }
   }
   
   public void DesabilitarOverlayResposta()
   {
      overlayResposta.GetComponent<Canvas>().gameObject.SetActive(false);
   }
   
   public void MostrarOverlayResposta()
   {
      overlayResposta.GetComponent<Canvas>().gameObject.SetActive(true);
   }

   void PararTimer()
   {
      temporizador.Parar();
   }

   void MostrarOverlayRespostaCorreta()
   {
      MostrarOverlayResposta();
      
      textoRetornoIntro.SetText("Parabéns, pequeno gafanhoto! Sua resposta está correta.");
      textoRespostaCorreta.SetText(perguntaAtual.GetTextoAlternativaCorreta());

      caixaTextoRetorno.GetComponent<Image>().sprite = spriteFundoRespCorreta;
   }
   
   void MostrarOverlayRespostaIncorreta()
   {
      MostrarOverlayResposta();
      
      textoRetornoIntro.SetText("Poxa vida, não foi dessa vez. Sua resposta está incorreta.");
      textoRespostaCorreta.SetText(perguntaAtual.GetTextoAlternativaCorreta());
      
      caixaTextoRetorno.GetComponent<Image>().sprite = spriteFundoRespIncorreta;
   }
   
   // Função utilizada para chamar a próxima questão do Quiz apos o timer ser interrompido
   void OnParadaTimer()
   {
      if (alternativaSelecionada == -1)
      {
         // mostrar o overlay quando o jogador erra
         MostrarOverlayRespostaIncorreta();
      }
      else
      {
         Image imgRespostaSelecionada = alternativasTMP[alternativaSelecionada].GetComponent<Image>();
         
         if (alternativaSelecionada == perguntaAtual.GetAlternativaCorreta())
         {
            // a alternativa selecionada está correta
            acertos++;
         
            // alterar o sprite do botão selecionado pelo jogador, assumindo que esse botão representa
            // a alternativa correta para a questão avaliada
            MudarSpriteBotao(imgRespostaSelecionada, spriteRespostaCorreta);
         
            // mostrar o overlay quando o jogador acerta
            MostrarOverlayRespostaCorreta();
         }
         else
         {
            // a alternativa selecionada está incorreta
            Image imgRespostaCorreta = alternativasTMP[perguntaAtual.GetAlternativaCorreta()].GetComponent<Image>();
         
            MudarSpriteBotao(imgRespostaSelecionada, spriteRespostaIncorreta);
            MudarSpriteBotao(imgRespostaCorreta, spriteRespostaCorreta);
         
            // mostrar o overlay quando o jogador erra
            MostrarOverlayRespostaIncorreta();
         }
      }
   }
   public void SairDoQuiz()
   {
      #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
      #else
            Application.Quit();
      #endif
   }
}




