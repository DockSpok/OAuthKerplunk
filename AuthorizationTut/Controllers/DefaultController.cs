using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using System.Net.Http;
using System.Linq;
using System.Security.Claims;

namespace AuthorizationTut.Controllers
{
    public class DefaultController : ApiController
    {   // Passo 00: Criado um ASP.NET WebApplication (Empty + opção de Web API)
        // Passo 01: Criando um controlador para o modelo (com ações de Read e Write).
        // GET: api/Default
        // Passo 10: [Authorize] (poderia ter sido criada no início mas é melhor testar primeiro sem restrições.
        // Para obrigar a autenticação. E para Limitar a papéis ou usuários, passar entre parenteses.
        // Fazer isso para cada método que precisar de autorização.
        [Authorize(Roles = "Admin, Supervisor")]
        public IEnumerable<string> Get()
        {
            // Só pra exemplificar como podemos usar dados do context...
            // Isso é importante porque o usuário está autorizado quando faz o login mas
            // o app, sem acessar o contexto, não sabe quem ele é ou de onde vem.
            // Request.GetRequestContext() precisa do NuGet Microsoft.AspNet.WebApi.Owin
            IOwinContext contexto = Request.GetOwinContext();
            string id = contexto.Authentication.User.Claims.FirstOrDefault(i => i.Type == "UserID").Value;
            string nome = contexto.Authentication.User.Claims.FirstOrDefault(n => n.Type == ClaimTypes.Name).Value;

            return new string[]
            {
                id,
                nome,
                // Strings para simples testes iniciais antes de implementar a autendicação.
                "valor 1 qualquer entregue pelo Get", 
                "valor 2 qualquer entregue pelo Get" 
            };

        }

        // GET: api/Default/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Default
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Default/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Default/5
        public void Delete(int id)
        {
        }
    }
}
