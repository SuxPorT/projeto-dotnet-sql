using System.Text;

namespace CustomExceptions
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

        public static void CriarLog(string entity, Exception exception)
        {
            string nomeArquivo = "./Logs/"
                                 + DateTime.Now.ToString("dd-MM-yyyy")
                                 + "_"
                                 + entity
                                 + "_"
                                 + exception.GetType().Name
                                 + ".log";

            string mensagem = DateTime.Now.ToString("HH:mm:ss\n")
                              + exception.GetType().Name
                              + ": "
                              + exception.Message
                              + "\n"
                              + exception.StackTrace
                              + "\n\n";

            File.AppendAllText(nomeArquivo, mensagem, Encoding.Default);
        }
    }
}