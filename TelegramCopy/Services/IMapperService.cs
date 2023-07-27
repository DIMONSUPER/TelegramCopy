using AutoMapper;

namespace TelegramCopy.Services;

public interface IMapperService
{
    IMapper GetMapper();
    T Map<T>(object source);
    OutT Map<InT, OutT>(InT source);
    T Map<T>(object source, Action<object, T> afterMap);
    OutT Map<InT, OutT>(InT source, Action<InT, OutT> afterMap);
    IEnumerable<T> MapRange<T>(IEnumerable<object> items);
    IEnumerable<OutT> MapRange<InT, OutT>(IEnumerable<InT> items);
    IEnumerable<T> MapRange<T>(IEnumerable<object> items, Action<object, T> afterMap);
    IEnumerable<OutT> MapRange<InT, OutT>(IEnumerable<InT> items, Action<InT, OutT> afterMap);
}

