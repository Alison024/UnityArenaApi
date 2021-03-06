using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace UnityArenaApi.Extensions
{
    public static class StateExtensions
    {
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary){
            return dictionary.SelectMany(x=>x.Value.Errors)
            .Select(x=>x.ErrorMessage)
            .ToList();
        }
    }
}