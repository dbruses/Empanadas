using EmpanadasWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpanadasWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpanadasController : ControllerBase
    {
        [HttpGet]
        [Route("GetGustos")]
        public IEnumerable<Gusto> GetGustos()
        {
            using (var db = new EmpanadasContext())
            {
                IEnumerable<Gusto> gustos = db.Gustos.ToList();
                return gustos;
            }
        }

        [HttpGet]
        [Route("GetGusto")]
        public Gusto GetGusto(int id)
        {
            using (var db = new EmpanadasContext())
            {
                Gusto gusto = db.Gustos.Find(id);
                return gusto;
            }
        }

        [HttpGet]
        [Route("GetMediosDePagos")]
        public IEnumerable<MediosDePago> GetMediosDePagos()
        {
            using (var db = new EmpanadasContext())
            {
                IEnumerable<MediosDePago> mediosDePagos = db.MediosDePagos.ToList();
                return mediosDePagos;
            }
        }

        [HttpGet]
        [Route("GetPedidos")]
        public IEnumerable<Pedido> GetPedidos()
        {
            using (var db = new EmpanadasContext())
            {
                IEnumerable<Pedido> pedidos = db.Pedidos.ToList();
                return pedidos;
            }
        }

        [HttpGet]
        [Route("GetPedido")]
        public Pedido GetPedido(long id)
        {
            using (var db = new EmpanadasContext())
            {
                Pedido pedido = new Pedido();
                if (id != 0)
                {
                    pedido = db.Pedidos.Find(id);
                    ICollection<DetallePedido> detPed = GetDetallePedido(id);
                    pedido.DetallePedidos = detPed;
                }
                
                return pedido;
            }
        }

        [HttpGet]
        [Route("GetPedidosActivos")]
        public IEnumerable<Pedido> GetPedidosActivos()
        {
            using (var db = new EmpanadasContext())
            {
                var pedidosActivos = db.Pedidos.Where(x => x.Estado == 1);
                if (pedidosActivos.Count() > 0)
                {
                    return (IEnumerable<Pedido>)pedidosActivos.ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        [HttpGet]
        [Route("GetDetallePedido")]
        public ICollection<DetallePedido> GetDetallePedido(long idPedido)
        {
            using (var db = new EmpanadasContext())
            {
                var detallePedidos = db.DetallePedidos.Where(x => x.IdPedido == idPedido);
                if (detallePedidos.Count() > 0)
                {
                    return (ICollection<DetallePedido>)detallePedidos.ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        [HttpDelete]
        [Route("PostEliminaDetallePedido")]
        public bool PostEliminaDetallePedido(long idPedido)
        {
            bool eliminaDetalle = false;

            ICollection<DetallePedido> detPedido = GetDetallePedido(idPedido);


            using (var dbDet = new EmpanadasContext())
            {
                if (detPedido != null)
                {
                    foreach (var det in detPedido)
                    {
                        dbDet.DetallePedidos.Remove(det);
                        dbDet.SaveChanges();
                    }
                }
                
                eliminaDetalle = true;
            }

            return eliminaDetalle;
        }

        [HttpPost]
        [Route("PostGuardaPedido")]
        public async Task<bool> PostGuardaPedido(Pedido pedido)
        {
            bool ok = false;

            using (var db = new EmpanadasContext())
            {
                if (pedido.IdPedido == 0)
                {
                    pedido.Fecha = DateTime.Now;
                    db.Pedidos.Add(pedido);
                }
                else
                {
                    if (PostEliminaDetallePedido(pedido.IdPedido))
                    {
                        db.Pedidos.Update(pedido);
                    }
                }
                
                db.SaveChanges();

                ok = true;
            }

            return ok;
        }

        [HttpDelete]
        [Route("DelPedido")]
        public bool DelPedido(long idPedido)
        {
            bool eliminaPedido = false;

            Pedido pedido = GetPedido(idPedido);

            using (var dbDel = new EmpanadasContext())
            {
                if ((pedido != null) && (pedido.IdPedido != 0))
                {

                    dbDel.Pedidos.Remove(pedido);
                    dbDel.SaveChanges();
                    eliminaPedido = true;
                }
            }

            return eliminaPedido;
        }

        [HttpDelete("{id:long}")]
        public async Task<string> DeletePedido(long id)
        {
            string mensage = string.Empty;
            using (var db = new EmpanadasContext())
            {
                if (id != 0)
                {
                    if (DelPedido(id))
                    {
                        mensage = "Pedido eliminado correctamente.";
                    }
                    else
                    {
                        mensage = "Error al eliminar el pedido.";
                    }
                }
            }

            return mensage;
        }
    }
}
