using LanchesON.Models;
using LanchesON.ViewModels;
using LanchesON.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LanchesON.Controllers 
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(ILancheRepository lancheRepository,
            CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            // Obtém os itens do carrinho de compras
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            // Define os itens do carrinho de compras
            _carrinhoCompra.CarrinhoCompraItems = itens;

            // Cria uma instância da ViewModel do carrinho de compras
            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                // Atribui o carrinho de compras atual
                CarrinhoCompra = _carrinhoCompra,
                // Calcula o total do carrinho
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };
            // Retorna a View com a ViewModel do carrinho de compras
            return View(carrinhoCompraVM);
        }

        [Authorize]
        public IActionResult AdicionarItemNoCarrinhoCompra(int lancheId)
        {
            // Seleciona o lanche com base no ID fornecido
            var lancheSelecionado = _lancheRepository.Lanches
                                        .FirstOrDefault(p => p.LancheId == lancheId);

            // Se o lanche for encontrado, adiciona ao carrinho
            if (lancheSelecionado != null)
            {
                _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);
            }
            /* 
               O return RedirectToAction("Index"); é utilizado para redirecionar o fluxo da aplicação para a ação Index do mesmo controlador. 
               Isso é útil após realizar operações como adicionar ou remover itens do carrinho, garantindo que a página seja atualizada com o 
               estado mais recente do carrinho de compras 
            */
            // Redireciona para a ação Index
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult RemoverItemDoCarrinhoCompra(int lancheId)
        {
            // Seleciona o lanche com base no ID fornecido
            var lancheSelecionado = _lancheRepository.Lanches
                                        .FirstOrDefault(p => p.LancheId == lancheId);

            // Se o lanche for encontrado, remove do carrinho
            if (lancheSelecionado != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
            }
            // Redireciona para a ação Index
            return RedirectToAction("Index");
        }
    }
}