using System;
namespace domain;
public interface IAnimalService
{
    Task<Animal[]> GetAll();
    Task<Animal> Add(string name);
    Task Delete(int id);
}

