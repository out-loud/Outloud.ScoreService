using Microsoft.AspNetCore.Mvc;
using Outloud.ScoreService.DTO;
using Outloud.ScoreService.Mappers;
using Outloud.ScoreService.Persistance;
using Outloud.ScoreService.Persistance.Repositories;
using System.Threading.Tasks;

namespace Outloud.ScoreService.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserDataRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUserDataRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _repository.GetUserData(id);
            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Post(string id, UserDataDTO userData)
        {
            var entity = Mapper.Map(userData);
            await _repository.AddOrUpdateProgressAsync(id, entity);
            await _unitOfWork.CompleteAsync();
            return Accepted();
        }
    }
}