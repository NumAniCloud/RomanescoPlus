using Romanesco.DataModel.Entities;
using Romanesco.DataModel.Entities.Component;

namespace Romanesco.DataModel.Test.Domain;

internal static class Model
{
    public static ClassModel Class(string title, Type type, params IDataModel[] children)
    {
        return new ClassModel()
        {
            Title = title,
            TypeId = new TypeId(type),
            Children = children.Select(x => new PropertyModel()
            {
                Model = x,
                Attributes = System.Array.Empty<ModelAttributeData>()
            }).ToArray()
        };
    }

    public static ArrayModel Array<T>(string title, Type type, T prototype, params Action<T>[] setup)
        where T : IDataModel
    {
        var array = new ArrayModel()
        {
            Title = title,
            Delegation = new ModelCollection<IDataModel>()
            {
                Prototype = prototype,
                ElementType = new TypeId(type)
            }
        };

        foreach (var action in setup)
        {
            if (array.New() is T item)
            {
                action(item);
            }
        }

        return array;
    }

    public static IntModel Int(string title) => new() { Title = title };
    public static BoolModel Bool(string title) => new() { Title = title };
    public static StringModel String(string title) => new() { Title = title };
    public static FloatModel Float(string title) => new() { Title = title };
}
