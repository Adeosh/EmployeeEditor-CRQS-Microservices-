using EmployeeEditor.Application.Employees.Create;
using EmployeeEditor.Application.Employees.Delete;
using EmployeeEditor.Application.Employees.Get.GetAll;
using EmployeeEditor.Application.Employees.Update;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeEditor.Web.Controllers
{
    public static class EmployeesMinimalApiEndpoints
    {
        public static void MapEmployeeEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/employees");

            group.MapGet("get", GetAllEmployees);

            group.MapPost("create", CreateEmployee);

            group.MapPut("update{id}", UpdateEmployee).WithName(nameof(UpdateEmployee));

            group.MapDelete("delete{id}", DeleteEmployee).WithName(nameof(DeleteEmployee));
        }

        public static async Task<IResult> GetAllEmployees(ISender sender)
        {
            var employees = await sender.Send(new GetAllEmployeesQuery());

            return Results.Ok(employees);
        }

        public static async Task<IResult> CreateEmployee(
            CreateEmployeeRequest request,
            ISender sender,
            IValidator<CreateEmployeeCommand> validator)
        {
            var command = new CreateEmployeeCommand(
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Age,
                request.Email,
                request.Department,
                request.Salary,
                request.IsActive,
                request.EmploymentDate,
                request.CreatedAt);

            var result = await validator.ValidateAsync(command);

            if (!result.IsValid)
            {
                return Results.ValidationProblem(result.ToDictionary());
            }

            await sender.Send(command);

            return Results.Ok(command);
        }

        public static async Task<IResult> UpdateEmployee(
            Guid id, 
            [FromBody] UpdateEmployeeRequest request,
            ISender sender)
        {
            try
            {
                var command = new UpdateEmployeeCommand(
                    id,
                    request.FirstName,
                    request.MiddleName,
                    request.LastName,
                    request.Age,
                    request.Email,
                    request.Department,
                    request.Salary,
                    request.IsActive,
                    request.UpdatedAt);

                await sender.Send(command);

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return TypedResults.NotFound(ex.Message);
            }
        }

        public static async Task<IResult> DeleteEmployee(
            Guid id,
            ISender sender)
        {
            try
            {
                await sender.Send(new DeleteEmployeeCommand(id));

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return TypedResults.NotFound(ex.Message);
            }
        }
    }
}
