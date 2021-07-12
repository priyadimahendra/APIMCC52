using API.Repository.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Keys> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Keys>
    {
        private readonly Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]//tambahan buat bedain getnya
        public ActionResult Get()
        {
            var get = repository.Get();
            return Ok(get);// jadiin objeck status code, result, pesan.
        }

        [HttpGet("{key}")]//tambahan buat bedain getnya
        public ActionResult Get(Keys key)
        {
            var get = repository.Get(key);
            if (get == null)
            {
                return Ok(get);
            }
            else
            {
                return Ok(get);
            }
        }
        //tambah trycatch

        [HttpPost]
        public ActionResult Post(Entity entity)
        {
            var insert = repository.Insert(entity);
            if (insert == 1)
            {
                return Ok("Post Success");
            }
            else
            {
                return BadRequest("Fail To Insert");
            }
        }

        [HttpDelete]
        public ActionResult Delete(Keys key)
        {
            var response = repository.Delete(key);
            if (key == null)
            {
                return BadRequest("Need NIK");
            }
            else
            {
                if (response == 1)
                {
                    return Ok("Delete Success");
                }
                else
                {
                    return Ok("Delete Success");
                }
            }
        }

        [HttpPut]
        public ActionResult Update(Entity entity, Keys key)
        {
            var response = repository.Update(entity, key);
            if (key == null)
            {
                return BadRequest("Need NIK");
            }
            else
            {
                if (response == 1)
                {
                    return Ok("Update Success");
                }
                else
                {
                    return Ok("Update Success");
                }
            }
        }
    }
}
