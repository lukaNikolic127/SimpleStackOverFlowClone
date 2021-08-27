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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Forum.WebApp.Controllers
{
    [LoggedInMemberFilter]
    public class PostController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public PostController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // GET: PostController
        // [LoggedInMemberFilter]
        public ActionResult Index()
        {
            // int memberId = ViewBag.MemberId;
            int? memberId = HttpContext.Session.GetInt32("member_id");
            List<Post> posts = unitOfWork.Post.GetAllByMember((int)memberId); //unitOfWork.Post.GetAll().Where(p => p.MemberId == memberId).ToList();
            foreach (var post in posts)
            {
                post.Topic = unitOfWork.Topic.FindById(post.TopicId);
            }
            if(posts.Count() == 0)
            {
                ViewBag.NoMembersPosts = true;
            }
            else
            {
                ViewBag.NoMembersPosts = false;
            }
            return View(posts);
        }

        // GET: PostController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostController/Create
        //[LoggedInMemberFilter]
        public ActionResult Create(int topicId)
        {
            List<SelectListItem> topics = new List<SelectListItem>();
            foreach(Topic t in unitOfWork.Topic.GetAll())
            {
                topics.Add(new SelectListItem { Value = t.TopicId.ToString(), Text = t.Name });
            }
            List<SelectListItem> members = new List<SelectListItem>();
            foreach (Member m in unitOfWork.Member.GetAll())
            {
                members.Add(new SelectListItem { Value = m.MemberId.ToString(), Text = m.Username });
            }
            //ViewBag.Topics = topics;
            int? memberId = HttpContext.Session.GetInt32("member_id");
            CreatePostViewModel model = new CreatePostViewModel { Topics = topics, MemberId = (int)memberId };
            return View(model);
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] CreatePostViewModel post)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                try
                {
                    Post newPost = new Post
                    {
                        Content = post.Content,
                        DateTime = DateTime.Now,
                        MemberId = post.MemberId,
                        TopicId = post.TopicId
                    };
                    unitOfWork.Post.Add(newPost);
                    unitOfWork.Commit();
                    return RedirectToAction(nameof(Details), nameof(Topic), new { id = newPost.TopicId });
                    //return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            
        }

        // GET: PostController/Edit/5
        public ActionResult Edit(int id)
        {
            Post post = unitOfWork.Post.FindById(id);
            List<SelectListItem> topics = new List<SelectListItem>();
            foreach (var topic in unitOfWork.Topic.GetAll())
            {
                topics.Add(new SelectListItem { Value = topic.TopicId.ToString(), Text = topic.Name });
            }

            EditPostViewModel model = new EditPostViewModel
            {
                Post = post,
                Topics = topics,
                Direct = "NotDirect"
            };
            return View(model);
        }

        public ActionResult EditDirect(int id)
        {
            Post post = unitOfWork.Post.FindById(id);
            List<SelectListItem> topics = new List<SelectListItem>();
            foreach (var topic in unitOfWork.Topic.GetAll())
            {
                topics.Add(new SelectListItem { Value = topic.TopicId.ToString(), Text = topic.Name });
            }
            EditPostViewModel model = new EditPostViewModel
            {
                Post = post,
                Topics = topics,
                Direct = "Direct"
            };
            return View(nameof(Edit), model);
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] EditPostViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                try
                {
                    Post p = unitOfWork.Post.FindById(model.Post.PostId);
                    p.Content = model.Post.Content;
                    p.DateTime = DateTime.Now;
                    p.TopicId = model.Post.TopicId;
                    unitOfWork.Commit();
                    if (model.Direct == "NotDirect")
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return RedirectToAction(nameof(Details), nameof(Topic), new { id = p.TopicId });
                    }
                }
                catch
                {
                    return View();
                }
            }
            
        }

        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
            Post model = unitOfWork.Post.FindById(id);
            model.Member = unitOfWork.Member.FindById(model.MemberId);
            return View(model);
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, [FromForm] Post post)
        {
            try
            {
                post.PostId = id; // iz rute
                foreach (var comment in unitOfWork.Comment.GetAllByPostId(post.PostId))
                {
                    unitOfWork.Rating.DeleteAllByCommentId(comment.CommentId);
                }
                unitOfWork.Comment.DeleteAllByPostId(post.PostId);
                unitOfWork.Post.Delete(post);
                unitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
