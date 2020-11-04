
using System;

namespace SoftUniServer.MvcFramework
{
    public interface IHttpServer
    {
        // func priema requers i vryshta response - 
        //metoda, koito se izpulnqwa kogato daden adres se otvori, se naricha actin.
        // action e krainata destinaciq na daden adres
        
        void AddRoute(string path, Func<HttpRequest, HttpResponse> action);

        void Start(int port);
    }
}
