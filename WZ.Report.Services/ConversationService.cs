using System;
using System.Collections.Generic;
using System.Text;
using WZ.Report.IRepository;
using WZ.Report.IServices;
using WZ.Report.Model;

namespace WZ.Report.Services
{
   public class ConversationService:Base.BaseServices<Conversation>, IConversationService
    {


        private readonly IConversationRepository _ConversationRepository;
        public ConversationService(IConversationRepository conversationRepository)
        {
            _ConversationRepository = conversationRepository;

            base.BaseDal = _ConversationRepository;
        }






    }
}
