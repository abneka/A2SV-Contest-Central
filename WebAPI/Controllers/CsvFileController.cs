using System.Globalization;
using System.Text;
using Application.DTOs.User;
using Application.Features.User.Commands.AddUserUsingCsvFile;
using CsvHelper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CsvFileController : ControllerBase
{
    private readonly IMediator _mediator;

    public CsvFileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("UploadCsvFile")]
    public async Task<IActionResult> UploadCsvFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Invalid file");
        }

        try
        {
            var command = new AddUserUsingCsvFileCommand { UserFile = file };
            var result = await _mediator.Send(command);

            // Check if there are invalid users and return the CSV content
            if (result != null)
            {
                var invalidUsersCsv = ConvertInvalidUsersToCsv(result);
                return File(Encoding.UTF8.GetBytes(invalidUsersCsv), "text/csv", "InvalidUsers.csv");
            }

            return Ok("File uploaded successfully");
        }
        catch (Exception ex)
        {
            // Handle exceptions and return an appropriate response
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpGet]
    [Route("GetSampleCsv")]
    public IActionResult GetSampleCsv()
    {
        // Sample CSV header
        var csvHeader = "FirstName,LastName,Email,Username,CodeforcesHandle,Phone,Gender,Password,Role,Group";

        // Sample CSV content
        var sampleCsvContent = "John,Doe,john.doe@a2sv.org,johndoe,jonee,0900112233,Male,password123,Admin,44\n" +
                            "Jane,Smith,jane.smith@a2sv.org,janesmith,janeyy,0900223344,Female,password456,User,46";

        // Combine header and content
        var sampleCsv = csvHeader + "\n" + sampleCsvContent;

        // Return the sample CSV file
        return File(Encoding.UTF8.GetBytes(sampleCsv), "text/csv", "SampleFile.csv");
    }


    private string ConvertInvalidUsersToCsv(IReadOnlyList<InvalidUserRecord> invalidUsers)
    {
        using (var memoryStream = new MemoryStream())
        using (var writer = new StreamWriter(memoryStream))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            // Write header
            csv.WriteField("FirstName");
            csv.WriteField("LastName");
            csv.WriteField("Email");
            csv.WriteField("Username");
            csv.WriteField("CodeforcesHandle");
            csv.WriteField("Phone");
            csv.WriteField("Gender");
            csv.WriteField("Password");
            csv.WriteField("Role");
            csv.WriteField("Group");
            csv.NextRecord();

            // Write data
            foreach (var invalidUser in invalidUsers)
            {
                csv.WriteField(invalidUser.FirstName);
                csv.WriteField(invalidUser.LastName);
                csv.WriteField(invalidUser.Email);
                csv.WriteField(invalidUser.Username);
                csv.WriteField(invalidUser.CodeforcesHandle);
                csv.WriteField(invalidUser.Phone);
                csv.WriteField(invalidUser.Gender);
                csv.WriteField(invalidUser.Password);
                csv.WriteField(invalidUser.Role);
                csv.WriteField(invalidUser.Group);
                csv.NextRecord();
            }

            writer.Flush();
            memoryStream.Position = 0;

            using (var reader = new StreamReader(memoryStream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
