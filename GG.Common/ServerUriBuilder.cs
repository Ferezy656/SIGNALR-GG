namespace GG.Common
{
    using System;

    public class ServerUriBuilder
    {
        public static string PrepareServerUri(string serverUriTemplate,string port)
        {
            return String.Format(serverUriTemplate, !String.IsNullOrWhiteSpace(port) ? port : "8080");
        }

        public static string GetServerUriForClient(string nonDefaultServerUri)
        {
            if(!String.IsNullOrWhiteSpace(nonDefaultServerUri))
            {
                UriBuilder ub = new UriBuilder(nonDefaultServerUri);
                return ub.ToString() + "/signalr";
            }

            return "http://localhost:8080/signalr";
        }
    }
}
