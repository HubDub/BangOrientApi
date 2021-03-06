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
    [ProducesAttribute("application/json")]  //this tells it to only produce json
    // [Route("api/[controller]")]
    [Route("[controller]")] //should this say [Route("[customer]")]?
    //routing maps incoming requests to route handlers and generates URLs used in responses. generally an app has a single collection of routes and is processed in order. requests look for a match in the route collection by URL matching. Routing is connected to middleware pipeline by the RouterMiddleware class. in our app MVC connects it.
    public class CustomersController : Controller
    {
        private BangazonContext context;

        public CustomersController(BangazonContext ctx)
        {
            context = ctx;
        }
        // GET api/values
        [HttpGet]       
         public IActionResult Get()
        {
            IQueryable<object> customers = from customer in context.Customer select customer; //this means select everything from the customer table. customer is each row in the spreadsheet. so select them all bring them back and hold them inside this customers collection. if their are none the value will be null. so if it is null, we will return "not found"

            if (customers == null)
            {
                return NotFound(); //helper method so you don't have to construct an http response, the system will create a valid 404 for you.
            }

            return Ok(customers);

        }
        

        // GET api/values/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Customer customer = context.Customer.Single(m => m.CustomerId == id);

                if (customer == null)
                {
                    return NotFound();
                }
                
                return Ok(customer);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Customer.Add(customer);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetCustomer", new { id = customer.CustomerId }, customer);
        }
        

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest(ModelState);
            }

            context.Customer.Update(customer);
            try
            {
                context.SaveChanges();

            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;

                }
            }
            return Ok(customer);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromBody] Customer customer)
        {
            
            if (id != customer.CustomerId)
            {
                return BadRequest(ModelState);
            }

            context.Customer.Remove(customer);
            try 
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return Ok(customer);
        }
            private bool CustomerExists(int id)
        {
            return context.Customer.Count(e => e.CustomerId == id) > 0;
        }
    }
}
