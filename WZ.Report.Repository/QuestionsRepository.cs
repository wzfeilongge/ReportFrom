﻿using System;
using System.Collections.Generic;
using System.Text;
using WZ.Report.IRepository;
using WZ.Report.Model;

namespace WZ.Report.Repository
{
   
   public class QuestionsRepository: Base.BaseRepository<Questions>, IQuestionsRepository
    {
        public QuestionsRepository()
        {

        }
    }
}
