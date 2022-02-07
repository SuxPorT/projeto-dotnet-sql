using System.Text;

namespace Exceptions
{
    public abstract class LogException : ApplicationException
    {
        protected String? Entity { get; set; }

        public LogException() { }

        public LogException(string entity, string message) : base(message)
        {
            Entity = entity;
        }

        public LogException(string entity, string message, Exception inner)
        : base(message, inner)
        {
            Entity = entity;
        }

        public void CriarLog()
        {
            string nomeArquivo = "./Logs/" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss") + "_" + Entity + "_NotFound";

            string mensagem = this.GetType().Name
                              + ": "
                              + Message
                              + "\n"
                              + StackTrace;

            File.AppendAllText(nomeArquivo, mensagem, Encoding.Default);
        }
    }
}