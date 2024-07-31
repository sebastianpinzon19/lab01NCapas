using BLL;
using BLL.Exceptions;
using entities.Models;
using Microsoft.AspNetCore.Mvc;
using SLC;
namespace Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase, ICustomerService
    {
        private readonly Customers bll; // Dependency injection for better testability

        public CustomerController(Customers bll)
        {
            this.bll = bll;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAll()
        {
            try
            {
                var result = await bll.RetrieveAllAsync();
                return Ok(result); // Use IActionResult for more flexibility (200 OK)
            }
            catch (CustomerExceptions ex) // Catch specific business logic exceptions
            {
                return BadRequest(ex.Message); // Return 400 Bad Request with error message
            }
            catch (Exception ex) // Catch unhandled exceptions for logging and generic error response
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        [HttpGet("{id}", Name = "RetrieveAsync")]
        public async Task<ActionResult<Customer>> RetrieveAsync(int id)
        {
            try
            {
                var customer = await bll.RetrieveByIDAsync(id);

                if (customer == null)
                {
                    return NotFound("Customer not found."); // Use NotFound result for missing resources
                }

                return Ok(customer);
            }
            catch (CustomerExceptions ce)
            {
                return BadRequest(ce.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Customer>> CreateAsync([FromBody] Customer toCreate)
        {
            try
            {
                var customer = await bll.CreateAsync(toCreate);
                return CreatedAtRoute("RetrieveAsync", new { id = customer.Id }, customer); // Use CreatedAtRoute for 201 Created
            }
            catch (CustomerExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Customer toUpdate)
        {
            // Asigna el ID a actualizar desde la ruta a la entidad a actualizar
            toUpdate.Id = id;

            try
            {
                // Llama a la lógica de negocio para actualizar el cliente
                var result = await bll.UpdateAsync(toUpdate);

                // Si la actualización no fue exitosa, devuelve un NotFound
                if (!result)
                {
                    return NotFound("Customer not found or update failed.");
                }

                // Si la actualización fue exitosa, devuelve un NoContent
                return NoContent();
            }
            catch (CustomerExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await bll.DeleteAsync(id);
                if (!result)
                {
                    return NotFound("Customer not found or deletion failed.");
                }
                return NoContent();
            }
            catch (CustomerExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
    }
}