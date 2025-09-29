using WebAPI.Dtos;
using System.Collections.Generic;

namespace WebAPI.Validators;

public static class AntrenorValidator
{
    public static Dictionary<string, string[]> ValidateForCreate(AntrenorCreateDto dto)
    {
        var errors = new Dictionary<string, string[]>();

        void Req(string? v, string field, int maxLen)
        {
            if (string.IsNullOrWhiteSpace(v))
                errors[field] = new[] { "This field is required." };
            else if (v.Length > maxLen)
                errors[field] = new[] { $"Max length is {maxLen}." };
        }

        Req(dto.tip_abonament, nameof(dto.tip_abonament), 20);
        Req(dto.first_name, nameof(dto.first_name), 100);
        Req(dto.last_name, nameof(dto.last_name), 100);
        Req(dto.nr_telefon, nameof(dto.nr_telefon), 20);
        Req(dto.username, nameof(dto.username), 50);
        Req(dto.password, nameof(dto.password), 100);

        if (!string.IsNullOrWhiteSpace(dto.tip) && dto.tip.Length > 10)
            errors[nameof(dto.tip)] = new[] { "Max length is 10." };

        if (!string.IsNullOrWhiteSpace(dto.mail) && dto.mail.Length > 255)
            errors[nameof(dto.mail)] = new[] { "Max length is 255." };

        return errors;
    }

    public static Dictionary<string, string[]> ValidateForUpdate(AntrenorUpdateDto dto)
    {
        var errors = new Dictionary<string, string[]>();

        void Check(string? v, string field, int maxLen)
        {
            if (v != null)
            {
                if (string.IsNullOrWhiteSpace(v))
                    errors[field] = new[] { "This field cannot be empty." };
                else if (v.Length > maxLen)
                    errors[field] = new[] { $"Max length is {maxLen}." };
            }
        }

        Check(dto.tip, nameof(dto.tip), 10);
        Check(dto.tip_abonament, nameof(dto.tip_abonament), 20);
        Check(dto.first_name, nameof(dto.first_name), 100);
        Check(dto.last_name, nameof(dto.last_name), 100);
        Check(dto.nr_telefon, nameof(dto.nr_telefon), 20);
        Check(dto.username, nameof(dto.username), 50);
        Check(dto.password, nameof(dto.password), 100);

        if (dto.mail != null && dto.mail.Length > 255)
            errors[nameof(dto.mail)] = new[] { "Max length is 255." };

        return errors;
    }
}
