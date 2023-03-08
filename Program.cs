using Exercici2;
using System.Net;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        ListenAsync();
        Console.ReadKey();
    }

    async static void ListenAsync()
    {
        
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:51111/");
        listener.Start();

        while (true)
        {
            HttpListenerContext context = await listener.GetContextAsync();

            var url = context.Request.RawUrl;

            string directori = "C:\\Users\\Uri\\Prova";
            var exercici2 = new Exercici2Codi(directori);


            string msg = "";
            int codiRetorn = (int)HttpStatusCode.OK;

            if (url == null)
            {
                msg = "La URL és nula!";
                codiRetorn = (int)HttpStatusCode.InternalServerError;
            }

            else
            {
                if (url.StartsWith("/llistarDirectori"))
                {
                    msg = exercici2.ObtenirDirectori();
                }

                else if (url.StartsWith("/recursiu"))
                {
                    List<String> llista = exercici2.ObtenirDirectoriRecursiu(directori);
                    foreach(string s in llista)
                    {
                        msg += s + "\n";
                    }
                }

                context.Response.ContentLength64 = Encoding.UTF8.GetByteCount(msg);
                context.Response.StatusCode = codiRetorn;
                using (Stream s = context.Response.OutputStream)
                using (StreamWriter writer = new StreamWriter(s))
                    await writer.WriteAsync(msg);

            }

        }

    }
}