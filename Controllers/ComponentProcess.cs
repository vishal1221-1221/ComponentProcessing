using AutoMapper;
using ComponentProcessing.API.Models;
using ComponentProcessing.API.Models.Dtos;
using ComponentProcessing.API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ComponentProcessing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentProcess : Controller
    {
        private IComponentProcessingRepository _cpRepo;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMapper _mapper;

        public ComponentProcess(IComponentProcessingRepository cpRepo, IMapper mapper, IHttpClientFactory clientFactory)
        {
            _cpRepo = cpRepo;
            _mapper = mapper;
            _clientFactory = clientFactory;


        }

        [HttpGet]
        public IActionResult GetComponentProcessings()
        {
            var objList = _cpRepo.GetComponentProcessings();

            var objDto = new List<ComponentProcessingModelDto>();

            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<ComponentProcessingModelDto>(obj));
            }
            return Ok(objDto);
        }

        [HttpGet("{cpId:int}",Name = "GetComponentProcessing")]
        public IActionResult GetComponentProcessing(int cpId)
        {
            var obj = _cpRepo.GetComponentProcessing(cpId);
            if(obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComponentProcessingAsync([FromBody] CreateDto compDto)
        {
            if(compDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:43475/api/PackageAndDeliveryProcessing?comType={compDto.componentType}&quantity={compDto.quantity}");
            request.Headers.Add("Accept", "application/json");
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            string str = await response.Content.ReadAsStringAsync();
            GetDto data = JsonSerializer.Deserialize<GetDto>(str);
            var objNewData = _mapper.Map<GetDto>(data);
            compDto.packageCharges = objNewData.packageCharges;
            compDto.deliveryCharges = objNewData.deliveryCharges;
            var componentProcessingObj = _mapper.Map<ComponentProcessingModel>(compDto);
            if(!_cpRepo.CreateComponentProcessing(componentProcessingObj))
            {
                ModelState.AddModelError("",$"Something went wrong when saving the record {componentProcessingObj.name}");
                return StatusCode(500, ModelState);
            }

            var componentProcessingObjResponse = _mapper.Map<ComponentProcessingModelDto>(componentProcessingObj); 
            return Ok(componentProcessingObjResponse);
        }

        [HttpPatch("{cpId:int}", Name = "UpdateComponentProcessing")]
        public IActionResult UpdateComponentProcessing(int cpID, [FromBody] UpdateDto compDto)
        {
            if (compDto == null)
            {
          
                return BadRequest(ModelState);
            }
            var componentProcessingObj = _mapper.Map<ComponentProcessingModel>(compDto);
            if (!_cpRepo.UpdateComponentProcessing( cpID,componentProcessingObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {componentProcessingObj.name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

    }
}
