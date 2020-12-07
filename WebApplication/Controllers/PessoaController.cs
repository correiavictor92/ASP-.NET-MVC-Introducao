using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PessoaController : Controller
    {

        // GET: /Pessoa/
        public ActionResult Index()
        {
            ViewBag.Mensagem = "Minha primeira View";
            return View();
        }


        // GET: /Create/
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Create/
        [HttpPost]
        public ActionResult Create(PessoaModels model)
        {
            ModelState.Remove("Codigo");

            List<PessoaModels> lista = new List<PessoaModels>();

            if (ModelState.IsValid)
            {
                if (Session["ListaPessoas"] != null)
                {
                    lista.AddRange((List<PessoaModels>)Session["ListaPessoas"]);
                }

                model.Id = lista.Count + 1;

                lista.Add(model);
                Session["ListaPessoas"] = lista;
            }
            else
                return View(model);
            return View("List", lista);
        }

        // GET : /List/
        public ActionResult List()
        {
            if (Session["ListaPessoas"] != null)
            {
                var model = (List<PessoaModels>)Session["ListaPessoas"];
                return View(model);
            }

            return View(new List<PessoaModels>());
        }


        // GET: /Edit/
        public ActionResult Edit(int id)
        {
            //Recuperar o objeto com o id
            //Enviar o objeto encontrado para a View de Edição

            if (((List<PessoaModels>)Session["ListaPessoas"]).Where(p => p.Id == id).Any())
            {
                var model = ((List<PessoaModels>)Session["ListaPessoas"])
                    .Where(p => p.Id == id).FirstOrDefault();

                return View("Create", model);
            }

            return View("Create", new PessoaModels());
        }

        // POST: /Edit/
        [HttpPost]
        public ActionResult Edit(PessoaModels model)
        {
            //Recuperar o objeto com o id
            //Alterar objeto com o objeto do parametro
            //Aplicar/Salvar objeto alterado na fonte de dados

            if (ModelState.IsValid)
            {
                if (Session["ListaPessoas"] != null)
                {
                    if (((List<PessoaModels>)Session["ListaPessoas"])
                        .Where(p => p.Id == model.Id).Any())
                    {
                        var modelBase = ((List<PessoaModels>)Session["ListaPessoas"])
                            .Where(p => p.Id == model.Id).FirstOrDefault();

                        //Atualiza seu registro com o model enviado por parametro...
                        ((List<PessoaModels>)Session["ListaPessoas"])[model.Id - 1] = model;
                    }

                    var lista = (List<PessoaModels>)Session["ListaPessoas"];
                    return View("List", lista);
                }
                else
                {
                    return View(new List<PessoaModels>());
                }
            }
            else
            {
                return View("Create", model);
            }  
        }

        public ActionResult Delete(int id)
        {
            if (Session["ListaPessoas"] != null && id > 0)
            {
                if (((List<PessoaModels>)Session["ListaPessoas"])
                    .Where(p => p.Id == id).Any())
                {
                    var modelBase = ((List<PessoaModels>)Session["ListaPessoas"])
                        .Where(p => p.Id == id).FirstOrDefault();

                    var lista = ((List<PessoaModels>)Session["ListaPessoas"]);
                    lista.Remove(modelBase);

                    Session["ListaPessoas"] = lista;
                    return View("List", lista);
                }
            }
            return View("List", new List<PessoaModels>());
        }
	}
}