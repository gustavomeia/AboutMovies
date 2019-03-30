using System;
using System.Collections.Generic;
using System.Text;

namespace AboutMovies.Interfaces
{
    public interface IRequestError
    {
        bool Error { get; set; }

        string MessageErro { get; set; }

        int StatusCode { get; set; }
    }
}
