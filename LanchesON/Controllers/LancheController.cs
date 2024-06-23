using LanchesON.Repositories.Interfaces;
using LanchesON.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesON.Controllers
{
    public class LancheController : Controller
    {
        /* Usado para acessar o banco de dados, para isso irei injetar uma instancia do repository, eu posso fazer isso pois eu
        ja referenciei no meu arquivo "startup" o meu repositório como "Servico", entao eu posso usa-lo como DI */
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        /* O método retorna uma view, passando o ViewModel lanchesListViewModel para ela. Isso significa que a view
        associada a esta ação será renderizada e receberá o LancheListViewModel como seu modelo */
        public IActionResult List() 
        {
            var lanchesListViewModel = new LancheListViewModel();
            lanchesListViewModel.Lanches = _lancheRepository.Lanches;
            lanchesListViewModel.CategoriaAtual = "Categoria atual";
            return View(lanchesListViewModel);
        }
    }
}
