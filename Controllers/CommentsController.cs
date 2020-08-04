using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wc_Blog.Models;

namespace wc_Blog.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Author).Include(c => c.BlogPost);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string commentBody, int blogPostId, string slug)
        {
            if (commentBody.IsNullOrWhiteSpace()) 
            {
                return RedirectToAction("Details", "BlogPosts", new { slug });
            }
            var comment = new Comment
            {
                CommentBody = commentBody,
                Created = DateTime.Now,
                BlogPostId = blogPostId,
                AuthorId = User.Identity.GetUserId()

            };


            db.Comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("Details", "BlogPosts", new { slug });
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment.AuthorId != User.Identity.GetUserId() && !(User.IsInRole("Admin") || User.IsInRole("Moderator")))
            {
                return RedirectToAction("Index", "Home");
            }
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FirstName", comment.AuthorId);
            ViewBag.BlogPostId = new SelectList(db.BlogPosts, "Id", "Title", comment.BlogPostId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BlogPostId,AuthorId,CommentBody,Created,Updated,UpdateReason")] Comment comment, string slug)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                comment.Updated = DateTime.Now;
                db.SaveChanges();
                slug = db.BlogPosts.Find(comment.BlogPostId).Slug;
                return RedirectToAction("Details", "BlogPosts", new { slug });
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FirstName", comment.AuthorId);
            ViewBag.BlogPostId = new SelectList(db.BlogPosts, "Id", "Title", comment.BlogPostId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment.AuthorId != User.Identity.GetUserId() && !(User.IsInRole("Admin")  || User.IsInRole("Moderator")) )
            {
                return RedirectToAction("Index", "Home");
            }
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Details", "BlogPosts", new  { slug = db.BlogPosts.Find(comment.BlogPostId).Slug});
             
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
