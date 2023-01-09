using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeApiWithMongoDB.Abstractions;
using OfficeApiWithMongoDB.Helpers;
using OfficeApiWithMongoDB.Models;
using OfficeApiWithMongoDB.Models.Customs;

namespace OfficeApiWithMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ImageHelper _imageHelper;

        public EmployeeController(IEmployeeRepository employeeRepository, ImageHelper imageHelper)
        {
            _employeeRepository = employeeRepository;
            _imageHelper = imageHelper;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(string id)
        {
            var data = await _employeeRepository.Get(id);
            data.Photo = _imageHelper.GetImages(Convert.ToBase64String(data.Photo));

            EmployeeModel employee = new EmployeeModel()
            {
                Id = data.Id,
                Name = data.Name,
                Salary = data.Salary,
                CardNumber = data.CardNumber,
                Birthdate = data.Birthdate,
                IsPermanent = data.IsPermanent,
                FileString = data.Photo == null ? string.Empty : "data:image/png;base64," + Convert.ToBase64String(data.Photo)
            };

            return Ok(employee);
        }

        [HttpGet]
        [Route("Select")]
        public async Task<IActionResult> Select()
        {
            var data = await _employeeRepository.Select();

            IList<EmployeeModel> employees = new List<EmployeeModel>();

            foreach(var employee in data)
            {
                employees.Add(new EmployeeModel
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Salary = employee.Salary,
                    Birthdate = employee.Birthdate,
                    CardNumber = employee.CardNumber,
                    IsPermanent = employee.IsPermanent,
                    FileString = employee.Photo == null ? string.Empty : "data:image/png;base64," + Convert.ToBase64String(employee.Photo)
                });
            }

            return Ok(employees);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save([FromForm] EmployeeModel formData)
        {
            Employee employee = new Employee
            {
                Id = formData.Id,
                Name = formData.Name,
                Birthdate = formData.Birthdate,
                CardNumber = formData.CardNumber,
                Salary = formData.Salary,
                IsPermanent = formData.IsPermanent
            };

            if (formData.File.Length > 0) 
            {
                using(var ms = new MemoryStream()) 
                {
                    formData.File.CopyTo(ms);
                    var fileBytes = ms.ToArray();

                    employee.Photo = fileBytes;
                }
            }

            await _employeeRepository.Save(employee);
            return StatusCode(201);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            await _employeeRepository.Delete(id);
            return NoContent();
        }

    }
}
