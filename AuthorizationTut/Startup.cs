using Microsoft.Owin.Security.OAuth;
using Owin;
using System;

namespace AuthorizationTut
{
    public partial class Startup
    {
        // O passo 04 foi instalar as referencias para Owin:
        // Microsoft.AspNet.WebApi.Owin
        // Microsoft.Owin.Host.Systemweb
        // Microsoft.AspNet.Identity.Owin
        // Passo 05: O nome dessa classe é pura convenção de nome. 
        // (partial) Lembrar que no projeto WebApi já tem uma classe com o mesmo nome, por isso esta deve ser partial
        // Nome da propriedade a seguir também é livre, apenas seguimos o modelo...
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            app.UseOAuthBearerTokens(OAuthOptions); 
        }

        static Startup()
        {
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1.0),
                AllowInsecureHttp = true, // Se tiver um certificado é aqui pode ser falso.
                TokenEndpointPath = new Microsoft.Owin.PathString("/api/token"),
                // Classe 'OAuthProvider' não existia, mando o intelisense criar.. passo 08
                Provider = new OAuthProvider() 
            };
        }
    }
}