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



//using System.Diagnostics.Metrics;
//using System.Runtime.ConstrainedExecution;

Biblioteca biblioteca = new Biblioteca(connessioneSql);

void menu()
{
    Console.WriteLine("ora creo le crud e mi fermo,ho capito finalmente ma si sta facendo tardi");

    Console.WriteLine("1. crea nuovo utente");
    Console.WriteLine("2. modifica utente");
    Console.WriteLine("3. elimina utente");
    int scelta = Convert.ToInt32(Console.ReadLine());
    switch (scelta)
    {
        case 1:
            biblioteca.CreaNuovoUtente(connessioneSql);
            break ;
        case 2:
            biblioteca.UpdateUtente(connessioneSql);
            break;
        case 3:
            biblioteca.eliminaUtente(connessioneSql);
            break;
    }
}
menu();
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
//finally
//{
//    connessioneSql.Close();
//}

//try
//{
//    connessioneSql.Open();


//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
//finally
//{
//    connessioneSql.Close();
//}


public class Biblioteca
{

    public List<Utente> utenti;
    public List<Documento> documenti;
    public Biblioteca(SqlConnection connessioneSql)
    {
        utenti = new List<Utente>();
        documenti = new List<Documento>();
        PopolaClassi(connessioneSql);
    }

    //L’utente deve poter eseguire delle ricerche per codice o per titolo e, eventualmente, effettuare dei prestiti registrando il periodo (Dal/Al) del prestito e il documento.
    //Deve essere possibile effettuare la ricerca dei prestiti dato nome e cognome di un utente.
    //List<Documento> RicercaPerCodiceOTitolo(string ricerca)
    //{
    //    List<Documento> trovato = new List<Documento>();
    //    foreach (var item in ListaDocumenti)
    //    {
    //        if (item.Titolo == ricerca || item.Codice == ricerca)
    //        {
    //            trovato.Add(item);
    //        }


    //    }
    //    return trovato;
    //}

