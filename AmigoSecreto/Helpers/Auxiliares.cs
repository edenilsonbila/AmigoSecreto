using System;
using System.Web;
using System.IO;
using System.Text;
using System.Data.Entity.Validation;

namespace AmigoSecretoSystem.Helpers
{
    public static class Auxiliares
    {
         public static void GravaLogErro(Exception erro, HttpRequestBase request = null, string msgExtra = "")
         {

            try
            {

                //Recebe o nome do arquivo com a DataAtual
                String diretorio = (HttpContext.Current != null ? HttpContext.Current?.Server?.MapPath("~") : AppDomain.CurrentDomain.BaseDirectory) + @"Logs\";
                //Valida e cria o diretório
                if (!Directory.Exists(diretorio))
                {
                    Directory.CreateDirectory(diretorio);
                }


                //Nome arquivo
                var nomeArquivo = diretorio + @"Erro_" + DateTime.Now.ToString("dd-MM-yyyy") + ".log";

                //Cria a mensagem
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("================================================================================");
                sb.AppendLine();
                sb.AppendLine("Data e Hora do Erro: " + DateTime.Now);
                sb.AppendLine("Erro Resumido:       " + erro.Message);
                sb.AppendLine("Erro Resumido Inner Exception:" + erro.InnerException?.InnerException?.Message +
                              " - " + erro.InnerException?.Message);
                sb.AppendLine("Recurso do Erro:     " + erro.Source);
                sb.AppendLine("Url da Requisição:       " + request?.UrlReferrer?.AbsolutePath);
                sb.AppendLine("Tipo de Exception:     " + erro.GetType().FullName);
                sb.AppendLine("Erro:     " + erro.StackTrace);
                sb.AppendLine("MsgExtra:     " + msgExtra);

                if (erro.TargetSite != null)
                {
                    sb.AppendLine();
                    sb.AppendLine("=============================== POSSÍVEL ORIGEM (TargetSite) ===============================");
                    sb.AppendLine();
                    sb.AppendLine("Nome: " + erro.TargetSite.Name);
                    sb.AppendLine("Método: " + erro.TargetSite.MethodHandle.Value);
                    sb.AppendLine("Varíavel: " + erro.TargetSite + "");
                    sb.AppendLine("DeclaringType: " + erro.TargetSite.DeclaringType);
                }

                if (erro.GetType().Name == "DbEntityValidationException")
                {
                    var entityException = (DbEntityValidationException)erro;
                    sb.AppendLine();
                    sb.AppendLine("=============================== Entity Validations ===============================");
                    sb.AppendLine();
                    foreach (var eve in entityException.EntityValidationErrors)
                    {
                        sb.AppendLine(
                            "Entity of type \"{0}\" in state \"{1}\" has the following validation errors: " +
                            eve.Entry.Entity.GetType().Name + " - " + eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            sb.AppendLine("- Property: \"{0}\", Error: \"{1}\"" + " - " + ve.PropertyName + " - " +
                                          ve.ErrorMessage);
                        }
                    }
                }

                sb.AppendLine();
                sb.AppendLine("================================================================================");


                //Verifica se o arquivoExiste
                if (File.Exists(nomeArquivo))
                {
                    StreamWriter sw = File.AppendText(nomeArquivo);
                    sw.WriteLine(sb.ToString());
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();
                }
                else
                {
                    StreamWriter sw = new StreamWriter(nomeArquivo);
                    sw.WriteLine(sb.ToString());
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception)
            {
                return;
            }

        }
    }
}