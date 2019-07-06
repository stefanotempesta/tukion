using evenito.Tukion.Server.Entities;
using evenito.Tukion.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace evenito.Tukion.Server.Data.Mocks
{
    public class MockVideoModelDataAdaptor : IDataAdaptor<VideoModel>
    {
        public void Dispose()
        {
        }

        public IEnumerable<VideoModel> LoadAll()
        {
            foreach (Video v in Videos)
            {
                yield return Load(v.Id);
            }
        }

        public VideoModel Load(Guid id)
        {
            Video video = Videos.SingleOrDefault(v => v.Id == id);
            if (video == null) return null;

            return new VideoModel
            {
                Video = video,
                Owner = Users.SingleOrDefault(u => u.Id == video.OwnerId),
                Channels = Channels.Where(c => ChannelVideos.Where(v => v.VideoId == video.Id).Select(v => v.ChannelId).Contains(c.Id)),
                Tags = Tags.Where(t => VideoTags.Where(v => v.VideoId == video.Id).Select(v => v.TagId).Contains(t.Id)),
                Views = Views.Where(v => v.VideoId == video.Id),
                Reactions = Reactions.Where(r => r.VideoId == video.Id),
                Favourites = Favourites.Where(f => f.VideoId == video.Id),
                Comments = Comments.Where(c => c.VideoId == video.Id)
            };
        }

        #region Mock Data

        private List<User> Users = new List<User>
        {
            new User
            {
                Id = Guid.Parse("0029208b-4217-46d8-87f2-85fb78defac3"),
                UserName = "stefano"
            }
        };

        private List<Channel> Channels = new List<Channel>
        {
            new Channel
            {
                Id = Guid.Parse("63198fcb-b459-43fa-82ca-1894bd96a91c"),
                OwnerId = Guid.Parse("0029208b-4217-46d8-87f2-85fb78defac3"),
                Title = "Channel #1"
            },
            new Channel
            {
                Id = Guid.Parse("0981aabe-aba6-402b-8a1e-07d5b162763d"),
                OwnerId = Guid.Parse("0029208b-4217-46d8-87f2-85fb78defac3"),
                Title = "Channel #2"
            }
        };

        private List<Video> Videos = new List<Video>
        {
            new Video
            {
                Id = Guid.Parse("ce200f0a-9582-47bc-8019-eaf9186794f7"),
                OwnerId = Guid.Parse("0029208b-4217-46d8-87f2-85fb78defac3"),
                AddedOn = new DateTime(2019, 6, 22, 10, 30, 0),
                Title = "Why Is Mozart Genius?",
                Description = "This video looks at why Mozart is widely considered as a genius - the greatest musical genius to walk this Earth.",
                Duration = 582,
                Visibility = VideoVisibility.Public,
                ContentURL = new Uri("https://www.youtube.com/watch?v=CN3v4fEZcQw"),
                ThumbnailURL = new Uri("http://i1.ytimg.com/vi/CN3v4fEZcQw/default.jpg")
            },
            new Video
            {
                Id = Guid.Parse("8ad38d08-fd45-43fc-ac96-5abb049917f0"),
                OwnerId = Guid.Parse("0029208b-4217-46d8-87f2-85fb78defac3"),
                AddedOn = new DateTime(2016, 12, 9, 12, 50, 0),
                Title = "How the blockchain will radically transform the economy",
                Description = "Say hello to the decentralized economy - the blockchain is about to change everything.",
                Duration = 897,
                Visibility = VideoVisibility.Public,
                ContentURL = new Uri("https://www.youtube.com/watch?v=RplnSVTzvnU"),
                ThumbnailURL = new Uri("http://i1.ytimg.com/vi/RplnSVTzvnU/default.jpg")
            },
            new Video
            {
                Id = Guid.Parse("4fc1550a-d4f2-45c6-8bf3-a170411f8672"),
                OwnerId = Guid.Parse("0029208b-4217-46d8-87f2-85fb78defac3"),
                AddedOn = new DateTime(2012, 2, 28, 15, 00, 0),
                Title = "Ellen's Favorite What's Wrong... Photos!",
                Description = "What's Wrong with These Photos? Photos is a longtime favorite of Ellen's show.",
                Duration = 183,
                Visibility = VideoVisibility.Public,
                ContentURL = new Uri("https://www.youtube.com/watch?v=nOIJ4b1L3_I"),
                ThumbnailURL = new Uri("http://i1.ytimg.com/vi/nOIJ4b1L3_I/default.jpg")
            }
        };

        private List<ChannelVideo> ChannelVideos = new List<ChannelVideo>
        {
            new ChannelVideo
            {
                ChannelId = Guid.Parse("63198fcb-b459-43fa-82ca-1894bd96a91c"),
                VideoId = Guid.Parse("ce200f0a-9582-47bc-8019-eaf9186794f7"),
                AddedOn = new DateTime(2019, 2, 5, 9, 30, 0),
                DisplaySequence = 1
            },
            new ChannelVideo
            {
                ChannelId = Guid.Parse("63198fcb-b459-43fa-82ca-1894bd96a91c"),
                VideoId = Guid.Parse("8ad38d08-fd45-43fc-ac96-5abb049917f0"),
                AddedOn = new DateTime(2019, 2, 5, 9, 31, 0),
                DisplaySequence = 2
            },
            new ChannelVideo
            {
                ChannelId = Guid.Parse("0981aabe-aba6-402b-8a1e-07d5b162763d"),
                VideoId = Guid.Parse("4fc1550a-d4f2-45c6-8bf3-a170411f8672"),
                AddedOn = new DateTime(2019, 2, 6, 19, 20, 0),
                DisplaySequence = 1
            },
        };

        private List<Tag> Tags = new List<Tag>
        {
            new Tag
            {
                Id = Guid.Parse("594ab825-a692-4ee0-b9cd-859311b88ff8"),
                Name = "Music"
            },
            new Tag
            {
                Id = Guid.Parse("9055b0f6-3770-4c26-be74-fea487c986d2"),
                Name = "Technology"
            }
        };

        private List<VideoTag> VideoTags = new List<VideoTag>
        {
            new VideoTag
            {
                TagId = Guid.Parse("594ab825-a692-4ee0-b9cd-859311b88ff8"),
                VideoId = Guid.Parse("ce200f0a-9582-47bc-8019-eaf9186794f7")
            }
        };

        private List<View> Views = new List<View>
        {
            new View
            {
                UserId = Guid.Parse("0029208b-4217-46d8-87f2-85fb78defac3"),
                VideoId = Guid.Parse("ce200f0a-9582-47bc-8019-eaf9186794f7"),
                AddedOn = new DateTime(2019, 7, 6, 10, 0, 0)
            }
        };

        private List<Reaction> Reactions = new List<Reaction>
        {
            new Reaction
            {
                UserId = Guid.Parse("0029208b-4217-46d8-87f2-85fb78defac3"),
                VideoId = Guid.Parse("ce200f0a-9582-47bc-8019-eaf9186794f7"),
                AddedOn = new DateTime(2019, 7, 6, 10, 0, 5),
                Type = ReactionType.Like
            }
        };

        private List<Favourite> Favourites = new List<Favourite>();

        private List<Comment> Comments = new List<Comment>
        {
            new Comment
            {
                UserId = Guid.Parse("0029208b-4217-46d8-87f2-85fb78defac3"),
                VideoId = Guid.Parse("ce200f0a-9582-47bc-8019-eaf9186794f7"),
                AddedOn = new DateTime(2019, 7, 6, 10, 0, 20),
                Text = "I love Mozart!"
            }
        };

        #endregion
    }
}
