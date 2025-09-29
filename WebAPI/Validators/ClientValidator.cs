using WebAPI.Dtos;

namespace WebAPI.Validators;

public static class ClientValidator
{
    public static Dictionary<string, string[]> ValidateForCreate(ClientCreateDto dto)
    {
        var errors = new Dictionary<string, string[]>();

        void Req(string? v, string field)
        {
            if (string.IsNullOrWhiteSpace(v))
                errors[field] = new[] { "This field is required." };
        }

        Req(dto.tip, nameof(dto.tip));
        Req(dto.password, nameof(dto.password));
        Req(dto.first_name, nameof(dto.first_name));
        Req(dto.last_name, nameof(dto.last_name));
        Req(dto.username, nameof(dto.username));
        Req(dto.nr_telefon, nameof(dto.nr_telefon));

        return errors;
    }

}
