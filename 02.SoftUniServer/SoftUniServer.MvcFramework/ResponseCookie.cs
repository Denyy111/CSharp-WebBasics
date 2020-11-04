using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace SoftUniServer.MvcFramework
{
    public class ResponseCookie : Cookie
    {
        public ResponseCookie(string name, string value)
            :base(name,value)
        {
        }

        //Max-Age
        //Domain,Secure
        public int MaxAge { get; set; }

        public bool HttpOnly { get; set; }

        public string Path { get; set; }

        //Polimorphisam- edna i syshta implementaciq na edin i sysht metod vyv 2 nasledqvashi se clasa (Cookie, ResponseCookie)
       // no sprqmo koi ot dvata klasa izvikame ili Cookie ili ResponseCookie - she se izpulni sledniqt podhodqsh metod ot 2ta

        public override string ToString()
        {
            return base.ToString();
        }
        

    }
}
