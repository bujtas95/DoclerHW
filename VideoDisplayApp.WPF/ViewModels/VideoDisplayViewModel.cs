using Caliburn.Micro;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoDisplayApp.UILibrary.API;
using VideoDisplayApp.WPF.Enums;
using VideoDisplayApp.WPF.Models;

namespace VideoDisplayApp.WPF.ViewModels
{
    public class VideoDisplayViewModel : Screen
    {
        #region Properties

        private VideoGroup _videoGroup;
        public VideoGroup VideoGroup
        {
            get { return _videoGroup; }
            set
            {
                _videoGroup = value;
                NotifyOfPropertyChange(() => VideoGroup);
            }
        }

        #endregion

        #region ctor

        private readonly IAPIHelper _videoApiService;
        public VideoDisplayViewModel(IAPIHelper videoApiService)
        {
            _videoApiService = videoApiService;
            _ = GetVideoListAsync();
        }

        #endregion

        #region API communication

        //It's a kind of "hacking"... normally I would use packages like Refit or generate code from RAML
        private async Task<bool> GetVideoListAsync(long currentPage = 1, QualityEnum quality = QualityEnum.Any, List<string> tags = null)
        {
            var result = false;
            var requestURL = string.Format("https://pt.ptawe.com/api/video-promotion/v1/list" +
                            "?psid=bujtas&accessKey=6ed04d4ad128f4f47d75530b0c49f8c1&clientIp=2a01:36d:114:6e02:d870:81ca:eec4:c1f5&" +
                            "limit=25&pageIndex={0}", currentPage);
            try
            {
                var response = await _videoApiService.GetAsync(requestURL);
                try
                {
                    VideoGroup = JsonConvert.DeserializeObject<VideoGroup>(response);
                    result = true;
                }
                catch (JsonException e)
                {
                    Console.WriteLine("JsonException source: {0}", e.Source);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("HttpResponseException source: {0}", e.Source);
            }
            return result;
        }

        #endregion
    }
}
