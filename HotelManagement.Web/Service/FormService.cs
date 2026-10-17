using HotelManagement.Interface;
using HotelManagement.Shared.DTOs.Forms;

namespace HotelManagement.Service;

public class FormService : IFormService
{
    private readonly HttpClient _http;

    public FormService(HttpClient http)
    {
        _http = http;
    }

    public async Task<IReadOnlyList<GetFormDTO>> GetFormsAsync()
    {
        IReadOnlyList<GetFormDTO>? forms = await _http.GetFromJsonAsync<IReadOnlyList<GetFormDTO>>("api/Form/GetForms");
        return forms ?? [];
    }

    public async Task CreateFormAsync(CreateFormDTO formDTO)
    {
        var response = await _http.PostAsJsonAsync("api/Form/CreateForm", formDTO);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateFormAsync(UpdateFormDTO formDTO)
    {
        var response = await _http.PutAsJsonAsync($"api/Form/UpdateForm/{formDTO.Id}", formDTO);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteFormAsync(int formId)
    {
        var response = await _http.DeleteAsync($"api/Form/DeleteForm/{formId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<IReadOnlyList<FormBreadCumDTO>> GetFormBreadCumsAsync()
    {
        IReadOnlyList<FormBreadCumDTO>? breadCums = await _http.GetFromJsonAsync<IReadOnlyList<FormBreadCumDTO>>("api/Form/GetFormBreadCums");
        return breadCums ?? [];
    }
}
