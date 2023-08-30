using AutoMapper;
using VeiculosAPI.Data.Dtos;
using VeiculosAPI.Models;

namespace VeiculosAPI.Profiles;

public class VeiculoProfile : Profile
{
    public VeiculoProfile()
    {
        CreateMap<CreateVeiculoDto, Veiculo>();
        CreateMap<UpdateVeiculoDto, Veiculo>();
        CreateMap<Veiculo, UpdateVeiculoDto>();
    }
}
