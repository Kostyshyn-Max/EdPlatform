﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Entities
{
    public class FillExercise : Exercise
    {
#pragma warning disable CS8618
        public string Answer { get; set; }
    }
}
