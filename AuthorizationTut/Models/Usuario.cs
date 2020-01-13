using System.Collections.Generic;

namespace AuthorizationTut.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Role { get; set; }

        public Usuario Get(string login, string senha)
        {
            // Quando o backend recebe via POST um desses pares, login e senha e ele está válido
            // conforme a regra da anotação [Authorize] (passo 10) então a api responde com um
            // JSON semelhante ao seguinte: 
            /*
             {
                 "access_token": "JfNtKRDa9VfexHd2p7A7s9yS8kSZAXmIzRSyjYSJgZWdHiLP_hdxpnBIraw0cMvSkavjr4EeTjvG1smzmX4ZeCbuSgb2uCMp1SbKHYAKln1EDh-0tUkEQ7C1cSYBwpxhy4Shfa08hSNRETS6Lly9NccS6k5IgtKjEXqv-d4Sar7WJKJZKeGkJgdwc3XMnHghfpJHKETXhHGb4_k49akcnEkp45H3u7i6Be9jOT0bxR0qzXLdpn6npzHCZUaolUNHngvgUrX9bdWlPvjX8sDhJz91LFjUtriFtMmH2Bb3pdo4zZvy_wPl-BW95Lggom1q",
                 "token_type": "bearer",
                 "expires_in": 86399
             }
             e o valor desse access_token tem que ser passado no méto GET para que que o frontEnd tenha acesso
             aos dados. Isso é passado no Header assim... Key: Authorization / Value(com a palavra Bearer): Bearer JfNtKRDa9VfexHd2p7A7s9yS8kSZAXmIzRSyjYSJgZWdHiLP_hdxpnBIraw0cMvSkavjr4EeTjvG1smzmX4ZeCbuSgb2uCMp1SbKHYAKln1EDh-0tUkEQ7C1cSYBwpxhy4Shfa08hSNRETS6Lly9NccS6k5IgtKjEXqv-d4Sar7WJKJZKeGkJgdwc3XMnHghfpJHKETXhHGb4_k49akcnEkp45H3u7i6Be9jOT0bxR0qzXLdpn6npzHCZUaolUNHngvgUrX9bdWlPvjX8sDhJz91LFjUtriFtMmH2Bb3pdo4zZvy_wPl-BW95Lggom1q
             */
            List<Usuario> usuarios = new List<Usuario>
            {
                new Usuario(){ Id=1, Login="alfa@a.com", Senha="alfa", Nome="Alfa", Role="Usuario"},
                new Usuario(){ Id=2, Login="beta@a.com", Senha="beta", Nome="Beta", Role="Supervisor"},
                new Usuario(){ Id=3, Login="gama@a.com", Senha="gama", Nome="Gama", Role="Admin"},

            };

            return usuarios.Find(u => u.Login == login && u.Senha == senha);
        }
    }
}