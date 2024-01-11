using System;
using System.Linq;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;

// Classe responsável pela interação com o Spotify
class SpotifyManager
{
    private SpotifyWebAPI _api;

    // Método para autenticar o aplicativo com o Spotify
    public async Task Authenticate(string clientId, string clientSecret)
    {
        var auth = new AuthorizationCodeAuth(
            clientId,
            clientSecret,
            new Uri("http://localhost:4002"),
            new Uri("http://localhost:4002"),
            Scope.UserReadPrivate | Scope.UserReadEmail | Scope.PlaylistReadPrivate | Scope.PlaylistModifyPrivate
        );

        var uri = auth.GetUri();
        Console.WriteLine($"Por favor, acesse: {uri}");
        Console.WriteLine("Insira o código de autorização:");

        var code = Console.ReadLine();

        var token = await auth.ExchangeCode(code);
        _api = new SpotifyWebAPI
        {
            AccessToken = token.AccessToken,
            TokenType = token.TokenType
        };
    }

    // Método para realizar uma pesquisa no Spotify e exibir os resultados
    public async Task SearchAndDisplayResults(string searchTerm)
    {
        if (_api == null)
        {
            Console.WriteLine("Erro: Autenticação necessária.");
            return;
        }

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            Console.WriteLine("Erro: Campo em branco. Insira um termo de pesquisa válido.");
            return;
        }

        var searchResult = await _api.SearchItemsAsync(searchTerm, SearchType.Track);

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

class SpotifySearch
{
    // Método principal
    static async Task Main(string[] args)
    {
        // Criar uma instância do gerenciador do Spotify
        var spotifyManager = new SpotifyManager();

        // Configurar credenciais do aplicativo
        var clientId = "SEU_CLIENT_ID";
        var clientSecret = "SEU_CLIENT_SECRET";

        // Autenticar com o Spotify
        await spotifyManager.Authenticate(clientId, clientSecret);

        // Solicitar ao usuário uma pesquisa e exibir os resultados
        Console.WriteLine("Digite o nome da música ou artista:");
        var searchTerm = Console.ReadLine();
        await spotifyManager.SearchAndDisplayResults(searchTerm);
    }
}
