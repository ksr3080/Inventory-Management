using HotelManagement.Shared.DTOs.Forms;

namespace HotelManagement.Interface;

public interface IFormService
{
    Task<IReadOnlyList<GetFormDTO>> GetFormsAsync();
    Task CreateFormAsync(CreateFormDTO formDTO);
    Task UpdateFormAsync(UpdateFormDTO formDTO);
    Task DeleteFormAsync(int formId);
    Task<IReadOnlyList<FormBreadCumDTO>> GetFormBreadCumsAsync();
}
