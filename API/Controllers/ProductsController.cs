using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;
        public ProductsController(StoreContext context)
        {
            _context = context;
        }

        /*
        Swagger UI will show these methods visually
        */

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            /*
                We want to use asynchronous code so that way we don't have to wait for this to finish
                to do other things. AKA it allows us to multitask
            */

            return await _context.Products.ToListAsync();
        }

        //Request an individual product
        //ex: api/products/3
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            //Find the product that matches the given id
            return await _context.Products.FindAsync(id);
        }
    }
}