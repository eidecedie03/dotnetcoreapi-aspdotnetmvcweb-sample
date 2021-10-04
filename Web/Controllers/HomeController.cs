using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        string baseAddress = "http://localhost:54455/api/";

        public ActionResult Login()
        {
            if (Session["User"] != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Contents.RemoveAll();
            return  RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login([Bind(Include = "Username")] string username)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(baseAddress);

                    var postTask = client.PostAsJsonAsync<string>("user", username);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var content = await result.Content.ReadAsStringAsync();
                        Session["User"] = username;
                        Session["token"] = content;
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError("CustomError", "Invalid username or password.");
            }
            return View();
        }


        public ActionResult Index(string SearchByName, string SearchByStatus)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login");
            }
            IEnumerable<Record> records = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                var token = Session["token"].ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());
                var responseTask = client.GetAsync($"record?SearchByName={SearchByName}&SearchByStatus={SearchByStatus}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Record>>();
                    readTask.Wait();

                    records = readTask.Result;
                }
                else
                {
                    records = Enumerable.Empty<Record>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(records);
        }

        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EmployeeName,ClockInTime,ClockOutTime,IsActive")] Record record)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());

                    var postTask = client.PostAsJsonAsync<Record>("record", record);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(record);
        }

        public ActionResult Details(int? id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login");
            }
            Record record = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());

                var responseTask = client.GetAsync("record/" + id);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Record>();
                    readTask.Wait();

                    record = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(record);
        }

        public ActionResult Edit(int? id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login");
            }
            Record record = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());

                var responseTask = client.GetAsync("record/" + id);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Record>();
                    readTask.Wait();

                    record = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EmployeeName,ClockInTime,ClockOutTime,IsActive")] Record record)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login");
            }
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());

                    var postTask = client.PutAsJsonAsync<Record>("record", record);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");
            }
            return View(record);
        }

        public ActionResult Delete(int? id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login");
            }
            Record record = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());

                var responseTask = client.GetAsync("record/" + id);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Record>();
                    readTask.Wait();

                    record = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login");
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());

                var responseTask = client.DeleteAsync("record/" + id);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Record>();
                    readTask.Wait();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}