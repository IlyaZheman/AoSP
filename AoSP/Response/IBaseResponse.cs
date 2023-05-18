using AoSP.Enums;

namespace AoSP.Response;

public interface IBaseResponse<T>
{
    string Description { get; }
    StatusCode StatusCode { get; }
    T Data { get; }
}