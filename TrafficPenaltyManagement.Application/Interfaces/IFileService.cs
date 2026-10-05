using Microsoft.AspNetCore.Http;

namespace TrafficPenaltyManagement.Application.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderName);
    }
}
