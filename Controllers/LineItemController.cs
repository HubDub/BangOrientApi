using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bangazon.Data;  //we created this so we need to add it as a using
using Bangazon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace BangOrientAPI.Controllers
{
    [ProducesAttribute("application/json")]
    [RouteAttribute("[controller]")]
    public class LineItemController : Controller
    {
        private BangazonContext context;  //what is this doing? 

        public LineItemController(BangazonContext ctx)
        {
            context = ctx; //where is it getting the ctx argument?
        }

        [HttpGet]
        public IActionResult Get() 
        {
            //TODOchange this one to get all items for one customer
            IQueryable<object> lineitems = from lineitem in context.LineItem select lineitem;

            if (lineitems == null)
            {
                return NotFound();
            }

            return Ok(lineitems);
        }

        [HttpGet("{id}", Name = "GetLineItems")]
        public IActionResult Get([FromRoute] int id)
        {
            try
            {
                LineItem lineitem = context.LineItem.Single(l => l.LineItemId == id);
                if (lineitem == null)
                {
                    return NotFound();
                }
            
                return Ok(lineitem);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] LineItem lineitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
            context.LineItem.Add(lineitem);
            context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LineItemExists(lineitem.LineItemId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetLineItem", new {id = lineitem.LineItemId }, lineitem);
        }

        private bool LineItemExists(int id)
        {
            return context.LineItem.Count(l => l.LineItemId == id) > 0;
        }

    }
}