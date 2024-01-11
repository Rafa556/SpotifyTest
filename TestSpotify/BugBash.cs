using System;
using System.Collections.Generic;

class BugBash
{
    private List<string> bugs;

    public BugBash()
    {
        bugs = new List<string>();
    }

    // Adicionar um bug à lista
    public void AddBug(string bugDescription)
    {
        bugs.Add(bugDescription);
        Console.WriteLine($"Bug adicionado: {bugDescription}");
    }

    // Exibir a lista de bugs
    public void DisplayBugs()
    {
        Console.WriteLine("Lista de Bugs:");

        if (bugs.Count == 0)
        {
            Console.WriteLine("Nenhum bug encontrado. Ótimo trabalho!");
        }
        else
        {
            foreach (var bug in bugs)
            {
                Console.WriteLine($"- {bug}");
            }
        }
    }
}

class program
{
    static async Task Main(string[] args)
    {
        var spotifyManager = new SpotifyManager();
        var bugBash = new BugBash();

        // Configurar credenciais do aplicativo
        var clientId = "SEU_CLIENT_ID";
        var clientSecret = "SEU_CLIENT_SECRET";

        // Autenticar com o Spotify
        await spotifyManager.Authenticate(clientId, clientSecret);

        // Solicitar ao usuário um termo de pesquisa e exibir os resultados
        Console.WriteLine("Digite o nome da música ou artista:");
        var searchTerm = Console.ReadLine();
        await spotifyManager.SearchAndDisplayResults(searchTerm);

        // Adicionar um bug à lista de bugs
        Console.WriteLine("Encontrou algum bug? Descreva-o:");
        var bugDescription = Console.ReadLine();
        bugBash.AddBug(bugDescription);

        // Exibir a lista de bugs
        bugBash.DisplayBugs();
    }
}
