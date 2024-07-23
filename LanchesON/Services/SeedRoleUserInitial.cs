using Microsoft.AspNetCore.Identity;

namespace LanchesMac.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        // Gerenciadores de usuários e roles do Identity
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        // Construtor que recebe os gerenciadores via injeção de dependência
        public SeedUserRoleInitial(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager; // Inicializa o gerenciador de usuários
            _roleManager = roleManager; // Inicializa o gerenciador de roles
        }

        // Implementação do método SeedRoles
        public void SeedRoles()
        {
            // Verifica se a role "Member" não existe
            if (!_roleManager.RoleExistsAsync("Member").Result)
            {
                IdentityRole role = new IdentityRole(); // Cria uma nova role
                role.Name = "Member"; // Define o nome da role
                role.NormalizedName = "MEMBER"; // Define o nome normalizado da role
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result; // Cria a role no banco de dados
            }
            // Mesmos comentários do "If" acima
            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }

        public void SeedUsers()
        {
            // Verifica se o usuário "usuario@localhost" não existe
            if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser(); // Cria um novo usuário
                user.UserName = "usuario@localhost"; // Define o nome de usuário
                user.Email = "usuario@localhost"; // Define o email do usuário
                user.NormalizedUserName = "USUARIO@LOCALHOST"; // Define o nome de usuário normalizado
                user.NormalizedEmail = "USUARIO@LOCALHOST"; // Define o email normalizado
                user.EmailConfirmed = true; // Confirma o email do usuário
                user.LockoutEnabled = false; // Desabilita o bloqueio do usuário
                user.SecurityStamp = Guid.NewGuid().ToString(); // Gera um carimbo de segurança único

                // Cria o usuário com a senha especificada
                IdentityResult result = _userManager.CreateAsync(user, "Numsey#2022").Result;

                // Verifica se a criação do usuário foi bem-sucedida
                if (result.Succeeded)
                {
                    // Adiciona o usuário à role "Member"
                    _userManager.AddToRoleAsync(user, "Member").Wait();
                }
            }

            // Verifica se o usuário "admin@localhost" não existe
            if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser(); // Cria um novo usuário
                user.UserName = "admin@localhost"; // Define o nome de usuário
                user.Email = "admin@localhost"; // Define o email do usuário
                user.NormalizedUserName = "ADMIN@LOCALHOST"; // Define o nome de usuário normalizado
                user.NormalizedEmail = "ADMIN@LOCALHOST"; // Define o email normalizado
                user.EmailConfirmed = true; // Confirma o email do usuário
                user.LockoutEnabled = false; // Desabilita o bloqueio do usuário
                user.SecurityStamp = Guid.NewGuid().ToString(); // Gera um carimbo de segurança único

                // Cria o usuário com a senha especificada
                IdentityResult result = _userManager.CreateAsync(user, "Numsey#2022").Result;

                // Verifica se a criação do usuário foi bem-sucedida
                if (result.Succeeded)
                {
                    // Adiciona o usuário à role "Admin"
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
