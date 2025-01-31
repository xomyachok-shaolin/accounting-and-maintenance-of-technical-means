﻿namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Workstations;
using WebApi.Services;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WorkstationsController : ControllerBase
{
    private IWorkstationService _workstationService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public WorkstationsController(
        IWorkstationService WorkstationService,
        IMapper mapper,
        IOptions<AppSettings> appSettings,
        IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
        _workstationService = WorkstationService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }
    
    [HttpPost("create")]
    public ActionResult<Workstation> Create(WorkstationRequest model)
    {
        _workstationService.Create(model);
        return Ok(new { message = "Создание рабочего места успешно выполнено" });
    }

    // [Authorize(Workstation.Admin)]
    [HttpGet("deviceTransfers")]
    public IActionResult GetAll()
    {
        var workstations = _workstationService.GetAllDT();


        return Ok(workstations);
    }

    [HttpGet("workstationTransfers")]
    public IActionResult GetAllWT()
    {
        var workstations = _workstationService.GetAllWT();


        return Ok(workstations);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var workstation = _workstationService.GetById(id);
        return Ok(workstation);
    }

    [HttpPut("{id}")]
    public ActionResult<Workstation> Update(string id, WorkstationRequest model)
    {
        _workstationService.Update(id, model);        
        return Ok(new { message = "Информация о рабочем месте успешно обновлена" });
    }

    [HttpPost("update/devices")]
    public ActionResult<WorkstationDeviceUpdateRequest> UpdateDevices(int id, WorkstationDeviceUpdateRequest model)
    {
        _workstationService.UpdateDevices(model);
        return Ok(new { message = "Информация об устройствах успешно обновлена" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        _workstationService.Delete(id);
        return Ok(new { message = "Информация о рабочем месте успешно удалена" });
    }
}