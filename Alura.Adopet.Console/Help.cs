using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Adopet.Console
{
    [DocComando(instrucao: "help",
        documentacao: "adopet help comando que exibe informações de ajuda \nadopet help <parametro> ous simplemente adopet help comando que exibe informações de ajuda dos comandos.")]
    internal class Help
    {
        private Dictionary<string, DocComando> docs;

        public Help()
        {
            docs = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetCustomAttributes<DocComando>().Any())
                .SelectMany(t => t.GetCustomAttributes<DocComando>())
                .ToDictionary(d => d.Instrucao);
        }
        public void ExibirAjuda(string[] parametros)
        {
            System.Console.WriteLine("Lista de comandos.");
            // se não passou mais nenhum argumento mostra help de todos os comandos
            if (parametros.Length == 1)
            {
                System.Console.WriteLine("Adopet (1.0) - Aplicativo de linha de comando (CLI).");
                System.Console.WriteLine("Realiza a importação em lote de um arquivos de pets.");
                System.Console.WriteLine("Comando possíveis: ");
                foreach (var doc in docs.Values)
                {
                    System.Console.WriteLine(doc.Documentacao);
                }
            }
            // exibe o help daquele comando específico
            else if (parametros.Length == 2)
            {
                string comandoAserExibido = parametros[1];
                if (docs.ContainsKey(comandoAserExibido))
                {
                    var comando = docs[comandoAserExibido];
                    System.Console.WriteLine(comando.Documentacao);
                }
            }
        }
    }
}
