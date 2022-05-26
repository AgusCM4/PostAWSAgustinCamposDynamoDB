using Microsoft.AspNetCore.Mvc;
using PostAWSAgustinCamposDynamoDB.Models;
using PostAWSAgustinCamposDynamoDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostAWSAgustinCamposDynamoDB.Controllers
{
    public class ProductosController : Controller
    {
        private ServiceDynamoDB service;

        public ProductosController(ServiceDynamoDB service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Producto> productos = await this.service.GetProductoAsync();
            return View(productos);
        }

        public async Task<IActionResult> Details(int id)
        {
            Producto pro = await this.service.FindProductoAsync(id);
            return View(pro);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.service.DeleteProductoAsync(id);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Producto pro)
        {
            await this.service.CreateProductoAsync(pro);
            return RedirectToAction("Index");
        }
    }
}
