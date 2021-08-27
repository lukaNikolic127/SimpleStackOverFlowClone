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
    public class CommentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CommentController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: CommentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CommentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CommentController/Create
        public ActionResult Create(int id) // za koji post pravim komentar, mora da se zove isto kao u anonimnom obj. u ActionLinku
        {
            int? memberId = HttpContext.Session.GetInt32("member_id");
            Post post = unitOfWork.Post.FindById(id);
            post.Member = unitOfWork.Member.FindById(post.MemberId);
            CreateCommentViewModel model = new CreateCommentViewModel
            {
                Post = post,
                PostContent = post.Content,
                PostMemberUsername = post.Member.Username,
                PostDateTime = post.DateTime,
                MemberId = (int)memberId
            };
            return View(model);
        }

        // POST: CommentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] CreateCommentViewModel model)
        {
            try
            {
                Comment comment = model.Comment;
                comment.DateTime = DateTime.Now;
                comment.MemberId = model.MemberId;
                comment.PostId = model.Post.PostId;
                comment.RatingUps = 0;
                unitOfWork.Comment.Add(comment);
                unitOfWork.Commit();
                return RedirectToAction(nameof(Details), nameof(Topic), new { id = model.Post.TopicId });
            }
            catch
            {
                return View();
            }
        }

        // GET: CommentController/Edit/5
        public ActionResult Edit(int id)
        {
            Comment model = unitOfWork.Comment.FindById(id);
            model.Post = unitOfWork.Post.FindById(model.PostId);
            return View(model);
        }

        // POST: CommentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [FromForm] Comment model)
        {
            try
            {
                Comment comment = unitOfWork.Comment.FindById(id);
                int topicId = unitOfWork.Post.FindById(comment.PostId).TopicId;
                
                comment.Content = model.Content;
                comment.DateTime = DateTime.Now;
                unitOfWork.Commit();
                return RedirectToAction(nameof(Details), nameof(Topic), new { id = topicId });
            }
            catch
            {
                return View();
            }
        }

        // GET: CommentController/Delete/5
        public ActionResult Delete(int id)
        {
            Comment model = unitOfWork.Comment.FindById(id);
            model.Member = unitOfWork.Member.FindById(model.MemberId);
            return View(model);
        }

        // POST: CommentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, [FromForm] Comment comment)
        {
            try
            {
                comment.CommentId = id;
                Post post = unitOfWork.Post.FindById(comment.PostId);
                int topicId = post.TopicId;
                unitOfWork.Rating.DeleteAllByCommentId(comment.CommentId);
                unitOfWork.Comment.Delete(comment);
                unitOfWork.Commit();
                return RedirectToAction(nameof(Details), nameof(Topic), new { id = topicId });
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Rating(RateViewModel request)
        {
            int? memberId = HttpContext.Session.GetInt32("member_id");

            RateViewModel model = new RateViewModel
            {
                CommentId = request.CommentId,
                RatingUp = unitOfWork.Rating.GetRatingUpCountByCommentId(request.CommentId), //unitOfWork.Rating.GetAll().Where(r => r.CommentId == request.CommentId && r.Like == true).ToList().Count(),
                RatingDown = unitOfWork.Rating.GetRatingDownCountByCommentId(request.CommentId),
                RatedUp = unitOfWork.Rating.IsCommentRatedUpByMember((int)memberId, request.CommentId), //unitOfWork.Rating.GetAll().Any(r => r.MemberId == memberId && r.CommentId == request.CommentId && r.Like == true),
                RatedDown = unitOfWork.Rating.IsCommentRatedDownByMember((int)memberId, request.CommentId)
            };
            return PartialView("RatePartial", model);
        }

        [HttpPost]
        public ActionResult RateUp(RateViewModel request)
        {
            int? memberId = HttpContext.Session.GetInt32("member_id");

            Rating rating = unitOfWork.Rating.GetMembersRatingForComment((int)memberId, request.CommentId); //unitOfWork.Rating.GetAll().SingleOrDefault(r => r.MemberId == memberId && r.CommentId == request.CommentId);
            if(rating == null)
            {
                Rating newRating = new Rating
                {
                    CommentId = request.CommentId,
                    DateTime = DateTime.Now,
                    Like = true,
                    Dislike = false,
                    MemberId = (int)memberId,
                };
                unitOfWork.Rating.Add(newRating);
                unitOfWork.Comment.RateUp(request.CommentId);
                unitOfWork.Commit();
            }
            else
            {
                if(rating.Like == true)
                {
                    rating.Like = false;
                    unitOfWork.Comment.RateDown(request.CommentId);
                    unitOfWork.Commit();
                }
                else if(rating.Like == false && rating.Dislike == true)
                {
                    rating.Dislike = false;
                    rating.Like = true;
                    unitOfWork.Comment.RateUp(request.CommentId);
                    unitOfWork.Commit();
                }
                if (rating.Like == false && rating.Dislike == false)
                {
                    unitOfWork.Rating.Delete(rating);
                    unitOfWork.Commit();
                }
            }
            
            return Rating(new RateViewModel { CommentId = request.CommentId });
        }

        [HttpPost]
        public ActionResult RateDown(RateViewModel request)
        {
            int? memberId = HttpContext.Session.GetInt32("member_id");

            Rating rating = unitOfWork.Rating.GetMembersRatingForComment((int)memberId, request.CommentId); //unitOfWork.Rating.GetAll().SingleOrDefault(r => r.MemberId == memberId && r.CommentId == request.CommentId);
            if (rating == null)
            {
                Rating newRating = new Rating
                {
                    CommentId = request.CommentId,
                    DateTime = DateTime.Now,
                    Like = false,
                    Dislike = true,
                    MemberId = (int)memberId
                };

                unitOfWork.Rating.Add(newRating);
                unitOfWork.Commit();
            }
            else
            {
                if (rating.Dislike == true)
                {
                    rating.Dislike = false;
                    unitOfWork.Commit();
                }
                else if (rating.Dislike == false && rating.Like == true)
                {
                    rating.Dislike = true;
                    rating.Like = false;
                    unitOfWork.Comment.RateDown(request.CommentId);
                    unitOfWork.Commit();
                }
                if (rating.Like == false && rating.Dislike == false)
                {
                    unitOfWork.Rating.Delete(rating);
                    unitOfWork.Commit();
                }
            }

            return Rating(new RateViewModel { CommentId = request.CommentId });
        }
    }
}
