﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteApplication.Models;

namespace NoteApplication.Controllers
{
    public class HomeController : Controller
    {
        private NoteContext db = new NoteContext();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult updateNote(Notes note)
        {
            Notes oldNoteModel = db.Notes.Find(note.Id);

            if (oldNoteModel == null)
            {
                //Add new
                note.Id = Guid.NewGuid();
                note.DateNote = DateTime.UtcNow;
                db.Notes.Add(note);
                db.SaveChanges();
            }
            else
            {
                //Update
                note.DateNote = DateTime.UtcNow;
                db.Entry(oldNoteModel).CurrentValues.SetValues(note);
                db.SaveChanges();
            }

            return Json(note);

        }

        [HttpGet]
        public IActionResult _note_list()
        {
            var result = db.Notes.OrderByDescending(n => n.DateNote).AsQueryable();
            return PartialView(result);
        }

        [HttpGet]
        public JsonResult getNoteDetailById(string noteId)
        {
            var result = db.Notes.Find(new Guid(noteId));
            return Json(result);
        }

        [HttpGet]
        public JsonResult deleteNoteById(string noteId)
        {
            Notes note = db.Notes.Find(new Guid(noteId));
            db.Entry(note).State = EntityState.Deleted;
            db.SaveChanges();

            return Json("Successfully deleted.");
        }
    }
}
