// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System;

Console.WriteLine("Hello, World!");

string stringaDiConnessione = "Data Source=localhost;Initial Catalog=biblioteca-DB;Integrated Security=True";

SqlConnection connessioneSql = new SqlConnection(stringaDiConnessione);

//try
//{
//    connessioneSql.Open();
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}finally
//{
//    connessioneSql.Close();
//}

//dotnet add package System.Data.SqlClient andato a buon fine


//1. creazione oggetti
//2. popolamento oggetti
//3. popolamento DB
//4. uso di query
//5. aggiunta controlli su query per injetion
//6. creazione menù
//7. creazione controlly try ecc ecc
//8 creazione classi superflue al esercizio
// testo esercizio a fondo pagina


//using System.Diagnostics.Metrics;
//using System.Runtime.ConstrainedExecution;

Biblioteca biblioteca = new Biblioteca();


//creazione pseudo clienti
for (int i = 0; i < 100; i++)
{
    Random rand = new Random();

    string nome = "utente" + i;
    string cognome = "cognome" + i;
    string email = "utente" + i;
    string password = "utente" + i + "@email.it" ;
    int numeroTelefono = rand.Next(111111111,999999999) ;
    Utente utente = new Utente(cognome,nome,email,password,numeroTelefono);
    biblioteca.ListaUtenti.Add(utente);
}

for (int i = 0; i < 100; i++)
{
    string[] seTTore = { "horror", "fantasy", "story", "math" };
    string[] auTTore = { "gianni", "maniaco", "storto", "pollo" };
    Random rand = new Random();



    string titolo = "titolo" + i;
    string anno = new DateTime( rand.Next(1900,2022),rand.Next(1,12),rand.Next(1,28) ).ToString("D");
    string settore = seTTore[rand.Next(0,3)];
    string scaffale = seTTore[rand.Next(0, 3)] + i;
    string autore = auTTore[rand.Next(0, 3)];
    Documento documento = new Documento(titolo, anno, settore, scaffale, autore);
    biblioteca.ListaDocumenti.Add(documento);
}

foreach (var item in biblioteca.ListaUtenti)
{
    Console.WriteLine(item.Nome);
}

try
{
    connessioneSql.Open();
    string query = "INSER INTO Utente (cognome, nome, email, password, numeroTelefono) VALUES (@dato1, @dato2, @dato3, @dato4, @dato5)";
    SqlCommand cmd = new SqlCommand(query, connessioneSql);

    foreach (var item in biblioteca.ListaUtenti)
    {
        cmd.Parameters.Add(new SqlParameter("@dato1",item.Cognome));
        cmd.Parameters.Add(new SqlParameter("@dato2", item.Nome));
        cmd.Parameters.Add(new SqlParameter("@dato3", item.Email));
        cmd.Parameters.Add(new SqlParameter("@dato4", item.Password));
        cmd.Parameters.Add(new SqlParameter("@dato5", item.NumeroTelefono));

        int affectedRows = cmd.ExecuteNonQuery();
    }


}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    connessioneSql.Close();
}


public class Biblioteca
{
    //lista utenti
    public List<Utente> ListaUtenti;

    //lista documenti
    public List<Documento> ListaDocumenti;


    public Biblioteca()
    {
        ListaUtenti = new List<Utente>();
        ListaDocumenti = new List<Documento>();
    }

    //L’utente deve poter eseguire delle ricerche per codice o per titolo e, eventualmente, effettuare dei prestiti registrando il periodo (Dal/Al) del prestito e il documento.
    //Deve essere possibile effettuare la ricerca dei prestiti dato nome e cognome di un utente.
    List<Documento> RicercaPerCodiceOTitolo(string ricerca)
    {
        List<Documento> trovato = new List<Documento>();
        foreach (var item in ListaDocumenti)
        {
            if (item.Titolo == ricerca || item.Codice == ricerca)
            {
                trovato.Add(item);
            }


        }
        return trovato;
    }


}

public class Documento
{
    //un codice identificativo di tipo stringa (ISBN per i libri, numero seriale per i DVD),
    public string Codice { get; set; }

    //titolo,
    public string Titolo { get; set; }

    //anno,
    public string Anno { get; set; }

    //settore(storia, matematica, economia, …),
    public string Settore { get; set; }


    //stato(In Prestito, Disponibile),
    public bool Stato { get; set; }

    //uno scaffale in cui è posizionato,
    public string Scaffale { get; set; }

