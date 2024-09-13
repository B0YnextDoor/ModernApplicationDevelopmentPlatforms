using System.ComponentModel.DataAnnotations;

namespace WEB_253502_KRASYOV.Blazor.SSR.Model
{
    public class CounterValidator
    {
        [Range(1, 10)]
        public int Value { get; set; }
    }
}
