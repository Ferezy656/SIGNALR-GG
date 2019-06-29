namespace GG.Common
{
    using System;

    public class ServerUriBuilder
    {
        public static string PrepareServerUri(string serverUriTemplate,string port)
        {
            return String.Format(serverUriTemplate, !String.IsNullOrWhiteSpace(port) ? port : "8080");
        }
    }
}
