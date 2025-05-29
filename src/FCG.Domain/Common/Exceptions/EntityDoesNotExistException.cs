namespace FCG.Domain.Common.Exceptions;

public class EntityDoesNotExistException(Guid id, Type type) : Exception($"{type} with id {id} does not exist")
{
    private Guid Id { get; set; } = id;
    private Type Type { get; set; } = type;
}
