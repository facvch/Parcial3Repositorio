using Application.DataTransferObjects;
//using Application.DomainEvents; // Descomentar si se usan eventos de dominio
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    /// <summary>
    /// El mapeo entre objetos debe ir definido aqui
    /// </summary>
    public class Mapping : Profile
    {
        public Mapping()
        {
        }
    }
}
