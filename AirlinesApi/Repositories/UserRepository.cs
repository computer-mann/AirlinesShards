using AirlinesApi.Telemetry;
using AirlinesApi.Telemetry.ManualTraces;
using AirlinesApi.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

namespace AirlinesApi.Repositories
{
    public class UserRepository:IUserRepository
    {
        //public Task<Results<Ok<ManualTraces>,ValidationProblem,EmptyHttpResult>> discriminatedUnion()
        //{
        //    if(DateTime.Now.Year == 2)
        //    {
        //        return Task.FromResult(TypedResults.Ok(new ManualTraces()));
        //    }else if(DateTime.Now.Year == 3)
        //    {
        //        return TypedResults.ValidationProblem(new Dictionary<string, string[]>());
        //    }
        //    return TypedResults.Empty;
        //}
    }
    public interface IUserRepository
    {

    }
}
