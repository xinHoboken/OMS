using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TeamAwesome1._0.Models;
using TeamAwesome1._0.ViewModels;

namespace TeamAwesome1._0.Controllers
{
    public class OrdersController : ApiController
    {
        private TeamAwesomeEntities db = new TeamAwesomeEntities();

        // GET: api/Orders
        public IQueryable<OrderViewModel> GetOrders()
        {
            return from o in db.Orders
                   join p in db.Parts on o.PartNo equals p.PartNo
                   join pa in db.Packers on o.PackerNo equals pa.PackerNo
                   join b in db.Boxes on o.BoxCode equals b.BoxCode
                   select new OrderViewModel {
                        AutoNumber = o.AutoNumber,
                        OrderNo = o.OrderNo,
                        OrderDate = o.OrderDate,
                        Die = o.Die,
                        MachNo = o.MachNo,
                        Std = o.Std,
                        Act = o.Act,
                        OnHrs = o.OnHrs,
                        Shift = o.Shift,
                        PartNo = o.PartNo,
                        BoxCode = o.BoxCode,
                        PackerNo = o.PackerNo,
                        BoxSize = o.Box.BoxSize,
                        PackerQty = pa.PackerQty,
                        Total = pa.Total,
                        BoxCount = pa.BoxCount,
                        Adj = pa.Adj,
                        PartDesc = p.PartDesc
    };
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.AutoNumber)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Orders
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = order.AutoNumber }, order);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.AutoNumber == id) > 0;
        }
    }
}