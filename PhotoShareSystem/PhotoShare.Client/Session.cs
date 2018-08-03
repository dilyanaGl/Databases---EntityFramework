using System;
using System.Collections.Generic;
using System.Text;
using PhotoShare.Models;

namespace PhotoShare.Client
{
    public static class Session
    {
        public static User CurrentUser { get; set; }
    }
}
