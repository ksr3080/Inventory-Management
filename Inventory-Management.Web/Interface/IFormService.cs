using Inventory_Management.Shared.DTOs.Forms;

namespace Inventory_Management.Interface;

public interface IFormService
{
    Task<IReadOnlyList<GetFormDTO>> GetFormsAsync();
    Task CreateFormAsync(CreateFormDTO formDTO);
    Task UpdateFormAsync(UpdateFormDTO formDTO);
    Task DeleteFormAsync(int formId);
    Task<IReadOnlyList<FormBreadCumDTO>> GetFormBreadCumsAsync();
}
