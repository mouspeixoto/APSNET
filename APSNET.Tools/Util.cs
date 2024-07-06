using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace APSNET.Tools
{
    public static class Util
    {
        /// <summary>
        /// Transforma uma String em um Inteiro
        /// </summary>
        /// <returns></returns>
        public static int GetInteiro(this string texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return 0;
            }

            if (texto.IndexOf(',') >= 0)
            {
                var spl = texto.Split(',');
                texto = spl[0];
            }

            if (texto.IndexOf('.') >= 0)
            {
                texto = texto.Replace(".", "");
            }

            int inteiro = 0;
            int.TryParse(texto, out inteiro);

            return inteiro;
        }

        /// <summary>
        /// Transforma uma double em um Inteiro
        /// </summary>
        /// <returns></returns>
        public static int GetInteiro(this double texto)
        {
            int inteiro = 0;
            int.TryParse(texto.ToString(), out inteiro);

            return inteiro;
        }

        /// <summary>
        /// Transforma uma String em um Long
        /// </summary>
        /// <returns></returns>
        public static long GetLong(this string texto)
        {
            long valor = 0;
            long.TryParse(texto, out valor);

            return valor;
        }

        /// <summary>
        /// Transforma uma String em bool
        /// </summary>
        /// <returns></returns>
        public static bool GetBool(this string texto)
        {
            bool retorno = false;
            bool.TryParse(texto, out retorno);

            return retorno;
        }

        /// <summary>
        /// Transforma uma String em DateTime
        /// </summary>
        /// <returns></returns>
        public static DateTime GetData(this string texto)
        {
            DateTime data = DateTime.Now;
            DateTime.TryParse(texto, out data);

            return data;
        }

        /// <summary>
        /// Transforma uma String em TimeSpan
        /// </summary>
        /// <returns></returns>
        public static TimeSpan GetTimeSpan(this string texto)
        {
            TimeSpan data = new TimeSpan();
            TimeSpan.TryParse(texto, out data);

            return data;
        }

        /// <summary>
        /// Transforma uma String em um Decimal
        /// </summary>
        /// <returns></returns>
        public static decimal GetDecimal(this string texto)
        {
            decimal valor = 0;
            decimal.TryParse(texto, out valor);

            return valor;
        }

        /// <summary>
        /// Transforma uma String em um Decimal
        /// </summary>
        /// <returns></returns>
        public static decimal GetDecimal(this string texto, bool jaEDecimal)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return 0;
            }
            else
            {
                if (jaEDecimal)
                {
                    texto = texto.Replace('.', ',');
                    decimal valor = 0;
                    decimal.TryParse(texto, out valor);

                    return valor;
                }
                else
                {
                    decimal valor = 0;
                    decimal.TryParse(texto, out valor);

                    return valor;
                }
            }
        }

        /// <summary>
        /// Transforma uma String(que já é um decimal) em um Decimal
        /// </summary>
        /// <returns></returns>
        public static decimal GetDecimalJaDecimal(this string texto)
        {
            decimal valor = 0;

            if (!string.IsNullOrEmpty(texto))
            {
                decimal.TryParse(texto.Replace('.', ','), out valor);
            }

            return valor;
        }

        /// <summary>
        /// Transforma um decimal para Formato en-US para inserção no banco de dados.
        /// </summary>
        /// <returns></returns>
        public static string GetStringDB(this decimal valor)
        {
            return valor.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"));
        }

        /// <summary>
        /// Transforma um DateTime em uma String com a Data (yyyy-MM-dd)
        /// </summary>
        /// <returns></returns>
        public static string GetFormatDate(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// Transforma um DateTime em uma string data e hora (yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <returns></returns>
        public static string GetFormatDateTime(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// Transforma um DateTime em uma String com a Data sem Hifen (yyyyMMdd)
        /// </summary>
        /// <returns></returns>
        public static string GetFormatDateSemHifen(this DateTime date)
        {
            return date.ToString("yyyyMMdd");
        }

        /// <summary>
        /// Transforma um DateTime em uma string hora (HH:mm:ss)
        /// </summary>
        /// <returns></returns>
        public static string GetFormatTime(this DateTime time)
        {
            return time.ToString("HH:mm:ss");
        }

        /// <summary>
        /// Transforma um DateTime em uma string data (dd/MM/yyyy)
        /// </summary>
        /// <returns></returns>
        public static string GetFormatDateUI(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Transforma um DateTime em uma string data e hora (dd/MM/yyyy HH:mm)
        /// </summary>
        /// <returns></returns>
        public static string GetFormatDateTimeUI(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy HH:mm");
        }

        /// <summary>
        /// Transforma um DateTime em uma string data e hora (HH:mm)
        /// </summary>
        /// <returns></returns>
        public static string GetFormatTimeUI(this DateTime dateTime)
        {
            return dateTime.ToString("HH:mm");
        }

        public static string FirstCharToUpper(this string input)
        {
            string word = input.ToLower();
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("Insira uma palavra diferente de nula ou vazia");
            return word.Length > 1 ? char.ToUpper(word[0]) + word.Substring(1) : word.ToUpper();
        }

        public static bool IsCnpj(this string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public static bool IsCpf(this string CPF)
        {
            int[] CalcARR = null;
            int Sum = 0;
            int DV1 = 0;
            int DV2 = 0;

            CPF = CPF.Replace(".", "").Replace("/", "").Replace("-", "").Trim();

            if (string.IsNullOrEmpty(CPF))
            {
                return true;
            }

            switch (CPF)
            {
                case "11111111111":
                    return true;

                case "22222222222":
                    return false;

                case "33333333333":
                    return false;

                case "44444444444":
                    return false;

                case "55555555555":
                    return false;

                case "66666666666":
                    return false;

                case "77777777777":
                    return false;

                case "88888888888":
                    return false;

                case "99999999999":
                    return false;

                default:
                    break;
            }

            if (long.Parse(CPF) == 0)
            {
                return false;
            }

            if (CPF.Length != 11)
            {
                CPF = string.Format("{0:D11}", long.Parse(CPF));
            }

            CalcARR = new int[11];

            for (int x = 0; x < CalcARR.Length; x++)
            {
                CalcARR[x] = int.Parse(CPF[x].ToString());
            }

            Sum = 0;

            for (int x = 1; x <= 9; x++)
            {
                Sum += CalcARR[x - 1] * (11 - x);
            }

            Math.DivRem(Sum, 11, out DV1);
            DV1 = 11 - DV1;
            DV1 = DV1 > 9 ? 0 : DV1;

            if (DV1 != CalcARR[9])
            {
                return false;
            }

            Sum = 0;

            for (int x = 1; x <= 10; x++)
            {
                Sum += CalcARR[x - 1] * (12 - x);
            }

            Math.DivRem(Sum, 11, out DV2);
            DV2 = 11 - DV2;
            DV2 = DV2 > 9 ? 0 : DV2;

            if (DV2 != CalcARR[10])
            {
                return false;
            }

            return true;
        }

        public static bool IsEmail(this string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            try
            {
                System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static string MesDescricaoPorNumero(string mesDigito)
        {
            string mesDescricao;
            switch (mesDigito)
            {
                case "1":
                case "01":
                    mesDescricao = "JANEIRO";
                    break;

                case "2":
                case "02":
                    mesDescricao = "FEVEREIRO";
                    break;

                case "3":
                case "03":
                    mesDescricao = "MARÇO";
                    break;

                case "4":
                case "04":
                    mesDescricao = "ABRIL";
                    break;

                case "5":
                case "05":
                    mesDescricao = "MAIO";
                    break;

                case "6":
                case "06":
                    mesDescricao = "JUNHO";
                    break;

                case "7":
                case "07":
                    mesDescricao = "JULHO";
                    break;

                case "8":
                case "08":
                    mesDescricao = "AGOSTO";
                    break;

                case "9":
                case "09":
                    mesDescricao = "SETEMBRO";
                    break;

                case "10":
                    mesDescricao = "OUTUBRO";
                    break;

                case "11":
                    mesDescricao = "NOVEMBRO";
                    break;

                case "12":
                    mesDescricao = "DEZEMBRO";
                    break;

                default:
                    mesDescricao = "";
                    break;
            }

            return mesDescricao;
        }

        public static string TransformaListaEmORQuery(string campo, List<string> lista, bool and = true)
        {
            if (lista.Count == 0)
            {
                return "";
            }
            string retorno = "";

            if (and)
                retorno = " AND " + campo + " IN(";
            else
                retorno = " " + campo + " IN(";

            string auxiliar = "";
            foreach (var item in lista)
            {
                auxiliar += "'" + item + "', ";
            }
            if (auxiliar != "")
                retorno += auxiliar.Substring(0, auxiliar.Length - 2) + ") ";
            else
                return "";

            return retorno;
        }

        public static string TransformaListaEmORParametros(string campo, List<string> lista)
        {
            string retorno = campo + " IN(";
            string auxiliar = "";
            foreach (var item in lista)
            {
                auxiliar += "'" + item + "', ";
            }
            if (auxiliar != "")
                retorno += auxiliar.Substring(0, auxiliar.Length - 2) + ") ";
            else
                return "";

            return retorno;
        }

        public static List<List<T>> DivideLista<T>(this List<T> lista, int tamanho)
        {
            return lista
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / tamanho)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }


        public static bool ApenasLetra(string str)
        {
            foreach (char c in str)
            {
                if (!Char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool ApenasNumero(string str)
        {
            foreach (char c in str)
            {
                if (!Char.IsNumber(c))
                {
                    return false;
                }
            }
            return true;
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
