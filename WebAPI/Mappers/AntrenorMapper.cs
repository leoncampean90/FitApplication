using WebAPI.Dtos;
using WebAPI.Persistence.Models;

namespace WebAPI.Mappers;

public static class AntrenorMapper
{
    public static Antrenori ToEntity(this AntrenorCreateDto dto)
    {
        return new Antrenori
        {
            tip = string.IsNullOrWhiteSpace(dto.tip) ? "a" : dto.tip,
            tip_abonament = dto.tip_abonament!,
            first_name = dto.first_name!,
            last_name = dto.last_name!,
            nr_telefon = dto.nr_telefon!,
            mail = dto.mail,
            username = dto.username!,
            password = dto.password!
        };
    }

    /// <summary>
    /// Applies only changes where the new value is different from the current one.
    /// Returns true if something actually changed.
    /// </summary>
    public static bool ApplyUpdate(this Antrenori entity, AntrenorUpdateDto dto)
    {
        bool changed = false;

        void ApplyIfDifferent(Action<string> setter, string? currentValue, string? newValue)
        {
            if (!string.IsNullOrWhiteSpace(newValue) && currentValue != newValue)
            {
                setter(newValue);
                changed = true;
            }
        }

        ApplyIfDifferent(v => entity.tip = v, entity.tip, dto.tip);
        ApplyIfDifferent(v => entity.tip_abonament = v, entity.tip_abonament, dto.tip_abonament);
        ApplyIfDifferent(v => entity.first_name = v, entity.first_name, dto.first_name);
        ApplyIfDifferent(v => entity.last_name = v, entity.last_name, dto.last_name);
        ApplyIfDifferent(v => entity.nr_telefon = v, entity.nr_telefon, dto.nr_telefon);
        ApplyIfDifferent(v => entity.username = v, entity.username, dto.username);
        ApplyIfDifferent(v => entity.password = v, entity.password, dto.password);

        if (dto.mail != null && entity.mail != dto.mail)
        {
            entity.mail = dto.mail;
            changed = true;
        }

        return changed;
    }
}