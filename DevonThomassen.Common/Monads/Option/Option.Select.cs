using System.IO.MemoryMappedFiles;

namespace DevonThomassen.Common.Monads.Option;

public readonly partial record struct Option<TValue>
{
    public Option<TResult> Select<TResult>(Func<TValue, TResult> mapper)
    {
        ArgumentNullException.ThrowIfNull(mapper);
        
        return HasValue 
            ? Option<TResult>.Some(mapper(_value)) 
            : Option<TResult>.None;
    }
    
    public Option<TResult> SelectMany<TResult>(Func<TValue, Option<TResult>> binder)
    {
        ArgumentNullException.ThrowIfNull(binder);
        
        return HasValue 
            ? binder(_value) 
            : Option<TResult>.None;
    }
}