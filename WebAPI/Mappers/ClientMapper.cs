using WebAPI.Dtos;
using WebAPI.Persistence.Models;

namespace WebAPI.Mappers;

public static class ClientMapper
{
    public static client ToEntity(this ClientCreateDto dto)
    {
        return new client
        {
            tip = dto.tip!,
            first_name = dto.first_name!,
            last_name = dto.last_name!,
            nr_telefon = dto.nr_telefon!,
            mail = dto.mail,
            username = dto.username!,
            password = dto.password!
        };
    }
}