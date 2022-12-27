using System.Diagnostics;
using System.Globalization;
using EstatisticasMVC.DataBase;
using EstatisticasMVC.Entitys;
using EstatisticasMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace EstatisticasMVC.Controllers
{
    
    public class ProdutosController : Controller
    {
      
        public IActionResult Index()
        {
            var repositorio = new MySqlDataBase<Produto>();

            ViewBag.produtos = repositorio.Todos();
            return View();
        }

        public IActionResult Novo()
        {
            var repositorio = new MySqlDataBase<Produto>();

            ViewBag.produtos = repositorio.Todos();
            return View();
        }
        public IActionResult Cadastrar([FromForm] Produto produto)
        {

            string date = produto.DataCriacao.ToString();
            DateOnly dt;
            if (!DateOnly.TryParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                DateOnly.TryParseExact(date, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
            produto.DataCriacao = dt;


            if (string.IsNullOrEmpty(produto.Nome))
            {
                ViewBag.erro = "O nome não pode ser vazio";
                return View();
            }

            var repositorio = new MySqlDataBase<Produto>();

            repositorio.Salvar(produto);
            return Redirect("/produtos");
        }
        [Route("/produtos/{id}/deletar")]
        public IActionResult Apagar([FromRoute]int id)
        {
            var repositorio = new MySqlDataBase<Produto>();

            repositorio.ApagarPorId(id);
            return Redirect("/produtos");
        }

        [Route("/produtos/{id}/editar")]
        public IActionResult Editar([FromRoute] int id)
        {
            var repositorio = new MySqlDataBase<Produto>();
            ViewBag.produto = repositorio.BuscarPorId(id);

            return View();
        }

        [Route("/produtos/{id}/atualizar")]
        public IActionResult Atualizar([FromRoute] int id, [FromForm] Produto produto)
        {
            var repositorio = new MySqlDataBase<Produto>();

            produto.Id = id;
            repositorio.Salvar(produto);
            return Redirect("/produtos");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}