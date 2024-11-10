using CrudApiSession3.Data;
using CrudApiSession3.DTOs.Departments;
using CrudApiSession3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudApiSession3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DepartmentsController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            var departments = context.Departments.Select(
                x=> new GetDepartmentsDto()
                {
                    Id= x.Id,
                    Name = x.Name,
                });
            return Ok(departments);
        }
        [HttpGet("Details")]
        public IActionResult GetById(int id)
        {
            var department = context.Departments.Find(id);
            if (department is null)
            {
                return NotFound("department not found");
            }
            CreateDepartmentDto dep = new CreateDepartmentDto()
            {
                Name = department.Name,
            };
            return Ok(dep);
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateDepartmentDto depDto)
        {
            Department dep = new Department()
            {
                Name = depDto.Name,
            };
            context.Departments.Add(dep);
            context.SaveChanges();
            return Ok(dep);
        }


        [HttpPut("Update")]
        public IActionResult Update(int id, CreateDepartmentDto department)
        {
            var current = context.Departments.Find(id);
            if (current is null)
            {
                return NotFound("department not found");
            }
            current.Name = department.Name;           
            context.SaveChanges();
            return Ok(department);
        }

        [HttpDelete("Remove")]
        public IActionResult Remove(int id)
        {
            var department = context.Departments.Find(id);
            if (department is null)
            {
                return NotFound("department not found");
            }
            context.Departments.Remove(department);
            context.SaveChanges();
            CreateDepartmentDto departmentDto = new CreateDepartmentDto();
            departmentDto.Name = department.Name;
            return Ok(departmentDto);
        }
    }
}