    private void PopolaClassi(SqlConnection connessioneSql)
    {
        try
        {

            connessioneSql.Open();
            string query = "SELECT * FROM Utente";
            SqlCommand cmd = new SqlCommand(query, connessioneSql);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                string cognome = reader.GetString(1);
                string nome = reader.GetString(2);
                string email = reader.GetString(3);
                string password = reader.GetString(4);
                int numeroTelefono = reader.GetInt32(5);

                Utente utente = new Utente(cognome, nome, email, password, numeroTelefono);
                utenti.Add(utente);
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

        try
        {
            connessioneSql.Open();
            string query2 = "SELECT * FROM Documento";
            SqlCommand cmd2 = new SqlCommand(query2, connessioneSql);
            SqlDataReader readerA = cmd2.ExecuteReader();

            while (readerA.Read())
            {
                string? codice = "00000000";
                string titolo = readerA.GetString(2);
                string anno = readerA.GetString(3);
                string settore = readerA.GetString(4);
                bool stato = true;
                string scaffale = readerA.GetString(6);
                string autore = readerA.GetString(7);

                Documento documento = new Documento(codice, titolo, anno, settore, stato, scaffale, autore);
                documenti.Add(documento);
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

    }

    public void CreaNuovoUtente(SqlConnection connessioneSql)
    {
        Console.WriteLine("inserire cognome");
        string cognome = Console.ReadLine();
        Console.WriteLine("inserire nome");
        string nome = Console.ReadLine();
        Console.WriteLine("inserire email");
        string email = Console.ReadLine();
        Console.WriteLine("inserire password");
        string password = Console.ReadLine();
        Console.WriteLine("inserire numero di telefono");
        int numeroTelefono =Convert.ToInt32( Console.ReadLine());
        
        try
        {
            connessioneSql.Open();
            
                string query = "INSERT INTO Utente (id, cognome, nome, email, password, numeroDiTelefono) VALUES (@dato1, @dato2, @dato3, @dato4, @dato5, @dato6)";
                SqlCommand cmd = new SqlCommand(query, connessioneSql);
            cmd.Parameters.Add(new SqlParameter("@dato1", utenti.Count + 1)) ;

                cmd.Parameters.Add(new SqlParameter("@dato2", cognome));
                cmd.Parameters.Add(new SqlParameter("@dato3", nome));
                cmd.Parameters.Add(new SqlParameter("@dato4", email));
                cmd.Parameters.Add(new SqlParameter("@dato5", password));
                cmd.Parameters.Add(new SqlParameter("@dato6", numeroTelefono));

                int affectedRows = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connessioneSql.Close();
        }



        Utente utente = new Utente(cognome, nome, email, password, numeroTelefono);
        utenti.Add(utente);
    }

    public void UpdateUtente(SqlConnection connessioneSql)
    {
        int utente = CercaUtente();

        Console.WriteLine("inserire cognome");
        string cognome = Console.ReadLine();
        Console.WriteLine("inserire nome");
        string nome = Console.ReadLine();
        Console.WriteLine("inserire email");
        string email = Console.ReadLine();
        Console.WriteLine("inserire password");
        string password = Console.ReadLine();
        Console.WriteLine("inserire numero di telefono");
        int numeroTelefono = Convert.ToInt32(Console.ReadLine());

        try
        {
            connessioneSql.Open();

            string query = "UPDATE Utente  SET cognome=@dato2, nome=@dato3, email=@dato4, password=@dato5, numeroDiTelefono=@dato6  WHERE Id=@Id;" ;
            SqlCommand cmd = new SqlCommand(query, connessioneSql);
            cmd.Parameters.Add(new SqlParameter("@Id", utente));
            cmd.Parameters.Add(new SqlParameter("@dato2", cognome));
            cmd.Parameters.Add(new SqlParameter("@dato3", nome));
            cmd.Parameters.Add(new SqlParameter("@dato4", email));
            cmd.Parameters.Add(new SqlParameter("@dato5", password));
            cmd.Parameters.Add(new SqlParameter("@dato6", numeroTelefono));

            int affectedRows = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connessioneSql.Close();
        }


    }

    public void eliminaUtente(SqlConnection connessioneSql)
    {
        try
        {
            int utente = CercaUtente();
            connessioneSql.Open();

            string query = "DELETE FROM Utente WHERE Id = @id";
            SqlCommand cmd = new SqlCommand(query, connessioneSql);
            cmd.Parameters.Add(new SqlParameter("@id", utente));
            int affectedRows = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connessioneSql.Close();
        }

    }
    public int CercaUtente()
    {
        Console.WriteLine("seleziona l'idice del utente che ti interessa");
        int i = 1;
        foreach (Utente utente in utenti)
        {
            Console.WriteLine(i + ". " + utente.Cognome);
            i++;
        }


        return Convert.ToInt32(Console.ReadLine()) ;
    }
}

public class Documento
{
    //un codice identificativo di tipo stringa (ISBN per i libri, numero seriale per i DVD),
    public string? Codice { get; set; }

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

    public Documento(string? codice, string titolo, string anno, string settore,bool? stato, string scaffale, string autore)
    {
        if (codice != null)
        {
            Codice = codice;
        }
        else
        {
            Codice = Generacodice();
        }

        if (stato != null)
        {
            Stato =  Convert.ToBoolean(stato);
            
        }
        else
        {
            Stato = true;
        }


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




//creazione pseudo clienti
//for (int i = 0; i < 100; i++)
//{
//    Random rand = new Random();

//    string nome = "utente" + i;
//    string cognome = "cognome" + i;
//    string email = "utente" + i;
//    string password = "utente" + i + "@email.it" ;
//    int numeroTelefono = rand.Next(111111111,999999999) ;
//    Utente utente = new Utente(cognome,nome,email,password,numeroTelefono);
//    biblioteca.ListaUtenti.Add(utente);
//}

//for (int i = 0; i < 100; i++)
//{
//    string[] seTTore = { "horror", "fantasy", "story", "math" };
//    string[] auTTore = { "gianni", "maniaco", "storto", "pollo" };
//    Random rand = new Random();



//    string titolo = "titolo" + i;
//    string anno = new DateTime( rand.Next(1900,2022),rand.Next(1,12),rand.Next(1,28) ).ToString("D");
//    string settore = seTTore[rand.Next(0,3)];
//    string scaffale = seTTore[rand.Next(0, 3)] + i;
//    string autore = auTTore[rand.Next(0, 3)];
//    Documento documento = new Documento(titolo, anno, settore, scaffale, autore);
//    biblioteca.ListaDocumenti.Add(documento);
//}


//try
//{
//    connessioneSql.Open();


//    int i = 1;
//    foreach (var item in biblioteca.ListaUtenti)
//    {
//        string query = "INSERT INTO Utente (id, cognome, nome, email, password, numeroDiTelefono) VALUES (@dato1, @dato2, @dato3, @dato4, @dato5, @dato6)";
//        SqlCommand cmd = new SqlCommand(query, connessioneSql);
//        cmd.Parameters.Add(new SqlParameter("@dato1", i));
//        cmd.Parameters.Add(new SqlParameter("@dato2", item.Cognome));
//        cmd.Parameters.Add(new SqlParameter("@dato3", item.Nome));
//        cmd.Parameters.Add(new SqlParameter("@dato4", item.Email));
//        cmd.Parameters.Add(new SqlParameter("@dato5", item.Password));
//        cmd.Parameters.Add(new SqlParameter("@dato6", item.NumeroTelefono));
//        i++;
//        int affectedRows = cmd.ExecuteNonQuery();

//    }

//    int i = 1;
//    foreach (var item in biblioteca.ListaDocumenti)
//    {
//        string query = "INSERT INTO Documento (id, titolo, anno, settore, stato, scaffale, autore) VALUES (@dato1, @dato2, @dato3, @dato4, @dato5, @dato6, @dato7)";
//        SqlCommand cmd = new SqlCommand(query, connessioneSql);
//        cmd.Parameters.Add(new SqlParameter("@dato1", i));
//        cmd.Parameters.Add(new SqlParameter("@dato2", item.Titolo));
//        cmd.Parameters.Add(new SqlParameter("@dato3", item.Anno));
//        cmd.Parameters.Add(new SqlParameter("@dato4", item.Settore));
//        cmd.Parameters.Add(new SqlParameter("@dato5", item.Stato));
//        cmd.Parameters.Add(new SqlParameter("@dato6", item.Scaffale));
//        cmd.Parameters.Add(new SqlParameter("@dato7", item.Autore));
//        i++;
//        int affectedRows = cmd.ExecuteNonQuery();
//    }
