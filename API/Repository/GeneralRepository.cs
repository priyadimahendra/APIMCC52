using API.Context;
using API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class GeneralRepository<Context, Entity, Keys> : IRepository<Entity, Keys>
        where Context : MyContext
        where Entity : class
    {
        private readonly MyContext context;
        private readonly DbSet<Entity> entities;

        public GeneralRepository(MyContext context)
        {
            this.context = context;
            entities = context.Set<Entity>();
        }

        public int Delete(Keys key)
        {
            var find = entities.Find(key);
            entities.Remove(find);
            var delete = context.SaveChanges();
            return delete;
        }

        public IEnumerable<Entity> Get()
        {
            var employee = entities.ToList();
            return employee;
        }

        public Entity Get(Keys key)
        {
            var find = entities.Find(key);
            return find;
        }

        public int Insert(Entity entity)
        {
            entities.Add(entity);
            var insert = context.SaveChanges();
            return insert;//catatan 
        }

        public int Update(Entity entity, Keys key)
        {
            var employees = entities.Find(key);
            entities.Attach(employees);
            entities.Update(employees);
            var update = context.SaveChanges();
            return update;
        }
    }
}
