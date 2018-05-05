using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VotingApp.Domain;

namespace VotingApp.Queries
{
    [Route("[controller]")]
    public class VotingController : Controller
    {
        private readonly VotingResultsService _appService;

        public VotingController(VotingResultsService appService) => _appService = appService;

        [HttpGet]
        public object Get() => _appService.Get();
    }
}
