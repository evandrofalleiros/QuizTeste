using System;
using System.Collections;
using System.Collections.Generic;using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName="Super Quiz/Nova Pergunta", fileName = "pergunta-")]
public class PerguntaSO : ScriptableObject
{
   [TextArea(2,6)]
   [SerializeField] private string enunciado;
   [SerializeField] private string[] alternativas;
   [SerializeField] private int alternativaCorreta;
   [SerializeField] private string id;

   // Métodos de exposição dos dados (Getters)
   public string GetEnunciado()
   {
      return enunciado;
   }

   public string[] GetAlternativas()
   {
      return alternativas;
   }

   public int GetAlternativaCorreta()
   {
      return alternativaCorreta;
   }

   public string GetId()
   {
      return id;
   }
   
}
