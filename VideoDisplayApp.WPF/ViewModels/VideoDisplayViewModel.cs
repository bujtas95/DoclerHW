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
        private bool _canExecuteGetVideoGroup = true;
        public bool CanExecuteGetVideoGroup
        {
            get { return _canExecuteGetVideoGroup; }
            set
            {
                _canExecuteGetVideoGroup = value;
                NotifyOfPropertyChange(() => CanExecuteGetVideoGroup);
            }
        }

        private long _currentPage;
        public long CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (value == 0)
                {
                    value = 1;
                }
                _currentPage = value;
                NotifyOfPropertyChange(() => CurrentPage);
            }
        }

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

        private string _selectedQuality = "Any";
        public string SelectedQuality
        {
            get { return _selectedQuality; }
            set
            {
                _selectedQuality = value.ToString();
                NotifyOfPropertyChange(() => SelectedQuality);
            }
        }

        private List<string> _qualitySource = new List<string>();
        public List<string> QualitySource
        {
            get
            {
                if (_qualitySource.Count > 0)
                {
                    return _qualitySource;
                }
                foreach (var quality in Enum.GetNames(typeof(QualityEnum)))
                {
                    _qualitySource.Add(quality);
                }
                return _qualitySource;
            }
        }

        private List<string> _selectedTags = new List<string>();
        public List<string> SelectedTags
        {
            get { return _selectedTags; }
            set
            {
                _selectedTags = value;
                NotifyOfPropertyChange(() => SelectedTags);
            }
        }

        private List<string> _tagsList = new List<string>();
        public List<string> TagsSource
        {
            get
            {
                if (_tagsList.Count > 0)
                {
                    return _tagsList;
                }
                foreach (var tag in Enum.GetNames(typeof(TagsEnum)))
                {
                    _tagsList.Add(tag);
                }
                return _tagsList;
            }
        }

        private List<Video> _videos;
        public List<Video> Videos
        {
            get { return _videos; }
            set
            {
                _videos = value;
                NotifyOfPropertyChange(() => Videos);
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
            CanExecuteGetVideoGroup = false;
            var requestURL = string.Format("https://pt.ptawe.com/api/video-promotion/v1/list" +
                            "?psid=bujtas&accessKey=6ed04d4ad128f4f47d75530b0c49f8c1&clientIp=2a01:36d:114:6e02:d870:81ca:eec4:c1f5&" +
                            "limit=25&pageIndex={0}", currentPage);
            if (!quality.Equals(QualityEnum.Any))
            {
                requestURL += string.Format("&quality={0}", quality.ToString().ToLower());
            }
            if (tags != null && tags.Count != 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(tags.First().ToLower());
                foreach (var element in tags.Skip(1))
                {
                    sb.Append(string.Format(",{0}", element).ToLower());
                }
                requestURL += "&tags=" + sb.ToString();
            }
            try
            {
                var response = await _videoApiService.GetAsync(requestURL);
                try
                {
                    VideoGroup = JsonConvert.DeserializeObject<VideoGroup>(response);
                    Videos = VideoGroup.Data.Videos.ToList();
                    CurrentPage = currentPage;
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
            CanExecuteGetVideoGroup = true;
            return result;
        }

        #endregion

        #region Event Handling

        public void SelectOrUnselectTag(string tag)
        {
            if (SelectedTags.Contains(tag))
            {
                SelectedTags.Remove(tag);
            }
            else
            {
                SelectedTags.Add(tag);
            }
        }

        #region Button clicks
        public void Search()
        {
            Enum.TryParse(SelectedQuality, out QualityEnum quality);
            _ = GetVideoListAsync(1, quality, SelectedTags);
        }

        public void GoToLastPage()
        {
            Enum.TryParse(SelectedQuality, out QualityEnum quality);
            _ = GetVideoListAsync(VideoGroup.Data.Pagination.TotalPages, quality, SelectedTags);
        }

        public void GoToFirstPage()
        {
            Enum.TryParse(SelectedQuality, out QualityEnum quality);
            _ = GetVideoListAsync(1, quality, SelectedTags);
        }

        public void GoToNextPage()
        {
            Enum.TryParse(SelectedQuality, out QualityEnum quality);
            if (CurrentPage < VideoGroup.Data.Pagination.TotalPages)
            {
                _ = GetVideoListAsync(CurrentPage + 1, quality, SelectedTags);
            }
        }

        public void GoToPreviousPage()
        {
            Enum.TryParse(SelectedQuality, out QualityEnum quality);
            if (CurrentPage > 1)
            {
                _ = GetVideoListAsync(CurrentPage - 1, quality, SelectedTags);
            }
        }
        #endregion
        #endregion
    }
}
