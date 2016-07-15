using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Desafio
{
    class Program
    {
        public static List<Arquivo> Arquivos = new List<Arquivo>();
        public static int geradorId = 0;
        static void Main(string[] args)
        {
            Menu();
        }

        public static void Menu()
        {
            try
            {
                Console.WriteLine("Escolha o menu de acordo com número que o represente");
                Console.WriteLine("1 - Adicionar Arquivo");
                Console.WriteLine("2 - Visualizar Arquivos Armazenados");
                Console.WriteLine("3 - Deletar Arquivo");
                Console.WriteLine("4 - Ler Arquivo");
                Console.WriteLine("5 - Fechar Aplicação");

                string resposta = Console.ReadLine();

                switch (resposta.ToLower())
                {
                    case "1":
                        AdicionarArquivo();
                        break;
                    case "2":
                        VisualizarArquivo();
                        break;
                    case "3":
                        RemoverArquivo();
                        break;
                    case "4":
                        LerArquivo();
                        break;
                    case "5":
                        FecharAplicacao();
                        break;
                    default:
                        Console.WriteLine("Reposta não é valida");
                        Menu();
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Menu();
            }
        }

        public static void AdicionarArquivo()
        {
            try
            {
                Console.WriteLine("Informe o caminho do arquivo");
                string resposta = Console.ReadLine();

                if (string.IsNullOrEmpty(resposta))
                {
                    AdicionarArquivo();
                    return;
                }

                if (!File.Exists(resposta))
                {
                    Console.WriteLine("Arquivo não encontrado.");
                    TentarNovamenteAdicionar();
                    return;
                }

                FileStream entrada = File.Open(resposta, FileMode.Open);
                Byte[] _bytes = new Byte[entrada.Length];
                entrada.Read(_bytes, 0, (int)entrada.Length);

                entrada.Close();

                Arquivo arquivo = new Arquivo();

                string[] nome = entrada.Name.Split('\\');

                arquivo.NomeArquivo = nome.Last();
                arquivo.ArquivoByte = _bytes;
                arquivo.TipoArquivo = nome.Last().Split('.')[1];

                if (Arquivos.Count <= 0)
                {
                    arquivo.IdArquivo = geradorId;
                    geradorId += 1;
                    Arquivos.Add(arquivo);
                    Console.WriteLine("Arquivo adicionado com sucesso!");
                    Menu();
                    return;
                }

                bool validador = false;

                foreach (var temp in Arquivos)
                {
                    validador = temp.ArquivoByte.SequenceEqual(arquivo.ArquivoByte);
                    if (validador)
                    {
                        break;
                    }
                }
                //C:\Users\squal\Downloads\Sql.txt

                if (validador)
                {
                    Console.WriteLine("Arquivo já está armazenado");
                    TentarNovamenteAdicionar();
                    return;
                }

                Console.WriteLine("Arquivo adicionado com sucesso!");
                arquivo.IdArquivo = geradorId;
                geradorId += 1;
                Arquivos.Add(arquivo);
                Menu();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                AdicionarArquivo();
            }
        }

        public static void TentarNovamenteAdicionar()
        {
            try
            {
                Console.WriteLine("Deseja tentar novamente?");
                Console.WriteLine("1 - Sim");
                Console.WriteLine("2 - Não");
                string resposta = Console.ReadLine();

                switch (resposta.ToLower())
                {
                    case "1":
                        AdicionarArquivo();
                        break;
                    case "2":
                        Menu();
                        break;
                    default:
                        Console.WriteLine("Reposta não é valida");
                        TentarNovamenteAdicionar();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TentarNovamenteAdicionar();
            }
        }

        public static void LerArquivo()
        {
            try
            {
                Console.WriteLine("Informe o Id do Arquivo que deseja ler");
                string resposta = Console.ReadLine();

                int numero = 0;

                if (!int.TryParse(resposta, out numero))
                {
                    Console.WriteLine("Valor informado não é número de Id valido");
                    TentarNovamenteLer();
                    return;
                }

                var temp = Arquivos.FirstOrDefault(m => m.IdArquivo == Convert.ToInt32(resposta));

                if (temp == null)
                {
                    Console.WriteLine("Nenhum arquivo encontrado com esse Id");
                    TentarNovamenteLer();
                    return;
                }

                if (!temp.TipoArquivo.Trim().ToLower().Contains("txt"))
                {
                    Console.WriteLine("Infelizmente não possível ler arquivos que não sejam txt");
                    TentarNovamenteLer();
                    return;
                }


                Console.WriteLine("-- Início do Arquivo --");

                MemoryStream ms = new MemoryStream(temp.ArquivoByte);
                StreamReader streamReader = new StreamReader(ms,Encoding.UTF8, true);

                while (streamReader.Peek() >= 0)
                {
                    Console.WriteLine(streamReader.ReadLine());
                }

                streamReader.Close();
                ms.Close();

                Console.WriteLine("-- Final do Arquivo --");

                Menu();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                LerArquivo();
            }
        }

        public static void TentarNovamenteLer()
        {
            try
            {
                Console.WriteLine("Deseja tentar novamente?");
                Console.WriteLine("1 - Sim");
                Console.WriteLine("2 - Não");
                string resposta = Console.ReadLine();

                switch (resposta.ToLower())
                {
                    case "1":
                        AdicionarArquivo();
                        break;
                    case "2":
                        Menu();
                        break;
                    default:
                        Console.WriteLine("Reposta não é valida");
                        TentarNovamenteLer();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TentarNovamenteLer();
            }
        }

        public static void VisualizarArquivo()
        {
            try
            {
                Console.WriteLine("-- Inicio --");
                Arquivos.ForEach(m => Console.WriteLine("Id do Arquivo : " + m.IdArquivo + " | Nome do Arquivo : " + m.NomeArquivo + " | Tipo do Arquivo : " + m.TipoArquivo));
                Console.WriteLine("-- Final --");
                Menu();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                VisualizarArquivo();
            }
        }

        public static void RemoverArquivo()
        {
            try
            {
                Console.WriteLine("Informe o Id do Arquivo que deseja remover");
                string resposta = Console.ReadLine();

                int numero = 0;

                if (!int.TryParse(resposta, out numero))
                {
                    Console.WriteLine("Valor informado não número de Id valido");
                    TentarNovamenteRemover();
                    return;
                }

                var temp = Arquivos.FirstOrDefault(m => m.IdArquivo == Convert.ToInt32(resposta));

                if (temp == null)
                {
                    Console.WriteLine("Nenhum arquivo encontrado com esse Id");
                    TentarNovamenteRemover();
                    return;
                }

                Arquivos.Remove(temp);
                Console.WriteLine("Arquivo removido com sucesso!");
                Menu();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                RemoverArquivo();
            }
        }

        public static void TentarNovamenteRemover()
        {
            try
            {
                Console.WriteLine("Deseja tentar novamente?");
                Console.WriteLine("1 - Sim");
                Console.WriteLine("2 - Não");
                string resposta = Console.ReadLine();

                switch (resposta.ToLower())
                {
                    case "1":
                        RemoverArquivo();
                        break;
                    case "2":
                        Menu();
                        break;
                    default:
                        Console.WriteLine("Reposta não é valida");
                        TentarNovamenteRemover();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TentarNovamenteRemover();
            }
        }

        public static void FecharAplicacao()
        {
            Console.WriteLine("Deseja realmente encerrar aplicação?");
            Console.WriteLine("1 - Sim");
            Console.WriteLine("2 - Não");
            string resposta = Console.ReadLine();

            switch (resposta.ToLower())
            {
                case "1":
                    Console.WriteLine("Aplicação será encerrada");
                    System.Threading.Thread.Sleep(1500);
                    break;
                case "2":
                    Menu();
                    break;
                default:
                    Console.WriteLine("Reposta não é valida");
                    FecharAplicacao();
                    break;
            }
        }
    }
}
