using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Threading.Tasks;
using BotInputQuestions.Models;

namespace BotInputQuestions.Controllers
{
    public class QuestionAnswerController : Controller
    {
        [ActionName("Index")]
        public async Task<ActionResult> IndexAsync()
        {
            var items = await DocumentDBRepository<QuestionAnswer>.GetItemsAsync(d => d.Id != null);
            return View(items);
        }

        [ActionName("Create")]
        public async Task<ActionResult> CreateAsync()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind(Include = "Id,Question,Answer,Topic")] QuestionAnswer item)
        {
            if (ModelState.IsValid)
            {
                await DocumentDBRepository<QuestionAnswer>.CreateItemAsync(item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind(Include = "Id,Question,Answer,Topic")] QuestionAnswer item)
        {
            if (ModelState.IsValid)
            {
                await DocumentDBRepository<QuestionAnswer>.UpdateItemAsync(item.Id, item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            QuestionAnswer item = await DocumentDBRepository<QuestionAnswer>.GetItemAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await DocumentDBRepository<QuestionAnswer>.DeIeteItemAsync(id);


            return RedirectToAction("Index");
        }

    }
}