﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Entities
{
    public class Case
    {
#pragma warning disable CS8618
        public int CaseId { get; set; }
        public string CaseName { get; set; }
        public bool IsCorrect { get; set; }
        public int QuizExerciseId { get; set; }
    }
}
