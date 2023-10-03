namespace BlogPessoal.Security
{
    public class Settings
    {
        private static string secret = "edc7ea633ef5a978d39e2323efa29c42ae67fbd9742d286ba53d70c55c83d340";
        public static string Secret { get => secret; set => secret = value; }
    }
}
