using LanchesON.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanchesON.Controllers
{
    public class LancheController : Controller
    {
        /* Usado para acessar o banco de dados, para isso irei injetar uma instancia do repository, eu posso fazer isso pois eu
        ja referenciei no meu arquivo "startup" o meu repositório como "Servico", entao eu posso usa-lo como DI */
        private readonly ILancheRepository _repository;

        public LancheController(ILancheRepository repository)
        {
            _repository = repository;
        }

        // Aqui eu quero listar os meus lanches, entao terei que acessar o banco de dados
        public IActionResult List() 
        {
            // Acessar lista de lanches
            var lanches = _repository.Lanches;
            // Retornar lista de lanches
            // Como nao foi informado o nome da View, será procurada uma view com o nome "List.cshtm" (nome do método)
            return View(lanches);
        }
    }
}
