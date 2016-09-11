﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JogoBarbie.Utils
{
    public class DesenhaMatriz
    {
        /// <summary>
        /// Numeração de cada array da matriz é definido de acordo com o custo 
        /// 0 - Edificios - Custo: 0; Cor: Laranja
        /// 1 - Asfalto - Custo: 1; Cor: Cinza Escuro
        /// 3 - Terra - Custo: 3; Cor: Marrom
        /// 05 - Grama - Custo: 05; Cor: Verde
        /// 10 - Paralelepipedo - Custo: 10; Cor: Cinza Claro
        /// 15 - Amigo da Barbie - 
        /// </summary>
        public int[,] GeraMatriz()
        {
            int[,] matriz = new int[,] {
      //Linha  //Coluna ->       05                  10                  15                  20                  25                  30                  35                  40      42
        /*01*/  {05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05},
                {05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 05, 05, 05, 05, 10, 10, 10, 10, 10, 01, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05},
                {05, 05, 10, 00, 00, 00, 00, 00, 10, 00, 00, 00, 00, 00, 00, 10, 05, 05, 05, 05, 10, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 05, 05},
                {05, 05, 10, 00, 00, 00, 00, 00, 10, 00, 00, 00, 00, 00, 00, 10, 05, 05, 05, 05, 10, 00, 00, 00, 05, 01, 05, 05, 05, 10, 05, 05, 05, 05, 05, 05, 05, 05, 05, 10, 05, 05},
        /*05*/  {05, 05, 10, 00, 00, 10, 00, 00, 10, 00, 00, 00, 15, 00, 00, 10, 00, 00, 05, 05, 10, 00, 00, 00, 05, 01, 05, 05, 05, 10, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 05, 05},
                {05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 00, 05, 05, 10, 00, 00, 00, 05, 01, 05, 05, 05, 10, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 05, 05},
                {05, 05, 10, 03, 03, 03, 03, 03, 03, 03, 03, 03, 03, 03, 03, 10, 10, 05, 05, 05, 10, 00, 00, 00, 05, 01, 05, 05, 05, 10, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 05, 05},
                {05, 05, 10, 03, 03, 03, 03, 03, 03, 03, 03, 03, 03, 03, 03, 10, 10, 05, 05, 05, 10, 00, 00, 00, 05, 01, 05, 05, 05, 10, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 05, 05},
                {05, 05, 10, 03, 03, 03, 03, 05, 05, 05, 05, 03, 03, 03, 03, 10, 10, 10, 05, 05, 10, 00, 00, 00, 05, 01, 05, 05, 05, 10, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 05, 05},
        /*10*/  {05, 05, 10, 03, 03, 03, 03, 05, 15, 05, 05, 03, 03, 03, 03, 10, 05, 05, 05, 05, 10, 00, 00, 00, 05, 01, 05, 05, 05, 10, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 05, 05},
                {05, 05, 10, 03, 03, 03, 03, 05, 05, 05, 05, 03, 03, 03, 03, 10, 05, 05, 05, 05, 10, 00, 00, 00, 05, 01, 05, 05, 05, 10, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05},
                {05, 05, 10, 03, 03, 03, 03, 03, 03, 03, 03, 03, 03, 03, 03, 10, 05, 05, 05, 05, 10, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 05, 05, 05},
                {05, 05, 10, 03, 03, 03, 03, 03, 03, 03, 03, 03, 03, 03, 03, 10, 05, 05, 05, 05, 10, 00, 00, 00, 05, 01, 05, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 05, 05, 05},
                {05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 01, 05, 05, 05, 10, 00, 00, 00, 05, 01, 05, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 01, 01, 01},
         /*15*/ {05, 05, 01, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 01, 05, 05, 05, 05, 10, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 05, 05},
                {05, 05, 01, 05, 00, 00, 00, 05, 05, 00, 00, 00, 00, 05, 05, 01, 10, 05, 05, 05, 10, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 05, 05, 05},
                {05, 05, 01, 10, 10, 00, 00, 05, 05, 00, 00, 00, 01, 01, 01, 01, 05, 05, 05, 05, 10, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 00, 05, 05},
                {05, 05, 01, 05, 00, 00, 00, 05, 05, 00, 00, 00, 00, 05, 05, 01, 05, 05, 05, 05, 10, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 00, 05, 05},
                {05, 05, 01, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 01, 05, 05, 05, 05, 10, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 00, 05, 05},
         /*20*/ {05, 05, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 05, 05, 05},
                {05, 05, 01, 10, 10, 10, 10, 10, 10, 01, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 01, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 05, 05},
                {05, 05, 01, 10, 00, 00, 00, 00, 10, 01, 05, 00, 00, 00, 05, 05, 00, 00, 00, 05, 01, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 01, 05, 05},
                {05, 05, 01, 10, 00, 00, 00, 00, 10, 01, 05, 00, 00, 00, 05, 05, 00, 00, 15, 01, 01, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 01, 05, 05},
                {05, 05, 01, 10, 00, 00, 00, 10, 10, 01, 01, 01, 00, 00, 05, 05, 00, 00, 00, 05, 01, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 01, 05, 05},
         /*25*/ {05, 05, 01, 10, 00, 00, 00, 00, 10, 01, 05, 00, 00, 00, 05, 05, 00, 00, 00, 05, 01, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 01, 05, 05},
                {05, 05, 01, 10, 00, 00, 00, 00, 10, 01, 05, 00, 00, 00, 05, 05, 00, 00, 00, 05, 01, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 01, 05, 05},
                {05, 05, 01, 10, 10, 10, 10, 10, 10, 01, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 01, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 01, 05, 05},
                {05, 05, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 01, 05, 05},
                {05, 05, 01, 05, 05, 05, 05, 05, 05, 05, 05, 01, 05, 05, 05, 05, 10, 05, 01, 05, 10, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 01, 05, 05},
         /*30*/ {05, 05, 01, 05, 00, 00, 00, 00, 00, 00, 05, 01, 05, 00, 00, 00, 10, 00, 01, 00, 10, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 01, 05, 05},
                {05, 05, 01, 05, 00, 00, 01, 01, 00, 00, 05, 01, 05, 00, 01, 00, 10, 00, 00, 00, 10, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 01, 05, 05},
                {05, 05, 01, 05, 05, 05, 01, 01, 05, 05, 05, 01, 05, 05, 01, 05, 10, 05, 05, 05, 10, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 01, 05, 05},
                {05, 05, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 01, 05, 05},
                {05, 05, 01, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 01, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 01, 05, 05},
         /*35*/ {05, 05, 01, 05, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 05, 01, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 01, 05, 05},
                {05, 05, 01, 05, 00, 01, 01, 01, 01, 01, 01, 01, 00, 00, 15, 01, 01, 00, 00, 05, 01, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 01, 05, 05},
                {05, 05, 01, 05, 00, 01, 00, 00, 00, 00, 00, 01, 00, 00, 00, 00, 01, 00, 00, 05, 01, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 01, 05, 05},
                {05, 05, 01, 05, 00, 01, 01, 01, 01, 00, 00, 01, 01, 01, 01, 01, 01, 00, 00, 05, 01, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 01, 05, 05},
                {05, 05, 01, 05, 00, 00, 00, 00, 01, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 05, 01, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 01, 05, 05},
                {05, 05, 01, 05, 05, 05, 05, 05, 01, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 01, 00, 00, 00, 05, 01, 05, 05, 05, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 01, 05, 05},
         /*40*/ {05, 05, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 05, 05},
                {05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05},
            };


            return matriz;
        }
    }
}
