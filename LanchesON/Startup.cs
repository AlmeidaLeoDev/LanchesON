using LanchesMac.Context;
using LanchesON.Repositories.Interfaces;
using LanchesON.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac;
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
        /* O que faz: Diz ao contêiner de DI para criar novas instâncias de LancheRepository e CategoriaRepository sempre que ILancheRepository
        e ICategoriaRepository forem solicitados */
        services.AddTransient<ILancheRepository, LancheRepository>();
        services.AddTransient<ICategoriaRepository, CategoriaRepository>();
        // O que faz: Adiciona suporte para controladores e visualizações MVC
        services.AddControllersWithViews();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}