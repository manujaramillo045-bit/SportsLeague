using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsLeague.DataAccess.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : AuditBase // recibe un tipo de dato generico (T), que es una clase que hereda de AuditBase, y se va a usar para crear un repositorio generico que me permita hacer operaciones CRUD (Create, Read, Update, Delete) en cualquier entidad que herede de AuditBase, como Team, Player, etc.
{
    // estas variables son solo de lectura y me sirven para inyectar la dependencia de la clase Generic
    protected readonly LeagueDbContext _context; // con esta variable puedo acceder a la base de datos y hacer operaciones CRUD, es el contexto de la base de datos
    protected readonly DbSet<T> _dbSet; // con esta variable puedo tener manipulacion de las tablas de la base de datos, es el conjunto de datos de la entidad que se va a usar para hacer operaciones CRUD

    public GenericRepository(LeagueDbContext context)
    {

        _context = context;
        _dbSet = context.Set<T>();
    }

    // se deben implementar los 6 métodos de la interfaz IGenericRepository<T>, que son GetAllAsync, GetByIdAsync, CreateAsync, UpdateAsync, DeleteAsync y ExistsAsync, y cada uno de ellos hace una operación CRUD diferente en la base de datos, usando el contexto y el conjunto de datos para hacer las operaciones correspondientes.

    public async Task<IEnumerable<T>> GetAllAsync()  //1. GetAllAsync: este método devuelve una lista de todas las entidades del tipo T que hay en la base de datos, usando el método ToListAsync() para convertir el conjunto de datos en una lista.
    {
        return await _dbSet.ToListAsync(); // Retorna una lista de todas las entidades del tipo T que hay en la base de datos, usando el método ToListAsync() para convertir el conjunto de datos en una lista.
    }
    public async Task<T?> GetByIdAsync(int id) //2. GetByIdAsync: este método recibe el id de una entidad del tipo T, y devuelve la entidad que tiene ese id en la base de datos, usando el método FindAsync() para buscar la entidad por su id, y devuelve null si no encuentra ninguna entidad con ese id. async: en la firma del metodo. Await: en el metodo o sea que se invocara de forma asincrona, lo que permite que el hilo de ejecucion no se bloquee mientras se espera la respuesta de la base de datos, lo que mejora el rendimiento y la escalabilidad de la aplicacion.
    {
        return await _dbSet.FindAsync(id); // recibe un id, lo busca y devuelve la entidad que tiene ese id en la base de datos, usando el método FindAsync() para buscar la entidad por su id, y devuelve null si no encuentra ninguna entidad con ese id.
    }

    public async Task<T> CreateAsync(T entity) //3. CreateAsync: este método recibe una entidad del tipo T, le asigna la fecha de creación y actualización, y luego la agrega al conjunto de datos y guarda los cambios en la base de datos, usando el método AddAsync() para agregar la entidad y SaveChangesAsync() para guardar los cambios.
    {
        entity.CreatedAt = DateTime.UtcNow; // define la fecha de creacion de la entidad, usando la fecha y hora actual en formato UTC, lo que permite tener una referencia temporal consistente y sin problemas de zona horaria, lo que es importante para aplicaciones distribuidas o con usuarios en diferentes regiones.
        entity.UpdatedAt = null; // define la fecha de actualizacion de la entidad, como null, lo que indica que la entidad no ha sido actualizada desde su creacion, lo que es importante para tener un control de las modificaciones y cambios en la entidad a lo largo del tiempo.
        await _dbSet.AddAsync(entity); // agrega la entidad al conjunto de datos, usando el método AddAsync() para agregar la entidad de forma asincrona, lo que permite que el hilo de ejecucion no se bloquee mientras se espera la respuesta de la base de datos, lo que mejora el rendimiento y la escalabilidad de la aplicacion.
        await _context.SaveChangesAsync(); // se agrega al contexto de la base de datos, y luego se guardan los cambios en la base de datos, usando el método SaveChangesAsync() para guardar los cambios de forma asincrona, lo que permite que el hilo de ejecucion no se bloquee mientras se espera la respuesta de la base de datos, lo que mejora el rendimiento y la escalabilidad de la aplicacion. ** MAS IMPORTANTE
        return entity; // retorna la entidad que se acaba de crear, lo que es importante para tener una referencia a la entidad creada, y poder usarla en otras partes del codigo, como por ejemplo para devolverla en una respuesta de una API, o para usarla en otra operacion que requiera la entidad creada.
    }

    public async Task UpdateAsync(T entity) //4. UpdateAsync: este método recibe una entidad del tipo T, le asigna la fecha de actualización, y luego la actualiza en el conjunto de datos y guarda los cambios en la base de datos, usando el método Update() para actualizar la entidad y SaveChangesAsync() para guardar los cambios.
    {
        entity.UpdatedAt = DateTime.UtcNow; // define la fecha de actualizacion de la entidad, usando la fecha y hora actual en formato UTC, lo que permite tener una referencia temporal consistente y sin problemas de zona horaria, lo que es importante para aplicaciones distribuidas o con usuarios en diferentes regiones.
        _dbSet.Update(entity); // actualiza la entidad en el conjunto de datos, usando el método Update() para actualizar la entidad, lo que marca la entidad como modificada en el contexto de la base de datos, lo que es importante para que el contexto sepa que debe actualizar esa entidad en la base de datos cuando se guarden los cambios.
        await _context.SaveChangesAsync(); // se actualiza en el contexto de la base de datos, y luego se guardan los cambios en la base de datos, usando el método SaveChangesAsync() para guardar los cambios de forma asincrona, lo que permite que el hilo de ejecucion no se bloquee mientras se espera la respuesta de la base de datos, lo que mejora el rendimiento y la escalabilidad de la aplicacion. ** MAS IMPORTANTE
    }

    public async Task DeleteAsync(int id) //5. DeleteAsync: este método recibe el id de una entidad del tipo T, busca la entidad en el conjunto de datos usando el método FindAsync(), y si la encuentra, la elimina del conjunto de datos y guarda los cambios en la base de datos, usando el método Remove() para eliminar la entidad y SaveChangesAsync() para guardar los cambios.
    {
        var entity = await GetByIdAsync(id); // recibe un id, lo busca y devuelve la entidad que tiene ese id en la base de datos, usando el método FindAsync() para buscar la entidad por su id, y devuelve null si no encuentra ninguna entidad con ese id.
        if (entity != null) // si la entidad existe, se elimina del conjunto de datos y se guardan los cambios en la base de datos, usando el método Remove() para eliminar la entidad y SaveChangesAsync() para guardar los cambios.
        {
            _dbSet.Remove(entity); // elimina la entidad del conjunto de datos, usando el método Remove() para eliminar la entidad, lo que marca la entidad como eliminada en el contexto de la base de datos, lo que es importante para que el contexto sepa que debe eliminar esa entidad en la base de datos cuando se guarden los cambios.
            await _context.SaveChangesAsync(); // se elimina en el contexto de la base de datos, y luego se guardan los cambios en la base de datos, usando el método SaveChangesAsync() para guardar los cambios de forma asincrona, lo que permite que el hilo de ejecucion no se bloquee mientras se espera la respuesta de la base de datos, lo que mejora el rendimiento y la escalabilidad de la aplicacion. ** MAS IMPORTANTE
        }
    }

    public async Task<bool> ExistsAsync(int id) //6. ExistsAsync: este método recibe el id de una entidad del tipo T, y devuelve un valor booleano que indica si existe o no una entidad con ese id en la base de datos, usando el método AnyAsync() para verificar si hay alguna entidad que cumpla con la condición de tener el id especificado.
    {
        return await _dbSet.AnyAsync(e => e.Id == id); // recibe un id, y devuelve un valor booleano que indica si existe o no una entidad con ese id en la base de datos, usando el método AnyAsync() para verificar si hay alguna entidad que cumpla con la condición de tener el id especificado, lo que permite saber si una entidad existe o no en la base de datos sin necesidad de cargar toda la entidad, lo que mejora el rendimiento y la eficiencia de la aplicacion.
    }
}


