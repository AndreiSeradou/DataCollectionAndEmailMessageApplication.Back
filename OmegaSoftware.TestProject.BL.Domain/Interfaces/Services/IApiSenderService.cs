﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSoftware.TestProject.BL.Domain.Interfaces.Services
{
    public interface IApiSenderService<T, K> where T : class where K : class
    {
        string SendOnApi(List<K> values = default);
    }
}
        