using System;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;

class Program
{
    static async Task Main(string[] args)
    {
        // Configurações do aplicativo no Spotify Developer Dashboard
        var clientId = "SEU_CLIENT_ID";
        var clientSecret = "SEU_CLIENT_SECRET";

        // Configurações de autenticação
        var auth = new AuthorizationCodeAuth(
            clientId,
            clientSecret,
            new Uri("http://localhost:4002"),
            new Uri("http://localhost:4002"),
            Scope.UserReadPrivate | Scope.UserReadEmail | Scope.PlaylistReadPrivate | Scope.PlaylistModifyPrivate
        );

        // Obtém a URL de autorização
        var uri = auth.GetUri();
        Console.WriteLine($"Por favor, acesse: {uri}");
        Console.WriteLine("Insira o código de autorização:");

        // Obtém o código de autorização do usuário
        var code = Console.ReadLine();

        // Faz a solicitação de token
        var token = await auth.ExchangeCode(code);
        var api = new SpotifyWebAPI
        {
            AccessToken = token.AccessToken,
            TokenType = token.TokenType
        };

        // Pesquisa por músicas
        // ...

// Pesquisa por músicas
Console.WriteLine("Digite o nome da música ou artista:");
var searchTerm = Console.ReadLine();

if (string.IsNullOrWhiteSpace(searchTerm))
{
    Console.WriteLine("Campo em branco. Por favor, insira um termo de pesquisa válido.");
}
else
{
    var searchResult = await api.SearchItemsAsync(searchTerm, SearchType.Track);

    if (searchResult.HasError())
    {
        Console.WriteLine($"Erro na pesquisa: {searchResult.Error.Message}");
    }
    else if (searchResult.Tracks != null && searchResult.Tracks.Items.Any())
    {
        foreach (var track in searchResult.Tracks.Items)
        {
            Console.WriteLine($"Música: {track.Name}, Artista: {track.Artists[0].Name}");
        }
    }
    else
    {
        Console.WriteLine("Nenhuma música ou artista encontrado.");
    }
}


    }
}
