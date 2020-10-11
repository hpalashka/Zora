using AutoMapper;

namespace Zora.Shared.Application.Mapping
{
    public interface IMapTo<T>
    {
        void Mapping(Profile mapper) => mapper.CreateMap(this.GetType(), typeof(T));
    }
}
