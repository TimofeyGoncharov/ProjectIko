using Microsoft.AspNetCore.Mvc;
using ProjectIko.Db.Interface;
using ProjectIko.Models;

namespace ProjectIko.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProjectIkoController : Controller
    {
        private readonly ILogger _logger;
        private readonly IIkoRepository _ikoRepository;

        public ProjectIkoController(ILogger<ProjectIkoController> logger, IIkoRepository ikoRepository)
        {
            _logger = logger;
            _ikoRepository = ikoRepository;
        }

        [HttpGet("get_all")]
        public async Task<ActionResult> GetData()
        {
            try
            {
                var data = await _ikoRepository.GetAllAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка получения данных: {ex.Message}", ex.HResult);
                return BadRequest($"Ошибка получения данных - {ex.Message}\n{ex.HResult}");
            }
        }

        [HttpPatch("patch_data")]
        public async Task<ActionResult> UpdateData(Model model)
        {
            try
            {
                var currentModel = await _ikoRepository.GetByIdAsync(model.ClientId);
                currentModel.Username = model.Username;
                currentModel.SystemId = model.SystemId;

                _ikoRepository.SaveChanges();

                return Ok(currentModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка обновления данных по ClientId: {ex.Message}\n ClientId - {model.ClientId}", ex.HResult);
                return BadRequest($"Ошибка обновления данных по ClientId - {ex.Message}\n ClientId - {model.ClientId}\n{ex.HResult}");
            }
        }

        [HttpDelete("delete_data/{clientId}")]
        public async Task<ActionResult> DeleteData(long clientId)
        {
            try
            {
                var currentModel = await _ikoRepository.GetByIdAsync(clientId);
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка удаления данных по ClientId: {ex.Message}\n id - {clientId}", ex.HResult);
                return BadRequest($"Ошибка удаления данных по ClientId - {ex.Message}\n id - {clientId}\n{ex.HResult}");
            }
        }

        /// <summary>
        /// Добавление новых данных
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("new_data")]
        public async Task<ActionResult> AddArrayData(long[] data)
        {
            try
            {
                var dates = await _ikoRepository.GetAllAsync();

                var newData = data.Where(d => !dates.Any(cl => cl.ClientId == d)).ToList();

                newData.ForEach(async d =>
                {
                    await _ikoRepository.InsertAsync(new Model()
                    {
                        ClientId = d,
                        SystemId = Guid.NewGuid(),
                        Username = $"{d}"
                    });
                });

                return Ok(newData);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка добавление новых данных: {ex.Message}", ex.HResult);
                return BadRequest($"Ошибка добавление новых данных - {ex.Message}\n{ex.HResult}");
            }
        }
    }
}
