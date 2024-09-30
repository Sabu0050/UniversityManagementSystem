using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.BLL.GenericResponseFormat
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }

        public ApiResponse(T data, bool isSuccess = true, string message="") { 
            Data = data;
            IsSuccess = isSuccess;
            Message = message;
        }
        public ApiResponse(List<ValidationFailure> errors) {
            IsSuccess = false;
            Message = "validation error happened";
            Errors = errors.GroupBy(e=>e.PropertyName).ToDictionary(g=>
            g.Key,g=>g.Select(e=>e.ErrorMessage).ToArray());
        }
        public ApiResponse(IEnumerable<IdentityError> errors) {
            IsSuccess = false;
            Message = "Identity error happened";
            Errors = errors.GroupBy(e=>e.Code).ToDictionary(g=>
            g.Key,g=>g.Select(e=>e.Description).ToArray());
        }
    }
}
