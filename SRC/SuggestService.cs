using BLL;
using BLL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SRC
{
    public class SuggestService:ISuggestService
    {
        private SuggestRepository suggetRepository;
        private UserRepository userRepository;
        private Suggest suggest;
        public SuggestService()
        {
            suggetRepository = new SuggestRepository();
            userRepository = new UserRepository();
            suggest = new Suggest();
        }

        public Suggest Publish(string title,string body,int authorId)
        {
            suggest.Title = title;
            suggest.Body = body;
            suggest.Author = userRepository.GetById(authorId.ToString());
            suggest.CreatedTime = DateTime.Now;
            suggetRepository.Publish(suggest,authorId);
            return suggest;
        }

        

    }
}
