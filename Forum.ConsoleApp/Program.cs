using Forum.Data;
using Forum.Data.Implementation;
using Forum.Data.UnitOfWork;
using Forum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Forum.ConsoleApp
{
    class Program
    {
        //static ForumContext context = new ForumContext();
        static void Main(string[] args)
        {
            ShowAllTopics();
        }

        public static void DeleteComment(int commentId)
        {
            using (IUnitOfWork uow = new ForumUnitOfWork(new ForumContext()))
            {
                Comment c = uow.Comment.FindById(commentId);
                uow.Rating.DeleteAllByCommentId(c.CommentId);
                uow.Comment.Delete(c);
                uow.Commit();
            }
        }

        public static void ShowAllTopics()
        {
            using (IUnitOfWork uow = new ForumUnitOfWork(new ForumContext()))
            {
                List<Topic> topics = uow.Topic.GetAll();
                Console.WriteLine(topics.Count() + " elemenata");
                uow.Topic.GetAll().ForEach(t => Console.WriteLine(t.Name + " Posts(" + t.Posts.Count() + ")"));
                uow.Commit();
            }
        }
        
    }
}
