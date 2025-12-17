using Microsoft.AspNetCore.Mvc;
using Polyclinic.Application.Contracts.Patients;
using Polyclinic.Generator.Generator;
using Polyclinic.Generator.Services;

namespace Polyclinic.Api.Host.Controllers;

/// <summary>
/// HTTP controller for generating patients
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class GeneratorController(IProducerService producerService, ILogger<GeneratorController> logger) : ControllerBase
{
    private readonly IProducerService _producerService = producerService;
    private readonly ILogger<GeneratorController> _logger = logger;

    /// <summary>
    /// Generates and saves patients in the database
    /// </summary>
    /// <param name="batchSize">The size of a single batch (default is 10)</param>
    /// <param name="payloadLimit">Number of batches (5 by default)</param>
    /// <param name="waitTime">Waiting time between batches in seconds (default is 2)</param>
    /// <returns>List of generated patients</returns>
    [HttpGet("generate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<PatientDto>>> Generate(
        [FromQuery] int batchSize = 10,
        [FromQuery] int payloadLimit = 5,
        [FromQuery] int waitTime = 2)
    {
        _logger.LogInformation("The beginning of patient generation. Parameters: BatchSize={batchSize}, PayloadLimit={payloadLimit}, WaitTime={waitTime}",
            batchSize, payloadLimit, waitTime);

        try
        {
            var allPatients = new List<PatientDto>();
            var batchCount = 0;

            while (batchCount < payloadLimit)
            {
                // Generating a Domain Entity
                var domainPatients = PatientGenerator.GeneratePatients(batchSize);

                // Convert to DTO
                var patientDtos = domainPatients
                    .Select(p => new PatientDto
                    {
                        Id = 0, // The database will be generated
                        PassportNumber = p.PassportNumber,
                        FullName = p.FullName,
                        Gender = p.Gender.ToString(),
                        BirthDate = p.BirthDate,
                        Address = p.Address,
                        BloodGroup = p.BloodGroup.ToString(),
                        RhesusFactor = p.RhesusFactor.ToString(),
                        Phone = p.Phone
                    })
                    .ToList();

                _logger.LogInformation("Sending a batch {currentBatch}/{totalBatches} с {size} пациентами",
                    batchCount + 1, payloadLimit, batchSize);

                // We send it to the Producer (it is saved in the database)
                await _producerService.SendAsync(patientDtos);
                allPatients.AddRange(patientDtos);

                batchCount++;

                // Pause between butches (but not after the last one)
                if (batchCount < payloadLimit)
                {
                    await Task.Delay(waitTime * 1000);
                }
            }

            var totalCreated = batchCount * batchSize;
            _logger.LogInformation("The generation has been completed successfully. Total batches: {batches}, patients: {total}",
                batchCount, totalCreated);

            return Ok(allPatients);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in generating patients");
            return StatusCode(500, new { message = "Error in generating patients", error = ex.Message });
        }
    }
}