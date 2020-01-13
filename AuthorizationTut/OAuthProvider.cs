using AuthorizationTut.Models;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationTut
{
    // Passo 8: A herança criada automaticamente é para uma interface mas 
    // 'preferimos' deixar como a classe ao invés de IOAuthAuthorizationServerProvider
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        // Sobrescrevendo o primeiro método...
        // Notar que esse parâmtro context vai receber todos os dados da conexão que
        // foi estabelecida com o cliente (IP, porta e tudo o mais) 
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                // O método retorna uma task então tem que retornar um método.
                // nesse caso, usando arrow, vamos retornar um método anônimo.
                // recebendo do contexto quais são username e password
                string login = context.UserName;
                string senha = context.Password;

                Usuario user = new Usuario().Get(login, senha);

                if (user != null)
                {
                    List<Claim> claims = new List<Claim>
                    {
                        // Passando para a lista de claims os outros parâmetros de usuário além de Login e Senha...
                        new Claim(ClaimTypes.Name, user.Nome),
                        new Claim("UserID", user.Id.ToString()), // Uma Claim pode ser definida pelo desenvolvedor.
                        new Claim(ClaimTypes.Role, user.Role)
                    };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, Startup.OAuthOptions.AuthenticationType);
                // Passando para o contexto o usuário validado...
                // A partir dos dados de identidade será gerado o Ticket
                context.Validated(new Microsoft.Owin.Security.AuthenticationTicket(
                    claimsIdentity, new Microsoft.Owin.Security.AuthenticationProperties()
                    { // sem parâmetros porque não precisa
                    }));
                }
                else
                {
                    context.SetError("Erro de validação:", "Erro ao tentar autenticar o usuário.");
                }
            });
        }
        // Passo 9: Sobrescrevendo um método para validar o 'cliente' que é quem está consumindo esse serviço aqui.
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Simplesmente valida qualquer cliente e retorna nada... mas num caso real
            // o cliente deve ser validado para maior segurança.
            // E está pronta a api com autenticação por token... 
            // basta colocar as restrições [Authorize] no controller e testar.
            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null); 
        }
    }
}