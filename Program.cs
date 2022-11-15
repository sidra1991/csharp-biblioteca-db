// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

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

    public Documento(string codice, string titolo, string anno, string settore, bool stato, string scaffale, string autore)
    {
        Codice = codice;
        Titolo = titolo;
        Anno = anno;
        Settore = settore;
        Stato = stato;
        Scaffale = scaffale;
        Autore = autore;
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
