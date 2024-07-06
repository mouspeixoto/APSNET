using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace APSNET.Tools
{
    public static class PrepararDadosPBanco
    {
        public static void Preparar(System.Type type, ref object Obj)
        {
            try
            {
                PropertyInfo[] propriedades = type.GetProperties();

                foreach (PropertyInfo propriedade in propriedades)
                {
                    if (propriedade.Name.ToLower().IndexOf("produtolote") >= 0)
                    {
                        continue;
                    }

                    string valor = "";
                    try
                    {
                        valor = propriedade.GetValue(Obj, null).ToString().Trim().Replace("'", "").Replace("\\", "");
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    //propriedade.Name;// pra pegar o nome da propriedade

                    string tipo = propriedade.PropertyType.Name.ToLower();

                    switch (tipo)
                    {
                        case "string":
#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
                            valor = propriedade.GetValue(Obj, null).ToString().Trim().Replace("'", "").Replace("\\", "");
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.
                            valor = RetirarAcentos(valor);

                            bool email = false;

                            if (propriedade.Name.ToLower().IndexOf("email") >= 0)
                            {
                                email = true;
                            }

                            if (!email)
                            {
                                if (propriedade.Name.ToLower().IndexOf("telefone") >= 0 || propriedade.Name.ToLower().IndexOf("observacao") >= 0 || propriedade.Name.ToLower().IndexOf("observacoes") >= 0 || propriedade.Name.ToLower().IndexOf("valor") >= 0 || propriedade.Name.ToLower().IndexOf("detalhes") >= 0 || propriedade.Name.ToLower().IndexOf("arquivo") >= 0 || propriedade.Name.ToLower().IndexOf("atualizadoporarquivo") >= 0 || propriedade.Name.ToLower().IndexOf("comandosretornadosbanco") >= 0 || propriedade.Name.ToLower().IndexOf("textoprodutosite") >= 0 || propriedade.Name.ToLower().IndexOf("correcao") >= 0 || propriedade.Name.ToLower().IndexOf("log") >= 0 || propriedade.Name.ToLower().IndexOf("codigoean") >= 0 || propriedade.Name.ToLower().IndexOf("senha") >= 0 || propriedade.Name.ToLower().IndexOf("produto_lote") >= 0 || propriedade.Name.ToLower().IndexOf("descricaocompleta") >= 0 || propriedade.Name.ToLower().IndexOf("inseriujustificativatexto") >= 0)
                                {
                                }
                                else
                                {
                                    valor = RemoverSimbolos(valor);
                                }

                                if (type.Name == "PreNFeItem" && propriedade.Name.ToLower().IndexOf("observacao") >= 0)
                                {
                                    valor = RemoverSimbolos(valor);
                                }
                            }
                            else
                            {
                                valor = RemoverSimbolos(valor, "@;");
                            }

                            propriedade.SetValue(Obj, valor, null);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private static string RetirarAcentos(string texto)
        {
            string s = texto.Normalize(NormalizationForm.FormD);

            StringBuilder sb = new StringBuilder();

            for (int k = 0; k < s.Length; k++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(s[k]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(s[k]);
                }
            }
            return sb.ToString();
        }

        private static string RemoverSimbolos(string texto, string permitir = "")
        {
            texto = Regex.Replace(texto, "[^0-9a-zA-Z.,_/ ()|;[]" + permitir + "-]+?", "");

            texto = texto.Replace("\r", "").Replace("\n", "").Replace("\t", "").Trim();

            return texto;
        }
    }
}
