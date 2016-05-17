using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using MusicScheduler.Objects;
using YoutubeExtractor;

namespace MusicScheduler.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager userManager;

        public HomeController()
        {
            this.userManager = ServiceLocator.Usermanager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public void Skip()
        {
            ServiceLocator.Musicplayer.SkipCurrentSong();
        }

        public void PauseResume()
        {
            ServiceLocator.Musicplayer.PauseResumeCurrentSong();
        }

        [HttpGet("api/info")]
        public Info GetInfo()
        {
            var info = new Info
            {
                CurrentlyPlaying = ServiceLocator.Musicplayer.CurrentPlayingSong != null
                    ? ServiceLocator.Musicplayer.CurrentPlayingSong.Name
                    : "Nothing is playing",
                IsPaused = ServiceLocator.Musicplayer.IsPaused,
                Users = new List<User>()
            };

            foreach (var user in this.userManager.Users)
            {
                info.Users.Add(user);
            }

            return info;
        }

        [HttpPost("api/bookSong")]
        public ActionResult BookSong([FromBody] BookSongModel model)
        {
            var videoUrl = model.URL;
            var userName = model.Name;

            var isYoutubeUrl = DownloadUrlResolver.TryNormalizeYoutubeUrl(videoUrl, out videoUrl);

            if (string.IsNullOrWhiteSpace(model.URL) || string.IsNullOrWhiteSpace(userName) || !isYoutubeUrl)
            {
                ViewData["ErrorMessage"] = "Dulli!";
                return View();
            }

            var user = this.userManager.Users.FirstOrDefault(x => x.Name == userName);

            if (user != null)
            {
                user.YoutubeLinks.Add(new YoutubeFile {Url = model.URL});
            }
            else
            {
                this.userManager.Users.Add(new User(userName, new YoutubeFile {Url = model.URL}));
            }

            return Content("success");
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

        private enum ActionType
        {
            SkipSong = 1,
            ToggleMusic
        }
    }
}