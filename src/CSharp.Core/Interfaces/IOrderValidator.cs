using CSharp.Core.Models;

namespace CSharp.Core.Interfaces;

public interface IOrderValidator
{
    bool Validate(Order order);
}