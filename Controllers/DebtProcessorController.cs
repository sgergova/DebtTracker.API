using Microsoft.AspNetCore.Mvc;
using DebtTracker.API.Models.DTO;
using DebtTracker.API.Services.Contracts;

namespace DebtTracker.API.Controllers
{
    [Route("api/debt")]
    [ApiController]
    public class DebtProcessorController(IDebtReader debtReader) : ControllerBase
    {
        private readonly IDebtReader _debtReader = debtReader;


        [HttpPost]
        public async Task<IActionResult> ProcessDebt()
        {
            try
            {
                var debtsStream = new MemoryStream();

                await HttpContext.Request.Body.CopyToAsync(debtsStream);

                var result = await _debtReader.ProcessDebts(debtsStream);

                return Ok(result);

            }
            catch (ArgumentException ex)
            {
                var error = CustomErrorDto.CreateCustomErrorDtoFromException(ex);

                return BadRequest(error);
            }
            catch (Exception)
            {
                var error = new CustomErrorDto
                {
                    Message = "Something bad happened",
                    Description = Common.Constants.CommonError
                }; 

            return BadRequest(error);
        }
    }
}
}
