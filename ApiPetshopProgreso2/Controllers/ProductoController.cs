using ApiPetshopProgreso2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace ApiPetshopProgreso2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        private readonly AppDbContext _db;
        protected ResultadoApi _resultadoApi;

        public ProductoController(AppDbContext db)
        {
            _db = db;
            _resultadoApi = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var productos = await _db.productos.ToListAsync();

            if (productos != null)
            {
                _resultadoApi.listaProductos = productos;
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
            return Ok(_resultadoApi);
        }


        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            Producto producto = await _db.productos.FirstOrDefaultAsync(x => x.Id == id);
            if (producto != null)
            {
                _resultadoApi.producto = producto;
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }



        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Producto producto)
        {
            Producto producto1 = await _db.productos.FirstOrDefaultAsync(x => x.Id == producto.Id);
            if (producto1 == null)
            {
                await _db.productos.AddAsync(producto);
                await _db.SaveChangesAsync();
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString().ToUpper();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString().ToUpper();
                return BadRequest(_resultadoApi);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Producto producto)
        {
            Producto producto1 = await _db.productos.FirstOrDefaultAsync(x => x.Id == id);

            if (producto1 != null)
            {
                producto1.Nombre = producto.Nombre != null ? producto.Nombre : producto1.Nombre;
                producto1.Descripcion = producto.Descripcion != null ? producto.Descripcion : producto1.Descripcion;
                producto1.Precio = producto.Precio != null ? producto.Precio : producto1.Precio;
                producto1.Cantidad = producto.Cantidad != null ? producto.Cantidad : producto1.Cantidad;

                _db.Update(producto1);
                await _db.SaveChangesAsync();
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Producto producto1 = await _db.productos.FirstOrDefaultAsync(x => x.Id == id);
            if (producto1 != null)
            {
                _db.productos.Remove(producto1);
                await _db.SaveChangesAsync();
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }
    }
}
