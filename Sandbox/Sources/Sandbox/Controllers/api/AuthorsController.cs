﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Sandbox.DAL;
using Sandbox.Models;
using Sandbox.ViewModels;

namespace Sandbox.Controllers.api
{
    public class AuthorsController : ApiController
    {
        private BookContext db = new BookContext();


        // GET: api/Authors
        public ResultList<AuthorViewModel> Get([FromUri]QueryOptions queryOptions)
        {
            var start = (queryOptions.CurrentPage - 1) * queryOptions.PageSize;

            var authors = db.Authors
              .OrderBy(queryOptions.Sort)
              .Skip(start)
              .Take(queryOptions.PageSize)
              .ToList();

            queryOptions.TotalPages =
              (int)Math.Ceiling((double)db.Authors.Count() / queryOptions.PageSize);

            Mapper.CreateMap<Author, AuthorViewModel>();

            return new ResultList<AuthorViewModel>(Mapper.Map<List<Author>, List<AuthorViewModel>>(authors), queryOptions);
        }

        // PUT: api/Authors/5
        [ResponseType(typeof (void))]
        public IHttpActionResult Put(AuthorViewModel author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.CreateMap<AuthorViewModel, Author>();
            db.Entry(Mapper.Map<AuthorViewModel, Author>(author)).State
                              = EntityState.Modified;

            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Authors
        [ResponseType(typeof(AuthorViewModel))]
        public IHttpActionResult Post(AuthorViewModel author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.CreateMap<AuthorViewModel, Author>();
            db.Authors.Add(Mapper.Map<AuthorViewModel, Author>(author));
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { Id = author.Id }, author);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
