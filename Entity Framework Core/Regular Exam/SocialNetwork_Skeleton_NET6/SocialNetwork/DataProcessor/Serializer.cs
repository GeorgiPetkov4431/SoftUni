using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SocialNetwork.Data;
using SocialNetwork.DataProcessor.ExportDTOs;
using SocialNetwork.Utilities;
using System.Globalization;

namespace SocialNetwork.DataProcessor
{
    public class Serializer
    {
        public static string ExportUsersWithFriendShipsCountAndTheirPosts(SocialNetworkDbContext dbContext)
        {
            ExportUsersWithFriendShipsCountAndTheirPostsDto[] usersWithFriendShipsCountAndTheirPostsDtos = dbContext.Users
                .Select(u => new ExportUsersWithFriendShipsCountAndTheirPostsDto
                {
                    Username = u.Username,
                    Posts = u.Posts
                        .OrderBy(p => p.Id)
                        .Select(p => new ExportUserPostsDto
                        {
                            Content = p.Content,
                            CreatedAt = p.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture)
                        })
                        .ToArray(),
                    Friendships = dbContext.Friendships.Count(f => f.UserOneId == u.Id || f.UserTwoId == u.Id)
                })
                .OrderBy(u => u.Username)
                .ToArray();

            string xmlResult = XmlSerializerWrapper
                .Serialize(usersWithFriendShipsCountAndTheirPostsDtos, "Users");

            return xmlResult;
        }

        public static string ExportConversationsWithMessagesChronologically(SocialNetworkDbContext dbContext)
        {
            var conversationsWithMessages = dbContext.Conversations
                .AsNoTracking()
                .Select(c => new
                {
                    c.Id,
                    c.Title,
                    c.StartedAt,
                    Messages = c.Messages
                        .OrderBy(m => m.SentAt)
                        .Select(m => new
                        {
                            m.Content,
                            m.SentAt,
                            m.Status,
                            SenderUsername = m.Sender.Username
                        })
                        .ToArray()
                })
                .OrderBy(c => c.StartedAt)
                .ToArray();

            string jsonResult = JsonConvert
                .SerializeObject(conversationsWithMessages, Formatting.Indented);

            return jsonResult;
        }
    }
}
