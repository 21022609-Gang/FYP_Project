using Microsoft.AspNetCore.Mvc.ModelBinding;
using OurWebAppTest.Models;

namespace OurWebAppTest.Services
{
    public class Helper
    {
        private readonly ModelStateDictionary _modelState; // holds state and bind process for asp

        public Helper(ModelStateDictionary modelState)
        {
            _modelState = modelState; //asign modelstate to _modelstate
        }

        public bool ValidatePart(string name)
        {

            if (_modelState.TryGetValue(name, out var propertyModelState) 
                && propertyModelState.Errors.Count == 0) // Coby here Basically take in some part of Model and validate it indiv 
            {
                return true;
            }
            return false;
        }

        public int CalcAge(DateOnly DoB)
        {
            DateOnly curDate = DateOnly.FromDateTime(DateTime.Now);
            int age = curDate.Year - DoB.Year;

            if (curDate.Month < DoB.Month || (curDate.Month == DoB.Month && curDate.Day < DoB.Day))
            {
                age--;
            }

            return age;
        }
    }
}
