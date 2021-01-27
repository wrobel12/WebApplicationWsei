using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApplicationWsei.Models.Hubs;

namespace WebApplicationWsei.Controllers
{
    public class ChatController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}