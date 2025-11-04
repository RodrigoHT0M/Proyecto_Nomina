namespace ML
{
    public class Resultado
    {
        public bool Correct { get; set; }

        public Exception? ex { get; set; }

        public string? ErrorMessagge { get; set; }

        public object?  Object { get; set; }

        public List<Object>? Objects{ get; set; }
    }
}
