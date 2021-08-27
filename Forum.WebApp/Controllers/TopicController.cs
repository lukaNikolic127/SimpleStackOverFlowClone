using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Data.UnitOfWork;
using Forum.Domain;
using Forum.WebApp.Filters;
using Forum.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum.WebApp.Controllers
{
    [LoggedInMemberFilter]

    public class TopicController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public TopicController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // GET: TopicController
        [NotLoggedInMemberFilter]
        public ActionResult Index()
        {
            List<Topic> model = unitOfWork.Topic.GetAll();
            
            int? memberId = HttpContext.Session.GetInt32("member_id");
            if(memberId != null)
            {
                ViewBag.IsLoggedIn = true;
                ViewBag.MemberId = memberId;
            }
            
            return View("Index", model);
        }

        // u ruti je "id", [FromRoute(Name="id")] za npr int topicId
        public ActionResult Details([FromRoute] int id) 
        {
            int? memberId = HttpContext.Session.GetInt32("member_id");
            if (memberId != null)
            {
                ViewBag.IsLoggedIn = true;
                ViewBag.MemberId = memberId;
            }
            else
            {
                return RedirectToAction("Index", "Member");
            }
            Topic topic = unitOfWork.Topic.FindById(id);
            List<Post> posts = unitOfWork.Post.GetAllByTopic(topic.TopicId);
            foreach (var post in posts)
            {
                post.Member = unitOfWork.Member.FindById(post.MemberId);
                post.Comments = unitOfWork.Comment.GetAllByPostId(post.PostId);
                foreach (var comment in post.Comments)
                {
                    comment.Member = unitOfWork.Member.FindById(comment.MemberId);
                }
            }
            DetailsTopicViewModel model = new DetailsTopicViewModel
            {
                Topic = topic,
                Posts = posts
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(); // ako se view zove != Create, return View("naziv")
        }

        [HttpPost]
        public ActionResult Create([FromForm] Topic topic)
        {
            if (!ModelState.IsValid) 
            {
                return View();
            }

            bool exists = unitOfWork.Topic.Search(t => t.Name == topic.Name).Any();
            if(exists)
            {
                ModelState.AddModelError("TopicNameError", "Topic already exists!");
                return View();
            }

            unitOfWork.Topic.Add(topic);
            unitOfWork.Commit();
            return Index();
        }
    }
}
