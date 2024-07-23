using LanchesON.Context;
using LanchesON.Repositories.Interfaces;
using LanchesON.Repositories;
using Microsoft.EntityFrameworkCore;
using LanchesON.Models;
using LanchesMac.Repositories;
using Microsoft.AspNetCore.Identity;
using LanchesMac.Services;

namespace LanchesON;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // Responsável por configurar os serviços que a aplicação vai usar através de injeção de dependência
    public void ConfigureServices(IServiceCollection services)
    {
        // Adiciona o contexto do banco de dados AppDbContext como um serviço e configura para usar o SQL Server como o provedor de banco de dados.
        // O 'options' aqui é um objeto DbContextOptionsBuilder que permite configurar o contexto.
        services.AddDbContext<AppDbContext>(options =>
            // Configura o contexto para usar o SQL Server.
            // Obtém a string de conexão do banco de dados a partir da configuração da aplicação,
            // usando a chave "DefaultConnection" do arquivo appsettings.json
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        // CASO VOCE QUEIRA SOBRESCREVER O PADRAO DO IDENTITY
        //services.Configure<IdentityOptions>(options =>
        //{
        //    // Default Password settings.
        //    options.Password.RequireDigit = false;
        //    options.Password.RequireLowercase = false;
        //    options.Password.RequireNonAlphanumeric = false;
        //    options.Password.RequireUppercase = false;
        //    options.Password.RequiredLength = 3;
        //    options.Password.RequiredUniqueChars = 1;
        //});

        /* O que faz: Diz ao contêiner de DI para criar novas instâncias de LancheRepository e CategoriaRepository sempre que ILancheRepository
        e ICategoriaRepository forem solicitados */
        services.AddTransient<ILancheRepository, LancheRepository>();
        services.AddTransient<ICategoriaRepository, CategoriaRepository>();
        services.AddTransient<IPedidoRepository, PedidoRepository>();
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin",
                politica =>
                {
                    politica.RequireRole("Admin");
                });
        });

        // Registrar IHttpContextAccessor
        /* Registra o serviço IHttpContextAccessor no contêiner de injeção de dependência do ASP.NET Core. Isso permite que você acesse o
         * HttpContext atual em qualquer lugar do seu aplicativo, incluindo classes que não são controllers ou middleware, como serviços e
         * repositórios. Em resumo, ela facilita o acesso ao contexto da requisição HTTP atual em qualquer parte do aplicativo. */
        services.AddHttpContextAccessor();
        // Instruçao lambda para definir a classe CarrinhoComrpra e "GetCarrinho" (Método do Model "CarrinhoCompra") para já obter um "Carrinho"
        // "AddScoped" criará instancia CarrinhoCompra a cada request (se dois clientes solicitarem objeto carrinho ao mesmo tempo, terao instancias diferentes)
        /* Quando um componente da aplicação solicitar uma instância de CarrinhoCompra, o método GetCarrinho será chamado, possivelmente
        configurando ou retornando um carrinho de compras específico para a requisição atual */
        services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

        // O que faz: Adiciona suporte para controladores e visualizações MVC
        services.AddControllersWithViews();

        // Registrando os Middlewares
        services.AddMemoryCache();
        services.AddSession();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedUserRoleInitial seedUserRoleInitial)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        // Ativar sessao
        app.UseSession();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

            endpoints.MapControllerRoute(
            name: "categoriaFiltro",
            pattern: "Lanche/{action}/{categoria?}",
            defaults: new { Controller = "Lanche", action = "List" });

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}