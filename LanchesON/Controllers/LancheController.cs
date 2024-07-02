﻿using LanchesON.Models;
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
        public IActionResult List(string categoria) 
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                //if (string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase))
                //{
                //    lanches = _lancheRepository.Lanches
                //        .Where(l => l.Categoria.CategoriaNome.Equals("Normal"))
                //        .OrderBy(l => l.Nome);
                //}
                //else
                //{
                //    lanches = _lancheRepository.Lanches
                //       .Where(l => l.Categoria.CategoriaNome.Equals("Natural"))
                //       .OrderBy(l => l.Nome);
                //}

                lanches = _lancheRepository.Lanches
                    .Where(l => l.Categoria.CategoriaNome.Equals(categoria))
                    .OrderBy(c => c.Nome);

                categoriaAtual = categoria;
            }

            var lanchesListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View(lanchesListViewModel);
        }
    }
}
