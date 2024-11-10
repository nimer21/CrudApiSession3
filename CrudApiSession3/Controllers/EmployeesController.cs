using CrudApiSession3.Data;
using CrudApiSession3.DTOs.Departments;
using CrudApiSession3.DTOs.Employees;
using CrudApiSession3.Migrations;
using CrudApiSession3.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace CrudApiSession3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EmployeesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("GetAll")]
        public IActionResult Get() {
            var employees = context.Employees.ToList();
            var response = employees.Adapt<IEnumerable<GetEmployeesDto>>();
            return Ok(response);
        }
        [HttpGet("Details")]
        public IActionResult GetById(int id)
        {
            var employee = context.Employees.Find(id);
            if (employee is null)
            {
                return NotFound("employee not found");
            }
            var response = employee.Adapt<GetEmployeesDto>();
            return Ok(response);
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateEmployeeDto empDto)
        {
            var employee = empDto.Adapt<Employee>();
            context.Employees.Add(employee);
            context.SaveChanges();
            return Ok();
        }


        [HttpPut("Update")]
        public IActionResult Update(int id, CreateEmployeeDto employee)
        {
            var current = context.Employees.Find(id);
            if (current is null)
            {
                return NotFound("employee not found");
            }
            var response  = employee.Adapt(current);
            context.SaveChanges();
            return Ok(response);
        }

        [HttpDelete("Remove")]
        public IActionResult Remove(int id)
        {
            var employee = context.Employees.Find(id);
            if (employee is null)
            {
                return NotFound("employee not found");
            }
            context.Employees.Remove(employee);
            context.SaveChanges();
            CreateEmployeeDto employeeDto = new CreateEmployeeDto();
            var response = employeeDto.Adapt<Employee>();

            return Ok(response);
        }
    }
}
