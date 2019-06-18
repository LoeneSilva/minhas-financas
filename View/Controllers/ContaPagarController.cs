using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ContaPagarController : Controller
    {
        // GET: ContaPagar
        public ActionResult Index(string pesquisa)
        {
            FinancasRepository repository = new FinancasRepository();
            List<ContaPagar> contasPagar = repository.ObterTodos(pesquisa);

            ViewBag.ContasPagar = contasPagar;
            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Store(string nome, decimal valor, decimal tipo, string descricao, string status)
        {

            ContaPagar contaPagar = new ContaPagar();
            contaPagar.Nome = nome; ;
            contaPagar.Valor = valor;
            contaPagar.Descricao = descricao;
            contaPagar.Tipo = tipo;
            contaPagar.Status = status;

            FinancasRepository repository = new FinancasRepository();
            repository.Inserir(contaPagar);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            FinancasRepository repository = new FinancasRepository();
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            FinancasRepository repository = new FinancasRepository();
            ContaPagar contaPagar = repository.ObterpeloId(id);
            ViewBag.ContaPagar = contaPagar;
            return View();
            
        }

        public ActionResult Update(int id, string nome, decimal valor, decimal tipo, string descricao, string status)
        {
            ContaPagar contaPagar = new ContaPagar();
            contaPagar.Id = id;
            contaPagar.Nome = nome;
            contaPagar.Valor = valor;
            contaPagar.Tipo = tipo;
            contaPagar.Descricao = descricao;
            contaPagar.Status = status;
            FinancasRepository repository = new FinancasRepository();
            repository.Atualizar(contaPagar);
            return RedirectToAction("Index");
        }
    }
}