using System;
using System.Collections.Generic;
using System.Text;
using WZ.Report.IRepository;
using WZ.Report.IServices;
using WZ.Report.Model;

namespace WZ.Report.Services
{
   public class QuestionsService :Base.BaseServices<Questions>, IQuestionsService
    {

        private readonly IQuestionsRepository questionsRepository;
        public QuestionsService(IQuestionsRepository _questionsRepository)
        {
            questionsRepository = _questionsRepository;
            base.BaseDal = questionsRepository;
        }




    }
}
