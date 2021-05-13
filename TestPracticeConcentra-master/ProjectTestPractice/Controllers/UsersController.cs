using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectTestPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ProjectTestPractice.Controllers
{
    public class UsersController : Controller
    {
        static List<User> users = new List<User>();

        // GET: List of users
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();

            string url = "https://randomuser.me/api/";

            users.Clear();

            WebClient w = new WebClient();

            var json_data = w.DownloadString(url);

            dynamic data = JObject.Parse(json_data);

            for (int i = 0; i < data.results.Count; i++)
            {
                User user = new User();

                DateTime dateN = Convert.ToDateTime(data.results[i].dob.date);

                user.UserID = i + 1;
                user.Abbreviation = data.results[i].name.title;
                user.FisrtName = data.results[i].name.first;
                user.LastName = data.results[i].name.last;
                user.Age = data.results[i].dob.age;
                user.DateOfBirth = dateN.Date.ToLongDateString();
                user.Gender = data.results[i].gender;
                user.Email = data.results[i].email;
                user.Phone = data.results[i].phone;
                user.Location = (data.results[i].location.city + "," + data.results[i].location.country);
                user.Picture = data.results[i].picture.medium;
                user.PictureLg = data.results[i].picture.large;

                users.Add(user);
            }

            ViewBag.Users = users;

            return View();
        }

        public ActionResult Detail(int id)
        {
            var user = users.Find(a => a.UserID == id);

            if (user != null)
            {
                ViewBag.User = user;
                ViewBag.Sucess = true;
            }
            else
                ViewBag.Sucess = false;

            return View();
        }
    }
}