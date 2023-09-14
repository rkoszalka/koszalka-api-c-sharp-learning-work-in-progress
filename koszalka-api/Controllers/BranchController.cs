using koszalka_api.Model;
using koszalka_api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace koszalka_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BranchController : Controller
    {
        private readonly IBranchRepository _branchRepository;

        public BranchController(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Branch CreateBranch(Branch branch)
        {
            _branchRepository.Create(branch);
            return branch;
        }
    }
}