    //un autore (Nome, Cognome).
    public string Autore { get; set; }

    public Documento( string titolo, string anno, string settore,string scaffale, string autore)
    {
        Codice = Generacodice();
        Titolo = titolo;
        Anno = anno;
        Settore = settore;
        Stato = true;
        Scaffale = scaffale;
        Autore = autore;
    }

    public void ModificaStato()
    {
        Stato = !Stato;
    } 

    public string Generacodice()
    {
        string[] vocale = { "a", "e", "i", "o", "u" };
        string codice = "";
        for (int i = 0; i < 10; i++)
        {
            Random random = new Random();
            if(i%2 == 0)
            {
                codice += (i + random.Next(1, 5));
            }
            else
            {
                codice += vocale[random.Next(0, 4)];
            }
        }
        return codice;
    }
}

public class Utente
{

    //cognome,
    public string Cognome { get; set; }

    //nome,
    public string Nome { get; set; }

    //email,
    public string Email { get; set; }

    //password,
    public string Password { get; set; }

    //recapito telefonico,
    public int NumeroTelefono { get; set; }
    public Utente(string cognome, string nome, string email, string password, int numeroTelefono)
    {
        Cognome = cognome;
        Nome = nome;
        Email = email;
        Password = password;
        NumeroTelefono = numeroTelefono;
    }
}



//per ora superflua
class Autore
{
    public string Nome { get; set; }
    public string Cognome { get; set; }

    public int eta { get; set; }
}

//per ora superflua
class Libro
{
    //Per i libri si ha in aggiunta il numero di pagine
    public int Paggine { get; set; }
}


//per ora superflua
class Dvd
{
    // mentre per i dvd la durata.
    public DateTime Time { get; set; }
}






//Esercizio di oggi (nome repo): csharp - biblioteca - db
//Riprendiamo l’esercizio della biblioteca considerando però aclune varianti:
//non è necessario (ma consigliato) ragionare con gli oggetti.
//evitiamo categoricamente la questione dell’eredità tra oggetti
//potete implementare una singola tabella per tutti i documenti: ovviamente in questo caso ci dovrà essere una colonna che gestisce il tipo di documento
//Realizzate almeno le tabelle dei documenti e dei prestiti con le opportune relazioni; qui potete inserire solo un campo nome cliente nel prestito e ignorare la parte di registrazione richiesta
//nel database potete usare una sola colonna varchar, direttamente nel documento, per inserire il nome e cognome dell’autore
//Bonus: implementate anche la tabella utente e i controllo di registrazione (che significa che l’utente è dentro al db e quindi prima di fare il prestito deve essere trovato dal bibliotecario attraverso il sistema)









//Esercizio di oggi (nome repo): csharp - biblioteca - db
//Riprendiamo l’esercizio della biblioteca considerando però aclune varianti:
//non è necessario (ma consigliato) ragionare con gli oggetti.
//evitiamo categoricamente la questione dell’eredità tra oggetti
//potete implementare una singola tabella per tutti i documenti: ovviamente in questo caso ci dovrà essere una colonna che gestisce il tipo di documento
//Realizzate almeno le tabelle dei documenti e dei prestiti con le opportune relazioni; qui potete inserire solo un campo nome cliente nel prestito e ignorare la parte di registrazione richiesta
//Bonus: implementate anche la tabella utente e i controllo di registrazione (che significa che l’utente è dentro al db e quindi prima di fare il prestito deve essere trovato dal bibliotecario attraverso il sistema)
//Si vuole progettare un sistema per la gestione di una biblioteca. Gli utenti si possono registrare al sistema, fornendo:
//cognome,
//nome,
//email,
//password,
//recapito telefonico,
//Gli utenti registrati possono effettuare dei prestiti sui documenti che sono di vario tipo (libri, DVD). I documenti sono caratterizzati da:
//un codice identificativo di tipo stringa (ISBN per i libri, numero seriale per i DVD),
//titolo,
//anno,
//settore(storia, matematica, economia, …),
//stato(In Prestito, Disponibile),
//uno scaffale in cui è posizionato,
//un autore (Nome, Cognome).
//Per i libri si ha in aggiunta il numero di pagine, mentre per i dvd la durata.
//L’utente deve poter eseguire delle ricerche per codice o per titolo e, eventualmente, effettuare dei prestiti registrando il periodo (Dal/Al) del prestito e il documento.
//Deve essere possibile effettuare la ricerca dei prestiti dato nome e cognome di un utente.
