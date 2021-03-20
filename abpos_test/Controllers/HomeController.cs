using abpos_test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using abpos_test.DataConnection.IRepository;
using abpos_test.DataConnection.Repository;
using abpos_test.Models.DB;
using X.PagedList;

namespace abpos_test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public IRepository<Vehiculos> _vehiculo = null;
        private abposus_dbContext _context = null;
        const int pageSize = 5;

        public HomeController(ILogger<HomeController> logger,abposus_dbContext context)
        {
            _logger = logger;
            this._context = context;
            _vehiculo = new BaseRepository<Vehiculos>(_context);
        }

        public IActionResult Index(int? page,string orden)
        {
            var result = _vehiculo.Get(filter: null, orderBy: null, includeProperties: null);
            ViewBag.ordenAnio = String.IsNullOrEmpty(orden) ? "anio_desc" : "";
            ViewBag.ordenMarca = orden == "marca" ? "marca_desc" : "marca";
            ViewBag.ordenModelo = orden == "modelo" ? "modelo_desc" : "modelo";
            int pageNumber = (page ?? 1);

            switch (orden)
            {
                case "anio_desc":
                    result = result.OrderByDescending(x => x.Anio);
                    break;
                case "marca":
                    result = result.OrderBy(x => x.Marca);
                    break;
                case "marca_desc":
                    result = result.OrderByDescending(x => x.Marca);
                    break;
                case "modelo":
                    result = result.OrderBy(x => x.Modelo);
                    break;
                case "modelo_desc":
                    result = result.OrderByDescending(x => x.Modelo);
                    break;
                default:
                    result = result.OrderBy(x => x.Anio);
                    break;
            }
            return View(result.ToList().ToPagedList(pageNumber,pageSize));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Vehiculos vehiculo)
        {
            if (!ModelState.IsValid)
            {
                return View(vehiculo);
            }
            _vehiculo.Insert(vehiculo);
            _vehiculo.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int? id)
        {
            if (id!=null)
            {
                var result = _vehiculo.GetById(id);
                return View(result);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                var result = _vehiculo.GetById(id);
                return View(result);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Vehiculos vehiculo)
        {
            if (!ModelState.IsValid)
            {
                return View(vehiculo);
            }
            Vehiculos vehiculos_old = _vehiculo.GetById(vehiculo.IdVehiculo);
            vehiculos_old.Marca = vehiculo.Marca;
            vehiculos_old.Anio = vehiculo.Anio;
            vehiculos_old.Modelo = vehiculo.Modelo;
            _vehiculo.Update(vehiculos_old);
            _vehiculo.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            _vehiculo.Delete(id);
            _vehiculo.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
