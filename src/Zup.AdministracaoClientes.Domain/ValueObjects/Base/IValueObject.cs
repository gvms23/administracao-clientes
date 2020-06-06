namespace Zup.AdministracaoClientes.Domain.ValueObjects.Base
{
    public interface IValueObject
    {
        bool Empty { get; }
        bool Invalid { get; }
        bool Valid { get; }
    }
}
