using ApiPetshopProgreso2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ApiPetshopProgreso2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        private readonly AppDbContext _db;
        protected ResultadoApi _resultadoApi;

        public ClienteController(AppDbContext db)
        {
            _db = db;
            _resultadoApi = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task <IActionResult> Get()
        {
            var clientes = await _db.clientes.ToListAsync();

            if(clientes != null)
            {
                _resultadoApi.listaClientes = clientes;
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


        [HttpGet("{id}", Name = "Action")]
        public async Task<IActionResult> Get(int id)
        {
            Cliente cliente = await _db.clientes.FirstOrDefaultAsync(x => x.Id == id);
            if(cliente != null)
            {
                _resultadoApi.cliente = cliente;
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();  
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode= HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }



        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cliente cliente)
        {
            Cliente cliente1 = await _db.clientes.FirstOrDefaultAsync(x => x.Id == cliente.Id);
            if(cliente1 == null)
            {
                await _db.clientes.AddAsync(cliente);   
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
        public async Task<IActionResult> Put(int id, [FromBody] Cliente cliente)
        {
            Cliente cliente1 = await _db.clientes.FirstOrDefaultAsync(x => x.Id == id);
        
            if (cliente1 != null)
            {
                cliente1.Cedula = cliente.Cedula != null ? cliente.Cedula : cliente1.Cedula;
                cliente1.Nombre = cliente.Nombre != null ? cliente.Nombre : cliente1.Nombre;
                cliente1.Apellido = cliente.Apellido != null ? cliente.Apellido : cliente1.Apellido;
                cliente1.Telefono = cliente.Telefono != null ? cliente.Telefono : cliente1.Telefono;
                cliente1.Email = cliente.Email != null ? cliente.Email : cliente1.Email;
                _db.Update(cliente1);
                await _db.SaveChangesAsync();
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode= HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Cliente cliente1 = await _db.clientes.FirstOrDefaultAsync(x => x.Id == id);
            if(cliente1 != null) 
            {
                _db.clientes.Remove(cliente1);
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
