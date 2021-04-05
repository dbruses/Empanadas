using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABMPedidos.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Text;
using Newtonsoft.Json;

namespace ABMPedidos.Controllers
{
    public class PedidosController : Controller
    {
        public string apiurl = "https://localhost:44365/api/Empanadas/";

        public async Task<IActionResult> Index()
        {
            List<Pedido> pedidosList = new List<Pedido>();
            using (var httpClient = new HttpClient())
            {
                string url = apiurl + "GetPedidosActivos";
                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pedidosList = JsonConvert.DeserializeObject<List<Pedido>>(apiResponse);
                }
            }
            return View(pedidosList);
        }

        public async Task<IActionResult> VerEdit(int id)
        {
            Pedido pedido = new Pedido();
            List<MediosDePago> mediosDePago = new List<MediosDePago>();
            List<Gusto> gustos = new List<Gusto>();
            
            using (var httpClient = new HttpClient())
            {
                string apiResponse = string.Empty;
                string url = apiurl + "GetPedido?Id=" + id.ToString();
                using (var response = await httpClient.GetAsync(url))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    pedido = JsonConvert.DeserializeObject<Pedido>(apiResponse);
                }
                url = apiurl + "GetMediosDePagos";
                using (var response = await httpClient.GetAsync(url))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    mediosDePago = JsonConvert.DeserializeObject<List<MediosDePago>>(apiResponse);
                }
                url = apiurl + "GetGustos";
                using (var response = await httpClient.GetAsync(url))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    gustos = JsonConvert.DeserializeObject<List<Gusto>>(apiResponse);
                }
            }
            ViewBag.MediosDePago = mediosDePago;
            ViewBag.Gustos = gustos;

            return View(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> GurdaPedido(Pedido pedido) 
        {
            bool pedidoGuardado = false;
            using (var httpClient = new HttpClient())
            {
                string url = apiurl + "PostGuardaPedido";
                string apiResponse = string.Empty;

                var jsonString = JsonConvert.SerializeObject(pedido);
                HttpContent c = new StringContent(jsonString, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(url,c))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    pedidoGuardado = JsonConvert.DeserializeObject<bool>(apiResponse);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<string> DeletePedido(long id)
        {
            string pedidoEliminado = string.Empty;
            using (var httpClient = new HttpClient())
            {
                string url = apiurl + "DelPedido?idPedido=" + id;
                string apiResponse = string.Empty;
                
                using (var response = await httpClient.DeleteAsync(url))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    pedidoEliminado = JsonConvert.DeserializeObject<string>(apiResponse);
                }
            }

            return pedidoEliminado;
        }
    }
}
